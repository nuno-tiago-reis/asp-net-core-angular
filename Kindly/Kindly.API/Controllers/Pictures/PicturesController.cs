using AutoMapper;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using Kindly.API.Contracts;
using Kindly.API.Contracts.Pictures;
using Kindly.API.Models.Repositories.Pictures;
using Kindly.API.Utility.Settings;
using Kindly.API.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kindly.API.Controllers.Pictures
{
	[Authorize]
	[ApiController]
	[ServiceFilter(typeof(KindlyActivityFilter))]
	[Route("api/users/{userID:Guid}/[controller]")]
	public sealed class PicturesController : KindlyController
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the cloudinary.
		/// </summary>
		private Cloudinary Cloudinary { get; set; }

		/// <summary>
		/// Gets or sets the repository.
		/// </summary>
		private IPictureRepository Repository { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="PicturesController"/> class.
		/// </summary>
		/// 
		/// <param name="mapper">The mapper.</param>
		/// <param name="repository">The repository.</param>
		/// <param name="authorizationService">The authorization service.</param>
		/// <param name="cloudinarySettings">The cloudinary settings.</param>
		public PicturesController
		(
			IMapper mapper,
			IPictureRepository repository,
			IAuthorizationService authorizationService,
			IOptions<CloudinarySettings> cloudinarySettings
		)
		: base(mapper, authorizationService)
		{
			this.Repository = repository;
			this.Cloudinary = new Cloudinary(new Account
			(
				cloud: cloudinarySettings.Value.Cloud,
				apiKey: cloudinarySettings.Value.ApiKey,
				apiSecret: cloudinarySettings.Value.ApiSecret
			));
		}
		#endregion

		#region [Interface Methods]
		/// <summary>
		/// Creates a picture for a user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="createPictureInfo">The create information.</param>
		[HttpPost]
		public async Task<IActionResult> Create(Guid userID, [FromForm][FromBody] CreatePictureDto createPictureInfo)
		{
			try
			{
				var picture = new Picture
				{
					UserID = userID
				};

				#region [Authorization]
				var result = await this.AuthorizationService.AuthorizeAsync
				(
					this.User, picture, nameof(KindlyPolicies.AllowIfOwner)
				);

				if (result.Succeeded == false)
				{
					return this.Unauthorized();
				}
				#endregion

				this.Mapper.Map(createPictureInfo, picture);

				this.UploadToCloudinary(picture, createPictureInfo.File);

				await this.Repository.Create(picture);

				return this.Created(new Uri($"{this.Request.GetDisplayUrl()}/{picture.ID}"), this.Mapper.Map<PictureDto>(picture));
			}
			catch (ArgumentException exception)
			{
				return this.BadRequest(exception.Message);
			}
		}

		/// <summary>
		/// Updates a users picture.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="pictureID">The picture identifier.</param>
		/// <param name="updatePictureInfo">The update information.</param>
		[HttpPut("{pictureID:Guid}")]
		public async Task<IActionResult> Update(Guid userID, Guid pictureID, UpdatePictureDto updatePictureInfo)
		{
			var picture = new Picture
			{
				ID = pictureID,
				UserID = userID
			};

			#region [Authorization]
			var result = await this.AuthorizationService.AuthorizeAsync
			(
				this.User, picture, nameof(KindlyPolicies.AllowIfOwner)
			);

			if (result.Succeeded == false)
			{
				return this.Unauthorized();
			}
			#endregion

			this.Mapper.Map(updatePictureInfo, picture);

			await this.Repository.Update(picture);

			return this.Ok();
		}

		/// <summary>
		/// Deletes a users picture.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="pictureID">The picture identifier.</param>
		[HttpDelete("{pictureID:Guid}")]
		public async Task<IActionResult> Delete(Guid userID, Guid pictureID)
		{
			try
			{
				var picture = await this.Repository.Get(pictureID);

				#region [Authorization]
				var result = await this.AuthorizationService.AuthorizeAsync
				(
					this.User, picture, nameof(KindlyPolicies.AllowIfOwner)
				);

				if (result.Succeeded == false)
				{
					return this.Unauthorized();
				}
				#endregion

				this.DeleteInCloudinary(picture);

				await this.Repository.Delete(pictureID);

				return this.Ok();
			}
			catch (ArgumentException exception)
			{
				return this.BadRequest(exception.Message);
			}
		}

		/// <summary>
		/// Gets a users picture.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="pictureID">The picture identifier.</param>
		[HttpGet("{pictureID:Guid}")]
		public async Task<IActionResult> Get(Guid userID, Guid pictureID)
		{
			var picture = await this.Repository.Get(pictureID);
			var pictureDto = this.Mapper.Map<PictureDto>(picture);

			return this.Ok(pictureDto);
		}

		/// <summary>
		/// Gets a users pictures.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="parameters">The parameters.</param>
		[HttpGet]
		public async Task<IActionResult> GetAll(Guid userID, [FromQuery] PictureParameters parameters)
		{
			var pictures = await this.Repository.GetByUser(userID, parameters);
			var pictureDtos = pictures.Select(p => this.Mapper.Map<PictureDto>(p));

			this.Response.AddPaginationHeader(new PaginationHeader
			(
				pictures.PageNumber,
				pictures.PageSize,
				pictures.TotalPages,
				pictures.TotalCount
			));

			return this.Ok(pictureDtos);
		}

		/// <summary>
		/// Approves a picture.
		/// </summary>
		/// 
		/// <param name="pictureID">The picture identifier.</param>
		[HttpPut("/api/users/pictures/{pictureID:Guid}")]
		[Authorize(Policy = nameof(KindlyPolicies.AllowIfElevatedUser))]
		public async Task<IActionResult> Approve(Guid pictureID)
		{
			var picture = await this.Repository.Get(pictureID);

			if (picture.IsApproved != null && picture.IsApproved.Value)
			{
				return this.BadRequest(Picture.CannotApproveApprovedPicture);
			}

			picture.IsApproved = true;

			await this.Repository.Update(picture);

			return this.Ok();
		}

		/// <summary>
		/// Rejects a picture.
		/// </summary>
		/// 
		/// <param name="pictureID">The picture identifier.</param>
		[HttpDelete("/api/users/pictures/{pictureID:Guid}")]
		[Authorize(Policy = nameof(KindlyPolicies.AllowIfElevatedUser))]
		public async Task<IActionResult> Reject(Guid pictureID)
		{
			try
			{
				var picture = await this.Repository.Get(pictureID);

				if (picture.IsApproved != null && picture.IsApproved.Value)
				{
					return this.BadRequest(Picture.CannotRejectApprovedPicture);
				}

				this.DeleteInCloudinary(picture);

				await this.Repository.Delete(pictureID);

				return this.Ok();
			}
			catch (ArgumentException exception)
			{
				return this.BadRequest(exception.Message);
			}
		}

		/// <summary>
		/// Gets a users pictures.
		/// </summary>
		/// 
		/// <param name="parameters">The parameters.</param>
		[HttpGet("/api/users/pictures")]
		[Authorize(Policy = nameof(KindlyPolicies.AllowIfElevatedUser))]
		public async Task<IActionResult> GetAll([FromQuery] PictureParameters parameters)
		{
			var pictures = await this.Repository.GetAll(parameters);
			var pictureDtos = pictures.Select(p => this.Mapper.Map<PictureDto>(p));

			this.Response.AddPaginationHeader(new PaginationHeader
			(
				pictures.PageNumber,
				pictures.PageSize,
				pictures.TotalPages,
				pictures.TotalCount
			));

			return this.Ok(pictureDtos);
		}
		#endregion

		#region [Utility Methods]
		/// <summary>
		/// Uploads the picture file to cloudinary.
		/// </summary>
		/// 
		/// <param name="picture">The picture.</param>
		/// <param name="file">The file.</param>
		private void UploadToCloudinary(Picture picture, IFormFile file)
		{
			if (file == null || file.Length <= 0)
				throw new ArgumentException("The picture is empty.");

			using (var stream = file.OpenReadStream())
			{
				var uploadParameters = new ImageUploadParams
				{
					File = new FileDescription(file.Name, stream),
					Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
				};

				var uploadResult = this.Cloudinary.Upload(uploadParameters);
				if (uploadResult.Error != null)
					throw new ArgumentException("There was an error uploading the picture: " + uploadResult.Error.Message);

				picture.Url = uploadResult.Uri.ToString();
				picture.PublicID = uploadResult.PublicId;
			}
		}

		/// <summary>
		/// Deletes the picture in cloudinary.
		/// </summary>
		/// 
		/// <param name="picture">The picture.</param>
		private void DeleteInCloudinary(Picture picture)
		{
			if (string.IsNullOrWhiteSpace(picture.PublicID))
				return;

			var deleteResult = this.Cloudinary.Destroy(new DeletionParams(picture.PublicID));
			if (deleteResult.Error != null)
				throw new ArgumentException("There was an error deleting the picture: " + deleteResult.Error.Message);
		}
		#endregion
	}
}
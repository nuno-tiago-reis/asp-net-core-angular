using AutoMapper;

using System;
using System.Linq;
using System.Threading.Tasks;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using Kindly.API.Contracts.Pictures;
using Kindly.API.Models.Domain;
using Kindly.API.Models.Repositories;
using Kindly.API.Utility;
using Kindly.API.Utility.Configurations;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Kindly.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/users/{userID}/[controller]")]
	[ServiceFilter(typeof(KindlyActivityFilter))]
	public sealed class PicturesController : KindlyController
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the mapper.
		/// </summary>
		private IMapper Mapper { get; set; }

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
		/// <param name="cloudinarySettings">The cloudinary settings.</param>
		public PicturesController(IMapper mapper, IPictureRepository repository, IOptions<CloudinarySettings> cloudinarySettings)
		{
			this.Mapper = mapper;
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
		/// Creates the specified picture.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="createPictureInfo">The create information.</param>
		[HttpPost]
		public async Task<IActionResult> Create(Guid userID, [FromForm][FromBody] CreatePictureDto createPictureInfo)
		{
			if (userID != this.GetInvocationUserID())
				return this.Unauthorized();

			if (createPictureInfo.File == null || createPictureInfo.File.Length <= 0)
				return this.BadRequest("The picture is empty.");

			// Create the file in cloudinary
			var file = createPictureInfo.File;

			using (var stream = file.OpenReadStream())
			{
				// Parametrize the cloudinary upload
				var uploadParameters = new ImageUploadParams
				{
					File = new FileDescription(file.Name, stream),
					Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
				};

				// Upload the file to cloudinary
				var uploadResult = this.Cloudinary.Upload(uploadParameters);
				if (uploadResult.Error != null)
					return this.BadRequest("There was an error uploading the picture: " + uploadResult.Error.Message);

				var picture = this.Mapper.Map<Picture>(createPictureInfo);
				picture.UserID = userID;
				picture.Url = uploadResult.Uri.ToString();
				picture.PublicID = uploadResult.PublicId;
				picture.Description = string.Empty;

				await this.Repository.Create(picture);

				return this.Created(new Uri($"{this.Request.GetDisplayUrl()}/{picture.ID}"), this.Mapper.Map<PictureDto>(picture));
			}
		}

		/// <summary>
		/// Updates the specified picture.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="pictureID">The picture identifier.</param>
		/// <param name="updatePictureInfo">The update information.</param>
		[HttpPut("{pictureID:Guid}")]
		public async Task<IActionResult> Update(Guid userID, Guid pictureID, UpdatePictureDto updatePictureInfo)
		{
			if (userID != this.GetInvocationUserID())
					return this.Unauthorized();

			if (await this.Repository.PictureBelongsToUser(userID, pictureID) == false)
				return this.NotFound();

			var picture = Mapper.Map<Picture>(updatePictureInfo);
			picture.ID = pictureID;
			picture.UserID = userID;

			await this.Repository.Update(picture);

			return this.Ok();
		}

		/// <summary>
		/// Deletes a picture.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="pictureID">The picture identifier.</param>
		[HttpDelete("{pictureID:Guid}")]
		public async Task<IActionResult> Delete(Guid userID, Guid pictureID)
		{
			if (userID != this.GetInvocationUserID())
				return this.Unauthorized();

			if (await this.Repository.PictureBelongsToUser(userID, pictureID) == false)
				return this.NotFound();

			var picture = await this.Repository.Get(pictureID);

			// Delete the file in cloudinary
			if (string.IsNullOrWhiteSpace(picture.PublicID) == false)
			{
				var deleteResult = this.Cloudinary.Destroy(new DeletionParams(picture.PublicID));
				if (deleteResult.Error != null)
					return this.BadRequest("There was an error deleting the picture: " + deleteResult.Error.Message);
			}

			await this.Repository.Delete(pictureID);

			return this.Ok();
		}

		/// <summary>
		/// Gets a picture.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="pictureID">The picture identifier.</param>
		[HttpGet("{pictureID:Guid}")]
		public async Task<IActionResult> Get(Guid userID, Guid pictureID)
		{
			if (userID != this.GetInvocationUserID())
				return this.Unauthorized();

			if (await this.Repository.PictureBelongsToUser(userID, pictureID) == false)
				return this.NotFound();

			var picture = await this.Repository.Get(pictureID);
			var pictureDto = this.Mapper.Map<PictureDetailedDto>(picture);

			return this.Ok(pictureDto);
		}

		/// <summary>
		/// Gets the pictures.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		[HttpGet]
		public async Task<IActionResult> GetAll(Guid userID)
		{
			var pictures = await this.Repository.GetByUser(userID);
			var pictureDtos = pictures.Select(picture => this.Mapper.Map<PictureDto>(picture));

			return this.Ok(pictureDtos);
		}
		#endregion
	}
}
using AutoMapper;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

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

			try
			{
				var uploadResult = this.UploadToCloudinary(createPictureInfo.File);

				var picture = this.Mapper.Map<Picture>(createPictureInfo);
				picture.UserID = userID;
				picture.Url = uploadResult.Uri.ToString();
				picture.PublicID = uploadResult.PublicId;

				await this.Repository.Create(picture);

				return this.Created(new Uri($"{this.Request.GetDisplayUrl()}/{picture.ID}"), this.Mapper.Map<PictureDto>(picture));
			}
			catch (ArgumentException exception)
			{
				return this.BadRequest(exception.Message);
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

			try
			{
				var picture = await this.Repository.Get(pictureID);

				this.DeleteInCloudinary(picture.PublicID);

				await this.Repository.Delete(pictureID);

				return this.Ok();
			}
			catch (ArgumentException exception)
			{
				return this.BadRequest(exception.Message);
			}
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
			var pictureDto = this.Mapper.Map<PictureDto>(picture);

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

		#region [Utility Methods]
		/// <summary>
		/// Uploads the picture file to cloudinary.
		/// </summary>
		/// 
		/// <param name="file">The file.</param>
		private ImageUploadResult UploadToCloudinary(IFormFile file)
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

				return uploadResult;
			}
		}

		/// <summary>
		/// Deletes the picture in cloudinary.
		/// </summary>
		/// 
		/// <param name="publicID">The public id.</param>
		private void DeleteInCloudinary(string publicID)
		{
			if (string.IsNullOrWhiteSpace(publicID))
				return;

			var deleteResult = this.Cloudinary.Destroy(new DeletionParams(publicID));
			if (deleteResult.Error != null)
				throw new ArgumentException("There was an error deleting the picture: " + deleteResult.Error.Message);
		}
		#endregion
	}
}
using AutoMapper;

using System;
using System.Linq;
using System.Threading.Tasks;

using Kindly.API.Contracts.Pictures;
using Kindly.API.Models.Domain;
using Kindly.API.Models.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Kindly.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public sealed class PicturesController : ControllerBase
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the mapper.
		/// </summary>
		private IMapper Mapper { get; set; }

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
		public PicturesController(IMapper mapper, IPictureRepository repository)
		{
			this.Repository = repository;
			this.Mapper = mapper;
		}
		#endregion

		#region [Interface Methods]
		/// <summary>
		/// Creates the specified picture.
		/// </summary>
		/// 
		/// <param name="createPictureInfo">The create information.</param>
		[HttpPost]
		public async Task<IActionResult> Create(CreatePictureDto createPictureInfo)
		{
			var picture = await this.Repository.Create(Mapper.Map<Picture>(createPictureInfo));

			return this.Created(new Uri($"{Request.GetDisplayUrl()}/{picture.ID}"), Mapper.Map<PictureDto>(picture));
		}

		/// <summary>
		/// Updates the specified picture.
		/// </summary>
		/// 
		/// <param name="updatePictureInfo">The update information.</param>
		[HttpPut]
		public async Task<IActionResult> Update(UpdatePictureDto updatePictureInfo)
		{
			await this.Repository.Update(Mapper.Map<Picture>(updatePictureInfo));

			return this.Ok();
		}

		/// <summary>
		/// Deletes a picture.
		/// </summary>
		/// 
		/// <param name="id">The picture identifier.</param>
		[HttpDelete("{id:Guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await this.Repository.Delete(id);

			return this.Ok();
		}

		/// <summary>
		/// Gets a picture.
		/// </summary>
		/// 
		/// <param name="id">The picture identifier.</param>
		[HttpGet("{id:Guid}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var picture = await this.Repository.Get(id);
			var pictureDto = this.Mapper.Map<PictureDto>(picture);

			return this.Ok(pictureDto);
		}

		/// <summary>
		/// Gets the pictures.
		/// </summary>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var pictures = await this.Repository.GetAll();
			var pictureDtos = pictures.Select(picture => this.Mapper.Map<PictureDto>(picture));

			return this.Ok(pictureDtos);
		}
		#endregion
	}
}
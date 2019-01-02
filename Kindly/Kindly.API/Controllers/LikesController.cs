using AutoMapper;

using System;
using System.Linq;
using System.Threading.Tasks;

using Kindly.API.Contracts.Likes;
using Kindly.API.Models.Domain;
using Kindly.API.Models.Repositories;
using Kindly.API.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Kindly.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/users/{userID}/[controller]")]
	[ServiceFilter(typeof(KindlyActivityFilter))]
	public sealed class LikesController : KindlyController
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the mapper.
		/// </summary>
		private IMapper Mapper { get; set; }

		/// <summary>
		/// Gets or sets the repository.
		/// </summary>
		private ILikeRepository Repository { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="LikesController"/> class.
		/// </summary>
		/// 
		/// <param name="mapper">The mapper.</param>
		/// <param name="repository">The repository.</param>
		public LikesController(IMapper mapper, ILikeRepository repository)
		{
			this.Mapper = mapper;
			this.Repository = repository;
		}
		#endregion

		#region [Interface Methods]
		/// <summary>
		/// Creates the specified like.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="createLikeInfo">The create information.</param>
		[HttpPost]
		public async Task<IActionResult> Create(Guid userID, CreateLikeDto createLikeInfo)
		{
			if (userID != this.GetInvocationUserID())
				return this.Unauthorized();

			var like = Mapper.Map<Like>(createLikeInfo);
			like.SourceID = userID;

			await this.Repository.Create(like);

			return this.Created(new Uri($"{Request.GetDisplayUrl()}/{like.ID}"), Mapper.Map<LikeDto>(like));
		}

		/// <summary>
		/// Updates the specified like.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="likeID">The like identifier.</param>
		/// <param name="updateLikeInfo">The update information.</param>
		[HttpPut("{likeID:Guid}")]
		public async Task<IActionResult> Update(Guid userID, Guid likeID, UpdateLikeDto updateLikeInfo)
		{
			if (userID != this.GetInvocationUserID())
					return this.Unauthorized();

			if (await this.Repository.LikeBelongsToUser(userID, likeID) == false)
				return this.NotFound();

			var like = Mapper.Map<Like>(updateLikeInfo);
			like.ID = likeID;
			like.SourceID = userID;

			await this.Repository.Update(like);

			return this.Ok();
		}

		/// <summary>
		/// Deletes a like.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="likeID">The like identifier.</param>
		[HttpDelete("{likeID:Guid}")]
		public async Task<IActionResult> Delete(Guid userID, Guid likeID)
		{
			if (userID != this.GetInvocationUserID())
				return this.Unauthorized();

			if (await this.Repository.LikeBelongsToUser(userID, likeID) == false)
				return this.NotFound();

			await this.Repository.Delete(likeID);

			return this.Ok();
		}

		/// <summary>
		/// Gets a like.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="likeID">The like identifier.</param>
		[HttpGet("{likeID:Guid}")]
		public async Task<IActionResult> Get(Guid userID, Guid likeID)
		{
			if (userID != this.GetInvocationUserID())
				return this.Unauthorized();

			if (await this.Repository.LikeBelongsToUser(userID, likeID) == false)
				return this.NotFound();

			var like = await this.Repository.Get(likeID);
			var likeDto = this.Mapper.Map<LikeDto>(like);

			return this.Ok(likeDto);
		}

		/// <summary>
		/// Gets the likes.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		[HttpGet]
		public async Task<IActionResult> GetAll(Guid userID)
		{
			var likes = await this.Repository.GetBySourceUser(userID);
			var likeDtos = likes.Select(like => this.Mapper.Map<LikeDto>(like));

			return this.Ok(likeDtos);
		}
		#endregion
	}
}
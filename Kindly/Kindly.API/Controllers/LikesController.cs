using AutoMapper;

using Kindly.API.Contracts;
using Kindly.API.Contracts.Likes;
using Kindly.API.Models.Repositories.Likes;
using Kindly.API.Utility.Collections;
using Kindly.API.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
		/// <param name="parameters">The parameters.</param>
		[HttpGet]
		public async Task<IActionResult> GetAll(Guid userID, [FromQuery] LikeParameters parameters)
		{
			PagedList<Like> likes;
			IEnumerable<LikeDto> likeDtos;

			switch (parameters.Mode)
			{
				case LikeMode.Targets:
					likes = await this.Repository.GetBySourceUser(userID, parameters);
					likeDtos = likes.Select(l => this.Mapper.Map<LikeDto>(l)).ToList();

					if (parameters.IncludeRequestUser)
						break;

					foreach (var likeDto in likeDtos)
						likeDto.CleanSource();
					break;

				case LikeMode.Sources:
					likes = await this.Repository.GetByTargetUser(userID, parameters);
					likeDtos = likes.Select(l => this.Mapper.Map<LikeDto>(l)).ToList();

					if (parameters.IncludeRequestUser)
						break;

					foreach (var likeDto in likeDtos)
						likeDto.CleanTarget();
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(parameters.Mode), parameters.Mode, null);
			}

			this.Response.AddPaginationHeader(new PaginationHeader
			(
				likes.PageNumber,
				likes.PageSize,
				likes.TotalPages,
				likes.TotalCount
			));

			return this.Ok(likeDtos);
		}
		#endregion
	}
}
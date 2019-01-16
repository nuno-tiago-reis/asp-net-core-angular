using AutoMapper;

using Kindly.API.Contracts;
using Kindly.API.Contracts.Likes;
using Kindly.API.Models.Repositories.Likes;
using Kindly.API.Models.Repositories.Users;
using Kindly.API.Utility.Collections;
using Kindly.API.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kindly.API.Controllers.Likes
{
	/// <summary>
	/// Provides manipulation operations over the like resources (including CRUD).
	/// Certain operations may only be invoked by the resource owner(s).
	/// </summary>
	/// 
	/// <seealso cref="KindlyController" />
	[Authorize]
	[ApiController]
	[ServiceFilter(typeof(KindlyActivityFilter))]
	[Route("api/users/{userID:Guid}/[controller]")]
	public sealed class LikesController : KindlyController
	{
		#region [Properties]
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
		/// <param name="authorizationService">The authorization service.</param>
		public LikesController
		(
			IMapper mapper,
			ILikeRepository repository,
			IAuthorizationService authorizationService
		)
		: base(mapper, authorizationService)
		{
			this.Repository = repository;
		}
		#endregion

		#region [Interface Methods]
		/// <summary>
		/// Creates a like for a user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="createLikeInfo">The create information.</param>
		[HttpPost]
		public async Task<IActionResult> Create(Guid userID, CreateLikeDto createLikeInfo)
		{
			var like = new Like
			{
				SenderID = userID
			};

			#region [Authorization]
			var result = await this.AuthorizationService.AuthorizeAsync
			(
				this.User, like, nameof(KindlyPolicies.AllowIfOwner)
			);

			if (result.Succeeded == false)
			{
				return this.Unauthorized();
			}
			#endregion

			this.Mapper.Map(createLikeInfo, like);

			await this.Repository.Create(like);

			return this.Created(new Uri($"{Request.GetDisplayUrl()}/{like.ID}"), this.Mapper.Map<LikeDto>(like));
		}

		/// <summary>
		/// Updates a users like.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="likeID">The like identifier.</param>
		/// <param name="updateLikeInfo">The update information.</param>
		[HttpPut("{likeID:Guid}")]
		public async Task<IActionResult> Update(Guid userID, Guid likeID, UpdateLikeDto updateLikeInfo)
		{
			var like = new Like
			{
				ID = likeID,
				SenderID = userID
			};

			#region [Authorization]
			var result = await this.AuthorizationService.AuthorizeAsync
			(
				this.User, like, nameof(KindlyPolicies.AllowIfOwner)
			);

			if (result.Succeeded == false)
			{
				return this.Unauthorized();
			}
			#endregion

			this.Mapper.Map(updateLikeInfo, like);

			await this.Repository.Update(like);

			return this.Ok();
		}

		/// <summary>
		/// Deletes a users like.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="likeID">The like identifier.</param>
		[HttpDelete("{likeID:Guid}")]
		public async Task<IActionResult> Delete(Guid userID, Guid likeID)
		{
			var like = new Like
			{
				ID = likeID,
				SenderID = userID
			};

			#region [Authorization]
			var result = await this.AuthorizationService.AuthorizeAsync
			(
				this.User, like, nameof(KindlyPolicies.AllowIfOwner)
			);

			if (result.Succeeded == false)
			{
				return this.Unauthorized();
			}
			#endregion

			await this.Repository.Delete(likeID);

			return this.Ok();
		}

		/// <summary>
		/// Gets a users like.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="likeID">The like identifier.</param>
		[HttpGet("{likeID:Guid}")]
		public async Task<IActionResult> Get(Guid userID, Guid likeID)
		{
			var like = await this.Repository.Get(likeID);

			#region [Authorization]
			var result = await this.AuthorizationService.AuthorizeAsync
			(
				this.User, like, nameof(KindlyPolicies.AllowIfOwner)
			);

			if (result.Succeeded == false)
			{
				return this.Unauthorized();
			}
			#endregion

			var likeDto = this.Mapper.Map<LikeDto>(like);

			return this.Ok(likeDto);
		}

		/// <summary>
		/// Gets a users likes.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="parameters">The parameters.</param>
		[HttpGet]
		public async Task<IActionResult> GetAll(Guid userID, [FromQuery] LikeParameters parameters)
		{
			var user = new User
			{
				ID = userID
			};

			#region [Authorization]
			var result = await this.AuthorizationService.AuthorizeAsync
			(
				this.User, user, nameof(KindlyPolicies.AllowIfOwner)
			);

			if (result.Succeeded == false)
			{
				return this.Unauthorized();
			}
			#endregion

			PagedList<Like> likes;
			IEnumerable<LikeDto> likeDtos;

			switch (parameters.Mode)
			{
				case LikeMode.Recipients:
					likes = await this.Repository.GetBySenderUser(userID, parameters);
					likeDtos = likes.Select(l => this.Mapper.Map<LikeDto>(l)).ToList();

					if (parameters.IncludeRequestUser)
						break;

					foreach (var likeDto in likeDtos)
						likeDto.RemoveSender();
					break;

				case LikeMode.Senders:
					likes = await this.Repository.GetByRecipientUser(userID, parameters);
					likeDtos = likes.Select(l => this.Mapper.Map<LikeDto>(l)).ToList();

					if (parameters.IncludeRequestUser)
						break;

					foreach (var likeDto in likeDtos)
						likeDto.RemoveRecipient();
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
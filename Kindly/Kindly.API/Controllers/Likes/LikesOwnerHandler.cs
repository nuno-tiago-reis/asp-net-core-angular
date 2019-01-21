using Kindly.API.Models.Repositories.Likes;

using Microsoft.AspNetCore.Authorization;

using System;
using System.Threading.Tasks;

namespace Kindly.API.Controllers.Likes
{
	/// <summary>
	/// Implements the owner handler for the likes controller.
	/// </summary>
	/// 
	/// <seealso cref="LikesController"/>.
	/// <seealso cref="ResourceOwnerHandler{Like}" />
	public sealed class LikesOwnerHandler : ResourceOwnerHandler<Like>
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the repository.
		/// </summary>
		private ILikeRepository Repository { get; set; }
		#endregion

		#region [Methods]
		/// <summary>
		/// Initializes a new instance of the <see cref="LikesOwnerHandler"/> class.
		/// </summary>
		/// 
		/// <param name="repository">The repository.</param>
		public LikesOwnerHandler(ILikeRepository repository)
		{
			this.Repository = repository;
		}

		/// <inheritdoc />
		protected override Task HandleRequirementAsync
		(
			AuthorizationHandlerContext context,
			ResourceOwnerRequirement requirement,
			Like like
		)
		{
			var userID = this.GetInvocationUserID(context);

			if
			(
				// The invoking user is the same as the api parameter
				(userID == like.SenderID || userID == like.RecipientID) &&
				// The picture belongs to the invoking user (which is the same as the api parameter)
				like.ID == default(Guid) || this.Repository.LikeBelongsToUser(like.SenderID, like.ID).Result
			)
			{
				context.Succeed(requirement);
			}
			else
			{
				context.Fail();
			}

			return Task.CompletedTask;
		}
		#endregion
	}
}
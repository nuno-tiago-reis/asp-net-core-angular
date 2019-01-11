using Kindly.API.Models.Repositories.Likes;

using Microsoft.AspNetCore.Authorization;

using System;
using System.Threading.Tasks;

namespace Kindly.API.Controllers.Likes
{
	public sealed class LikesAuthorizationHandler : KindlyAuthorizationHandler<AllowIfOwnerRequirement, Like>
	{
		/// <summary>
		/// Gets or sets the repository.
		/// </summary>
		public ILikeRepository Repository { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="LikesAuthorizationHandler"/> class.
		/// </summary>
		/// 
		/// <param name="repository">The repository.</param>
		public LikesAuthorizationHandler(ILikeRepository repository)
		{
			this.Repository = repository;
		}

		/// <inheritdoc />
		protected override Task HandleRequirementAsync
		(
			AuthorizationHandlerContext context,
			AllowIfOwnerRequirement requirement,
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

			return Task.FromResult(0);
		}
	}
}
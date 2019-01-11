using Kindly.API.Models.Repositories.Pictures;

using Microsoft.AspNetCore.Authorization;

using System;
using System.Threading.Tasks;

namespace Kindly.API.Controllers.Pictures
{
	public sealed class PicturesAuthorizationHandler : KindlyAuthorizationHandler<AllowIfOwnerRequirement, Picture>
	{
		/// <summary>
		/// Gets or sets the repository.
		/// </summary>
		public IPictureRepository Repository { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PicturesAuthorizationHandler"/> class.
		/// </summary>
		/// 
		/// <param name="repository">The repository.</param>
		public PicturesAuthorizationHandler(IPictureRepository repository)
		{
			this.Repository = repository;
		}

		/// <inheritdoc />
		protected override Task HandleRequirementAsync
		(
			AuthorizationHandlerContext context,
			AllowIfOwnerRequirement requirement,
			Picture picture
		)
		{
			var userID = this.GetInvocationUserID(context);

			if
			(
				// The invoking user is the same as the api parameter
				userID == picture.UserID &&
				// The picture belongs to the invoking user (which is the same as the api parameter)
				picture.ID == default(Guid) || this.Repository.PictureBelongsToUser(picture.UserID, picture.ID).Result
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
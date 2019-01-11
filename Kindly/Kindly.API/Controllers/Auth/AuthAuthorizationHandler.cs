using Kindly.API.Models.Repositories.Users;

using Microsoft.AspNetCore.Authorization;

using System.Threading.Tasks;

namespace Kindly.API.Controllers.Auth
{
	public sealed class AuthAuthorizationHandler : KindlyAuthorizationHandler<AllowIfOwnerRequirement, User>
	{
		/// <inheritdoc />
		protected override Task HandleRequirementAsync
		(
			AuthorizationHandlerContext context,
			AllowIfOwnerRequirement requirement,
			User user
		)
		{
			var userID = this.GetInvocationUserID(context);

			if (userID == user.ID)
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
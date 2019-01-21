using Kindly.API.Models.Repositories.Users;

using Microsoft.AspNetCore.Authorization;

using System.Threading.Tasks;

namespace Kindly.API.Controllers.Auth
{
	/// <summary>
	/// Implements the owner handler for the authorization controller.
	/// </summary>
	/// 
	/// <seealso cref="AuthController"/>.
	/// <seealso cref="ResourceOwnerHandler{User}" />
	public sealed class AuthOwnerHandler : ResourceOwnerHandler<User>
	{
		#region [Methods]
		/// <inheritdoc />
		protected override Task HandleRequirementAsync
		(
			AuthorizationHandlerContext context,
			ResourceOwnerRequirement requirement,
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

			return Task.CompletedTask;
		}
		#endregion
	}
}
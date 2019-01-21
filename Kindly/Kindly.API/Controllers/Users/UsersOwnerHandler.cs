using Kindly.API.Models.Repositories.Users;

using Microsoft.AspNetCore.Authorization;

using System;
using System.Threading.Tasks;

namespace Kindly.API.Controllers.Users
{
	/// <summary>
	/// Implements the owner handler for the users controller.
	/// </summary>
	/// 
	/// <seealso cref="UsersController"/>.
	/// <seealso cref="ResourceOwnerHandler{User}" />
	public sealed class UsersOwnerHandler : ResourceOwnerHandler<User>
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

			if (userID == user.ID || user.ID == default(Guid))
			{
				context.Succeed(requirement);
			}
			else
			{
				context.Fail();
			}

			return Task.FromResult(0);
		}
		#endregion
	}
}
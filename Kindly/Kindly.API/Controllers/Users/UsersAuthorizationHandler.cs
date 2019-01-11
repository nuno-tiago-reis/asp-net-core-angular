using Kindly.API.Models.Repositories.Users;

using Microsoft.AspNetCore.Authorization;

using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kindly.API.Controllers.Users
{
	public sealed class UsersAuthorizationHandler : KindlyAuthorizationHandler<AllowIfOwnerRequirement, User>
	{
		/// <inheritdoc />
		protected override Task HandleRequirementAsync
		(
			AuthorizationHandlerContext context,
			AllowIfOwnerRequirement requirement,
			User user
		)
		{
			Console.WriteLine(JsonConvert.SerializeObject(user));

			var userID = this.GetInvocationUserID(context);

			Console.WriteLine(userID);

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
	}
}
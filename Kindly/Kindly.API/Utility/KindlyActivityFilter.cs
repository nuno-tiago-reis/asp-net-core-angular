using Kindly.API.Models.Repositories.Users;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kindly.API.Utility
{
	public sealed class KindlyActivityFilter : IAsyncActionFilter
	{
		/// <inheritdoc />
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var resultContext = await next();

			if (resultContext.HttpContext.User.HasClaim(claim => claim.Type == ClaimTypes.NameIdentifier))
			{
				// Fetch the user
				var repository = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
				var userID = Guid.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
				var user = await repository.Get(userID);

				if (user != null)
				{
					// Update the last active field
					user.LastActiveAt = DateTime.Now;

					// Update the user
					await repository.Update(user);
				}

			}
		}
	}
}
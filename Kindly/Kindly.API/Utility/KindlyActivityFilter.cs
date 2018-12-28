using System;
using System.Threading.Tasks;

using Kindly.API.Models.Repositories;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Kindly.API.Utility
{
	public sealed class KindlyActivityFilter : IAsyncActionFilter
	{
		/// <summary>
		/// The user identifier claim.
		/// </summary>
		private static readonly string UserIdClaim = nameof(KindlyClaimTypes.ID).ToLowerCamelCase();

		/// <inheritdoc />
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var resultContext = await next();

			if (resultContext.HttpContext.User.HasClaim(claim => claim.Type == UserIdClaim))
			{
				// Fetch the repository
				var repository = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();

				// Fetch the user 
				var userID = Guid.Parse(resultContext.HttpContext.User.FindFirst(UserIdClaim).Value);

				// Update the last active field
				var user = await repository.Get(userID);
				user.LastActiveAt = DateTime.Now;

				// Update the user
				await repository.Update(user);
			}
		}
	}
}
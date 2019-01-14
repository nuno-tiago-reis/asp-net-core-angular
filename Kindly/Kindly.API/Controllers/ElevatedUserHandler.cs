using Kindly.API.Models.Repositories.Users;
using Kindly.API.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using System.Threading.Tasks;

namespace Kindly.API.Controllers
{
	public sealed class ElevatedUserHandler : GenericHandler<ElevatedUserRequirement>
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the user manager.
		/// </summary>
		public UserManager<User> UserManager { get; set; }
		#endregion

		#region [Methods]
		/// <summary>
		/// Initializes a new instance of the <see cref="ElevatedUserHandler"/> class.
		/// </summary>
		/// 
		/// <param name="userManager">The user manager.</param>
		public ElevatedUserHandler(UserManager<User> userManager)
		{
			this.UserManager = userManager;
		}

		/// <inheritdoc />
		protected override Task HandleRequirementAsync
		(
			AuthorizationHandlerContext context,
			ElevatedUserRequirement requirement
		)
		{
			var userID = this.GetInvocationUserID(context);
			var user = this.UserManager.FindByIdAsync(userID.ToString()).Result;

			if
			(
				this.UserManager.IsInRoleAsync(user, nameof(KindlyRoles.Administrator)).Result ||
				this.UserManager.IsInRoleAsync(user, nameof(KindlyRoles.Moderator)).Result
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
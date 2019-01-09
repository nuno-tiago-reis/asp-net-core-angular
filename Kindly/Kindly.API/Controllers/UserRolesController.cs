using AutoMapper;

using Kindly.API.Contracts.Roles;
using Kindly.API.Models.Repositories.Roles;
using Kindly.API.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kindly.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/users/{userID:Guid}/roles")]
	[ServiceFilter(typeof(KindlyActivityFilter))]
	public sealed class UserRolesController : KindlyController
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the mapper.
		/// </summary>
		private IMapper Mapper { get; set; }

		/// <summary>
		/// Gets or sets the repository.
		/// </summary>
		private IRoleRepository Repository { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="RolesController"/> class.
		/// </summary>
		/// 
		/// <param name="mapper">The mapper.</param>
		/// <param name="repository">The repository.</param>
		public UserRolesController(IMapper mapper, IRoleRepository repository)
		{
			this.Mapper = mapper;
			this.Repository = repository;
		}
		#endregion

		#region [Interface Methods]
		/// <summary>
		/// Adds a role to the user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="role">The role.</param>
		[HttpPost]
		public async Task<IActionResult> AddRoleToUser(Guid userID, RoleDto role)
		{
			if (userID != this.GetInvocationUserID())
				return this.Unauthorized();

			await this.Repository.AddRoleToUser(userID, Mapper.Map<Role>(role));

			return this.Ok();
		}

		/// <summary>
		/// Gets the role from the user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		[HttpGet]
		public async Task<IActionResult> GetRolesFromUser(Guid userID)
		{
			if (userID != this.GetInvocationUserID())
				return this.Unauthorized();

			var roles = await this.Repository.GetRolesFromUser(userID);
			var roleDtos = roles.Select(r => Mapper.Map<RoleDto>(r));

			return this.Ok(roleDtos);
		}

		/// <summary>
		/// Deletes a role from the user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="roleID">The role identifier.</param>
		[HttpDelete("{roleID:Guid}")]
		public async Task<IActionResult> RemoveRoleFromUser(Guid userID, Guid roleID)
		{
			if (userID != this.GetInvocationUserID())
				return this.Unauthorized();

			await this.Repository.RemoveRoleFromUser(userID, roleID);

			return this.Ok();
		}
		#endregion
	}
}
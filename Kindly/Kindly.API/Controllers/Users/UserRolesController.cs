using AutoMapper;

using Kindly.API.Contracts;
using Kindly.API.Contracts.Roles;
using Kindly.API.Contracts.Users;
using Kindly.API.Models.Repositories.Users;
using Kindly.API.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kindly.API.Controllers
{
	/// <summary>
	/// Provides manipulation operations over the user roles resources (including CRUD).
	/// These operations can only be invoked by administrators.
	/// </summary>
	/// 
	/// <seealso cref="KindlyController" />
	[Authorize(Roles = nameof(KindlyRoles.Administrator))]
	[ApiController]
	[ServiceFilter(typeof(KindlyActivityFilter))]
	[Route("api/users/{userID:Guid}/roles")]
	public sealed class UserRolesController : KindlyController
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the repository.
		/// </summary>
		private IUserRepository Repository { get; set; }

		/// <summary>
		/// Gets or sets the user manager.
		/// </summary>
		private UserManager<User> UserManager { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="UserRolesController"/> class.
		/// </summary>
		/// 
		/// <param name="mapper">The mapper.</param>
		/// <param name="userManager">The user manager.</param>
		/// <param name="repository">The repository.</param>
		public UserRolesController
		(
			IMapper mapper,
			IUserRepository repository,
			UserManager<User> userManager
		)
		: base(mapper)
		{
			this.Repository = repository;
			this.UserManager = userManager;
		}
		#endregion

		#region [Interface Methods]
		/// <summary>
		/// Adds a role to the user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="roleID">The role identifier.</param>
		[HttpPost("{roleID:Guid}")]
		public async Task<IActionResult> AddRoleToUser(Guid userID, Guid roleID)
		{
			await this.Repository.AddRoleToUser(userID, roleID);

			return this.Ok();
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
			await this.Repository.RemoveRoleFromUser(userID, roleID);

			return this.Ok();
		}

		/// <summary>
		/// Adds a role to the user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="userRoles">The user roles.</param>
		[HttpPut]
		public async Task<IActionResult> UpdateUserRoles(Guid userID, RolesDto userRoles)
		{
			var user = await this.Repository.GetUserWithRoles(userID);

			var addedRoles = userRoles.Roles;
			var removedRoles = new List<string>();

			foreach (var role in user.UserRoles)
			{
				if (userRoles.Roles.Any(roleName => roleName.Equals(role.Role.Name, StringComparison.InvariantCultureIgnoreCase)))
				{
					addedRoles.Remove(role.Role.Name);
				}
				else
				{
					removedRoles.Add(role.Role.Name);
				}
			}

			var addResult = await this.UserManager.AddToRolesAsync(user, addedRoles);
			if (addResult.Succeeded == false)
			{
				return this.BadRequest(addResult.Errors);
			}

			var removeResult = await this.UserManager.RemoveFromRolesAsync(user, removedRoles);
			if (removeResult.Succeeded == false)
			{
				return this.BadRequest(removeResult.Errors);
			}

			return this.Ok();
		}

		/// <summary>
		/// Gets the user with its roles.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		[HttpGet]
		public async Task<IActionResult> GetRolesFromUser(Guid userID)
		{
			var user = await this.Repository.GetUserWithRoles(userID);
			var userDto = this.Mapper.Map<UserWithRolesDto>(user);

			return this.Ok(userDto);
		}

		/// <summary>
		/// Gets the users with their roles.
		/// </summary>
		/// 
		/// <param name="parameters">The parameters.</param>
		[HttpGet("/api/users/roles")]
		public async Task<IActionResult> GetRolesFromUsers([FromQuery] UserParameters parameters)
		{
			var users = await this.Repository.GetUsersWithRoles(parameters);
			var userDtos = users.Select(u => this.Mapper.Map<UserWithRolesDto>(u));

			this.Response.AddPaginationHeader(new PaginationHeader
			(
				users.PageNumber,
				users.PageSize,
				users.TotalPages,
				users.TotalCount
			));

			return this.Ok(userDtos);
		}
		#endregion
	}
}
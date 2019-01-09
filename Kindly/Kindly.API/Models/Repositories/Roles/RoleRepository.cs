using Kindly.API.Utility;
using Kindly.API.Utility.Collections;
using Kindly.API.Models.Repositories.Users;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Kindly.API.Models.Repositories.Roles
{
	public sealed class RoleRepository : IRoleRepository
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the user manager.
		/// </summary>
		public UserManager<User> UserManager { get; set; }

		/// <summary>
		/// Gets or sets the role manager.
		/// </summary>
		public RoleManager<Role> RoleManager { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="RoleRepository"/> class.
		/// </summary>
		/// 
		/// <param name="userManager">The user manager.</param>
		/// <param name="roleManager">The role manager.</param>
		public RoleRepository(UserManager<User> userManager, RoleManager<Role> roleManager)
		{
			this.UserManager = userManager;
			this.RoleManager = roleManager;
		}
		#endregion

		#region [Methods] IEntityRepository
		/// <inheritdoc />
		public async Task<Role> Create(Role role)
		{
			// Properties
			if (string.IsNullOrWhiteSpace(role.Name))
				throw new KindlyException(role.InvalidFieldMessage(p => p.Name));

			// Create
			var result = await this.RoleManager.CreateAsync(role);
			if (result.Succeeded)
			{
				return role;
			}
			else
			{
				throw new KindlyException(result.Errors);
			}
		}

		/// <inheritdoc />
		public async Task<Role> Update(Role role)
		{
			var databaseRole = await this.RoleManager.FindByIdAsync(role.ID.ToString());
			if (databaseRole == null)
				throw new KindlyException(Role.DoesNotExist, true);

			// Properties
			databaseRole.Name =
				!string.IsNullOrWhiteSpace(role.Name) ? role.Name : databaseRole.Name;

			// Update
			var result = await this.RoleManager.UpdateAsync(databaseRole);
			if (result.Succeeded)
			{
				return databaseRole;
			}
			else
			{
				throw new KindlyException(result.Errors);
			}
		}

		/// <inheritdoc />
		public async Task Delete(Guid roleID)
		{
			var role = await this.RoleManager.FindByIdAsync(roleID.ToString());
			if (role == null)
				throw new KindlyException(Role.DoesNotExist, true);

			// Delete
			var result = await this.RoleManager.DeleteAsync(role);
			if (result.Succeeded)
			{
				// Nothing to do here.
			}
			else
			{
				throw new KindlyException(result.Errors);
			}
		}

		/// <inheritdoc />
		public async Task<Role> Get(Guid roleID)
		{
			return await this.GetQueryable().SingleOrDefaultAsync(p => p.ID == roleID);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Role>> GetAll()
		{
			return await this.GetQueryable().ToListAsync();
		}

		/// <inheritdoc />
		public async Task<PagedList<Role>> GetAll(RoleParameters parameters)
		{
			var roles = this.GetQueryable();

			return await PagedList<Role>.CreateAsync(roles, parameters.PageNumber, parameters.PageSize);
		}
		#endregion

		#region [Methods] IRoleRepository
		/// <inheritdoc />
		public async Task AddRoleToUser(Guid userID, Role role)
		{
			var databaseUser = await this.UserManager.FindByIdAsync(userID.ToString());
			if (databaseUser == null)
				throw new KindlyException(User.DoesNotExist, true);

			var databaseRole = await this.RoleManager.FindByNameAsync(role.Name);
			if (databaseRole == null)
				throw new KindlyException(Role.DoesNotExist, true);

			if (await this.UserManager.IsInRoleAsync(databaseUser, databaseRole.Name))
				throw new KindlyException(string.Format(Role.UserIsAlreadyInRole, databaseRole.Name));

			var result = await this.UserManager.AddToRoleAsync(databaseUser, databaseRole.Name);
			if (result.Succeeded)
			{
				// Nothing to do here.
			}
			else
			{
				throw new KindlyException(result.Errors);
			}
		}

		/// <inheritdoc />
		public async Task RemoveRoleFromUser(Guid userID, Guid roleID)
		{
			var databaseUser = await this.UserManager.FindByIdAsync(userID.ToString());
			if (databaseUser == null)
				throw new KindlyException(User.DoesNotExist, true);

			var databaseRole = await this.RoleManager.FindByIdAsync(roleID.ToString());
			if (databaseRole == null)
				throw new KindlyException(Role.DoesNotExist, true);

			if (await this.UserManager.IsInRoleAsync(databaseUser, databaseRole.Name) == false)
				throw new KindlyException(string.Format(Role.UserIsNotInRole, databaseRole.Name));

			var result = await this.UserManager.RemoveFromRoleAsync(databaseUser, databaseRole.Name);
			if (result.Succeeded)
			{
				// Nothing to do here.
			}
			else
			{
				throw new KindlyException(result.Errors);
			}
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Role>> GetRolesFromUser(Guid userID)
		{
			var databaseUser = await this.UserManager.FindByIdAsync(userID.ToString());
			if (databaseUser == null)
				throw new KindlyException(User.DoesNotExist, true);

			var roleNames = await this.UserManager.GetRolesAsync(databaseUser);

			return await this.RoleManager.Roles.Where(role => roleNames.Contains(role.Name)).ToListAsync();
		}
		#endregion

		#region [Methods] Utility
		/// <summary>
		/// Gets the queryable.
		/// </summary>
		private IQueryable<Role> GetQueryable()
		{
			return this.RoleManager.Roles;
		}
		#endregion
	}
}
using Kindly.API.Utility;
using Kindly.API.Utility.Collections;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;
using System.Linq;

namespace Kindly.API.Models.Repositories.Roles
{
	public sealed class RoleRepository : IRoleRepository
	{
		#region [Properties]
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
		/// <param name="roleManager">The role manager.</param>
		public RoleRepository(RoleManager<Role> roleManager)
		{
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
			return await this.GetQueryable().SingleOrDefaultAsync(p => p.Id == roleID);
		}

		/// <inheritdoc />
		public async Task<PagedList<Role>> GetAll(RoleParameters parameters = null)
		{
			var roles = this.GetQueryable();

			if (parameters != null)
			{
			}
			else
			{
				parameters = new RoleParameters();
			}

			return await PagedList<Role>.CreateAsync(roles, parameters.PageNumber, parameters.PageSize);
		}
		#endregion

		#region [Methods] IRoleRepository
		// Nothing to do here.
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
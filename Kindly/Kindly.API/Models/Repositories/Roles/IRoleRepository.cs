using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kindly.API.Models.Repositories.Roles
{
	public interface IRoleRepository : IEntityRepository<Role, RoleParameters>
	{
		/// <summary>
		/// Adds the role to the user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="role">The role.</param>
		Task AddRoleToUser(Guid userID, Role role);

		/// <summary>
		/// Removes the role from the user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="roleID">The role identifier.</param>
		Task RemoveRoleFromUser(Guid userID, Guid roleID);

		/// <summary>
		/// Gets the roles from the user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		Task<IEnumerable<Role>> GetRolesFromUser(Guid userID);
	}
}
using Kindly.API.Models.Repositories.Identity;

using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;

namespace Kindly.API.Models.Repositories.Roles
{
	public class Role : IdentityRole<Guid>
	{
		#region [Constants]
		/// <summary>
		/// The role does not exist message.
		/// </summary>
		public const string DoesNotExist = "The role does not exist.";

		/// <summary>
		/// The user is not in role message.
		/// </summary>
		public const string UserIsNotInRole = "The user is not in the role {0}.";

		/// <summary>
		/// The user is already in role message.
		/// </summary>
		public const string UserIsAlreadyInRole = "The role is already in the role {0}.";
		#endregion

		#region [Properties]
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		public Guid ID
		{
			get { return this.Id; }
			set { this.Id = value; }
		}

		/// <summary>
		/// Gets or sets the users.
		/// </summary>
		public ICollection<UserRole> RoleUsers { get; set; }

		/// <summary>
		/// Gets or sets the claims.
		/// </summary>
		public ICollection<RoleClaim> RoleClaims { get; set; }
		#endregion
	}
}
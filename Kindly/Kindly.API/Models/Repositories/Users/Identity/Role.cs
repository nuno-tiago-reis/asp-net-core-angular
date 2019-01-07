using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;

namespace Kindly.API.Models.Repositories.Users.Identity
{
	public class Role : IdentityRole<Guid>
	{
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
	}
}
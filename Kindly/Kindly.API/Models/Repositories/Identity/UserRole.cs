using Kindly.API.Models.Repositories.Users;
using Kindly.API.Models.Repositories.Roles;

using Microsoft.AspNetCore.Identity;

using System;

namespace Kindly.API.Models.Repositories.Identity
{
	public class UserRole : IdentityUserRole<Guid>
	{
		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		public Guid UserID
		{
			get { return this.UserId; }
			set { this.UserId = value; }
		}

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		public User User { get; set; }

		/// <summary>
		/// Gets or sets the role identifier.
		/// </summary>
		public Guid RoleID
		{
			get { return this.RoleId; }
			set { this.RoleId = value; }
		}

		/// <summary>
		/// Gets or sets the role.
		/// </summary>
		public Role Role { get; set; }
	}
}
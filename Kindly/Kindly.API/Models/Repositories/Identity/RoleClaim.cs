using Microsoft.AspNetCore.Identity;

using System;

namespace Kindly.API.Models.Repositories.Identity
{
	/// <summary>
	/// Defines the role claim entity.
	/// </summary>
	/// 
	/// <seealso cref="IdentityRoleClaim{Guid}" />
	public class RoleClaim : IdentityRoleClaim<Guid>
	{
		/// <summary>
		/// Gets or sets the role identifier.
		/// </summary>
		public Guid RoleID
		{
			get { return this.RoleId; }
			set { this.RoleId = value; }
		}
	}
}
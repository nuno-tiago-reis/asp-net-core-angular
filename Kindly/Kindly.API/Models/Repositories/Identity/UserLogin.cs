using Microsoft.AspNetCore.Identity;

using System;

namespace Kindly.API.Models.Repositories.Identity
{
	/// <summary>
	/// Defines the user login entity.
	/// </summary>
	/// 
	/// <seealso cref="IdentityUserLogin{Guid}" />
	public class UserLogin : IdentityUserLogin<Guid>
	{
		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		public Guid UserID
		{
			get { return this.UserId; }
			set { this.UserId = value; }
		}
	}
}
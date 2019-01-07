using Microsoft.AspNetCore.Identity;

using System;

namespace Kindly.API.Models.Repositories.Users
{
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
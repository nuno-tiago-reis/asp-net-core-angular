using Microsoft.AspNetCore.Identity;

using System;

namespace Kindly.API.Models.Repositories.Identity
{
	/// <summary>
	/// Defines the user claim entity.
	/// </summary>
	/// 
	/// <seealso cref="IdentityUserClaim{Guid}" />
	public class UserClaim : IdentityUserClaim<Guid>
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
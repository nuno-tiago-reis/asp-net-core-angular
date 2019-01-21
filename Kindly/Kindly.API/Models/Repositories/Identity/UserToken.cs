using Microsoft.AspNetCore.Identity;

using System;

namespace Kindly.API.Models.Repositories.Identity
{
	/// <summary>
	/// Defines the user token entity.
	/// </summary>
	/// 
	/// <seealso cref="IdentityUserToken{Guid}" />
	public class UserToken : IdentityUserToken<Guid>
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
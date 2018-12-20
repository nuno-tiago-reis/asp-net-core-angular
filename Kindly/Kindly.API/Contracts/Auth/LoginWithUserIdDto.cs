using System;

namespace Kindly.API.Contracts.Auth
{
	public sealed class LoginWithUserIdDto
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		public string Password { get; set; }
	}
}
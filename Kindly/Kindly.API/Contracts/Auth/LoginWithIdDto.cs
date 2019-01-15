using System;
using System.ComponentModel.DataAnnotations;

namespace Kindly.API.Contracts.Auth
{
	public sealed class LoginWithIdDto
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[Required]
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		[Required]
		public string Password { get; set; }
	}
}
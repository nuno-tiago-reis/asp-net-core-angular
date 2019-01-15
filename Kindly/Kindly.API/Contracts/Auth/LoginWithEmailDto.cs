using System.ComponentModel.DataAnnotations;

namespace Kindly.API.Contracts.Auth
{
	public sealed class LoginWithEmailDto
	{
		/// <summary>
		/// Gets or sets the email address.
		/// </summary>
		[Required]
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		[Required]
		public string Password { get; set; }
	}
}
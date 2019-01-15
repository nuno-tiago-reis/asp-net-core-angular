using System.ComponentModel.DataAnnotations;

namespace Kindly.API.Contracts.Auth
{
	public sealed class LoginWithUserNameDto
	{
		/// <summary>
		/// Gets or sets the user name.
		/// </summary>
		[Required]
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		[Required]
		public string Password { get; set; }
	}
}
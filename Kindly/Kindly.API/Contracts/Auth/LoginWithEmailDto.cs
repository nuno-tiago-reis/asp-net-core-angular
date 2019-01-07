namespace Kindly.API.Contracts.Auth
{
	public sealed class LoginWithEmailDto
	{
		/// <summary>
		/// Gets or sets the email address.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		public string Password { get; set; }
	}
}
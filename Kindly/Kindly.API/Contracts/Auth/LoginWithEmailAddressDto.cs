namespace Kindly.API.Contracts.Auth
{
	public sealed class LoginWithEmailAddressDto
	{
		/// <summary>
		/// Gets or sets the email address.
		/// </summary>
		public string EmailAddress { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		public string Password { get; set; }
	}
}
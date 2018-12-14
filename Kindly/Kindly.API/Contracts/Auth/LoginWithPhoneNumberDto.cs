namespace Kindly.API.Contracts.Auth
{
	public sealed class LoginWithPhoneNumberDto
	{
		/// <summary>
		/// Gets or sets the phone number.
		/// </summary>
		public string PhoneNumber { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		public string Password { get; set; }
	}
}

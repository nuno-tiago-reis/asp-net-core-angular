namespace Kindly.API.Contracts.Auth
{
	public sealed class LoginWithUserNameDto
	{
		/// <summary>
		/// Gets or sets the user name.
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		public string Password { get; set; }
	}
}

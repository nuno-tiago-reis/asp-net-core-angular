using Kindly.API.Contracts.Users;

namespace Kindly.API.Contracts.Auth
{
	public sealed class LoginResponseDto
	{
		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		/// <value>
		/// The user.
		/// </value>
		public UserDto User { get; set; }

		/// <summary>
		/// Gets or sets the token.
		/// </summary>
		/// <value>
		/// The token.
		/// </value>
		public string Token { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="LoginResponseDto"/> class.
		/// </summary>
		/// 
		/// <param name="user">The user.</param>
		/// <param name="token">The token.</param>
		public LoginResponseDto(UserDto user, string token)
		{
			this.User = user;
			this.Token = token;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LoginResponseDto"/> class.
		/// </summary>
		public LoginResponseDto()
		{
			// Nothing to do here.
		}
	}
}
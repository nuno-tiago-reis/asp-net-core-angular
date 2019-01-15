using Kindly.API.Contracts.Users;

using System.ComponentModel.DataAnnotations;

namespace Kindly.API.Contracts.Auth
{
	public sealed class LoginResponseDto
	{
		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		[Required]
		public UserDto User { get; set; }

		/// <summary>
		/// Gets or sets the token.
		/// </summary>
		[Required]
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
	}
}
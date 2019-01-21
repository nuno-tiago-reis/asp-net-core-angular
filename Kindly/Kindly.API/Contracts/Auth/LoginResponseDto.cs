using Kindly.API.Contracts.Users;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Auth
{
	/// <summary>
	/// The response data transfer object for the log in operation.
	/// </summary>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
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
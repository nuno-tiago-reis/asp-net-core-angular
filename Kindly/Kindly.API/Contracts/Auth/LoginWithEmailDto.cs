using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Auth
{
	/// <summary>
	/// The request data transfer object for the log in with email operation.
	/// </summary>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
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
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Auth
{
	/// <summary>
	/// The request data transfer object for the log in with user name operation.
	/// </summary>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
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
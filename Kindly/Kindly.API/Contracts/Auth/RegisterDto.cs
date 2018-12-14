using System.ComponentModel.DataAnnotations;

namespace Kindly.API.Contracts.Auth
{
	public sealed class RegisterDto
	{
		/// <summary>
		/// Gets or sets the user name.
		/// </summary>
		[Required]
		[MinLength(5)]
		[DataType(DataType.Text)]
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets the phone number.
		/// </summary>
		[Required]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		/// <summary>
		/// Gets or sets the email address.
		/// </summary>
		[Required]
		[DataType(DataType.EmailAddress)]
		public string EmailAddress { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		[Required]
		[MinLength(8)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}

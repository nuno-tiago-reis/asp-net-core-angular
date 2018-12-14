using System.ComponentModel.DataAnnotations;

namespace Kindly.API.Contracts.Users
{
	public sealed class UpdateDto
	{
		/// <summary>
		/// Gets or sets the phone number.
		/// </summary>
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		/// <summary>
		/// Gets or sets the email address.
		/// </summary>
		[DataType(DataType.EmailAddress)]
		public string EmailAddress { get; set; }
	}
}

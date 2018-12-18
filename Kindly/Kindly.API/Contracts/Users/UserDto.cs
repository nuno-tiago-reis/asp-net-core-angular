using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Users
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class UserDto
	{
		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the user name.
		/// </summary>
		[Required]
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
	}
}

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using Kindly.API.Models.Domain;

namespace Kindly.API.Contracts.Users
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public class UserDto
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

		/// <summary>
		/// Gets or sets the known as name.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		public string KnownAs { get; set; }

		/// <summary>
		/// Gets or sets the gender.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		public Gender Gender { get; set; }

		/// <summary>
		/// Gets or sets the age.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		public int Age { get; set; }

		/// <summary>
		/// Gets or sets the city.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		public string City { get; set; }

		/// <summary>
		/// Gets or sets the country.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		public string Country { get; set; }

		/// <summary>
		/// Gets or sets the created at date.
		/// </summary>
		[Required]
		[DataType(DataType.Date)]
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Gets or sets the last active at date.
		/// </summary>
		[Required]
		[DataType(DataType.Date)]
		public DateTime LastActiveAt { get; set; }

		/// <summary>
		/// Gets or sets the picture url.
		/// </summary>
		[Required]
		[DataType(DataType.ImageUrl)]
		public string ProfilePictureUrl { get; set; }
	}
}
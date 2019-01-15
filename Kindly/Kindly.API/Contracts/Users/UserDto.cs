using Kindly.API.Models.Repositories.Users;

using Newtonsoft.Json;

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Users
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public class UserDto
	{
		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		[Required]
		[JsonProperty(Order = 1)]
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the user name.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		[JsonProperty(Order = 2)]
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets the email address.
		/// </summary>
		[Required]
		[DataType(DataType.EmailAddress)]
		[JsonProperty(Order = 3)]
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the phone number.
		/// </summary>
		[Required]
		[DataType(DataType.PhoneNumber)]
		[JsonProperty(Order = 4)]
		public string PhoneNumber { get; set; }

		/// <summary>
		/// Gets or sets the known as name.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		[JsonProperty(Order = 5)]
		public string KnownAs { get; set; }

		/// <summary>
		/// Gets or sets the gender.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		[JsonProperty(Order = 6)]
		public Gender Gender { get; set; }

		/// <summary>
		/// Gets or sets the age.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		[JsonProperty(Order = 7)]
		public int Age { get; set; }

		/// <summary>
		/// Gets or sets the city.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		[JsonProperty(Order = 8)]
		public string City { get; set; }

		/// <summary>
		/// Gets or sets the country.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		[JsonProperty(Order = 9)]
		public string Country { get; set; }

		/// <summary>
		/// Gets or sets the created at date.
		/// </summary>
		[Required]
		[DataType(DataType.Date)]
		[JsonProperty(Order = 10)]
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Gets or sets the last active at date.
		/// </summary>
		[Required]
		[DataType(DataType.Date)]
		[JsonProperty(Order = 11)]
		public DateTime LastActiveAt { get; set; }

		/// <summary>
		/// Gets or sets the picture url.
		/// </summary>
		[Required]
		[DataType(DataType.ImageUrl)]
		[JsonProperty(Order = 12)]
		public string ProfilePictureUrl { get; set; }
	}
}
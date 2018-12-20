using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using Kindly.API.Models.Domain;

namespace Kindly.API.Contracts.Users
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class UpdateUserDto
	{
		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		public Guid ID { get; set; }

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

		/// <summary>
		/// Gets or sets the known as name.
		/// </summary>
		[DataType(DataType.Text)]
		public string KnownAs { get; set; }

		/// <summary>
		/// Gets or sets the gender.
		/// </summary>
		[DataType(DataType.Text)]
		public Gender Gender { get; set; }

		/// <summary>
		/// Gets or sets the birth date.
		/// </summary>
		[DataType(DataType.Date)]
		public DateTime BirthDate { get; set; }

		/// <summary>
		/// Gets or sets the introduction.
		/// </summary>
		[DataType(DataType.Text)]
		public string Introduction { get; set; }

		/// <summary>
		/// Gets or sets the interests.
		/// </summary>
		[DataType(DataType.Text)]
		public string Interests { get; set; }

		/// <summary>
		/// Gets or sets what the user is looking for.
		/// </summary>
		[DataType(DataType.Text)]
		public string LookingFor { get; set; }

		/// <summary>
		/// Gets or sets the city.
		/// </summary>
		[DataType(DataType.Text)]
		public string City { get; set; }

		/// <summary>
		/// Gets or sets the country.
		/// </summary>
		[DataType(DataType.Text)]
		public string Country { get; set; }
	}
}
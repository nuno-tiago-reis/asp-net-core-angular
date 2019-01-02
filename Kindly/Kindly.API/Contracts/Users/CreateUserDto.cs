using Kindly.API.Models.Repositories.Users;

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Users
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class CreateUserDto
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
		/// Gets or sets the birth date.
		/// </summary>
		[Required]
		[DataType(DataType.Date)]
		public DateTime BirthDate { get; set; }

		/// <summary>
		/// Gets or sets the introduction.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		public string Introduction { get; set; }

		/// <summary>
		/// Gets or sets the interests.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		public string Interests { get; set; }

		/// <summary>
		/// Gets or sets what the user is looking for.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		public string LookingFor { get; set; }

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
	}
}
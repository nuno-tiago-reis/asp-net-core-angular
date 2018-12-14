using System;

namespace Kindly.API.Models
{
	public class User
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the user name.
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets the phone number.
		/// </summary>
		public string PhoneNumber { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the [phone number is confirmed].
		/// </summary>
		public bool PhoneNumberConfirmed { get; set; }

		/// <summary>
		/// Gets or sets the email address.
		/// </summary>
		public string EmailAddress { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the [email address is confirmed].
		/// </summary>
		public bool EmailAddressConfirmed { get; set; }

		/// <summary>
		/// Gets or sets the password hash.
		/// </summary>
		public byte[] PasswordHash { get; set; }

		/// <summary>
		/// Gets or sets the password salt.
		/// </summary>
		public byte[] PasswordSalt { get; set; }
	}
}
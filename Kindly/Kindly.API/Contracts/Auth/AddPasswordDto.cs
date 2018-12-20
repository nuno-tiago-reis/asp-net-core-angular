using System;
using System.ComponentModel.DataAnnotations;

namespace Kindly.API.Contracts.Auth
{
	public sealed class AddPasswordDto
	{
		/// <summary>
		/// Gets or sets the user identifier
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		[Required]
		[MinLength(8)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
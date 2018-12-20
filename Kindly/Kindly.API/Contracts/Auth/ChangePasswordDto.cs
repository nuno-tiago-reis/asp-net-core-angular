using System;
using System.ComponentModel.DataAnnotations;

namespace Kindly.API.Contracts.Auth
{
	public sealed class ChangePasswordDto
	{
		/// <summary>
		/// Gets or sets the user identifier
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the old password.
		/// </summary>
		[Required]
		[MinLength(8)]
		[DataType(DataType.Password)]
		public string OldPassword { get; set; }

		/// <summary>
		/// Gets or sets the new password.
		/// </summary>
		[Required]
		[MinLength(8)]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }
	}
}
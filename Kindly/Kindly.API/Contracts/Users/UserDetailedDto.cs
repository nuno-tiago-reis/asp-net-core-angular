using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using Kindly.API.Contracts.Pictures;

namespace Kindly.API.Contracts.Users
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class UserDetailedDto : UserDto
	{
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
		/// Gets or sets the pictures.
		/// </summary>
		[Required]
		public ICollection<PictureDto> Pictures { get; set; }
	}
}
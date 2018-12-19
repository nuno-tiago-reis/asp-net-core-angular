using System;
using System.ComponentModel.DataAnnotations;

namespace Kindly.API.Contracts.Pictures
{
	public sealed class UpdatePictureDto
	{
		/// <summary>
		/// Gets or sets the picture identifier.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the url.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		public string Url { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is the profile picture.
		/// </summary>
		[Required]
		public bool IsProfilePicture { get; set; }
	}
}

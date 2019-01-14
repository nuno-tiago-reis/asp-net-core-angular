using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Pictures
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class PictureDto
	{
		/// <summary>
		/// Gets or sets the picture identifier.
		/// </summary>
		[Required]
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the url.
		/// </summary>
		[Required]
		[DataType(DataType.ImageUrl)]
		public string Url { get; set; }

		/// <summary>
		/// Gets or sets the public identifier.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		public string PublicID { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is approved.
		/// </summary>
		[Required]
		public bool IsApproved { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is the profile picture.
		/// </summary>
		[Required]
		public bool IsProfilePicture { get; set; }

		/// <summary>
		/// Gets or sets the created at date.
		/// </summary>
		[Required]
		[DataType(DataType.Date)]
		public DateTime CreateAt { get; set; }

		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		[Required]
		public Guid UserID { get; set; }
	}
}
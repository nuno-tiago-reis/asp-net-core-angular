using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace Kindly.API.Contracts.Pictures
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class PictureDto
	{
		/// <summary>
		/// Gets or sets the picture identifier.
		/// </summary>
		[Required]
		[JsonProperty(Order = 1)]
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the url.
		/// </summary>
		[Required]
		[DataType(DataType.ImageUrl)]
		[JsonProperty(Order = 2)]
		public string Url { get; set; }

		/// <summary>
		/// Gets or sets the public identifier.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		[JsonProperty(Order = 3)]
		public string PublicID { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		[JsonProperty(Order = 4)]
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is the profile picture.
		/// </summary>
		[Required]
		[JsonProperty(Order = 5)]
		public bool IsProfilePicture { get; set; }

		/// <summary>
		/// Gets or sets the created at date.
		/// </summary>
		[Required]
		[DataType(DataType.Date)]
		[JsonProperty(Order = 6)]
		public DateTime CreateAt { get; set; }

		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		[Required]
		[JsonProperty(Order = 7)]
		public Guid UserID { get; set; }
	}
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace Kindly.API.Contracts.Pictures
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public class PictureDto
	{
		/// <summary>
		/// Gets or sets the picture identifier.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		[JsonProperty(Order = 1)]
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the url.
		/// </summary>
		[Required]
		[DataType(DataType.Text)]
		[JsonProperty(Order = 2)]
		public string Url { get; set; }

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
		/// Gets or sets the added at.
		/// </summary>
		[Required]
		[DataType(DataType.Date)]
		[JsonProperty(Order = 6)]
		public DateTime AddedAt { get; set; }

		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		[Required]
		[JsonProperty(Order = 7)]
		public Guid UserID { get; set; }
	}
}
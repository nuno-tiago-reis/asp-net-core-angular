using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Http;

namespace Kindly.API.Contracts.Pictures
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class CreatePictureDto
	{
		/// <summary>
		/// Gets or sets the file.
		/// </summary>
		[Required]
		[DataType(DataType.Upload)]
		public IFormFile File { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		[DataType(DataType.Text)]
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is the profile picture.
		/// </summary>
		public bool? IsProfilePicture { get; set; }
	}
}
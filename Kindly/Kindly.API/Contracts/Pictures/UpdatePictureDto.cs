using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Pictures
{
	/// <summary>
	/// The request data transfer object for the update picture operation.
	/// </summary>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class UpdatePictureDto
	{
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
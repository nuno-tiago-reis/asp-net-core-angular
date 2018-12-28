using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace Kindly.API.Contracts.Pictures
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class PictureDetailedDto : PictureDto
	{
		/// <summary>
		/// Gets or sets the public identifier.
		/// </summary>
		[Required]
		[JsonProperty(Order = 3)]
		[DataType(DataType.Text)]
		public string PublicID { get; set; }
	}
}
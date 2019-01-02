using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace Kindly.API.Contracts.Likes
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public class LikeDto
	{
		/// <summary>
		/// Gets or sets the like identifier.
		/// </summary>
		[Required]
		[JsonProperty(Order = 1)]
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the target identifier.
		/// </summary>
		[Required]
		[JsonProperty(Order = 1)]
		public Guid TargetID { get; set; }

		/// <summary>
		/// Gets or sets the source identifier.
		/// </summary>
		[Required]
		[JsonProperty(Order = 2)]
		public Guid SourceID { get; set; }

		/// <summary>
		/// Gets or sets the created at date.
		/// </summary>
		[Required]
		[DataType(DataType.Date)]
		[JsonProperty(Order = 6)]
		public DateTime CreateAt { get; set; }
	}
}
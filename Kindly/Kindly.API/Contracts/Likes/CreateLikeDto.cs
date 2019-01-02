using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace Kindly.API.Contracts.Likes
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class CreateLikeDto
	{
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
	}
}
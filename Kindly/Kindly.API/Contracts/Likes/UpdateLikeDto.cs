using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Likes
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class UpdateLikeDto
	{
		/// <summary>
		/// Gets or sets the target identifier.
		/// </summary>
		[Required]
		public Guid TargetID { get; set; }
	}
}
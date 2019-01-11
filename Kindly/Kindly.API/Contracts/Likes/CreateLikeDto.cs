using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Likes
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class CreateLikeDto
	{
		/// <summary>
		/// Gets or sets the recipient identifier.
		/// </summary>
		[Required]
		public Guid RecipientID { get; set; }
	}
}
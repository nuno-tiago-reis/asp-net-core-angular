using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Messages
{
	/// <summary>
	/// The request data transfer object for the create message operation.
	/// </summary>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class CreateMessageDto
	{
		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		[Required]
		public string Content { get; set; }

		/// <summary>
		/// Gets or sets the recipient identifier.
		/// </summary>
		[Required]
		public Guid RecipientID { get; set; }
	}
}
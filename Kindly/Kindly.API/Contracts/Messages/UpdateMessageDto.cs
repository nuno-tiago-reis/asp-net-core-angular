using System;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Messages
{
	/// <summary>
	/// The request data transfer object for the update message operation.
	/// </summary>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class UpdateMessageDto
	{
		/// <summary>
		/// Gets or sets a value indicating whether this message is read.
		/// </summary>
		public bool? IsRead { get; set; }

		/// <summary>
		/// Gets or sets the date at which the message was read.
		/// </summary>
		public DateTime? ReadAt { get; set; }
	}
}
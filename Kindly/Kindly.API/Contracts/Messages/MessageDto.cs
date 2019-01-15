using Kindly.API.Contracts.Users;

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Messages
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class MessageDto
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[Required]
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		[Required]
		public string Content { get; set; }

		/// <summary>
		/// Gets or sets the sender identifier.
		/// </summary>
		[Required]
		public Guid SenderID { get; set; }

		/// <summary>
		/// Gets or sets the sender.
		/// </summary>
		[Required]
		public UserDto Sender { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the [sender has deleted] the message.
		/// </summary>
		[Required]
		public bool SenderDeleted { get; set; }

		/// <summary>
		/// Gets or sets the recipient identifier.
		/// </summary>
		[Required]
		public Guid RecipientID { get; set; }

		/// <summary>
		/// Gets or sets the recipient.
		/// </summary>
		[Required]
		public UserDto Recipient { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the [recipient has deleted] the message.
		/// </summary>
		[Required]
		public bool RecipientDeleted { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this message is read.
		/// </summary>
		[Required]
		public bool? IsRead { get; set; }

		/// <summary>
		/// Gets or sets the date at which the message was read.
		/// </summary>
		[Required]
		public DateTime? ReadAt { get; set; }

		/// <summary>
		/// Gets or sets the date at which the message was created.
		/// </summary>
		[Required]
		[DataType(DataType.Date)]
		public DateTime CreatedAt { get; set; }
	}
}
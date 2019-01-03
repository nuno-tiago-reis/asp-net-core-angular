using Kindly.API.Models.Repositories.Users;

using System;

namespace Kindly.API.Models.Repositories.Messages
{
	public class Message
	{
		#region [Constants]
		/// <summary>
		/// The message does not exist message.
		/// </summary>
		public const string DoesNotExist = "The message does not exist.";

		/// <summary>
		/// The message cannot be deleted message.
		/// </summary>
		public const string CannotBeDeleted = "The message can only be deleted if the sender and recipient delete it first..";
		#endregion

		#region [Properties]
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Gets or sets the sender identifier.
		/// </summary>
		public Guid SenderID { get; set; }

		/// <summary>
		/// Gets or sets the sender.
		/// </summary>
		public User Sender { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the [sender has deleted] the message.
		/// </summary>
		public bool? SenderDeleted { get; set; }

		/// <summary>
		/// Gets or sets the recipient identifier.
		/// </summary>
		public Guid RecipientID { get; set; }

		/// <summary>
		/// Gets or sets the recipient.
		/// </summary>
		public User Recipient { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the [recipient has deleted] the message.
		/// </summary>
		public bool? RecipientDeleted { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this message is read.
		/// </summary>
		public bool? IsRead { get; set; }

		/// <summary>
		/// Gets or sets the date at which the message was read.
		/// </summary>
		public DateTime? ReadAt { get; set; }

		/// <summary>
		/// Gets or sets the date at which the message was created.
		/// </summary>
		public DateTime CreatedAt { get; set; }
		#endregion
	}
}
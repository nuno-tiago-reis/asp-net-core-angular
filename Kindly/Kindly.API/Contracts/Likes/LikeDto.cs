using Kindly.API.Contracts.Users;

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Likes
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class LikeDto
	{
		/// <summary>
		/// Gets or sets the like identifier.
		/// </summary>
		[Required]
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the sender identifier.
		/// </summary>
		[Required]
		public Guid? SenderID { get; set; }

		/// <summary>
		/// Gets or sets the sender.
		/// </summary>
		[Required]
		public UserDto Sender { get; set; }

		/// <summary>
		/// Gets or sets the recipient identifier.
		/// </summary>
		[Required]
		public Guid? RecipientID { get; set; }

		/// <summary>
		/// Gets or sets the recipient.
		/// </summary>
		[Required]
		public UserDto Recipient { get; set; }

		/// <summary>
		/// Gets or sets the created at date.
		/// </summary>
		[Required]
		[DataType(DataType.Date)]
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Cleans the sender.
		/// </summary>
		public void RemoveSender()
		{
			this.SenderID = null;
			this.Sender = null;
		}

		/// <summary>
		/// Cleans the recipient.
		/// </summary>
		public void RemoveRecipient()
		{
			this.RecipientID = null;
			this.Recipient = null;
		}
	}
}
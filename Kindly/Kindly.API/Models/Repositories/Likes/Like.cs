using Kindly.API.Models.Repositories.Users;

using System;

namespace Kindly.API.Models.Repositories.Likes
{
	/// <summary>
	/// Defines the like entity.
	/// </summary>
	public class Like
	{
		#region [Constants]
		/// <summary>
		/// The like does not exist message.
		/// </summary>
		public const string DoesNotExist = "The like does not exist.";

		/// <summary>
		/// The user recipient already exists message.
		/// </summary>
		public const string AlreadyExists = "Already liked this user.";
		#endregion

		#region [Properties]
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the sender.
		/// </summary>
		public User Sender { get; set; }

		/// <summary>
		/// Gets or sets the sender identifier.
		/// </summary>
		public Guid SenderID { get; set; }

		/// <summary>
		/// Gets or sets the recipient.
		/// </summary>
		public User Recipient { get; set; }

		/// <summary>
		/// Gets or sets the recipient identifier.
		/// </summary>
		public Guid RecipientID { get; set; }

		/// <summary>
		/// Gets or sets the date at which the like was created.
		/// </summary>
		public DateTime CreatedAt { get; set; }
		#endregion
	}
}
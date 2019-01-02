using System;

namespace Kindly.API.Models.Domain
{
	public class Like
	{
		#region [Constants]
		/// <summary>
		/// The like does not exist message.
		/// </summary>
		public const string DoesNotExist = "The like does not exist.";

		/// <summary>
		/// The user target already exists message.
		/// </summary>
		public const string AlreadyExists = "Already liked this user.";
		#endregion

		#region [Properties]
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the source.
		/// </summary>
		public User Source { get; set; }

		/// <summary>
		/// Gets or sets the source identifier.
		/// </summary>
		public Guid SourceID { get; set; }

		/// <summary>
		/// Gets or sets the target.
		/// </summary>
		public User Target { get; set; }

		/// <summary>
		/// Gets or sets the target identifier.
		/// </summary>
		public Guid TargetID { get; set; }

		/// <summary>
		/// Gets or sets the date at which the like was created.
		/// </summary>
		public DateTime CreatedAt { get; set; }
		#endregion
	}
}
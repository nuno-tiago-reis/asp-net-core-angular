using System;

namespace Kindly.API.Models.Domain
{
	public class Picture
	{
		#region [Constants]
		/// <summary>
		/// The picture does not exist message.
		/// </summary>
		public const string DoesNotExist = "The picture does not exist.";
		#endregion

		#region [Properties]
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		public Guid ID { get; set; }

		/// <summary>
		/// Gets or sets the url.
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Gets or sets the public identifier.
		/// </summary>
		public string PublicID { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the added at.
		/// </summary>
		public DateTime AddedAt { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is the profile picture.
		/// </summary>
		public bool? IsProfilePicture { get; set; }

		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		public Guid UserID { get; set; }

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		public User User { get; set; }
		#endregion
	}
}
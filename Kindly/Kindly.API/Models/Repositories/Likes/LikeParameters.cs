namespace Kindly.API.Models.Repositories.Likes
{
	/// <summary>
	/// The like container to be returned in a query.
	/// </summary>
	public enum LikeContainer
	{
		/// <summary>
		/// Returns only the recipients.
		/// </summary>
		Recipients = 0,

		/// <summary>
		/// Returns only the senders.
		/// </summary>
		Senders = 1
	}

	/// <summary>
	/// Provides pagination parameters as well as filtering over the like queries.
	/// </summary>
	/// 
	/// <seealso cref="PaginationParameters" />
	public sealed class LikeParameters : PaginationParameters
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the mode.
		/// </summary>
		public LikeContainer Container { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to [include the request user].
		/// </summary>
		public bool IncludeRequestUser { get; set; }
		#endregion
	}
}
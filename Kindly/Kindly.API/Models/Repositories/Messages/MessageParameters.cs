namespace Kindly.API.Models.Repositories.Messages
{
	/// <summary>
	/// The message container to be returned in a query.
	/// </summary>
	public enum MessageContainer
	{
		/// <summary>
		/// Returns the unread messages.
		/// </summary>
		Unread = 0,

		/// <summary>
		/// Returns the outbox messages.
		/// </summary>
		Outbox = 1,

		/// <summary>
		/// Returns the inbox messages.
		/// </summary>
		Inbox = 2
	}

	/// <summary>
	/// Provides pagination parameters as well as filtering over the message queries.
	/// </summary>
	/// 
	/// <seealso cref="PaginationParameters" />
	public sealed class MessageParameters : PaginationParameters
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the container.
		/// </summary>
		public MessageContainer Container { get; set; }
		#endregion
	}
}
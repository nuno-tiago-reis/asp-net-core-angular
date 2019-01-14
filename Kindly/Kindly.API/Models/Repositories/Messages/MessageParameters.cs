namespace Kindly.API.Models.Repositories.Messages
{
	public enum MessageContainer
	{
		Unread = 0,
		Outbox = 1,
		Inbox = 2
	}

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
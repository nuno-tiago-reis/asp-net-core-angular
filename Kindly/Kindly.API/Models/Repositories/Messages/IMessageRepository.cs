using Kindly.API.Utility.Collections;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kindly.API.Models.Repositories.Messages
{
	public interface IMessageRepository : IEntityRepository<Message, MessageParameters>
	{
		/// <summary>
		/// Checks if a message belongs to a user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="messageID">The message identifier.</param>
		Task<bool> MessageBelongsToUser(Guid userID, Guid messageID);

		/// <summary>
		/// Gets messages by user id.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		Task<IEnumerable<Message>> GetByUser(Guid userID);

		/// <summary>
		/// Gets messages by user id using pagination.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="parameters">The parameters.</param>
		Task<PagedList<Message>> GetByUser(Guid userID, MessageParameters parameters);

		/// <summary>
		/// Gets messages by thread
		/// </summary>
		/// 
		/// <param name="senderID">The sender identifier.</param>
		/// <param name="recipientID">The recipient identifier.</param>
		Task<IEnumerable<Message>> GetThreadByUsers(Guid senderID, Guid recipientID);
	}
}
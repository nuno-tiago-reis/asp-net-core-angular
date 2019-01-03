using Kindly.API.Models.Repositories.Users;
using Kindly.API.Utility;
using Kindly.API.Utility.Collections;

using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Kindly.API.Models.Repositories.Messages
{
	public sealed class MessageRepository : IMessageRepository
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		public KindlyContext Context { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageRepository"/> class.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>
		public MessageRepository(KindlyContext context)
		{
			this.Context = context;
		}
		#endregion

		#region [Methods] IEntityRepository
		/// <inheritdoc />
		public async Task<Message> Create(Message message)
		{
			// Properties
			if (string.IsNullOrWhiteSpace(message.Content))
				throw new KindlyException(message.InvalidFieldMessage(m => m.Content));

			// Foreign Keys
			var sender = await this.Context.Users
				.Include(u => u.Pictures)
				.SingleOrDefaultAsync(u => u.ID == message.SenderID);
			if (sender == null)
				throw new KindlyException(User.DoesNotExist, true);

			var recipient = await this.Context.Users
				.Include(u => u.Pictures)
				.SingleOrDefaultAsync(u => u.ID == message.RecipientID);
			if (recipient == null)
				throw new KindlyException(User.DoesNotExist, true);

			// Create
			this.Context.Add(message);

			await this.Context.SaveChangesAsync();

			return message;
		}

		/// <inheritdoc />
		public async Task<Message> Update(Message message)
		{
			var databaseMessage = await this.Context.Messages.FindAsync(message.ID);
			if (databaseMessage == null)
				throw new KindlyException(Message.DoesNotExist, true);

			// Properties
			databaseMessage.IsRead =
				message.IsRead ?? databaseMessage.IsRead;

			databaseMessage.ReadAt =
				message.ReadAt ?? databaseMessage.ReadAt;

			// Update
			await this.Context.SaveChangesAsync();

			return databaseMessage;
		}

		/// <inheritdoc />
		public async Task Delete(Guid messageID)
		{
			var message = await this.Context.Messages.FindAsync(messageID);
			if (message == null)
				throw new KindlyException(Message.DoesNotExist, true);
			if (message.SenderDeleted.HasValue && message.SenderDeleted.Value == false ||
			    message.RecipientDeleted.HasValue && message.RecipientDeleted.Value == false)
				throw new KindlyException(Message.CannotBeDeleted);

			// Delete
			this.Context.Messages.Remove(message);

			await this.Context.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<Message> Get(Guid messageID)
		{
			return await this.GetQueryable().SingleOrDefaultAsync(p => p.ID == messageID);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Message>> GetAll()
		{
			return await this.GetQueryable().ToListAsync();
		}

		/// <inheritdoc />
		public async Task<PagedList<Message>> GetAll(MessageParameters parameters)
		{
			var messages = this.GetQueryable();

			if (string.IsNullOrWhiteSpace(parameters.OrderBy) == false)
			{
				if (parameters.OrderBy == nameof(Message.CreatedAt).ToLowerCamelCase())
				{
					messages = messages.OrderByDescending(m => m.CreatedAt);
				}
				else
				{
					throw new ArgumentOutOfRangeException(nameof(parameters.OrderBy), parameters.OrderBy, null);
				}
			}

			return await PagedList<Message>.CreateAsync(messages, parameters.PageNumber, parameters.PageSize);
		}
		#endregion

		#region [Methods] IMessageRepository
		/// <inheritdoc />
		public async Task<bool> MessageBelongsToUser(Guid userID, Guid messageID)
		{
			var user = await this.Context.Users.FindAsync(userID);
			if (user == null)
				throw new KindlyException(User.DoesNotExist, true);

			var message = await this.Context.Messages.SingleOrDefaultAsync
			(
				m => m.ID == messageID && (m.SenderID == userID || m.RecipientID == userID)
			);
			if (message == null)
				throw new KindlyException(Message.DoesNotExist, true);

			return true;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Message>> GetByUser(Guid userID)
		{
			return await this.GetQueryableByUser(userID).ToListAsync();
		}

		/// <inheritdoc />
		public async Task<PagedList<Message>> GetByUser(Guid userID, MessageParameters parameters)
		{
			var messages = this.GetQueryableByUser(userID);

			switch (parameters.Container)
			{
				case MessageContainer.Unread:
					messages = messages
						.Where(m => m.RecipientID == userID && m.RecipientDeleted == false && m.IsRead == false);
					break;

				case MessageContainer.Inbox:
					messages = messages
						.Where(m => m.RecipientID == userID && m.RecipientDeleted == false);
					break;

				case MessageContainer.Outbox:
					messages = messages
						.Where(m => m.SenderID == userID && m.SenderDeleted == false);
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(parameters.Container), parameters.Container, null);
			}

			if (string.IsNullOrWhiteSpace(parameters.OrderBy) == false)
			{
				if (parameters.OrderBy == nameof(Message.CreatedAt).ToLowerCamelCase())
				{
					messages = messages.OrderByDescending(m => m.CreatedAt);
				}
				else
				{
					throw new ArgumentOutOfRangeException(nameof(parameters.OrderBy), parameters.OrderBy, null);
				}
			}

			return await PagedList<Message>.CreateAsync(messages, parameters.PageNumber, parameters.PageSize);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Message>> GetThread(Guid senderID, Guid recipientID)
		{
			var messages = this.GetQueryable()
				.Where
				(
					m =>
						m.SenderID == senderID && m.RecipientID == recipientID && m.SenderDeleted == false ||
						m.SenderID == recipientID && m.RecipientID == senderID && m.RecipientDeleted == false
				)
				.OrderBy(m => m.CreatedAt);

			return await messages.ToListAsync();
		}
		#endregion

		#region [Methods] Utility
		/// <summary>
		/// Gets the queryable.
		/// </summary>
		private IQueryable<Message> GetQueryable()
		{
			return this.Context.Messages
				.Include(m => m.Sender.Pictures)
				.Include(m => m.Recipient.Pictures)
				.OrderBy(m => m.CreatedAt);
		}

		/// <summary>
		/// Gets the queryable by user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		private IQueryable<Message> GetQueryableByUser(Guid userID)
		{
			return this.Context.Messages
				.Include(m => m.Sender.Pictures)
				.Include(m => m.Recipient.Pictures)
				.Where(l => l.SenderID == userID || l.RecipientID == userID)
				.OrderByDescending(l => l.CreatedAt);
		}
		#endregion
	}
}
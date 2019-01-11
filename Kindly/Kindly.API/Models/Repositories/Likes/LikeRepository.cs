using Kindly.API.Models.Repositories.Users;
using Kindly.API.Utility;
using Kindly.API.Utility.Collections;

using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Kindly.API.Models.Repositories.Likes
{
	public sealed class LikeRepository : ILikeRepository
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		public KindlyContext Context { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="LikeRepository"/> class.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>
		public LikeRepository(KindlyContext context)
		{
			this.Context = context;
		}
		#endregion

		#region [Methods] IEntityRepository
		/// <inheritdoc />
		public async Task<Like> Create(Like like)
		{
			// Foreign Keys
			var sender = await this.Context.Users
				.Include(u => u.Pictures)
				.SingleOrDefaultAsync(u => u.Id == like.SenderID);
			if (sender == null)
				throw new KindlyException(User.DoesNotExist, true);

			var recipient = await this.Context.Users
				.Include(u => u.Pictures)
				.SingleOrDefaultAsync(u => u.Id == like.RecipientID);
			if (recipient == null)
				throw new KindlyException(User.DoesNotExist, true);

			if (await this.Context.Likes.AnyAsync(l => l.SenderID == like.SenderID && l.RecipientID == like.RecipientID))
				throw new KindlyException(Like.AlreadyExists);

			// Create
			this.Context.Add(like);

			await this.Context.SaveChangesAsync();

			return like;
		}

		/// <inheritdoc />
		public async Task<Like> Update(Like like)
		{
			var databaseLike = await this.Context.Likes.FindAsync(like.ID);
			if (databaseLike == null)
				throw new KindlyException(Like.DoesNotExist, true);

			// Properties

			// Update
			await this.Context.SaveChangesAsync();

			return databaseLike;
		}

		/// <inheritdoc />
		public async Task Delete(Guid likeID)
		{
			var like = await this.Context.Likes.FindAsync(likeID);
			if (like == null)
				throw new KindlyException(Like.DoesNotExist, true);

			// Delete
			this.Context.Likes.Remove(like);

			await this.Context.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<Like> Get(Guid likeID)
		{
			return await this.GetQueryable().SingleOrDefaultAsync(p => p.ID == likeID);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Like>> GetAll()
		{
			return await this.GetQueryable().ToListAsync();
		}

		/// <inheritdoc />
		public async Task<PagedList<Like>> GetAll(LikeParameters parameters)
		{
			var likes = this.GetQueryable();

			if (string.IsNullOrWhiteSpace(parameters.OrderBy) == false)
			{
				if (parameters.OrderBy == nameof(Like.CreatedAt).ToLowerCamelCase())
				{
					likes = likes.OrderByDescending(l => l.CreatedAt);
				}
				else
				{
					throw new ArgumentOutOfRangeException(nameof(parameters.OrderBy), parameters.OrderBy, null);
				}
			}

			return await PagedList<Like>.CreateAsync(likes, parameters.PageNumber, parameters.PageSize);
		}
		#endregion

		#region [Methods] ILikeRepository
		/// <inheritdoc />
		public async Task<bool> LikeBelongsToUser(Guid userID, Guid likeID)
		{
			bool exists = await this.Context.Likes
				.AnyAsync(p => p.ID == likeID && p.SenderID == userID);

			if (exists == false)
				throw new KindlyException(Like.DoesNotExist, true);

			return true;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Like>> GetBySenderUser(Guid userID)
		{
			return await this.GetQueryableBySenderUser(userID).ToListAsync();
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Like>> GetByRecipientUser(Guid userID)
		{
			return await this.GetQueryableByRecipientUser(userID).ToListAsync();
		}

		/// <inheritdoc />
		public async Task<PagedList<Like>> GetBySenderUser(Guid userID, LikeParameters parameters)
		{
			var likes = this.GetQueryableBySenderUser(userID);

			return await PagedList<Like>.CreateAsync(likes, parameters.PageNumber, parameters.PageSize);
		}

		/// <inheritdoc />
		public async Task<PagedList<Like>> GetByRecipientUser(Guid userID, LikeParameters parameters)
		{
			var likes = this.GetQueryableByRecipientUser(userID);

			return await PagedList<Like>.CreateAsync(likes, parameters.PageNumber, parameters.PageSize);
		}
		#endregion

		#region [Methods] Utility
		/// <summary>
		/// Gets the queryable.
		/// </summary>
		private IQueryable<Like> GetQueryable()
		{
			return this.Context.Likes
				.Include(l => l.Sender.Pictures)
				.Include(l => l.Recipient.Pictures)
				.OrderByDescending(l => l.CreatedAt);
		}
		
		/// <summary>
		/// Gets the queryable by sender user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		private IQueryable<Like> GetQueryableBySenderUser(Guid userID)
		{
			return this.GetQueryable().Where(l => l.SenderID == userID);
		}

		/// <summary>
		/// Gets the queryable by recipient user.
		/// </summary>
		/// <param name="userID">The user identifier.</param>
		private IQueryable<Like> GetQueryableByRecipientUser(Guid userID)
		{
			return this.GetQueryable().Where(l => l.RecipientID == userID);
		}
		#endregion
	}
}
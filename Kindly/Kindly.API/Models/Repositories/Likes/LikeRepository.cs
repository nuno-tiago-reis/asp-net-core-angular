using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using Kindly.API.Contracts;
using Kindly.API.Contracts.Likes;
using Kindly.API.Models.Domain;
using Kindly.API.Utility;

namespace Kindly.API.Models.Repositories
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
			var sourceUser = await this.Context.Users.FindAsync(like.SourceID);
			if (sourceUser == null)
				throw new KindlyException(User.DoesNotExist, true);

			var targetUser = await this.Context.Users.FindAsync(like.TargetID);
			if (targetUser == null)
				throw new KindlyException(User.DoesNotExist, true);

			if (await this.Context.Likes.AnyAsync(l => l.SourceID == like.SourceID && l.TargetID == like.TargetID))
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
			return await this.Context.Likes.FindAsync(likeID);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Like>> GetAll()
		{
			return await this.Context.Likes.ToListAsync();
		}

		/// <inheritdoc />
		public async Task<PagedList<Like>> GetAll(LikeParameters parameters)
		{
			var likes = this.Context.Likes.OrderByDescending(u => u.CreatedAt);

			return await PagedList<Like>.CreateAsync(likes, parameters.PageNumber, parameters.PageSize);
		}
		#endregion

		#region [Methods] ILikeRepository
		/// <inheritdoc />
		public async Task<bool> LikeBelongsToUser(Guid userID, Guid likeID)
		{
			var user = await this.Context.Users.FindAsync(userID);
			if (user == null)
				throw new KindlyException(User.DoesNotExist, true);

			var like = await this.Context.Likes.SingleOrDefaultAsync(l => l.ID == likeID && l.SourceID == userID);
			if (like == null)
				throw new KindlyException(Like.DoesNotExist, true);

			return true;
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Like>> GetBySourceUser(Guid userID)
		{
			return await this.Context.Likes
				.Where(picture => picture.SourceID == userID)
				.ToListAsync();
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Like>> GetByTargetUser(Guid userID)
		{
			return await this.Context.Likes
				.Where(picture => picture.SourceID == userID)
				.ToListAsync();
		}
		#endregion
	}
}
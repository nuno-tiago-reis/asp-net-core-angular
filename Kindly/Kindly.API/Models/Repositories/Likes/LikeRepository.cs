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
			var source = await this.Context.Users
				.Include(u => u.Pictures)
				.SingleOrDefaultAsync(u => u.ID == like.SourceID);
			if (source == null)
				throw new KindlyException(User.DoesNotExist, true);

			var target = await this.Context.Users
				.Include(u => u.Pictures)
				.SingleOrDefaultAsync(u => u.ID == like.TargetID);
			if (target == null)
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
			return await this.GetQueryableBySourceUser(userID).ToListAsync();
		}

		/// <inheritdoc />
		public async Task<PagedList<Like>> GetBySourceUser(Guid userID, LikeParameters parameters)
		{
			var likes = this.GetQueryableBySourceUser(userID);

			return await PagedList<Like>.CreateAsync(likes, parameters.PageNumber, parameters.PageSize);
		}

		/// <inheritdoc />
		public async Task<IEnumerable<Like>> GetByTargetUser(Guid userID)
		{
			return await this.GetQueryableByTargetUser(userID).ToListAsync();
		}

		/// <inheritdoc />
		public async Task<PagedList<Like>> GetByTargetUser(Guid userID, LikeParameters parameters)
		{
			var likes = this.GetQueryableByTargetUser(userID);

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
				.Include(l => l.Source.Pictures)
				.Include(l => l.Target.Pictures)
				.OrderByDescending(l => l.CreatedAt);
		}
		
		/// <summary>
		/// Gets the queryable by source user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		private IQueryable<Like> GetQueryableBySourceUser(Guid userID)
		{
			return this.GetQueryable().Where(l => l.SourceID == userID);
		}

		/// <summary>
		/// Gets the queryable by target user.
		/// </summary>
		/// <param name="userID">The user identifier.</param>
		private IQueryable<Like> GetQueryableByTargetUser(Guid userID)
		{
			return this.GetQueryable().Where(l => l.TargetID == userID);
		}
		#endregion
	}
}
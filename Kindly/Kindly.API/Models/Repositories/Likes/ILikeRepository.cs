using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kindly.API.Models.Repositories.Likes
{
	public interface ILikeRepository : IEntityRepository<Like, LikeParameters>
	{
		/// <summary>
		/// Checks if a like belongs to a user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="likeID">The like identifier.</param>
		Task<bool> LikeBelongsToUser(Guid userID, Guid likeID);

		/// <summary>
		/// Gets likes by source user id.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		Task<IEnumerable<Like>> GetBySourceUser(Guid userID);

		/// <summary>
		/// Gets likes by target user id.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		Task<IEnumerable<Like>> GetByTargetUser(Guid userID);
	}
}
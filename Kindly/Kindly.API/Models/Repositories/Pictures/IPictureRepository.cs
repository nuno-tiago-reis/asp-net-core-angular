using Kindly.API.Utility.Collections;

using System;
using System.Threading.Tasks;

namespace Kindly.API.Models.Repositories.Pictures
{
	public interface IPictureRepository : IEntityRepository<Picture, PictureParameters>
	{
		/// <summary>
		/// Checks if a picture belongs to a user.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="pictureID">The picture identifier.</param>
		Task<bool> PictureBelongsToUser(Guid userID, Guid pictureID);

		/// <summary>
		/// Gets pictures by user id using pagination.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		/// <param name="parameters">The parameters.</param>
		Task<PagedList<Picture>> GetByUser(Guid userID, PictureParameters parameters);
	}
}
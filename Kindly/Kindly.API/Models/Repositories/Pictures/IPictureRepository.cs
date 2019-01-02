using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Kindly.API.Contracts.Pictures;
using Kindly.API.Models.Domain;

namespace Kindly.API.Models.Repositories
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
		/// Gets pictures by user id.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		Task<IEnumerable<Picture>> GetByUser(Guid userID);
	}
}
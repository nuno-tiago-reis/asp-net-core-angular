using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Kindly.API.Models.Domain;

namespace Kindly.API.Models.Repositories
{
	public interface IPictureRepository : IEntityRepository<Picture>
	{
		/// <summary>
		/// Gets the pictures by user id.
		/// </summary>
		/// 
		/// <param name="userID">The user identifier.</param>
		Task<IEnumerable<Picture>> GetByUserID(Guid userID);
	}
}
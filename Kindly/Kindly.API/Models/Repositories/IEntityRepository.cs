using System;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

using Kindly.API.Utility.Collections;

namespace Kindly.API.Models.Repositories
{
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public interface IEntityRepository<T, in P> where T : class where P : PaginationParameters
	{
		/// <summary>
		/// Creates the specified entity.
		/// </summary>
		/// 
		/// <param name="picture">The entity.</param>
		Task<T> Create(T picture);

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// 
		/// <param name="entity">The entity.</param>
		Task<T> Update(T entity);

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// 
		/// <param name="pictureID">The entity identifier.</param>
		Task Delete(Guid pictureID);

		/// <summary>
		/// Gets a entity by entity id.
		/// </summary>
		/// 
		/// <param name="entityID">The entity identifier.</param>
		Task<T> Get(Guid entityID);

		/// <summary>
		/// Gets all the entities using pagination.
		/// </summary>
		/// 
		/// <param name="parameters">The parameters.</param>
		Task<PagedList<T>> GetAll(P parameters = null);
	}
}
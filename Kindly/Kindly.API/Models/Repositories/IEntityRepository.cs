using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Kindly.API.Models.Repositories
{
	public interface IEntityRepository<T> where T : class
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
		/// Gets the entities.
		/// </summary>
		Task<IEnumerable<T>> GetAll();
	}
}
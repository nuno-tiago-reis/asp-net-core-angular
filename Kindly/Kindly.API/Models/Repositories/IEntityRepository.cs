using Kindly.API.Utility.Collections;

using System;
using System.Threading.Tasks;

namespace Kindly.API.Models.Repositories
{
	/// <summary>
	/// Provides CRUD methods over an entity.
	/// </summary>
	/// 
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TParameters"></typeparam>
	public interface IEntityRepository<TEntity, in TParameters> where TEntity : class where TParameters : PaginationParameters
	{
		/// <summary>
		/// Creates the specified entity.
		/// </summary>
		/// 
		/// <param name="entity">The entity.</param>
		Task<TEntity> Create(TEntity entity);

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// 
		/// <param name="entity">The entity.</param>
		Task<TEntity> Update(TEntity entity);

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// 
		/// <param name="entityID">The entity identifier.</param>
		Task Delete(Guid entityID);

		/// <summary>
		/// Gets a entity by entity id.
		/// </summary>
		/// 
		/// <param name="entityID">The entity identifier.</param>
		Task<TEntity> Get(Guid entityID);

		/// <summary>
		/// Gets all the entities using pagination.
		/// </summary>
		/// 
		/// <param name="parameters">The parameters.</param>
		Task<PagedList<TEntity>> GetAll(TParameters parameters = null);
	}
}
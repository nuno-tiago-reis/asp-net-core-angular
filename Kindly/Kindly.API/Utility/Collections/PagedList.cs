using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Kindly.API.Utility.Collections
{
	/// <summary>
	/// Provides pagination over a generic list.
	/// </summary>
	/// 
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="List{T}" />
	public sealed class PagedList<T> : List<T> where T : class
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the page number.
		/// </summary>
		public int PageNumber { get; set; }

		/// <summary>
		/// Gets or sets the size of the page.
		/// </summary>
		public int PageSize { get; set; }

		/// <summary>
		/// Gets or sets the total pages.
		/// </summary>
		public int TotalPages { get; set; }

		/// <summary>
		/// Gets or sets the total totalCount.
		/// </summary>
		public int TotalCount { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="PagedList{T}"/> class.
		/// </summary>
		/// 
		/// <param name="items">The items.</param>
		/// <param name="totalCount">The totalCount.</param>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="pageSize">The page size.</param>
		private PagedList(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
		{
			this.PageNumber = pageNumber;
			this.PageSize = pageSize;
			this.TotalPages = totalCount / pageSize + (totalCount % pageSize == 0 ? 0 : 1);
			this.TotalCount = totalCount;

			this.AddRange(items);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="PagedList{T}"/> class asynchronously.
		/// </summary>
		/// 
		/// <param name="queryable">The queryable.</param>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="pageSize">The page size.</param>
		public static async Task<PagedList<T>> CreateAsync(IQueryable<T> queryable, int pageNumber, int pageSize)
		{
			int count = await queryable.CountAsync();
			var items = await queryable.Skip((pageNumber -1) * pageSize).Take(pageSize).ToListAsync();

			return new PagedList<T>(items, count, pageNumber, pageSize);
		}
		#endregion
	}
}
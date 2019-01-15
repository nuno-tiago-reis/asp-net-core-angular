namespace Kindly.API.Contracts
{
	public sealed class PaginationHeader
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
		/// Gets or sets the total count.
		/// </summary>
		public int TotalCount { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="PaginationHeader"/> class.
		/// </summary>
		/// 
		/// <param name="pageNumber">The page number.</param>
		/// <param name="pageSize">The page size.</param>
		/// <param name="totalPages">The total pages.</param>
		/// <param name="totalCount">The total count.</param>
		public PaginationHeader(int pageNumber, int pageSize, int totalPages, int totalCount)
		{
			this.PageNumber = pageNumber;
			this.PageSize = pageSize;
			this.TotalPages = totalPages;
			this.TotalCount = totalCount;
		}
		#endregion
	}
}
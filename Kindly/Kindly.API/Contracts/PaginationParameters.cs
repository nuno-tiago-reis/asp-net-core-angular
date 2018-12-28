using System;

namespace Kindly.API.Contracts
{
	public sealed class PaginationParameters
	{
		/// <summary>
		/// The maximum page size.
		/// </summary>
		private const int MaximumPageSize = 50;

		/// <summary>
		/// The minimum page size.
		/// </summary>
		private const int MinimumPageSize = 10;

		/// <summary>
		/// The page number.
		/// </summary>
		private int pageNumber;

		/// <summary>
		/// Gets or sets the page number.
		/// </summary>
		public int PageNumber
		{
			get { return this.pageNumber; }
			set { this.pageNumber = Math.Max(value, 1); }

		}

		/// <summary>
		/// The page size.
		/// </summary>
		private int pageSize;

		/// <summary>
		/// Gets or sets the size of the page.
		/// </summary>
		public int PageSize
		{
			get { return this.pageSize; }
			set { this.pageSize = Math.Clamp(value, MinimumPageSize, MaximumPageSize); }

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PaginationParameters"/> class.
		/// </summary>
		/// 
		/// <param name="pageNumber">The page number.</param>
		/// <param name="pageSize">Size of the page.</param>
		public PaginationParameters(int pageNumber, int pageSize)
		{
			this.PageNumber = pageNumber;
			this.PageSize = pageSize;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PaginationParameters"/> class.
		/// </summary>
		public PaginationParameters()
		{
			this.PageNumber = 1;
			this.PageSize = 10;
		}
	}
}

using System;

namespace Kindly.API.Contracts
{
	public abstract class PaginationParameters
	{
		#region [Constants]
		/// <summary>
		/// The maximum page size.
		/// </summary>
		protected const int MaximumPageSize = 50;

		/// <summary>
		/// The minimum page size.
		/// </summary>
		protected const int MinimumPageSize = 10;
		#endregion

		#region [Properties]
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
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="PaginationParameters"/> class.
		/// </summary>
		protected PaginationParameters()
		{
			this.PageNumber = 1;
			this.PageSize = 10;
		}
	}
}
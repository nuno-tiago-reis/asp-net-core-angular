namespace Kindly.API.Models.Repositories.Pictures
{
	/// <summary>
	/// The picture container to be returned in a query.
	/// </summary>
	public enum PictureContainer
	{
		/// <summary>
		/// Returns approved pictures.
		/// </summary>
		Approved = 2,

		/// <summary>
		/// Returns unapproved pictures.
		/// </summary>
		Unapproved = 1,

		/// <summary>
		/// Returns everything.
		/// </summary>
		Everything = 0
	}

	/// <summary>
	/// Provides pagination parameters as well as filtering over the picture queries.
	/// </summary>
	/// 
	/// <seealso cref="PaginationParameters" />
	public sealed class PictureParameters : PaginationParameters
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the container.
		/// </summary>
		public PictureContainer Container { get; set; }
		#endregion
	}
}
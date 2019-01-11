namespace Kindly.API.Models.Repositories.Likes
{
	public enum LikeMode
	{
		Recipients = 0,
		Senders = 1
	}

	public sealed class LikeParameters : PaginationParameters
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the mode.
		/// </summary>
		public LikeMode Mode { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to [include the request user].
		/// </summary>
		public bool IncludeRequestUser { get; set; }
		#endregion
	}
}
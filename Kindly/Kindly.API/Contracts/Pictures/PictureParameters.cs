namespace Kindly.API.Contracts.Pictures
{
	public sealed class PictureParameters : PaginationParameters
	{
		#region [Properties]
		/// <summary>
		/// Gets or sets the value indicating whether to exclude profile pictures.
		/// </summary>
		public bool? ExcludeProfilePictures { get; set; }
		#endregion
	}
}
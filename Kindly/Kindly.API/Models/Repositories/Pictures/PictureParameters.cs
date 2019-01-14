namespace Kindly.API.Models.Repositories.Pictures
{
	public enum PictureContainer
	{
		Approved = 2,
		Unapproved = 1,
		Everything = 0
	}

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
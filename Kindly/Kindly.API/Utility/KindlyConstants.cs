namespace Kindly.API.Utility
{
	public static class KindlyConstants
	{
		/// <summary>
		/// The invalid field message.
		/// </summary>
		public const string InvalidFieldMessage = "The {0} field is invalid.";

		/// <summary>
		/// The existing field message.
		/// </summary>
		public const string ExistingFieldMessage = "The {0} field is taken.";
	}

	public enum KindlyClaimTypes
	{
		ID,
		ProfileName
	}
}
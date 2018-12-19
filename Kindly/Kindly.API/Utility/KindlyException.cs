using System;

namespace Kindly.API.Utility
{
	public sealed class KindlyException : Exception
	{
		/// <summary>
		/// The status code to be returned.
		/// </summary>
		public int? StatusCode { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="KindlyException"/> class.
		/// </summary>
		/// 
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public KindlyException(string message) : base(message)
		{
			this.StatusCode = null;
		}
	}
}

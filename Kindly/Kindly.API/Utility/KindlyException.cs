using System;

namespace Kindly.API.Utility
{
	public sealed class KindlyException : Exception
	{
		/// <summary>
		/// The status code to be returned.
		/// </summary>
		public int StatusCode { get; set; }

		/// <summary>
		/// Whether the exception is due to a missing resource.
		/// </summary>
		public bool MissingResource { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="KindlyException"/> class.
		/// </summary>
		/// 
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="missingResource">Whether the exception is due to a missing resource.</param>
		public KindlyException(string message, bool missingResource = false) : base(message)
		{
			this.MissingResource = missingResource;
		}
	}
}
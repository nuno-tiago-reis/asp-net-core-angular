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

		/// <summary>
		/// Initializes a new instance of the <see cref="KindlyException"/> class.
		/// </summary>
		/// 
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="statusCode">The error status code to be returned in the response along with the message.</param>
		public KindlyException(string message, int statusCode) : base(message)
		{
			this.StatusCode = statusCode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="KindlyException"/> class.
		/// </summary>
		/// 
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="statusCode">The error status code to be returned in the response along with the message.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		public KindlyException(string message, int statusCode, Exception innerException) : base(message, innerException)
		{
			this.StatusCode = statusCode;
		}
	}
}

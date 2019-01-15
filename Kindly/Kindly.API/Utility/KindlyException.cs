using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Kindly.API.Utility
{
	public sealed class KindlyException : Exception
	{
		#region [Properties]
		/// <value>
		/// The messages.
		/// </value>
		public string[] Messages { get; set; }

		/// <summary>
		/// Whether the exception is due to a missing resource.
		/// </summary>
		public bool MissingResource { get; set; }
		#endregion

		#region [Constructors]
		/// <summary>
		/// Initializes a new instance of the <see cref="KindlyException"/> class.
		/// </summary>
		/// 
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="missingResource">Whether the exception is due to a missing resource.</param>
		public KindlyException(string message, bool missingResource = false) : base(message)
		{
			this.MissingResource = missingResource;
			this.Messages = new[] { message };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="KindlyException"/> class.
		/// </summary>
		/// 
		/// <param name="errors">The error messages that explains the reason for the exception.</param>
		/// <param name="missingResource">Whether the exception is due to a missing resource.</param>
		public KindlyException(IEnumerable<IdentityError> errors, bool missingResource = false)
		{
			this.MissingResource = missingResource;
			this.Messages = errors.Select(error => error.Description).ToArray();
		}
		#endregion
	}
}
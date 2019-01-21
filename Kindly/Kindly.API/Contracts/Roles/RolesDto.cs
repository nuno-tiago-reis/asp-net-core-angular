using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Roles
{
	/// <summary>
	/// The data transfer object for a list of role entities.
	/// </summary>
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class RolesDto
	{
		/// <summary>
		/// Gets or sets the roles.
		/// </summary>
		[Required]
		public List<string> Roles { get; set; }
	}
}
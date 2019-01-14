using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Roles
{
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
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Roles
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class CreateRoleDto
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		[Required]
		public string Name { get; set; }
	}
}
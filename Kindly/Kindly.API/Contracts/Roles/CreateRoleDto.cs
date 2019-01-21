using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Roles
{
	/// <summary>
	/// The request data transfer object for the create role operation.
	/// </summary>
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
using System.Diagnostics.CodeAnalysis;

namespace Kindly.API.Contracts.Roles
{
	[SuppressMessage("ReSharper", "UnusedMember.Global")]
	public sealed class UpdateRoleDto
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name { get; set; }
	}
}
using Microsoft.AspNetCore.Authorization;

namespace Kindly.API.Controllers
{
	/// <summary>
	/// Implements the resource owner requirement.
	/// </summary>
	/// 
	/// <seealso cref="IAuthorizationRequirement" />
	public sealed class ResourceOwnerRequirement : IAuthorizationRequirement
	{
		// Nothing to do here.
	}
}
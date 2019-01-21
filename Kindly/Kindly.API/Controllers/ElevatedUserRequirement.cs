using Microsoft.AspNetCore.Authorization;

namespace Kindly.API.Controllers
{
	/// <summary>
	/// Implements the elevated user requirement.
	/// </summary>
	/// 
	/// <seealso cref="IAuthorizationRequirement" />
	public sealed class ElevatedUserRequirement : IAuthorizationRequirement
	{
		// Nothing to do here.
	}
}
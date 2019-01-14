using Microsoft.AspNetCore.Authorization;

namespace Kindly.API.Controllers
{
	public sealed class ElevatedUserRequirement : IAuthorizationRequirement
	{
		// Nothing to do here.
	}
}
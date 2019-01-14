using Microsoft.AspNetCore.Authorization;

using System;
using System.Security.Claims;

namespace Kindly.API.Controllers
{
	public abstract class GenericHandler<TRequirement, TResource> : AuthorizationHandler<TRequirement, TResource>
		where TRequirement : IAuthorizationRequirement
	{
		/// <summary>
		/// Gets the invocation user identifier.
		/// </summary>
		protected Guid GetInvocationUserID(AuthorizationHandlerContext context)
		{
			return Guid.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
		}
	}

	public abstract class GenericHandler<TRequirement> : AuthorizationHandler<TRequirement>
		where TRequirement : IAuthorizationRequirement
	{
		/// <summary>
		/// Gets the invocation user identifier.
		/// </summary>
		protected Guid GetInvocationUserID(AuthorizationHandlerContext context)
		{
			return Guid.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
		}
	}
}
using System;

using Kindly.API.Utility;

using Microsoft.AspNetCore.Mvc;

namespace Kindly.API.Controllers
{
	public abstract class KindlyController : ControllerBase
	{
		/// <summary>
		/// Gets the invocation user identifier.
		/// </summary>
		/// <returns></returns>
		protected Guid GetInvocationUserID()
		{
			return Guid.Parse(User.FindFirst(KindlyClaimTypes.ID.ToString().ToLowerCamelCase()).Value);
		}
	}
}
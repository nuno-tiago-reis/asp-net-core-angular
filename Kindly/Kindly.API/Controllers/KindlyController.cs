using Kindly.API.Utility;

using Microsoft.AspNetCore.Mvc;

using System;

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
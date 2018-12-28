using System.Threading.Tasks;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Kindly.API.Utility
{
	public static class KindlyUtilities
	{
		/// <summary>
		/// Processes an exception and updates the context accordingly.
		/// </summary>
		/// 
		/// <param name="context">The context.</param>
		public static async Task ProcessException(HttpContext context)
		{
			var error = context.Features.Get<IExceptionHandlerFeature>();

			if (error.Error is KindlyException kindlyException)
			{
				if (kindlyException.StatusCode != 0)
					context.Response.StatusCode = kindlyException.StatusCode;
				else if (kindlyException.MissingResource)
					context.Response.StatusCode = StatusCodes.Status404NotFound;
				else
					context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			}
			else
			{
				context.Response.StatusCode = StatusCodes.Status500InternalServerError;
			}

			await context.Response.AddApplicationErrorHeader(error.Error.Message);
		}
	}
}
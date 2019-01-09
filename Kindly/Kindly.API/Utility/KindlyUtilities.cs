using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

using System.Threading.Tasks;

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
			string message;

			var error = context.Features.Get<IExceptionHandlerFeature>();

			if (error.Error is KindlyException kindlyException)
			{
				context.Response.StatusCode = kindlyException.MissingResource
					? StatusCodes.Status404NotFound
					: StatusCodes.Status500InternalServerError;

				message = kindlyException.Messages.Length == 1
					? kindlyException.Messages[0]
					: JsonConvert.SerializeObject(kindlyException.Messages);
			}
			else
			{
				context.Response.StatusCode = StatusCodes.Status500InternalServerError;

				message = error.Error.Message;
			}

			await context.Response.AddApplicationErrorHeader(message);
		}
	}
}
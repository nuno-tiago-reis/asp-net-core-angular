using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Kindly.API.Utility
{
	public static class KindlyExtensions
	{
		public static async Task AddApplicationError(this HttpResponse response, string message)
		{
			response.Headers.Add("Application-Error", message);
			response.Headers.Add("Access-Control-Allow-Origin", "*");
			response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");

			await response.WriteAsync(message);
		}
	}
}
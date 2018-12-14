using System.Threading.Tasks;

using Kindly.API.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kindly.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public sealed class ValuesController : ControllerBase
	{
		/// <summary>
		/// The context.
		/// </summary>
		private readonly KindlyContext context;

		/// <summary>
		/// Initializes a new instance of the <see cref="ValuesController"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public ValuesController(KindlyContext context)
		{
			this.context = context;
		}

		/// <summary>
		/// GET api/values
		/// </summary>
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var values = await this.context.Values.ToListAsync();

			return this.Ok(values);
		}

		/// <summary>
		/// GET api/values/5
		/// </summary>
		/// <param name="id">The identifier.</param>
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var value = await this.context.Values.FindAsync(id);
			if (value == null)
				return this.NotFound();

			return this.Ok(value);
		}

		/// <summary>
		/// POST api/values
		/// </summary>
		/// <param name="content">The content.</param>
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] string content)
		{
			var value = new Value
			{
				Content = content
			};

			this.context.Add(value);
			await this.context.SaveChangesAsync();

			return this.Ok();
		}

		/// <summary>
		/// PUT api/values/5
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="content">The content.</param>
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] string content)
		{
			var value = this.context.Values.Find(id);
			if (value == null)
				return this.NotFound();

			value.Content = content;
			await this.context.SaveChangesAsync();

			return this.Ok();
		}

		/// <summary>
		/// DELETE api/values/5
		/// </summary>
		/// <param name="id">The identifier.</param>
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var value = this.context.Values.Find(id);
			if (value == null)
				return this.NotFound();

			this.context.Remove(value);
			await this.context.SaveChangesAsync();

			return this.Ok();
		}
	}
}
using Microsoft.EntityFrameworkCore;

namespace Kindly.API.Models
{
	public class KindlyContext : DbContext
	{
		/// <summary>
		/// The values.
		/// </summary>
		public DbSet<Value> Values { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="KindlyContext"/> class.
		/// </summary>
		public KindlyContext(DbContextOptions options) : base(options)
		{
			// Nothing to do here.
		}
	}
}
using Kindly.API.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Kindly.API.Models
{
	public class KindlyContext : DbContext
	{
		/// <summary>
		/// The users.
		/// </summary>
		public DbSet<User> Users { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="KindlyContext" /> class.
		/// </summary>
		public KindlyContext(DbContextOptions options) : base(options)
		{
			// Nothing to do here.
		}

		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Apply the custom configurations
			builder.ApplyConfiguration(new UserConfiguration());
		}
	}
}
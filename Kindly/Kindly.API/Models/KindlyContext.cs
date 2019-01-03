using Kindly.API.Models.Repositories.Likes;
using Kindly.API.Models.Repositories.Messages;
using Kindly.API.Models.Repositories.Pictures;
using Kindly.API.Models.Repositories.Users;

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
		/// The likes.
		/// </summary>
		public DbSet<Like> Likes { get; set; }

		/// <summary>
		/// The pictures.
		/// </summary>
		public DbSet<Picture> Pictures { get; set; }

		/// <summary>
		/// The messages.
		/// </summary>
		public DbSet<Message> Messages { get; set; }

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

			builder.ApplyConfiguration(new UserConfiguration());
			builder.ApplyConfiguration(new LikeConfiguration());
			builder.ApplyConfiguration(new PictureConfiguration());
			builder.ApplyConfiguration(new MessageConfiguration());
		}
	}
}
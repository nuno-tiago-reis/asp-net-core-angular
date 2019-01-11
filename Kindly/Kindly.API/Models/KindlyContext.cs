using Kindly.API.Models.Repositories.Likes;
using Kindly.API.Models.Repositories.Messages;
using Kindly.API.Models.Repositories.Pictures;
using Kindly.API.Models.Repositories.Identity;
using Kindly.API.Models.Repositories.Users;
using Kindly.API.Models.Repositories.Roles;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

using System;
using Microsoft.Extensions.Logging.Console;

namespace Kindly.API.Models
{
	public class KindlyContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
	{
		/// <summary>
		/// The logger factory.
		/// </summary>
		public static readonly LoggerFactory LoggerFactory = new LoggerFactory
		(
			new[] { new ConsoleLoggerProvider((_, __) => true, true) }
		);

		/// <summary>
		/// The likes.
		/// </summary>
		public DbSet<Like> Likes { get; set; }

		/// <summary>
		/// The messages.
		/// </summary>
		public DbSet<Message> Messages { get; set; }

		/// <summary>
		/// The pictures.
		/// </summary>
		public DbSet<Picture> Pictures { get; set; }

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

			// Identity
			builder.ApplyConfiguration(new UserRoleConfiguration());
			builder.ApplyConfiguration(new UserClaimConfiguration());
			builder.ApplyConfiguration(new UserLoginConfiguration());
			builder.ApplyConfiguration(new UserTokenConfiguration());
			builder.ApplyConfiguration(new RoleClaimConfiguration());

			// Kindly
			builder.ApplyConfiguration(new LikeConfiguration());
			builder.ApplyConfiguration(new PictureConfiguration());
			builder.ApplyConfiguration(new MessageConfiguration());
			builder.ApplyConfiguration(new UserConfiguration());
			builder.ApplyConfiguration(new RoleConfiguration());
		}

		/// <inheritdoc />
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//optionsBuilder.UseLoggerFactory(LoggerFactory);
		}
	}
}
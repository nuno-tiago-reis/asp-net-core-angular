using Kindly.API.Models.Repositories.Likes;
using Kindly.API.Models.Repositories.Messages;
using Kindly.API.Models.Repositories.Pictures;
using Kindly.API.Models.Repositories.Users;
using Kindly.API.Models.Repositories.Users.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;

namespace Kindly.API.Models
{
	public class KindlyContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
	{
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
			builder.ApplyConfiguration(new RoleConfiguration());
			builder.ApplyConfiguration(new RoleClaimConfiguration());
			builder.ApplyConfiguration(new UserConfiguration());
			builder.ApplyConfiguration(new UserRoleConfiguration());
			builder.ApplyConfiguration(new UserClaimConfiguration());
			builder.ApplyConfiguration(new UserLoginConfiguration());
			builder.ApplyConfiguration(new UserTokenConfiguration());

			// Kindly
			builder.ApplyConfiguration(new LikeConfiguration());
			builder.ApplyConfiguration(new PictureConfiguration());
			builder.ApplyConfiguration(new MessageConfiguration());
		}
	}
}
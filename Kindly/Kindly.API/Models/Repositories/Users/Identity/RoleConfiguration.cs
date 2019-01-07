using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindly.API.Models.Repositories.Users.Identity
{
	public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			// Properties
			builder.Property(role => role.Id)
				.IsRequired()
				.HasColumnName(nameof(User.ID));

			// Properties - Ignored
			builder.Ignore(role => role.ID);

			// Relationships
			builder
				.HasMany(role => role.RoleUsers)
				.WithOne(userRole => userRole.Role)
				.HasForeignKey(roleUser => roleUser.RoleId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasMany(role => role.RoleClaims)
				.WithOne()
				.HasForeignKey(roleClaim => roleClaim.RoleId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindly.API.Models.Repositories.Identity
{
	public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<UserRole> builder)
		{
			// Properties
			builder.Property(userLogin => userLogin.UserId)
				.IsRequired()
				.HasColumnName(nameof(UserRole.UserID));

			builder.Property(userLogin => userLogin.RoleId)
				.IsRequired()
				.HasColumnName(nameof(UserRole.RoleID));

			// Properties - Ignored
			builder.Ignore(userRole => userRole.UserID);
			builder.Ignore(userRole => userRole.RoleID);
		}
	}
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindly.API.Models.Repositories.Users.Identity
{
	public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<UserRole> builder)
		{
			// Keys
			//builder.HasKey(userRole => new { userRole.UserId, userRole .RoleId });

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
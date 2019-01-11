using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindly.API.Models.Repositories.Users
{
	public sealed class UserConfiguration : IEntityTypeConfiguration<User>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<User> builder)
		{
			// Indices
			builder.HasIndex(user => user.Email);
			builder.HasIndex(user => user.UserName);
			builder.HasIndex(user => user.PhoneNumber);

			// Properties
			builder.Property(user => user.Id)
				.IsRequired()
				.HasColumnName(nameof(User.ID));

			builder.Property(user => user.UserName)
				.IsRequired()
				.HasMaxLength(25);

			builder.Property(user => user.Email)
				.IsRequired()
				.HasMaxLength(254);

			builder.Property(user => user.UserName)
				.IsRequired()
				.HasMaxLength(25);

			builder.Property(user => user.PhoneNumber)
				.IsRequired()
				.HasMaxLength(15);

			builder.Property(user => user.Gender)
				.IsRequired()
				.HasMaxLength(10);

			builder.Property(user => user.BirthDate)
				.IsRequired()
				.HasColumnType("Date");

			builder.Property(user => user.Introduction)
				.HasMaxLength(500);

			builder.Property(user => user.LookingFor)
				.HasMaxLength(250);

			builder.Property(user => user.Interests)
				.HasMaxLength(250);

			builder.Property(user => user.KnownAs)
				.IsRequired()
				.HasMaxLength(25);

			builder.Property(user => user.City)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(user => user.Country)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(user => user.CreatedAt)
				.IsRequired()
				.ValueGeneratedOnAdd()
				.HasDefaultValueSql("GetUtcDate()");

			builder.Property(user => user.LastActiveAt)
				.IsRequired()
				.ValueGeneratedOnAdd()
				.HasDefaultValueSql("GetUtcDate()");

			// Properties - Ignored
			builder.Ignore(user => user.ID);

			// Relationships
			builder
				.HasMany(user => user.UserRoles)
				.WithOne(userRole => userRole.User)
				.HasForeignKey(userRole => userRole.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasMany(user => user.UserClaims)
				.WithOne()
				.HasForeignKey(userClaim => userClaim.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasMany(user => user.UserLogins)
				.WithOne()
				.HasForeignKey(userLogin => userLogin.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasMany(user => user.UserTokens)
				.WithOne()
				.HasForeignKey(userToken => userToken.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
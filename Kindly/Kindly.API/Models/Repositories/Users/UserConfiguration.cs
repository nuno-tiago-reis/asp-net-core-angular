using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindly.API.Models.Repositories.Users
{
	public sealed class UserConfiguration : IEntityTypeConfiguration<User>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<User> builder)
		{
			// Keys
			builder.HasKey(user => user.ID);

			// Indices
			builder.HasIndex(user => user.UserName).IsUnique();
			builder.HasIndex(user => user.PhoneNumber).IsUnique();
			builder.HasIndex(user => user.EmailAddress).IsUnique();

			// Properties
			builder.Property(user => user.UserName)
				.IsRequired()
				.HasMaxLength(25);

			builder.Property(user => user.PhoneNumber)
				.IsRequired()
				.HasMaxLength(15);

			builder.Property(user => user.EmailAddress)
				.IsRequired()
				.HasMaxLength(254);

			builder.Property(user => user.KnownAs)
				.IsRequired()
				.HasMaxLength(25);

			builder.Property(user => user.Gender)
				.IsRequired()
				.HasMaxLength(10);

			builder.Property(user => user.BirthDate)
				.IsRequired()
				.HasColumnType("Date");

			builder.Property(user => user.Introduction)
				.HasMaxLength(500);

			builder.Property(user => user.Interests)
				.HasMaxLength(250);

			builder.Property(user => user.LookingFor)
				.HasMaxLength(250);

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
		}
	}
}
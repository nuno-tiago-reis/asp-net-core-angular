using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindly.API.Models.Configurations
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
				.HasMaxLength(200);

			builder.Property(user => user.PhoneNumber)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(user => user.EmailAddress)
				.IsRequired()
				.HasMaxLength(200);
		}
	}
}
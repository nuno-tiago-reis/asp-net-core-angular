using Kindly.API.Models.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindly.API.Models.Configurations
{
	public sealed class PictureConfiguration : IEntityTypeConfiguration<Picture>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Picture> builder)
		{
			// Keys
			builder.HasKey(user => user.ID);

			// Indices
			builder.HasIndex(user => user.Url).IsUnique();
			builder.HasIndex(user => user.PublicID).IsUnique();

			// Properties
			builder.Property(user => user.Url)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(user => user.PublicID)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(user => user.Description)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(user => user.IsProfilePicture)
				.IsRequired()
				.HasDefaultValue(false);

			builder.Property(user => user.CreatedAt)
				.IsRequired()
				.ValueGeneratedOnAdd()
				.HasDefaultValueSql("GetUtcDate()");
		}
	}
}
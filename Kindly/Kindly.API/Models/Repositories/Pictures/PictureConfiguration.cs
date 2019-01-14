using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindly.API.Models.Repositories.Pictures
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
				.HasMaxLength(200);

			builder.Property(user => user.IsApproved)
				.IsRequired()
				.HasDefaultValue(false);

			builder.Property(user => user.IsProfilePicture)
				.IsRequired()
				.HasDefaultValue(false);

			builder.Property(user => user.CreatedAt)
				.IsRequired()
				.ValueGeneratedOnAdd()
				.HasDefaultValueSql("GetUtcDate()");

			// Relationships
			builder
				.HasOne(picture => picture.User)
				.WithMany(user => user.Pictures)
				.HasForeignKey(like => like.UserID)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
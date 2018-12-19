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

			// Properties
			builder.Property(user => user.Url)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(user => user.Description)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(user => user.IsProfilePicture)
				.IsRequired()
				.HasDefaultValue(false);

			builder.Property(user => user.AddedAt)
				.IsRequired()
				.ValueGeneratedOnAdd()
				.HasDefaultValueSql("GetUtcDate()");
		}
	}
}
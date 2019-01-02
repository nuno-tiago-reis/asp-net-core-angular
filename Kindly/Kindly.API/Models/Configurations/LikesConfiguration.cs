using Kindly.API.Models.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindly.API.Models.Configurations
{
	public sealed class LikesConfiguration : IEntityTypeConfiguration<Like>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Like> builder)
		{
			// Keys
			builder.HasKey(like => like.ID);

			// Indices
			builder.HasIndex(like => like.SourceID);
			builder.HasIndex(like => like.TargetID);
			builder.HasIndex(like => new { like.SourceID, like.TargetID }).IsUnique();

			// Properties
			builder.Property(like => like.SourceID)
				.IsRequired();

			builder.Property(like => like.TargetID)
				.IsRequired();

			builder.Property(like => like.CreatedAt)
				.IsRequired()
				.ValueGeneratedOnAdd()
				.HasDefaultValueSql("GetUtcDate()");

			// Relationships
			builder
				.HasOne(like => like.Source)
				.WithMany(user => user.LikeTargets)
				.HasForeignKey(like => like.SourceID)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(like => like.Target) 
				.WithMany(user => user.LikeSources)
				.HasForeignKey(like => like.TargetID)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
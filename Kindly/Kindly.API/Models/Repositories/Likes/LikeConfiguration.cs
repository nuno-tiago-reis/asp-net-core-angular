using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindly.API.Models.Repositories.Likes
{
	/// <summary>
	/// Implements the like entity framework configuration.
	/// </summary>
	/// 
	/// <seealso cref="IEntityTypeConfiguration{User}" />
	public sealed class LikeConfiguration : IEntityTypeConfiguration<Like>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Like> builder)
		{
			// Keys
			builder.HasKey(like => like.ID);

			// Indices
			builder.HasIndex(like => like.SenderID);
			builder.HasIndex(like => like.RecipientID);
			builder.HasIndex(like => new { like.SenderID, like.RecipientID }).IsUnique();

			// Properties
			builder.Property(like => like.SenderID)
				.IsRequired();

			builder.Property(like => like.RecipientID)
				.IsRequired();

			builder.Property(like => like.CreatedAt)
				.IsRequired()
				.ValueGeneratedOnAdd()
				.HasDefaultValueSql("GetUtcDate()");

			// Relationships
			builder
				.HasOne(like => like.Sender)
				.WithMany(user => user.LikeRecipients)
				.HasForeignKey(like => like.SenderID)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(like => like.Recipient)
				.WithMany(user => user.LikeSenders)
				.HasForeignKey(like => like.RecipientID)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
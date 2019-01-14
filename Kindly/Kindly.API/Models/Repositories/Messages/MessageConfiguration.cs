using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindly.API.Models.Repositories.Messages
{
	public sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Message> builder)
		{
			// Keys
			builder.HasKey(user => user.ID);

			// Indices
			builder.HasIndex(like => like.SenderID);
			builder.HasIndex(like => like.RecipientID);
			builder.HasIndex(like => new { like.SenderID, like.RecipientID });

			// Properties
			builder.Property(user => user.Content)
				.IsRequired()
				.HasMaxLength(200);

			// Properties
			builder.Property(like => like.SenderID)
				.IsRequired();

			builder.Property(like => like.RecipientID)
				.IsRequired();

			builder.Property(like => like.SenderDeleted)
				.IsRequired()
				.HasDefaultValue(false);

			builder.Property(like => like.RecipientDeleted)
				.IsRequired()
				.HasDefaultValue(false);

			builder.Property(like => like.IsRead)
				.IsRequired()
				.HasDefaultValue(false);

			builder.Property(like => like.CreatedAt)
				.IsRequired()
				.ValueGeneratedOnAdd()
				.HasDefaultValueSql("GetUtcDate()");

			// Relationships
			builder
				.HasOne(like => like.Sender)
				.WithMany(user => user.MessagesReceived)
				.HasForeignKey(like => like.SenderID)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(like => like.Recipient)
				.WithMany(user => user.MessagesSent)
				.HasForeignKey(like => like.RecipientID)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
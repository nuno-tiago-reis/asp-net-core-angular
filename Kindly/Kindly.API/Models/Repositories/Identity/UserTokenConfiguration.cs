using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindly.API.Models.Repositories.Identity
{
	public sealed class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<UserToken> builder)
		{
			// Properties
			builder.Property(userToken => userToken.UserId)
				.IsRequired()
				.HasColumnName(nameof(UserToken.UserID));

			// Properties - Ignored
			builder.Ignore(userToken => userToken.UserID);
		}
	}
}
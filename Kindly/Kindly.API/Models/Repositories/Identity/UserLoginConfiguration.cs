using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindly.API.Models.Repositories.Identity
{
	public sealed class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<UserLogin> builder)
		{
			// Keys
			//builder.HasKey(userLogin => new { userLogin.UserId, userLogin.ProviderKey });

			// Properties
			builder.Property(userLogin => userLogin.UserId)
				.IsRequired()
				.HasColumnName(nameof(UserLogin.UserID));

			// Properties - Ignored
			builder.Ignore(userLogin => userLogin.UserID);
		}
	}
}
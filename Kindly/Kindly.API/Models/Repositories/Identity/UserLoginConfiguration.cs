using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindly.API.Models.Repositories.Identity
{
	/// <summary>
	/// Implements the user login entity framework configuration.
	/// </summary>
	/// 
	/// <seealso cref="IEntityTypeConfiguration{User}" />
	public sealed class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<UserLogin> builder)
		{
			// Properties
			builder.Property(userLogin => userLogin.UserId)
				.IsRequired()
				.HasColumnName(nameof(UserLogin.UserID));

			// Properties - Ignored
			builder.Ignore(userLogin => userLogin.UserID);
		}
	}
}
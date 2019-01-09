using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindly.API.Models.Repositories.Identity
{
	public sealed class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<RoleClaim> builder)
		{
			// Keys
			//builder.HasKey(roleClaim => new { roleClaim.RoleId, roleClaim.ClaimType });

			// Properties
			builder.Property(roleClaim => roleClaim.RoleId)
				.IsRequired()
				.HasColumnName(nameof(RoleClaim.RoleID));

			// Properties - Ignored
			builder.Ignore(roleClaim => roleClaim.RoleID);
		}
	}
}
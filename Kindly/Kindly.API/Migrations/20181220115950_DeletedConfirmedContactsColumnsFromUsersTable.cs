using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	/// <summary>
	/// Implements an entity framework migration.
	/// </summary>
	/// 
	/// <seealso cref="Migration" />
	public partial class DeletedConfirmedContactsColumnsFromUsersTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn
			(
				"EmailAddressConfirmed",
				"Users"
			);

			migrationBuilder.DropColumn
			(
				"PhoneNumberConfirmed",
				"Users"
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<bool>
			(
				"EmailAddressConfirmed",
				"Users",
				nullable: false,
				defaultValue: false
			);

			migrationBuilder.AddColumn<bool>
			(
				"PhoneNumberConfirmed",
				"Users",
				nullable: false,
				defaultValue: false
			);
		}
	}
}
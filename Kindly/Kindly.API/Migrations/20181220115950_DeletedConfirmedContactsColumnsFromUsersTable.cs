using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	public partial class DeletedConfirmedContactsColumnsFromUsersTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn
			(
				name: "EmailAddressConfirmed",
				table: "Users"
			);

			migrationBuilder.DropColumn
			(
				name: "PhoneNumberConfirmed",
				table: "Users"
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<bool>
			(
				name: "EmailAddressConfirmed",
				table: "Users",
				nullable: false,
				defaultValue: false
			);

			migrationBuilder.AddColumn<bool>
			(
				name: "PhoneNumberConfirmed",
				table: "Users",
				nullable: false,
				defaultValue: false
			);
		}
	}
}
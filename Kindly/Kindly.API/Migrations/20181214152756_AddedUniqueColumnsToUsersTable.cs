using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	public partial class AddedUniqueColumnsToUsersTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex
			(
				name: "IX_Users_EmailAddress",
				table: "Users"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_Users_PhoneNumber",
				table: "Users"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_Users_UserName",
				table: "Users"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Users_EmailAddress",
				table: "Users",
				column: "EmailAddress",
				unique: true
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Users_PhoneNumber",
				table: "Users",
				column: "PhoneNumber",
				unique: true
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Users_UserName",
				table: "Users",
				column: "UserName",
				unique: true
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex
			(
				name: "IX_Users_EmailAddress",
				table: "Users"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_Users_PhoneNumber",
				table: "Users"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_Users_UserName",
				table: "Users"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Users_EmailAddress",
				table: "Users",
				column: "EmailAddress"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Users_PhoneNumber",
				table: "Users",
				column: "PhoneNumber"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Users_UserName",
				table: "Users",
				column: "UserName"
			);
		}
	}
}
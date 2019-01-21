using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	/// <summary>
	/// Implements an entity framework migration.
	/// </summary>
	/// 
	/// <seealso cref="Migration" />
	public partial class AddedUniqueColumnsToUsersTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex
			(
				"IX_Users_EmailAddress",
				"Users"
			);

			migrationBuilder.DropIndex
			(
				"IX_Users_PhoneNumber",
				"Users"
			);

			migrationBuilder.DropIndex
			(
				"IX_Users_UserName",
				"Users"
			);

			migrationBuilder.CreateIndex
			(
				"IX_Users_EmailAddress",
				"Users",
				"EmailAddress",
				unique: true
			);

			migrationBuilder.CreateIndex
			(
				"IX_Users_PhoneNumber",
				"Users",
				"PhoneNumber",
				unique: true
			);

			migrationBuilder.CreateIndex
			(
				"IX_Users_UserName",
				"Users",
				"UserName",
				unique: true
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex
			(
				"IX_Users_EmailAddress",
				"Users"
			);

			migrationBuilder.DropIndex
			(
				"IX_Users_PhoneNumber",
				"Users"
			);

			migrationBuilder.DropIndex
			(
				"IX_Users_UserName",
				"Users"
			);

			migrationBuilder.CreateIndex
			(
				"IX_Users_EmailAddress",
				"Users",
				"EmailAddress"
			);

			migrationBuilder.CreateIndex
			(
				"IX_Users_PhoneNumber",
				"Users",
				"PhoneNumber"
			);

			migrationBuilder.CreateIndex
			(
				"IX_Users_UserName",
				"Users",
				"UserName"
			);
		}
	}
}
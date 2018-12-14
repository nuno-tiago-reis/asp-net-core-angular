using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	public partial class AddedUsersTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable
			(
				name: "Users",
				columns: table => new
				{
					ID = table.Column<Guid>(nullable: false),
					UserName = table.Column<string>(maxLength: 200, nullable: false),
					PhoneNumber = table.Column<string>(maxLength: 200, nullable: false),
					PhoneNumberConfirmed = table.Column<bool>(nullable: false),
					EmailAddress = table.Column<string>(maxLength: 200, nullable: false),
					EmailAddressConfirmed = table.Column<bool>(nullable: false),
					PasswordHash = table.Column<byte[]>(nullable: true),
					PasswordSalt = table.Column<byte[]>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.ID);
				}
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

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(name: "Users");
		}
	}
}
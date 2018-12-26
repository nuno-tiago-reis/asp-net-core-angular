using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	public partial class RemovedRequiredFromColumnsInUsersTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>
			(
				name: "LookingFor",
				table: "Users",
				maxLength: 250,
				nullable: true,
				oldClrType: typeof(string),
				oldMaxLength: 250
			);

			migrationBuilder.AlterColumn<string>
			(
				name: "Introduction",
				table: "Users",
				maxLength: 500,
				nullable: true,
				oldClrType: typeof(string),
				oldMaxLength: 500
			);

			migrationBuilder.AlterColumn<string>
			(
				name: "Interests",
				table: "Users",
				maxLength: 250,
				nullable: true,
				oldClrType: typeof(string),
				oldMaxLength: 250
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>
			(
				name: "LookingFor",
				table: "Users",
				maxLength: 250,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 250,
				oldNullable: true
			);

			migrationBuilder.AlterColumn<string>
			(
				name: "Introduction",
				table: "Users",
				maxLength: 500,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 500,
				oldNullable: true
			);

			migrationBuilder.AlterColumn<string>
			(
				name: "Interests",
				table: "Users",
				maxLength: 250,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 250,
				oldNullable: true
			);
		}
	}
}
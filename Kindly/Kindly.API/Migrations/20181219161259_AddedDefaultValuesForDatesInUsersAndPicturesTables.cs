using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	public partial class AddedDefaultValuesForDatesInUsersAndPicturesTables : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<DateTime>
			(
				name: "LastActiveAt",
				table: "Users",
				nullable: false,
				defaultValueSql: "GetUtcDate()",
				oldClrType: typeof(DateTime)
			);

			migrationBuilder.AlterColumn<DateTime>
			(
				name: "CreatedAt",
				table: "Users",
				nullable: false,
				defaultValueSql: "GetUtcDate()",
				oldClrType: typeof(DateTime)
			);

			migrationBuilder.AlterColumn<DateTime>
			(
				name: "AddedAt",
				table: "Pictures",
				nullable: false,
				defaultValueSql: "GetUtcDate()",
				oldClrType: typeof(DateTime)
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<DateTime>
			(
				name: "LastActiveAt",
				table: "Users",
				nullable: false,
				oldClrType: typeof(DateTime),
				oldDefaultValueSql: "GetUtcDate()"
			);

			migrationBuilder.AlterColumn<DateTime>
			(
				name: "CreatedAt",
				table: "Users",
				nullable: false,
				oldClrType: typeof(DateTime),
				oldDefaultValueSql: "GetUtcDate()"
			);

			migrationBuilder.AlterColumn<DateTime>
			(
				name: "AddedAt",
				table: "Pictures",
				nullable: false,
				oldClrType: typeof(DateTime),
				oldDefaultValueSql: "GetUtcDate()"
			);
		}
	}
}
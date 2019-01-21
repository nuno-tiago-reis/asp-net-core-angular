using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	/// <summary>
	/// Implements an entity framework migration.
	/// </summary>
	/// 
	/// <seealso cref="Migration" />
	public partial class AddedDefaultValuesForDatesInUsersAndPicturesTables : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<DateTime>
			(
				"LastActiveAt",
				"Users",
				nullable: false,
				defaultValueSql: "GetUtcDate()",
				oldClrType: typeof(DateTime)
			);

			migrationBuilder.AlterColumn<DateTime>
			(
				"CreatedAt",
				"Users",
				nullable: false,
				defaultValueSql: "GetUtcDate()",
				oldClrType: typeof(DateTime)
			);

			migrationBuilder.AlterColumn<DateTime>
			(
				"AddedAt",
				"Pictures",
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
				"LastActiveAt",
				"Users",
				nullable: false,
				oldClrType: typeof(DateTime),
				oldDefaultValueSql: "GetUtcDate()"
			);

			migrationBuilder.AlterColumn<DateTime>
			(
				"CreatedAt",
				"Users",
				nullable: false,
				oldClrType: typeof(DateTime),
				oldDefaultValueSql: "GetUtcDate()"
			);

			migrationBuilder.AlterColumn<DateTime>
			(
				"AddedAt",
				"Pictures",
				nullable: false,
				oldClrType: typeof(DateTime),
				oldDefaultValueSql: "GetUtcDate()"
			);
		}
	}
}
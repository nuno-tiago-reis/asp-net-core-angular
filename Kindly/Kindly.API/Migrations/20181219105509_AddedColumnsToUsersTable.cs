using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	/// <summary>
	/// Implements an entity framework migration.
	/// </summary>
	/// 
	/// <seealso cref="Migration" />
	public partial class AddedColumnsToUsersTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>
			(
				"UserName",
				"Users",
				maxLength: 25,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 200
			);

			migrationBuilder.AlterColumn<string>
			(
				"PhoneNumber",
				"Users",
				maxLength: 15,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 200
			);

			migrationBuilder.AlterColumn<string>
			(
				"EmailAddress",
				"Users",
				maxLength: 254,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 200);

			migrationBuilder.AddColumn<DateTime>
			(
				"BirthDate",
				"Users",
				"Date",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
			);

			migrationBuilder.AddColumn<string>
			(
				"City",
				"Users",
				maxLength: 50,
				nullable: false,
				defaultValue: ""
			);

			migrationBuilder.AddColumn<string>
			(
				"Country",
				"Users",
				maxLength: 50,
				nullable: false,
				defaultValue: ""
			);

			migrationBuilder.AddColumn<DateTime>
			(
				"CreatedAt",
				"Users",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
			);

			migrationBuilder.AddColumn<int>
			(
				"Gender",
				"Users",
				maxLength: 10,
				nullable: false,
				defaultValue: 0
			);

			migrationBuilder.AddColumn<string>
			(
				"Interests",
				"Users",
				maxLength: 200,
				nullable: false,
				defaultValue: ""
			);

			migrationBuilder.AddColumn<string>
			(
				"Introduction",
				"Users",
				maxLength: 200,
				nullable: false,
				defaultValue: ""
			);

			migrationBuilder.AddColumn<string>
			(
				"KnownAs",
				"Users",
				maxLength: 25,
				nullable: false,
				defaultValue: ""
			);

			migrationBuilder.AddColumn<DateTime>
			(
				"LastActiveAt",
				"Users",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
			);

			migrationBuilder.AddColumn<string>
			(
				"LookingFor",
				"Users",
				maxLength: 200,
				nullable: false,
				defaultValue: ""
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn
			(
				"BirthDate",
				"Users"
			);

			migrationBuilder.DropColumn
			(
				"City",
				"Users"
			);

			migrationBuilder.DropColumn
			(
				"Country",
				"Users"
			);

			migrationBuilder.DropColumn
			(
				"CreatedAt",
				"Users"
			);

			migrationBuilder.DropColumn
			(
				"Gender",
				"Users"
			);

			migrationBuilder.DropColumn
			(
				"Interests",
				"Users"
			);

			migrationBuilder.DropColumn
			(
				"Introduction",
				"Users"
			);

			migrationBuilder.DropColumn
			(
				"KnownAs",
				"Users"
			);

			migrationBuilder.DropColumn
			(
				"LastActiveAt",
				"Users"
			);

			migrationBuilder.DropColumn
			(
				"LookingFor",
				"Users"
			);

			migrationBuilder.AlterColumn<string>
			(
				"UserName",
				"Users",
				maxLength: 200,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 25
			);

			migrationBuilder.AlterColumn<string>
			(
				"PhoneNumber",
				"Users",
				maxLength: 200,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 15
			);

			migrationBuilder.AlterColumn<string>
			(
				"EmailAddress",
				"Users",
				maxLength: 200,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 254
			);
		}
	}
}
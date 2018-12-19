using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	public partial class AddedColumnsToUsersTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>
			(
				name: "UserName",
				table: "Users",
				maxLength: 25,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 200
			);

			migrationBuilder.AlterColumn<string>
			(
				name: "PhoneNumber",
				table: "Users",
				maxLength: 15,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 200
			);

			migrationBuilder.AlterColumn<string>
			(
				name: "EmailAddress",
				table: "Users",
				maxLength: 254,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 200);

			migrationBuilder.AddColumn<DateTime>
			(
				name: "BirthDate",
				table: "Users",
				type: "Date",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
			);

			migrationBuilder.AddColumn<string>
			(
				name: "City",
				table: "Users",
				maxLength: 50,
				nullable: false,
				defaultValue: ""
			);

			migrationBuilder.AddColumn<string>
			(
				name: "Country",
				table: "Users",
				maxLength: 50,
				nullable: false,
				defaultValue: ""
			);

			migrationBuilder.AddColumn<DateTime>
			(
				name: "CreatedAt",
				table: "Users",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
			);

			migrationBuilder.AddColumn<int>
			(
				name: "Gender",
				table: "Users",
				maxLength: 10,
				nullable: false,
				defaultValue: 0
			);

			migrationBuilder.AddColumn<string>
			(
				name: "Interests",
				table: "Users",
				maxLength: 200,
				nullable: false,
				defaultValue: ""
			);

			migrationBuilder.AddColumn<string>
			(
				name: "Introduction",
				table: "Users",
				maxLength: 200,
				nullable: false,
				defaultValue: ""
			);

			migrationBuilder.AddColumn<string>
			(
				name: "KnownAs",
				table: "Users",
				maxLength: 25,
				nullable: false,
				defaultValue: ""
			);

			migrationBuilder.AddColumn<DateTime>
			(
				name: "LastActiveAt",
				table: "Users",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
			);

			migrationBuilder.AddColumn<string>
			(
				name: "LookingFor",
				table: "Users",
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
				name: "BirthDate",
				table: "Users"
			);

			migrationBuilder.DropColumn
			(
				name: "City",
				table: "Users"
			);

			migrationBuilder.DropColumn
			(
				name: "Country",
				table: "Users"
			);

			migrationBuilder.DropColumn
			(
				name: "CreatedAt",
				table: "Users"
			);

			migrationBuilder.DropColumn
			(
				name: "Gender",
				table: "Users"
			);

			migrationBuilder.DropColumn
			(
				name: "Interests",
				table: "Users"
			);

			migrationBuilder.DropColumn
			(
				name: "Introduction",
				table: "Users"
			);

			migrationBuilder.DropColumn
			(
				name: "KnownAs",
				table: "Users"
			);

			migrationBuilder.DropColumn
			(
				name: "LastActiveAt",
				table: "Users"
			);

			migrationBuilder.DropColumn
			(
				name: "LookingFor",
				table: "Users"
			);

			migrationBuilder.AlterColumn<string>
			(
				name: "UserName",
				table: "Users",
				maxLength: 200,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 25
			);

			migrationBuilder.AlterColumn<string>
			(
				name: "PhoneNumber",
				table: "Users",
				maxLength: 200,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 15
			);

			migrationBuilder.AlterColumn<string>
			(
				name: "EmailAddress",
				table: "Users",
				maxLength: 200,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 254
			);
		}
	}
}
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	/// <summary>
	/// Implements an entity framework migration.
	/// </summary>
	/// 
	/// <seealso cref="Migration" />
	public partial class ChangedColumnLengthsInUsersTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>
			(
				"LookingFor",
				"Users",
				maxLength: 250,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 200
			);

			migrationBuilder.AlterColumn<string>
			(
				"Introduction",
				"Users",
				maxLength: 500,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 200
			);

			migrationBuilder.AlterColumn<string>
			(
				"Interests",
				"Users",
				maxLength: 250,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 200
			);

			migrationBuilder.AlterColumn<bool>
			(
				"IsProfilePicture",
				"Pictures",
				nullable: false,
				defaultValue: false,
				oldClrType: typeof(bool)
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("UPDATE Users SET LookingFor = LEFT(LookingFor, 200) WHERE LEN(LookingFor) > 200");

			migrationBuilder.AlterColumn<string>
			(
				"LookingFor",
				"Users",
				maxLength: 200,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 250
			);

			migrationBuilder.Sql(
				"UPDATE Users SET Introduction = LEFT(Introduction, 200) WHERE LEN(Introduction) > 200");

			migrationBuilder.AlterColumn<string>
			(
				"Introduction",
				"Users",
				maxLength: 200,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 500
			);

			migrationBuilder.Sql("UPDATE Users SET Interests = LEFT(Interests, 200) WHERE LEN(Interests) > 200");

			migrationBuilder.AlterColumn<string>
			(
				"Interests",
				"Users",
				maxLength: 200,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 250
			);

			migrationBuilder.AlterColumn<bool>
			(
				"IsProfilePicture",
				"Pictures",
				nullable: false,
				oldClrType: typeof(bool),
				oldDefaultValue: false
			);
		}
	}
}
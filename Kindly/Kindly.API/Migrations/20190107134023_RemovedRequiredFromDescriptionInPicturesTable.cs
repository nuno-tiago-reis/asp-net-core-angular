using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	/// <summary>
	/// Implements an entity framework migration.
	/// </summary>
	/// 
	/// <seealso cref="Migration" />
	public partial class RemovedRequiredFromDescriptionInPicturesTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>
			(
				"Description",
				"Pictures",
				maxLength: 200,
				nullable: true,
				oldClrType: typeof(string),
				oldMaxLength: 200
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>
			(
				"Description",
				"Pictures",
				maxLength: 200,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 200,
				oldNullable: true
			);
		}
	}
}
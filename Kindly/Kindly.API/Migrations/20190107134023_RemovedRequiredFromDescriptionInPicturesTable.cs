using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	public partial class RemovedRequiredFromDescriptionInPicturesTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>
			(
				name: "Description",
				table: "Pictures",
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
				name: "Description",
				table: "Pictures",
				maxLength: 200,
				nullable: false,
				oldClrType: typeof(string),
				oldMaxLength: 200,
				oldNullable: true
			);
		}
	}
}
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	public partial class AddedPublicIdColumnToPicturesTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>
			(
				name: "PublicID",
				table: "Pictures",
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
				name: "PublicID",
				table: "Pictures"
			);
		}
	}
}
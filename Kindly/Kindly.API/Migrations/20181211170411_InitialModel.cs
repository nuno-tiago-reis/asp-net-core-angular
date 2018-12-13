using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	public partial class InitialModel : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable
			(
				name: "Values",
				columns: table => new
				{
					ID = table.Column<int>(nullable: false).Annotation
					(
						"SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn
					),
					Content = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Values", x => x.ID);
				}
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable
			(
				name: "Values"
			);
		}
	}
}
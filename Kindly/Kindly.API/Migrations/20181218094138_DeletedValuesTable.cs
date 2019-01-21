using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	/// <summary>
	/// Implements an entity framework migration.
	/// </summary>
	/// 
	/// <seealso cref="Migration" />
	public partial class DeletedValuesTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable("Values");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable
			(
				"Values",
				table => new
				{
					ID = table.Column<int>(nullable: false).Annotation
					(
						"SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn
					),
					Content = table.Column<string>(nullable: true)
				},
				constraints: table => { table.PrimaryKey("PK_Values", x => x.ID); }
			);
		}
	}
}
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	/// <summary>
	/// Implements an entity framework migration.
	/// </summary>
	/// 
	/// <seealso cref="Migration" />
	public partial class AddedPicturesTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable
			(
				"Pictures",
				table => new
				{
					ID = table.Column<Guid>(nullable: false),
					Url = table.Column<string>(maxLength: 200, nullable: false),
					Description = table.Column<string>(maxLength: 200, nullable: false),
					AddedAt = table.Column<DateTime>(nullable: false),
					IsProfilePicture = table.Column<bool>(nullable: false, defaultValue: false),
					UserID = table.Column<Guid>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Pictures", x => x.ID);
					table.ForeignKey(
						"FK_Pictures_Users_UserID",
						x => x.UserID,
						"Users",
						"ID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex
			(
				"IX_Pictures_UserID",
				"Pictures",
				"UserID"
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable("Pictures");
		}
	}
}
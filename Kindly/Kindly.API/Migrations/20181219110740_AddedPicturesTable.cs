using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	public partial class AddedPicturesTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable
			(
				name: "Pictures",
				columns: table => new
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
						name: "FK_Pictures_Users_UserID",
						column: x => x.UserID,
						principalTable: "Users",
						principalColumn: "ID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex
			(
				name: "IX_Pictures_UserID",
				table: "Pictures",
				column: "UserID"
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(name: "Pictures");
		}
	}
}
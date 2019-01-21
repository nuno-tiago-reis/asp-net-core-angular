using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	/// <summary>
	/// Implements an entity framework migration.
	/// </summary>
	/// 
	/// <seealso cref="Migration" />
	public partial class AddedLikesTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn
			(
				"AddedAt",
				"Pictures",
				"CreatedAt"
			);

			migrationBuilder.CreateTable
			(
				"Likes",
				table => new
				{
					ID = table.Column<Guid>(nullable: false),
					SourceID = table.Column<Guid>(nullable: false),
					TargetID = table.Column<Guid>(nullable: false),
					CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetUtcDate()")
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Likes", x => x.ID);
					table.ForeignKey
					(
						"FK_Likes_Users_SourceID",
						x => x.SourceID,
						"Users",
						"ID",
						onDelete: ReferentialAction.Restrict
					);
					table.ForeignKey
					(
						"FK_Likes_Users_TargetID",
						x => x.TargetID,
						"Users",
						"ID",
						onDelete: ReferentialAction.Restrict
					);
				});

			migrationBuilder.CreateIndex
			(
				"IX_Pictures_PublicID",
				"Pictures",
				"PublicID",
				unique: true
			);

			migrationBuilder.CreateIndex
			(
				"IX_Pictures_Url",
				"Pictures",
				"Url",
				unique: true
			);

			migrationBuilder.CreateIndex
			(
				"IX_Likes_SourceID",
				"Likes",
				"SourceID"
			);

			migrationBuilder.CreateIndex
			(
				"IX_Likes_TargetID",
				"Likes",
				"TargetID"
			);

			migrationBuilder.CreateIndex
			(
				"IX_Likes_SourceID_TargetID",
				"Likes",
				new[] {"SourceID", "TargetID"},
				unique: true
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable("Likes");

			migrationBuilder.DropIndex
			(
				"IX_Pictures_PublicID",
				"Pictures"
			);

			migrationBuilder.DropIndex
			(
				"IX_Pictures_Url",
				"Pictures"
			);

			migrationBuilder.RenameColumn
			(
				"CreatedAt",
				"Pictures",
				"AddedAt"
			);
		}
	}
}
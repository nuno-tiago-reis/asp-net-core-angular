using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	/// <summary>
	/// Implements an entity framework migration.
	/// </summary>
	/// 
	/// <seealso cref="Migration" />
	public partial class AddedMessagesTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable
			(
				"Messages",
				table => new
				{
					ID = table.Column<Guid>(nullable: false),
					Content = table.Column<string>(maxLength: 200, nullable: false),
					SenderID = table.Column<Guid>(nullable: false),
					SenderDeleted = table.Column<bool>(nullable: false, defaultValue: false),
					RecipientID = table.Column<Guid>(nullable: false),
					RecipientDeleted = table.Column<bool>(nullable: false, defaultValue: false),
					IsRead = table.Column<bool>(nullable: false, defaultValue: false),
					ReadAt = table.Column<DateTime>(nullable: true),
					CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetUtcDate()")
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Messages", x => x.ID);
					table.ForeignKey(
						"FK_Messages_Users_RecipientID",
						x => x.RecipientID,
						"Users",
						"ID",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						"FK_Messages_Users_SenderID",
						x => x.SenderID,
						"Users",
						"ID",
						onDelete: ReferentialAction.Restrict);
				}
			);

			migrationBuilder.CreateIndex
			(
				"IX_Messages_RecipientID",
				"Messages",
				"RecipientID"
			);

			migrationBuilder.CreateIndex
			(
				"IX_Messages_SenderID",
				"Messages",
				"SenderID"
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable("Messages");
		}
	}
}
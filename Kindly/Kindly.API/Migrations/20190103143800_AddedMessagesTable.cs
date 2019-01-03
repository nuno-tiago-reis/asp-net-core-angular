using Microsoft.EntityFrameworkCore.Migrations;

using System;

namespace Kindly.API.Migrations
{
	public partial class AddedMessagesTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable
			(
				name: "Messages",
				columns: table => new
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
						name: "FK_Messages_Users_RecipientID",
						column: x => x.RecipientID,
						principalTable: "Users",
						principalColumn: "ID",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Messages_Users_SenderID",
						column: x => x.SenderID,
						principalTable: "Users",
						principalColumn: "ID",
						onDelete: ReferentialAction.Restrict);
				}
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Messages_RecipientID",
				table: "Messages",
				column: "RecipientID"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Messages_SenderID",
				table: "Messages",
				column: "SenderID"
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(name: "Messages");
		}
	}
}
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	public partial class AddedTriggersOnDeleteUsers : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey
			(
				name: "FK_Likes_AspNetUsers_SourceID",
				table: "Likes"
			);

			migrationBuilder.DropForeignKey
			(
				name: "FK_Likes_AspNetUsers_TargetID",
				table: "Likes"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_Likes_SourceID_TargetID",
				table: "Likes"
			);

			migrationBuilder.RenameColumn
			(
				name: "TargetID",
				table: "Likes",
				newName: "SenderID"
			);

			migrationBuilder.RenameColumn
			(
				name: "SourceID",
				table: "Likes",
				newName: "RecipientID"
			);

			migrationBuilder.RenameIndex
			(
				name: "IX_Likes_TargetID",
				table: "Likes",
				newName: "IX_Likes_SenderID"
			);

			migrationBuilder.RenameIndex
			(
				name: "IX_Likes_SourceID",
				table: "Likes",
				newName: "IX_Likes_RecipientID"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Likes_SenderID_RecipientID",
				table: "Likes",
				columns: new[] { "SenderID", "RecipientID" },
				unique: true
			);

			migrationBuilder.AddForeignKey
			(
				name: "FK_Likes_AspNetUsers_RecipientID",
				table: "Likes",
				column: "RecipientID",
				principalTable: "AspNetUsers",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			migrationBuilder.AddForeignKey
			(
				name: "FK_Likes_AspNetUsers_SenderID",
				table: "Likes",
				column: "SenderID",
				principalTable: "AspNetUsers",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			migrationBuilder.Sql
			(@"
				CREATE TRIGGER [TR_AspNetUsers_Delete]
				ON [AspNetUsers]
				INSTEAD OF DELETE
					AS
				BEGIN
					SET NOCOUNT ON;
					DELETE FROM [Likes] WHERE [SenderID] IN (SELECT [ID] FROM DELETED)
					DELETE FROM [Likes] WHERE [RecipientID] IN (SELECT [ID] FROM DELETED)
					DELETE FROM [Messages] WHERE [SenderID] IN (SELECT [ID] FROM DELETED)
					DELETE FROM [Messages] WHERE [RecipientID] IN (SELECT [ID] FROM DELETED)
					DELETE FROM [AspNetUsers] WHERE [ID] IN (SELECT [ID] FROM DELETED)
				END
				GO
			");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql
			(@"
				DROP TRIGGER [TR_AspNetUsers_Delete]
				GO
			");

			migrationBuilder.DropForeignKey
			(
				name: "FK_Likes_AspNetUsers_RecipientID",
				table: "Likes"
			);

			migrationBuilder.DropForeignKey
			(
				name: "FK_Likes_AspNetUsers_SenderID",
				table: "Likes"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_Likes_SenderID_RecipientID",
				table: "Likes"
			);

			migrationBuilder.RenameColumn
			(
				name: "SenderID",
				table: "Likes",
				newName: "TargetID"
			);

			migrationBuilder.RenameColumn
			(
				name: "RecipientID",
				table: "Likes",
				newName: "SourceID"
			);

			migrationBuilder.RenameIndex
			(
				name: "IX_Likes_SenderID",
				table: "Likes",
				newName: "IX_Likes_TargetID"
			);

			migrationBuilder.RenameIndex
			(
				name: "IX_Likes_RecipientID",
				table: "Likes",
				newName: "IX_Likes_SourceID"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Likes_SourceID_TargetID",
				table: "Likes",
				columns: new[] { "SourceID", "TargetID" },
				unique: true
			);

			migrationBuilder.AddForeignKey
			(
				name: "FK_Likes_AspNetUsers_SourceID",
				table: "Likes",
				column: "SourceID",
				principalTable: "AspNetUsers",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			migrationBuilder.AddForeignKey
			(
				name: "FK_Likes_AspNetUsers_TargetID",
				table: "Likes",
				column: "TargetID",
				principalTable: "AspNetUsers",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);
		}
	}
}
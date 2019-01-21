using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	/// <summary>
	/// Implements an entity framework migration.
	/// </summary>
	/// 
	/// <seealso cref="Migration" />
	public partial class AddedTriggersOnDeleteUsers : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey
			(
				"FK_Likes_AspNetUsers_SourceID",
				"Likes"
			);

			migrationBuilder.DropForeignKey
			(
				"FK_Likes_AspNetUsers_TargetID",
				"Likes"
			);

			migrationBuilder.DropIndex
			(
				"IX_Likes_SourceID_TargetID",
				"Likes"
			);

			migrationBuilder.RenameColumn
			(
				"TargetID",
				"Likes",
				"SenderID"
			);

			migrationBuilder.RenameColumn
			(
				"SourceID",
				"Likes",
				"RecipientID"
			);

			migrationBuilder.RenameIndex
			(
				"IX_Likes_TargetID",
				table: "Likes",
				newName: "IX_Likes_SenderID"
			);

			migrationBuilder.RenameIndex
			(
				"IX_Likes_SourceID",
				table: "Likes",
				newName: "IX_Likes_RecipientID"
			);

			migrationBuilder.CreateIndex
			(
				"IX_Likes_SenderID_RecipientID",
				"Likes",
				new[] {"SenderID", "RecipientID"},
				unique: true
			);

			migrationBuilder.AddForeignKey
			(
				"FK_Likes_AspNetUsers_RecipientID",
				"Likes",
				"RecipientID",
				"AspNetUsers",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			migrationBuilder.AddForeignKey
			(
				"FK_Likes_AspNetUsers_SenderID",
				"Likes",
				"SenderID",
				"AspNetUsers",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			/*migrationBuilder.Sql
			(@"
				GO
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
			");*/
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
				"FK_Likes_AspNetUsers_RecipientID",
				"Likes"
			);

			migrationBuilder.DropForeignKey
			(
				"FK_Likes_AspNetUsers_SenderID",
				"Likes"
			);

			migrationBuilder.DropIndex
			(
				"IX_Likes_SenderID_RecipientID",
				"Likes"
			);

			migrationBuilder.RenameColumn
			(
				"SenderID",
				"Likes",
				"TargetID"
			);

			migrationBuilder.RenameColumn
			(
				"RecipientID",
				"Likes",
				"SourceID"
			);

			migrationBuilder.RenameIndex
			(
				"IX_Likes_SenderID",
				table: "Likes",
				newName: "IX_Likes_TargetID"
			);

			migrationBuilder.RenameIndex
			(
				"IX_Likes_RecipientID",
				table: "Likes",
				newName: "IX_Likes_SourceID"
			);

			migrationBuilder.CreateIndex
			(
				"IX_Likes_SourceID_TargetID",
				"Likes",
				new[] {"SourceID", "TargetID"},
				unique: true
			);

			migrationBuilder.AddForeignKey
			(
				"FK_Likes_AspNetUsers_SourceID",
				"Likes",
				"SourceID",
				"AspNetUsers",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			migrationBuilder.AddForeignKey
			(
				"FK_Likes_AspNetUsers_TargetID",
				"Likes",
				"TargetID",
				"AspNetUsers",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);
		}
	}
}
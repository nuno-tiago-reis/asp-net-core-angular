using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	public partial class AddedIsApprovedColumnToPicturesTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<bool>
			(
				name: "IsApproved",
				table: "Pictures",
				nullable: false,
				defaultValue: false
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Messages_SenderID_RecipientID",
				table: "Messages",
				columns: new[] { "SenderID", "RecipientID" },
				unique: false
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex
			(
				name: "IX_Messages_SenderID_RecipientID",
				table: "Messages"
			);

			migrationBuilder.DropColumn
			(
				name: "IsApproved",
				table: "Pictures"
			);
		}
	}
}
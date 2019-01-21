using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	/// <summary>
	/// Implements an entity framework migration.
	/// </summary>
	/// 
	/// <seealso cref="Migration" />
	public partial class AddedIsApprovedColumnToPicturesTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<bool>
			(
				"IsApproved",
				"Pictures",
				nullable: false,
				defaultValue: false
			);

			migrationBuilder.CreateIndex
			(
				"IX_Messages_SenderID_RecipientID",
				"Messages",
				new[] {"SenderID", "RecipientID"},
				unique: false
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex
			(
				"IX_Messages_SenderID_RecipientID",
				"Messages"
			);

			migrationBuilder.DropColumn
			(
				"IsApproved",
				"Pictures"
			);
		}
	}
}
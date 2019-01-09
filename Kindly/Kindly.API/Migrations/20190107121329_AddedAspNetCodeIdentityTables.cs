using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

using System;

namespace Kindly.API.Migrations
{
	public partial class AddedAspNetCodeIdentityTables : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey
			(
				name: "FK_Likes_Users_SourceID",
				table: "Likes"
			);

			migrationBuilder.DropForeignKey
			(
				name: "FK_Likes_Users_TargetID",
				table: "Likes"
			);

			migrationBuilder.DropForeignKey
			(
				name: "FK_Messages_Users_RecipientID",
				table: "Messages"
			);

			migrationBuilder.DropForeignKey
			(
				name: "FK_Messages_Users_SenderID",
				table: "Messages"
			);

			migrationBuilder.DropForeignKey
			(
				name: "FK_Pictures_Users_UserID",
				table: "Pictures"
			);

			migrationBuilder.DropPrimaryKey
			(
				name: "PK_Users",
				table: "Users"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_Users_EmailAddress",
				table: "Users"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_Users_PhoneNumber",
				table: "Users"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_Users_UserName",
				table: "Users"
			);

			migrationBuilder.DropColumn
			(
				name: "PasswordSalt",
				table: "Users"
			);

			migrationBuilder.RenameTable
			(
				name: "Users",
				newName: "AspNetUsers"
			);

			migrationBuilder.RenameColumn
			(
				name: "EmailAddress",
				table: "AspNetUsers",
				newName: "Email"
			);

			migrationBuilder.AlterColumn<string>
			(
				name: "PasswordHash",
				table: "AspNetUsers",
				nullable: true,
				oldClrType: typeof(byte[]),
				oldNullable: true
			);

			migrationBuilder.AddColumn<int>
			(
				name: "AccessFailedCount",
				table: "AspNetUsers",
				nullable: false,
				defaultValue: 0
			);

			migrationBuilder.AddColumn<string>
			(
				name: "ConcurrencyStamp",
				table: "AspNetUsers",
				nullable: true
			);

			migrationBuilder.AddColumn<bool>
			(
				name: "EmailConfirmed",
				table: "AspNetUsers",
				nullable: false,
				defaultValue: false
			);

			migrationBuilder.AddColumn<bool>
			(
				name: "LockoutEnabled",
				table: "AspNetUsers",
				nullable: false,
				defaultValue: false
			);

			migrationBuilder.AddColumn<DateTimeOffset>
			(
				name: "LockoutEnd",
				table: "AspNetUsers",
				nullable: true
			);

			migrationBuilder.AddColumn<string>
			(
				name: "NormalizedEmail",
				table: "AspNetUsers",
				maxLength: 256,
				nullable: true
			);

			migrationBuilder.AddColumn<string>
			(
				name: "NormalizedUserName",
				table: "AspNetUsers",
				maxLength: 256,
				nullable: true
			);

			migrationBuilder.AddColumn<bool>
			(
				name: "PhoneNumberConfirmed",
				table: "AspNetUsers",
				nullable: false,
				defaultValue: false
			);

			migrationBuilder.AddColumn<string>
			(
				name: "SecurityStamp",
				table: "AspNetUsers",
				nullable: true
			);

			migrationBuilder.AddColumn<bool>
			(
				name: "TwoFactorEnabled",
				table: "AspNetUsers",
				nullable: false,
				defaultValue: false
			);

			migrationBuilder.AddPrimaryKey
			(
				name: "PK_AspNetUsers",
				table: "AspNetUsers",
				column: "ID"
			);

			migrationBuilder.CreateTable
			(
				name: "AspNetRoles",
				columns: table => new
				{
					ID = table.Column<Guid>(nullable: false),
					Name = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
					ConcurrencyStamp = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoles", x => x.ID);
				}
			);

			migrationBuilder.CreateTable
			(
				name: "AspNetUserClaims",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					UserID = table.Column<Guid>(nullable: false),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
					table.ForeignKey
					(
						name: "FK_AspNetUserClaims_AspNetUsers_UserID",
						column: x => x.UserID,
						principalTable: "AspNetUsers",
						principalColumn: "ID",
						onDelete: ReferentialAction.Cascade
					);
				}
			);

			migrationBuilder.CreateTable
			(
				name: "AspNetUserLogins",
				columns: table => new
				{
					LoginProvider = table.Column<string>(nullable: false),
					ProviderKey = table.Column<string>(nullable: false),
					ProviderDisplayName = table.Column<string>(nullable: true),
					UserID = table.Column<Guid>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
					table.ForeignKey
					(
						name: "FK_AspNetUserLogins_AspNetUsers_UserID",
						column: x => x.UserID,
						principalTable: "AspNetUsers",
						principalColumn: "ID",
						onDelete: ReferentialAction.Cascade
					);
				}
			);

			migrationBuilder.CreateTable
			(
				name: "AspNetUserTokens",
				columns: table => new
				{
					UserID = table.Column<Guid>(nullable: false),
					LoginProvider = table.Column<string>(nullable: false),
					Name = table.Column<string>(nullable: false),
					Value = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserID, x.LoginProvider, x.Name });
					table.ForeignKey
					(
						name: "FK_AspNetUserTokens_AspNetUsers_UserID",
						column: x => x.UserID,
						principalTable: "AspNetUsers",
						principalColumn: "ID",
						onDelete: ReferentialAction.Cascade
					);
				}
			);

			migrationBuilder.CreateTable
			(
				name: "AspNetRoleClaims",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					RoleID = table.Column<Guid>(nullable: false),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
					table.ForeignKey
					(
						name: "FK_AspNetRoleClaims_AspNetRoles_RoleID",
						column: x => x.RoleID,
						principalTable: "AspNetRoles",
						principalColumn: "ID",
						onDelete: ReferentialAction.Cascade
					);
				}
			);

			migrationBuilder.CreateTable
			(
				name: "AspNetUserRoles",
				columns: table => new
				{
					UserID = table.Column<Guid>(nullable: false),
					RoleID = table.Column<Guid>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserID, x.RoleID });
					table.ForeignKey
					(
						name: "FK_AspNetUserRoles_AspNetRoles_RoleID",
						column: x => x.RoleID,
						principalTable: "AspNetRoles",
						principalColumn: "ID",
						onDelete: ReferentialAction.Cascade
					);
					table.ForeignKey
					(
						name: "FK_AspNetUserRoles_AspNetUsers_UserID",
						column: x => x.UserID,
						principalTable: "AspNetUsers",
						principalColumn: "ID",
						onDelete: ReferentialAction.Cascade
					);
				}
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_AspNetUsers_Email",
				table: "AspNetUsers",
				column: "Email"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_AspNetUsers_NormalizedEmail",
				table: "AspNetUsers",
				column: "NormalizedEmail"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_AspNetUsers_NormalizedUserName",
				table: "AspNetUsers",
				column: "NormalizedUserName",
				unique: true,
				filter: "[NormalizedUserName] IS NOT NULL"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_AspNetUsers_PhoneNumber",
				table: "AspNetUsers",
				column: "PhoneNumber"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_AspNetUsers_UserName",
				table: "AspNetUsers",
				column: "UserName"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_AspNetRoleClaims_RoleID",
				table: "AspNetRoleClaims",
				column: "RoleID"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_AspNetRoles_NormalizedName",
				table: "AspNetRoles",
				column: "NormalizedName",
				unique: true,
				filter: "[NormalizedName] IS NOT NULL"
			);

			migrationBuilder.CreateIndex(
				name: "IX_AspNetUserClaims_UserID",
				table: "AspNetUserClaims",
				column: "UserID");

			migrationBuilder.CreateIndex
			(
				name: "IX_AspNetUserLogins_UserID",
				table: "AspNetUserLogins",
				column: "UserID"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_AspNetUserRoles_RoleID",
				table: "AspNetUserRoles",
				column: "RoleID"
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

			migrationBuilder.AddForeignKey
			(
				name: "FK_Messages_AspNetUsers_RecipientID",
				table: "Messages",
				column: "RecipientID",
				principalTable: "AspNetUsers",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			migrationBuilder.AddForeignKey
			(
				name: "FK_Messages_AspNetUsers_SenderID",
				table: "Messages",
				column: "SenderID",
				principalTable: "AspNetUsers",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			migrationBuilder.AddForeignKey
			(
				name: "FK_Pictures_AspNetUsers_UserID",
				table: "Pictures",
				column: "UserID",
				principalTable: "AspNetUsers",
				principalColumn: "ID",
				onDelete: ReferentialAction.Cascade
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
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

			migrationBuilder.DropForeignKey
			(
				name: "FK_Messages_AspNetUsers_RecipientID",
				table: "Messages"
			);

			migrationBuilder.DropForeignKey
			(
				name: "FK_Messages_AspNetUsers_SenderID",
				table: "Messages"
			);

			migrationBuilder.DropForeignKey
			(
				name: "FK_Pictures_AspNetUsers_UserID",
				table: "Pictures"
			);

			migrationBuilder.DropTable
			(
				name: "AspNetRoleClaims"
			);

			migrationBuilder.DropTable
			(
				name: "AspNetUserClaims"
			);

			migrationBuilder.DropTable
			(
				name: "AspNetUserLogins"
			);

			migrationBuilder.DropTable
			(
				name: "AspNetUserRoles"
			);

			migrationBuilder.DropTable
			(
				name: "AspNetUserTokens"
			);

			migrationBuilder.DropTable
			(
				name: "AspNetRoles"
			);

			migrationBuilder.DropPrimaryKey
			(
				name: "PK_AspNetUsers",
				table: "AspNetUsers"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_AspNetUsers_Email",
				table: "AspNetUsers"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_AspNetUsers_NormalizedEmail",
				table: "AspNetUsers"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_AspNetUsers_NormalizedUserName",
				table: "AspNetUsers"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_AspNetUsers_PhoneNumber",
				table: "AspNetUsers"
			);

			migrationBuilder.DropIndex
			(
				name: "IX_AspNetUsers_UserName",
				table: "AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				name: "AccessFailedCount",
				table: "AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				name: "ConcurrencyStamp",
				table: "AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				name: "EmailConfirmed",
				table: "AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				name: "LockoutEnabled",
				table: "AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				name: "LockoutEnd",
				table: "AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				name: "NormalizedEmail",
				table: "AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				name: "NormalizedUserName",
				table: "AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				name: "PhoneNumberConfirmed",
				table: "AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				name: "SecurityStamp",
				table: "AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				name: "TwoFactorEnabled",
				table: "AspNetUsers"
			);

			migrationBuilder.RenameTable
			(
				name: "AspNetUsers",
				newName: "Users"
			);

			migrationBuilder.RenameColumn
			(
				name: "Email",
				table: "Users",
				newName: "EmailAddress"
			);

			#region [Alter Column byte => varchar]
			/*migrationBuilder.AlterColumn<byte[]>
			(
				name: "PasswordHash",
				table: "Users",
				nullable: true,
				oldClrType: typeof(string),
				oldNullable: true
			);*/

			migrationBuilder.AddColumn<byte[]>
			(
				name: "PasswordHashTmp",
				table: "Users",
				nullable: true
			);

			migrationBuilder.Sql("Update Users SET PasswordHashTmp = CONVERT(varbinary, PasswordHash)");

			migrationBuilder.DropColumn
			(
				name: "PasswordHash",
				table: "Users"
			);

			migrationBuilder.RenameColumn
			(
				name: "PasswordHashTmp",
				table: "Users",
				newName: "PasswordHash"
			);
			#endregion

			migrationBuilder.AddColumn<byte[]>
			(
				name: "PasswordSalt",
				table: "Users",
				nullable: true
			);

			migrationBuilder.AddPrimaryKey
			(
				name: "PK_Users",
				table: "Users",
				column: "ID"
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Users_EmailAddress",
				table: "Users",
				column: "EmailAddress",
				unique: true
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Users_PhoneNumber",
				table: "Users",
				column: "PhoneNumber",
				unique: true
			);

			migrationBuilder.CreateIndex
			(
				name: "IX_Users_UserName",
				table: "Users",
				column: "UserName",
				unique: true
			);

			migrationBuilder.AddForeignKey
			(
				name: "FK_Likes_Users_SourceID",
				table: "Likes",
				column: "SourceID",
				principalTable: "Users",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			migrationBuilder.AddForeignKey
			(
				name: "FK_Likes_Users_TargetID",
				table: "Likes",
				column: "TargetID",
				principalTable: "Users",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			migrationBuilder.AddForeignKey
			(
				name: "FK_Messages_Users_RecipientID",
				table: "Messages",
				column: "RecipientID",
				principalTable: "Users",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			migrationBuilder.AddForeignKey
			(
				name: "FK_Messages_Users_SenderID",
				table: "Messages",
				column: "SenderID",
				principalTable: "Users",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			migrationBuilder.AddForeignKey
			(
				name: "FK_Pictures_Users_UserID",
				table: "Pictures",
				column: "UserID",
				principalTable: "Users",
				principalColumn: "ID",
				onDelete: ReferentialAction.Cascade
			);
		}
	}
}
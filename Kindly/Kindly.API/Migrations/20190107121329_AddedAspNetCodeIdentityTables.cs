using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kindly.API.Migrations
{
	/// <summary>
	/// Implements an entity framework migration.
	/// </summary>
	/// 
	/// <seealso cref="Migration" />
	public partial class AddedAspNetCoreIdentityTables : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey
			(
				"FK_Likes_Users_SourceID",
				"Likes"
			);

			migrationBuilder.DropForeignKey
			(
				"FK_Likes_Users_TargetID",
				"Likes"
			);

			migrationBuilder.DropForeignKey
			(
				"FK_Messages_Users_RecipientID",
				"Messages"
			);

			migrationBuilder.DropForeignKey
			(
				"FK_Messages_Users_SenderID",
				"Messages"
			);

			migrationBuilder.DropForeignKey
			(
				"FK_Pictures_Users_UserID",
				"Pictures"
			);

			migrationBuilder.DropPrimaryKey
			(
				"PK_Users",
				"Users"
			);

			migrationBuilder.DropIndex
			(
				"IX_Users_EmailAddress",
				"Users"
			);

			migrationBuilder.DropIndex
			(
				"IX_Users_PhoneNumber",
				"Users"
			);

			migrationBuilder.DropIndex
			(
				"IX_Users_UserName",
				"Users"
			);

			migrationBuilder.DropColumn
			(
				"PasswordSalt",
				"Users"
			);

			migrationBuilder.RenameTable
			(
				"Users",
				newName: "AspNetUsers"
			);

			migrationBuilder.RenameColumn
			(
				"EmailAddress",
				"AspNetUsers",
				"Email"
			);

			migrationBuilder.AlterColumn<string>
			(
				"PasswordHash",
				"AspNetUsers",
				nullable: true,
				oldClrType: typeof(byte[]),
				oldNullable: true
			);

			migrationBuilder.AddColumn<int>
			(
				"AccessFailedCount",
				"AspNetUsers",
				nullable: false,
				defaultValue: 0
			);

			migrationBuilder.AddColumn<string>
			(
				"ConcurrencyStamp",
				"AspNetUsers",
				nullable: true
			);

			migrationBuilder.AddColumn<bool>
			(
				"EmailConfirmed",
				"AspNetUsers",
				nullable: false,
				defaultValue: false
			);

			migrationBuilder.AddColumn<bool>
			(
				"LockoutEnabled",
				"AspNetUsers",
				nullable: false,
				defaultValue: false
			);

			migrationBuilder.AddColumn<DateTimeOffset>
			(
				"LockoutEnd",
				"AspNetUsers",
				nullable: true
			);

			migrationBuilder.AddColumn<string>
			(
				"NormalizedEmail",
				"AspNetUsers",
				maxLength: 256,
				nullable: true
			);

			migrationBuilder.AddColumn<string>
			(
				"NormalizedUserName",
				"AspNetUsers",
				maxLength: 256,
				nullable: true
			);

			migrationBuilder.AddColumn<bool>
			(
				"PhoneNumberConfirmed",
				"AspNetUsers",
				nullable: false,
				defaultValue: false
			);

			migrationBuilder.AddColumn<string>
			(
				"SecurityStamp",
				"AspNetUsers",
				nullable: true
			);

			migrationBuilder.AddColumn<bool>
			(
				"TwoFactorEnabled",
				"AspNetUsers",
				nullable: false,
				defaultValue: false
			);

			migrationBuilder.AddPrimaryKey
			(
				"PK_AspNetUsers",
				"AspNetUsers",
				"ID"
			);

			migrationBuilder.CreateTable
			(
				"AspNetRoles",
				table => new
				{
					ID = table.Column<Guid>(nullable: false),
					Name = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
					ConcurrencyStamp = table.Column<string>(nullable: true)
				},
				constraints: table => { table.PrimaryKey("PK_AspNetRoles", x => x.ID); }
			);

			migrationBuilder.CreateTable
			(
				"AspNetUserClaims",
				table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy",
							SqlServerValueGenerationStrategy.IdentityColumn),
					UserID = table.Column<Guid>(nullable: false),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
					table.ForeignKey
					(
						"FK_AspNetUserClaims_AspNetUsers_UserID",
						x => x.UserID,
						"AspNetUsers",
						"ID",
						onDelete: ReferentialAction.Cascade
					);
				}
			);

			migrationBuilder.CreateTable
			(
				"AspNetUserLogins",
				table => new
				{
					LoginProvider = table.Column<string>(nullable: false),
					ProviderKey = table.Column<string>(nullable: false),
					ProviderDisplayName = table.Column<string>(nullable: true),
					UserID = table.Column<Guid>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserLogins", x => new {x.LoginProvider, x.ProviderKey});
					table.ForeignKey
					(
						"FK_AspNetUserLogins_AspNetUsers_UserID",
						x => x.UserID,
						"AspNetUsers",
						"ID",
						onDelete: ReferentialAction.Cascade
					);
				}
			);

			migrationBuilder.CreateTable
			(
				"AspNetUserTokens",
				table => new
				{
					UserID = table.Column<Guid>(nullable: false),
					LoginProvider = table.Column<string>(nullable: false),
					Name = table.Column<string>(nullable: false),
					Value = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserTokens", x => new {x.UserID, x.LoginProvider, x.Name});
					table.ForeignKey
					(
						"FK_AspNetUserTokens_AspNetUsers_UserID",
						x => x.UserID,
						"AspNetUsers",
						"ID",
						onDelete: ReferentialAction.Cascade
					);
				}
			);

			migrationBuilder.CreateTable
			(
				"AspNetRoleClaims",
				table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy",
							SqlServerValueGenerationStrategy.IdentityColumn),
					RoleID = table.Column<Guid>(nullable: false),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
					table.ForeignKey
					(
						"FK_AspNetRoleClaims_AspNetRoles_RoleID",
						x => x.RoleID,
						"AspNetRoles",
						"ID",
						onDelete: ReferentialAction.Cascade
					);
				}
			);

			migrationBuilder.CreateTable
			(
				"AspNetUserRoles",
				table => new
				{
					UserID = table.Column<Guid>(nullable: false),
					RoleID = table.Column<Guid>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUserRoles", x => new {x.UserID, x.RoleID});
					table.ForeignKey
					(
						"FK_AspNetUserRoles_AspNetRoles_RoleID",
						x => x.RoleID,
						"AspNetRoles",
						"ID",
						onDelete: ReferentialAction.Cascade
					);
					table.ForeignKey
					(
						"FK_AspNetUserRoles_AspNetUsers_UserID",
						x => x.UserID,
						"AspNetUsers",
						"ID",
						onDelete: ReferentialAction.Cascade
					);
				}
			);

			migrationBuilder.CreateIndex
			(
				"IX_AspNetUsers_Email",
				"AspNetUsers",
				"Email"
			);

			migrationBuilder.CreateIndex
			(
				"IX_AspNetUsers_NormalizedEmail",
				"AspNetUsers",
				"NormalizedEmail"
			);

			migrationBuilder.CreateIndex
			(
				"IX_AspNetUsers_NormalizedUserName",
				"AspNetUsers",
				"NormalizedUserName",
				unique: true,
				filter: "[NormalizedUserName] IS NOT NULL"
			);

			migrationBuilder.CreateIndex
			(
				"IX_AspNetUsers_PhoneNumber",
				"AspNetUsers",
				"PhoneNumber"
			);

			migrationBuilder.CreateIndex
			(
				"IX_AspNetUsers_UserName",
				"AspNetUsers",
				"UserName"
			);

			migrationBuilder.CreateIndex
			(
				"IX_AspNetRoleClaims_RoleID",
				"AspNetRoleClaims",
				"RoleID"
			);

			migrationBuilder.CreateIndex
			(
				"IX_AspNetRoles_NormalizedName",
				"AspNetRoles",
				"NormalizedName",
				unique: true,
				filter: "[NormalizedName] IS NOT NULL"
			);

			migrationBuilder.CreateIndex(
				"IX_AspNetUserClaims_UserID",
				"AspNetUserClaims",
				"UserID");

			migrationBuilder.CreateIndex
			(
				"IX_AspNetUserLogins_UserID",
				"AspNetUserLogins",
				"UserID"
			);

			migrationBuilder.CreateIndex
			(
				"IX_AspNetUserRoles_RoleID",
				"AspNetUserRoles",
				"RoleID"
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

			migrationBuilder.AddForeignKey
			(
				"FK_Messages_AspNetUsers_RecipientID",
				"Messages",
				"RecipientID",
				"AspNetUsers",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			migrationBuilder.AddForeignKey
			(
				"FK_Messages_AspNetUsers_SenderID",
				"Messages",
				"SenderID",
				"AspNetUsers",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			migrationBuilder.AddForeignKey
			(
				"FK_Pictures_AspNetUsers_UserID",
				"Pictures",
				"UserID",
				"AspNetUsers",
				principalColumn: "ID",
				onDelete: ReferentialAction.Cascade
			);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
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

			migrationBuilder.DropForeignKey
			(
				"FK_Messages_AspNetUsers_RecipientID",
				"Messages"
			);

			migrationBuilder.DropForeignKey
			(
				"FK_Messages_AspNetUsers_SenderID",
				"Messages"
			);

			migrationBuilder.DropForeignKey
			(
				"FK_Pictures_AspNetUsers_UserID",
				"Pictures"
			);

			migrationBuilder.DropTable
			(
				"AspNetRoleClaims"
			);

			migrationBuilder.DropTable
			(
				"AspNetUserClaims"
			);

			migrationBuilder.DropTable
			(
				"AspNetUserLogins"
			);

			migrationBuilder.DropTable
			(
				"AspNetUserRoles"
			);

			migrationBuilder.DropTable
			(
				"AspNetUserTokens"
			);

			migrationBuilder.DropTable
			(
				"AspNetRoles"
			);

			migrationBuilder.DropPrimaryKey
			(
				"PK_AspNetUsers",
				"AspNetUsers"
			);

			migrationBuilder.DropIndex
			(
				"IX_AspNetUsers_Email",
				"AspNetUsers"
			);

			migrationBuilder.DropIndex
			(
				"IX_AspNetUsers_NormalizedEmail",
				"AspNetUsers"
			);

			migrationBuilder.DropIndex
			(
				"IX_AspNetUsers_NormalizedUserName",
				"AspNetUsers"
			);

			migrationBuilder.DropIndex
			(
				"IX_AspNetUsers_PhoneNumber",
				"AspNetUsers"
			);

			migrationBuilder.DropIndex
			(
				"IX_AspNetUsers_UserName",
				"AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				"AccessFailedCount",
				"AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				"ConcurrencyStamp",
				"AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				"EmailConfirmed",
				"AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				"LockoutEnabled",
				"AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				"LockoutEnd",
				"AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				"NormalizedEmail",
				"AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				"NormalizedUserName",
				"AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				"PhoneNumberConfirmed",
				"AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				"SecurityStamp",
				"AspNetUsers"
			);

			migrationBuilder.DropColumn
			(
				"TwoFactorEnabled",
				"AspNetUsers"
			);

			migrationBuilder.RenameTable
			(
				"AspNetUsers",
				newName: "Users"
			);

			migrationBuilder.RenameColumn
			(
				"Email",
				"Users",
				"EmailAddress"
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
				"PasswordHashTmp",
				"Users",
				nullable: true
			);

			migrationBuilder.Sql("Update Users SET PasswordHashTmp = CONVERT(varbinary, PasswordHash)");

			migrationBuilder.DropColumn
			(
				"PasswordHash",
				"Users"
			);

			migrationBuilder.RenameColumn
			(
				"PasswordHashTmp",
				"Users",
				"PasswordHash"
			);

			#endregion

			migrationBuilder.AddColumn<byte[]>
			(
				"PasswordSalt",
				"Users",
				nullable: true
			);

			migrationBuilder.AddPrimaryKey
			(
				"PK_Users",
				"Users",
				"ID"
			);

			migrationBuilder.CreateIndex
			(
				"IX_Users_EmailAddress",
				"Users",
				"EmailAddress",
				unique: true
			);

			migrationBuilder.CreateIndex
			(
				"IX_Users_PhoneNumber",
				"Users",
				"PhoneNumber",
				unique: true
			);

			migrationBuilder.CreateIndex
			(
				"IX_Users_UserName",
				"Users",
				"UserName",
				unique: true
			);

			migrationBuilder.AddForeignKey
			(
				"FK_Likes_Users_SourceID",
				"Likes",
				"SourceID",
				"Users",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			migrationBuilder.AddForeignKey
			(
				"FK_Likes_Users_TargetID",
				"Likes",
				"TargetID",
				"Users",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			migrationBuilder.AddForeignKey
			(
				"FK_Messages_Users_RecipientID",
				"Messages",
				"RecipientID",
				"Users",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			migrationBuilder.AddForeignKey
			(
				"FK_Messages_Users_SenderID",
				"Messages",
				"SenderID",
				"Users",
				principalColumn: "ID",
				onDelete: ReferentialAction.Restrict
			);

			migrationBuilder.AddForeignKey
			(
				"FK_Pictures_Users_UserID",
				"Pictures",
				"UserID",
				"Users",
				principalColumn: "ID",
				onDelete: ReferentialAction.Cascade
			);
		}
	}
}
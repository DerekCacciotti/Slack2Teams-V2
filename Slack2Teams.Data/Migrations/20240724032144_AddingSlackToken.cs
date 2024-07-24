using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slack2Teams.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingSlackToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlackOAuthTokn",
                table: "Tenant");

            migrationBuilder.AddColumn<Guid>(
                name: "SlackTokenFK",
                table: "Tenant",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SlackTokenUserSlackTokenPK",
                table: "Tenant",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserSlackToken",
                columns: table => new
                {
                    UserSlackTokenPK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SlackToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSlackToken", x => x.UserSlackTokenPK);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_SlackTokenUserSlackTokenPK",
                table: "Tenant",
                column: "SlackTokenUserSlackTokenPK");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenant_UserSlackToken_SlackTokenUserSlackTokenPK",
                table: "Tenant",
                column: "SlackTokenUserSlackTokenPK",
                principalTable: "UserSlackToken",
                principalColumn: "UserSlackTokenPK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenant_UserSlackToken_SlackTokenUserSlackTokenPK",
                table: "Tenant");

            migrationBuilder.DropTable(
                name: "UserSlackToken");

            migrationBuilder.DropIndex(
                name: "IX_Tenant_SlackTokenUserSlackTokenPK",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "SlackTokenFK",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "SlackTokenUserSlackTokenPK",
                table: "Tenant");

            migrationBuilder.AddColumn<string>(
                name: "SlackOAuthTokn",
                table: "Tenant",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

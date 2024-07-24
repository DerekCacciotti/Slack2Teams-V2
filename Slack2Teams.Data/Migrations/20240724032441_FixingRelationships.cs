using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slack2Teams.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixingRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenant_UserSlackToken_SlackTokenUserSlackTokenPK",
                table: "Tenant");

            migrationBuilder.DropIndex(
                name: "IX_Tenant_SlackTokenUserSlackTokenPK",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "SlackTokenUserSlackTokenPK",
                table: "Tenant");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SlackTokenUserSlackTokenPK",
                table: "Tenant",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slack2Teams.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fixingmessagerelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SlackMessages_SlackMessageType_SlackMessageTypePK",
                table: "SlackMessages");

            migrationBuilder.DropIndex(
                name: "IX_SlackMessages_SlackMessageTypePK",
                table: "SlackMessages");

            migrationBuilder.DropColumn(
                name: "SlackMessageTypePK",
                table: "SlackMessages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SlackMessageTypePK",
                table: "SlackMessages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SlackMessages_SlackMessageTypePK",
                table: "SlackMessages",
                column: "SlackMessageTypePK");

            migrationBuilder.AddForeignKey(
                name: "FK_SlackMessages_SlackMessageType_SlackMessageTypePK",
                table: "SlackMessages",
                column: "SlackMessageTypePK",
                principalTable: "SlackMessageType",
                principalColumn: "SlackMessageTypePK",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slack2Teams.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingSlackMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SlackMessageType",
                columns: table => new
                {
                    SlackMessageTypePK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlackMessageType", x => x.SlackMessageTypePK);
                });

            migrationBuilder.CreateTable(
                name: "SlackMessages",
                columns: table => new
                {
                    SlackMessagePK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChannelFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MesaageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SlackCreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlackCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SlackMessageTypeFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SlackMessageTypePK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlackMessages", x => x.SlackMessagePK);
                    table.ForeignKey(
                        name: "FK_SlackMessages_SlackMessageType_SlackMessageTypePK",
                        column: x => x.SlackMessageTypePK,
                        principalTable: "SlackMessageType",
                        principalColumn: "SlackMessageTypePK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SlackMessages_SlackMessageTypePK",
                table: "SlackMessages",
                column: "SlackMessageTypePK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlackMessages");

            migrationBuilder.DropTable(
                name: "SlackMessageType");
        }
    }
}

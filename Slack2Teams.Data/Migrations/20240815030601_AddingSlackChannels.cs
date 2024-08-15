using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slack2Teams.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingSlackChannels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SlackChannels",
                columns: table => new
                {
                    SlackChannelPK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChannelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChannelDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlackCreator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SlackCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isPrivate = table.Column<bool>(type: "bit", nullable: true),
                    isArchived = table.Column<bool>(type: "bit", nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlackChannels", x => x.SlackChannelPK);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlackChannels");
        }
    }
}

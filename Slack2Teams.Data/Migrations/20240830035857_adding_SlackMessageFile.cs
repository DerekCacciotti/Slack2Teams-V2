using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slack2Teams.Data.Migrations
{
    /// <inheritdoc />
    public partial class adding_SlackMessageFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SlackMessageFiles",
                columns: table => new
                {
                    StagedSlackFilePK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StagedSlackMessageFK = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SlackDownloadUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublicSlackFile = table.Column<bool>(type: "bit", nullable: true),
                    IsSlackFileExternal = table.Column<bool>(type: "bit", nullable: true),
                    SourceID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlackTimeStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlackCreator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlackFileCreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AzureBlobUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlackMessageFiles", x => x.StagedSlackFilePK);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlackMessageFiles");
        }
    }
}

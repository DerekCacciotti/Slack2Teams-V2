using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Slack2Teams.Data.Migrations
{
    /// <inheritdoc />
    public partial class SlackMessageDataModelEdits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlackCreateDate",
                table: "SlackMessages");

            migrationBuilder.DropColumn(
                name: "SlackCreatedBy",
                table: "SlackMessages");

            migrationBuilder.AlterColumn<Guid>(
                name: "SlackMessageTypeFK",
                table: "SlackMessages",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<string>(
                name: "SlackTimeStamp",
                table: "SlackMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlackTimeStamp",
                table: "SlackMessages");

            migrationBuilder.AlterColumn<Guid>(
                name: "SlackMessageTypeFK",
                table: "SlackMessages",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<DateTime>(
                name: "SlackCreateDate",
                table: "SlackMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<string>(
                name: "SlackCreatedBy",
                table: "SlackMessages",
                type: "nvarchar(max)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 3);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrowdFunding.Migrations
{
    public partial class chris3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectBacker",
                table: "ProjectBacker");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "ProjectBacker",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 29, 16, 27, 3, 703, DateTimeKind.Utc).AddTicks(2928),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 28, 15, 35, 33, 352, DateTimeKind.Utc).AddTicks(5851));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProjectBacker",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectBacker",
                table: "ProjectBacker",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBacker_BackerId",
                table: "ProjectBacker",
                column: "BackerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectBacker",
                table: "ProjectBacker");

            migrationBuilder.DropIndex(
                name: "IX_ProjectBacker_BackerId",
                table: "ProjectBacker");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectBacker");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "ProjectBacker",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 11, 28, 15, 35, 33, 352, DateTimeKind.Utc).AddTicks(5851),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 11, 29, 16, 27, 3, 703, DateTimeKind.Utc).AddTicks(2928));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectBacker",
                table: "ProjectBacker",
                columns: new[] { "BackerId", "ProjectId" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrowdFunding.Migrations
{
    public partial class chris5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Projects_ProjectId",
                table: "Photo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "ProjectBacker",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 6, 23, 26, 3, 326, DateTimeKind.Utc).AddTicks(4264),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 6, 23, 24, 47, 521, DateTimeKind.Utc).AddTicks(4358));

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Projects_ProjectId",
                table: "Photo",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Projects_ProjectId",
                table: "Photo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "ProjectBacker",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 6, 23, 24, 47, 521, DateTimeKind.Utc).AddTicks(4358),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 6, 23, 26, 3, 326, DateTimeKind.Utc).AddTicks(4264));

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Projects_ProjectId",
                table: "Photo",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}

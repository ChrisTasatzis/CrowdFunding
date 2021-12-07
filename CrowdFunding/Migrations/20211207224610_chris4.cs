using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrowdFunding.Migrations
{
    public partial class chris4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 7, 20, 40, 59, 902, DateTimeKind.Utc).AddTicks(6428));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "ProjectBacker",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 7, 22, 46, 10, 777, DateTimeKind.Utc).AddTicks(1251),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 7, 20, 40, 59, 902, DateTimeKind.Utc).AddTicks(2038));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 7, 20, 40, 59, 902, DateTimeKind.Utc).AddTicks(8015));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 7, 20, 40, 59, 902, DateTimeKind.Utc).AddTicks(6428),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "ProjectBacker",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 7, 20, 40, 59, 902, DateTimeKind.Utc).AddTicks(2038),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 7, 22, 46, 10, 777, DateTimeKind.Utc).AddTicks(1251));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 7, 20, 40, 59, 902, DateTimeKind.Utc).AddTicks(8015),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");
        }
    }
}

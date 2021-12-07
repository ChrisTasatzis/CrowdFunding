using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrowdFunding.Migrations
{
    public partial class chris5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Videos",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "ProjectBacker",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 7, 22, 50, 28, 607, DateTimeKind.Utc).AddTicks(3302),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 7, 22, 46, 10, 777, DateTimeKind.Utc).AddTicks(1251));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Photos",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Videos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "ProjectBacker",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 7, 22, 46, 10, 777, DateTimeKind.Utc).AddTicks(1251),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 12, 7, 22, 50, 28, 607, DateTimeKind.Utc).AddTicks(3302));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Photos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");
        }
    }
}

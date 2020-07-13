using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmarkHealthKidoPack.Migrations
{
    public partial class initialdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "dateselected",
                table: "childfoodviewmodels",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "dateselected",
                table: "childfoodviewmodels",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

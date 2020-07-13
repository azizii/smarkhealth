using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmarkHealthKidoPack.Migrations
{
    public partial class initialmdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "dateselected",
                table: "childfoodviewmodels",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dateselected",
                table: "childfoodviewmodels");
        }
    }
}

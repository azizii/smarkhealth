using Microsoft.EntityFrameworkCore.Migrations;

namespace SmarkHealthKidoPack.Migrations
{
    public partial class fingerprintinchild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fingerprint",
                table: "children",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fingerprint",
                table: "children");
        }
    }
}

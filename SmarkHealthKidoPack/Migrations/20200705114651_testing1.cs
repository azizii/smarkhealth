using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmarkHealthKidoPack.Migrations
{
    public partial class testing1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ajiabdullahs");

            migrationBuilder.DropTable(
                name: "abdullahs");

            migrationBuilder.DropTable(
                name: "ajis");

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "messages2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "adminname",
                table: "messages2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdminName = table.Column<string>(nullable: false),
                    Passward = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_messages2_AdminId",
                table: "messages2",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_messages2_Admin_AdminId",
                table: "messages2",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages2_Admin_AdminId",
                table: "messages2");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_messages2_AdminId",
                table: "messages2");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "messages2");

            migrationBuilder.DropColumn(
                name: "adminname",
                table: "messages2");

            migrationBuilder.CreateTable(
                name: "abdullahs",
                columns: table => new
                {
                    abdullahId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abdullahs", x => x.abdullahId);
                });

            migrationBuilder.CreateTable(
                name: "ajis",
                columns: table => new
                {
                    ajiId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ajis", x => x.ajiId);
                });

            migrationBuilder.CreateTable(
                name: "ajiabdullahs",
                columns: table => new
                {
                    abdullahId = table.Column<int>(nullable: false),
                    ajiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ajiabdullahs", x => new { x.abdullahId, x.ajiId });
                    table.ForeignKey(
                        name: "FK_ajiabdullahs_abdullahs_abdullahId",
                        column: x => x.abdullahId,
                        principalTable: "abdullahs",
                        principalColumn: "abdullahId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ajiabdullahs_ajis_ajiId",
                        column: x => x.ajiId,
                        principalTable: "ajis",
                        principalColumn: "ajiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ajiabdullahs_ajiId",
                table: "ajiabdullahs",
                column: "ajiId");
        }
    }
}

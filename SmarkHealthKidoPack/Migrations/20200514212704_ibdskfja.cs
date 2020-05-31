using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmarkHealthKidoPack.Migrations
{
    public partial class ibdskfja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Messes",
                columns: table => new
                {
                    MessId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MessName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    photopath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messes", x => x.MessId);
                });

            migrationBuilder.CreateTable(
                name: "Registers",
                columns: table => new
                {
                    RegisterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    fingerprints = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registers", x => x.RegisterId);
                });

            migrationBuilder.CreateTable(
                name: "ajiabdullahs",
                columns: table => new
                {
                    ajiId = table.Column<int>(nullable: false),
                    abdullahId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "foodCategories",
                columns: table => new
                {
                    FoodCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FoodCategoryName = table.Column<string>(maxLength: 10, nullable: false),
                    MessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foodCategories", x => x.FoodCategoryId);
                    table.ForeignKey(
                        name: "FK_foodCategories_Messes_MessId",
                        column: x => x.MessId,
                        principalTable: "Messes",
                        principalColumn: "MessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "guardians",
                columns: table => new
                {
                    GuardianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GuardianName = table.Column<string>(nullable: true),
                    phonenumber = table.Column<string>(nullable: true),
                    adress = table.Column<string>(nullable: true),
                    passward = table.Column<string>(nullable: true),
                    messId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guardians", x => x.GuardianId);
                    table.ForeignKey(
                        name: "FK_guardians_Messes_messId",
                        column: x => x.messId,
                        principalTable: "Messes",
                        principalColumn: "MessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "food",
                columns: table => new
                {
                    FoodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FoodName = table.Column<string>(maxLength: 10, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    foodCalories = table.Column<int>(maxLength: 3, nullable: false),
                    photopath = table.Column<string>(nullable: true),
                    foodCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food", x => x.FoodId);
                    table.ForeignKey(
                        name: "FK_food_foodCategories_foodCategoryId",
                        column: x => x.foodCategoryId,
                        principalTable: "foodCategories",
                        principalColumn: "FoodCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "children",
                columns: table => new
                {
                    ChildId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChildName = table.Column<string>(nullable: true),
                    age = table.Column<int>(nullable: false),
                    height = table.Column<int>(nullable: false),
                    weight = table.Column<int>(nullable: false),
                    password = table.Column<string>(nullable: true),
                    guardianId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_children", x => x.ChildId);
                    table.ForeignKey(
                        name: "FK_children_guardians_guardianId",
                        column: x => x.guardianId,
                        principalTable: "guardians",
                        principalColumn: "GuardianId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "childFoods",
                columns: table => new
                {
                    FoodId = table.Column<int>(nullable: false),
                    ChildId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_childFoods", x => new { x.ChildId, x.FoodId });
                    table.ForeignKey(
                        name: "FK_childFoods_children_ChildId",
                        column: x => x.ChildId,
                        principalTable: "children",
                        principalColumn: "ChildId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_childFoods_food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "food",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ajiabdullahs_ajiId",
                table: "ajiabdullahs",
                column: "ajiId");

            migrationBuilder.CreateIndex(
                name: "IX_childFoods_FoodId",
                table: "childFoods",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_children_guardianId",
                table: "children",
                column: "guardianId");

            migrationBuilder.CreateIndex(
                name: "IX_food_foodCategoryId",
                table: "food",
                column: "foodCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_foodCategories_MessId",
                table: "foodCategories",
                column: "MessId");

            migrationBuilder.CreateIndex(
                name: "IX_guardians_messId",
                table: "guardians",
                column: "messId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ajiabdullahs");

            migrationBuilder.DropTable(
                name: "childFoods");

            migrationBuilder.DropTable(
                name: "Registers");

            migrationBuilder.DropTable(
                name: "abdullahs");

            migrationBuilder.DropTable(
                name: "ajis");

            migrationBuilder.DropTable(
                name: "children");

            migrationBuilder.DropTable(
                name: "food");

            migrationBuilder.DropTable(
                name: "guardians");

            migrationBuilder.DropTable(
                name: "foodCategories");

            migrationBuilder.DropTable(
                name: "Messes");
        }
    }
}

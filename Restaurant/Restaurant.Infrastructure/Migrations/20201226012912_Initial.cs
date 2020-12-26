using Microsoft.EntityFrameworkCore.Migrations;

namespace Restaurant.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence(
                name: "resimgseq",
                schema: "dbo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "FoodItem",
                schema: "dbo",
                columns: table => new
                {
                    FoodId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FoodInfo_ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoodInfo_FoodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoodInfo_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItem", x => x.FoodId);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                schema: "dbo",
                columns: table => new
                {
                    MenuId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MenuInfo_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuInfo_Des = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantType",
                schema: "dbo",
                columns: table => new
                {
                    ResTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantType", x => x.ResTypeId);
                });

            migrationBuilder.CreateTable(
                name: "FoodAndMenu",
                schema: "dbo",
                columns: table => new
                {
                    FoodAndMenuId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FoodId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MenuId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodAndMenu", x => new { x.FoodAndMenuId, x.MenuId, x.FoodId });
                    table.ForeignKey(
                        name: "FK_FoodAndMenu_FoodItem_FoodId",
                        column: x => x.FoodId,
                        principalSchema: "dbo",
                        principalTable: "FoodItem",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodAndMenu_Menu_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "dbo",
                        principalTable: "Menu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant",
                schema: "dbo",
                columns: table => new
                {
                    ResId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Ward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    RestaurantTypeTenantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    WorkTime_OpenTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkTime_CloseTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.ResId);
                    table.ForeignKey(
                        name: "FK_Restaurant_RestaurantType_RestaurantTypeTenantId",
                        column: x => x.RestaurantTypeTenantId,
                        principalSchema: "dbo",
                        principalTable: "RestaurantType",
                        principalColumn: "ResTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResAndMenu",
                schema: "dbo",
                columns: table => new
                {
                    ResAndMenuId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MenuId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResAndMenu", x => new { x.MenuId, x.ResId, x.ResAndMenuId });
                    table.ForeignKey(
                        name: "FK_ResAndMenu_Menu_MenuId",
                        column: x => x.MenuId,
                        principalSchema: "dbo",
                        principalTable: "Menu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResAndMenu_Restaurant_ResId",
                        column: x => x.ResId,
                        principalSchema: "dbo",
                        principalTable: "Restaurant",
                        principalColumn: "ResId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResImages",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestaurantsTenantId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResImages_Restaurant_RestaurantsTenantId",
                        column: x => x.RestaurantsTenantId,
                        principalSchema: "dbo",
                        principalTable: "Restaurant",
                        principalColumn: "ResId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodAndMenu_FoodId",
                schema: "dbo",
                table: "FoodAndMenu",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodAndMenu_MenuId",
                schema: "dbo",
                table: "FoodAndMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_ResAndMenu_ResId",
                schema: "dbo",
                table: "ResAndMenu",
                column: "ResId");

            migrationBuilder.CreateIndex(
                name: "IX_ResImages_RestaurantsTenantId",
                schema: "dbo",
                table: "ResImages",
                column: "RestaurantsTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_Name",
                schema: "dbo",
                table: "Restaurant",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_RestaurantTypeTenantId",
                schema: "dbo",
                table: "Restaurant",
                column: "RestaurantTypeTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantType_TypeName",
                schema: "dbo",
                table: "RestaurantType",
                column: "TypeName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodAndMenu",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ResAndMenu",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ResImages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FoodItem",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Menu",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Restaurant",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RestaurantType",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "resimgseq",
                schema: "dbo");
        }
    }
}

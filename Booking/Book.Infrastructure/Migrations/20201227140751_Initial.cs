using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Book.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Booker",
                schema: "dbo",
                columns: table => new
                {
                    BookerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookerInf_UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookerInf_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookerInf_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAnnonymous = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booker", x => x.BookerId);
                });

            migrationBuilder.CreateTable(
                name: "BookTicket",
                schema: "dbo",
                columns: table => new
                {
                    BookId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookInfo_AtDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookInfo_AtHour = table.Column<int>(type: "int", nullable: true),
                    BookInfo_AtMinute = table.Column<int>(type: "int", nullable: true),
                    BookInfo_Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTicket", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_BookTicket_Booker_BookerId",
                        column: x => x.BookerId,
                        principalSchema: "dbo",
                        principalTable: "Booker",
                        principalColumn: "BookerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookTicket_BookerId",
                schema: "dbo",
                table: "BookTicket",
                column: "BookerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookTicket",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Booker",
                schema: "dbo");
        }
    }
}

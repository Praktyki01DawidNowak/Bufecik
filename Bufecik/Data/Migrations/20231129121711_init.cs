using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bufecik.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoria",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoria", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Klient",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klient", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Klient_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kanapka",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cena = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Skladniki = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zdjecie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KategoriaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kanapka", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Kanapka_Kategoria_KategoriaID",
                        column: x => x.KategoriaID,
                        principalTable: "Kategoria",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zamowienie",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataZ = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KlientID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zamowienie", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Zamowienie_Klient_KlientID",
                        column: x => x.KlientID,
                        principalTable: "Klient",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zamowienie_Status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Szczegoly",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ilosc = table.Column<int>(type: "int", nullable: false),
                    KanapkaID = table.Column<int>(type: "int", nullable: false),
                    ZamowienieID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Szczegoly", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Szczegoly_Kanapka_KanapkaID",
                        column: x => x.KanapkaID,
                        principalTable: "Kanapka",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Szczegoly_Zamowienie_ZamowienieID",
                        column: x => x.ZamowienieID,
                        principalTable: "Zamowienie",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kanapka_KategoriaID",
                table: "Kanapka",
                column: "KategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Klient_UserID",
                table: "Klient",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Szczegoly_KanapkaID",
                table: "Szczegoly",
                column: "KanapkaID");

            migrationBuilder.CreateIndex(
                name: "IX_Szczegoly_ZamowienieID",
                table: "Szczegoly",
                column: "ZamowienieID");

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienie_KlientID",
                table: "Zamowienie",
                column: "KlientID");

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienie_StatusID",
                table: "Zamowienie",
                column: "StatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Szczegoly");

            migrationBuilder.DropTable(
                name: "Kanapka");

            migrationBuilder.DropTable(
                name: "Zamowienie");

            migrationBuilder.DropTable(
                name: "Kategoria");

            migrationBuilder.DropTable(
                name: "Klient");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaseronTakip.Migrations
{
    /// <inheritdoc />
    public partial class InitAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bolumler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ad = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    KisaKod = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Sorumlu = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Aciklama = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    Aktif = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolumler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Firmalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ad = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    VergiNo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Yetkili = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Telefon = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Eposta = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Adres = table.Column<string>(type: "TEXT", maxLength: 400, nullable: true),
                    Aktif = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firmalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loglar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Kullanici = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Eylem = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    Nesne = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Zaman = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Detay = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loglar", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bolumler");

            migrationBuilder.DropTable(
                name: "Firmalar");

            migrationBuilder.DropTable(
                name: "Loglar");
        }
    }
}

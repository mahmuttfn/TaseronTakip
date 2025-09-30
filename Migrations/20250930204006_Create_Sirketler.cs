using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaseronTakip.Migrations
{
    /// <inheritdoc />
    public partial class Create_Sirketler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sirketler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Unvan = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
                    VergiDairesi = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true),
                    VergiNo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    MersisNo = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    TicaretSicilNo = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    SGKIşyeriSicilNo = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true),
                    KEPAdres = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    WebSite = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Telefon = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    YetkiliAdSoyad = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    YetkiliGorev = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    YetkiliTelefon = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    IBAN = table.Column<string>(type: "TEXT", maxLength: 34, nullable: true),
                    BankaAdi = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true),
                    Adres = table.Column<string>(type: "TEXT", maxLength: 240, nullable: true),
                    Aktif = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sirketler", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sirketler");
        }
    }
}

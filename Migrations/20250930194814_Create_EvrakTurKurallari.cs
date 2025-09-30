using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaseronTakip.Migrations
{
    /// <inheritdoc />
    public partial class Create_EvrakTurKurallari : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvrakTurKurallari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ad = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Kategori = table.Column<int>(type: "INTEGER", nullable: false),
                    GecerlilikSuresiDeger = table.Column<int>(type: "INTEGER", nullable: false),
                    GecerlilikSuresiBirim = table.Column<int>(type: "INTEGER", nullable: false),
                    BelgeUzerindekiTarihtenHesapla = table.Column<bool>(type: "INTEGER", nullable: false),
                    KaynakBelgeTazelikDeger = table.Column<int>(type: "INTEGER", nullable: false),
                    KaynakBelgeTazelikBirim = table.Column<int>(type: "INTEGER", nullable: false),
                    TazelikKuraliniZorunluKil = table.Column<bool>(type: "INTEGER", nullable: false),
                    ZorunluMu = table.Column<bool>(type: "INTEGER", nullable: false),
                    HatirlatmaGunOnce = table.Column<int>(type: "INTEGER", nullable: false),
                    Aciklama = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    KabulEdilenUzantilarCsv = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    MaxDosyaBoyutuMb = table.Column<int>(type: "INTEGER", nullable: true),
                    MinDosyaAdedi = table.Column<int>(type: "INTEGER", nullable: false),
                    TekilBelgeMi = table.Column<bool>(type: "INTEGER", nullable: false),
                    EDevlettenAlinmaliMi = table.Column<bool>(type: "INTEGER", nullable: false),
                    BarkodQrZorunluMu = table.Column<bool>(type: "INTEGER", nullable: false),
                    PlakaZorunluMu = table.Column<bool>(type: "INTEGER", nullable: false),
                    TCKNZorunluMu = table.Column<bool>(type: "INTEGER", nullable: false),
                    Pasif = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvrakTurKurallari", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvrakTurKurallari");
        }
    }
}

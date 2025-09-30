using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaseronTakip.Migrations
{
    /// <inheritdoc />
    public partial class RandevuInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Firma = table.Column<string>(type: "TEXT", nullable: true),
                    IlgiliKisi = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    IletisimNo = table.Column<string>(type: "TEXT", nullable: true),
                    ZiyaretTarihi = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ZiyaretSaati = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    Aciklama = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentStaff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppointmentRequestId = table.Column<int>(type: "INTEGER", nullable: false),
                    Bolum = table.Column<string>(type: "TEXT", nullable: true),
                    Personel = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentStaff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestedDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppointmentRequestId = table.Column<int>(type: "INTEGER", nullable: false),
                    Tip = table.Column<string>(type: "TEXT", nullable: true),
                    Not = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestedDocuments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentRequests");

            migrationBuilder.DropTable(
                name: "DepartmentStaff");

            migrationBuilder.DropTable(
                name: "RequestedDocuments");
        }
    }
}

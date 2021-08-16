using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikacijaDijeljenihRadnihMjesta.Migrations
{
    public partial class DodavanjeRezervacijeOtkazivanje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoviZahtjeva",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoviZahtjeva", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zahtjevi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipZahtjevaId = table.Column<int>(type: "int", nullable: false),
                    DjelatnikId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zahtjevi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zahtjevi_Djelatnici_DjelatnikId",
                        column: x => x.DjelatnikId,
                        principalTable: "Djelatnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zahtjevi_TipoviZahtjeva_TipZahtjevaId",
                        column: x => x.TipZahtjevaId,
                        principalTable: "TipoviZahtjeva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RezervacijeOtkazivanje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RadnoMjestoId = table.Column<int>(type: "int", nullable: false),
                    ProvjeraOtkazivanjaRezerviranja = table.Column<int>(type: "int", nullable: false),
                    ZahtjevId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RezervacijeOtkazivanje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RezervacijeOtkazivanje_RadnaMjesta_RadnoMjestoId",
                        column: x => x.RadnoMjestoId,
                        principalTable: "RadnaMjesta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RezervacijeOtkazivanje_Zahtjevi_ZahtjevId",
                        column: x => x.ZahtjevId,
                        principalTable: "Zahtjevi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijeOtkazivanje_RadnoMjestoId",
                table: "RezervacijeOtkazivanje",
                column: "RadnoMjestoId");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijeOtkazivanje_ZahtjevId",
                table: "RezervacijeOtkazivanje",
                column: "ZahtjevId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevi_DjelatnikId",
                table: "Zahtjevi",
                column: "DjelatnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Zahtjevi_TipZahtjevaId",
                table: "Zahtjevi",
                column: "TipZahtjevaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RezervacijeOtkazivanje");

            migrationBuilder.DropTable(
                name: "Zahtjevi");

            migrationBuilder.DropTable(
                name: "TipoviZahtjeva");
        }
    }
}

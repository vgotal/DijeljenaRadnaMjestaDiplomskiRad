using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikacijaDijeljenihRadnihMjesta.Migrations
{
    public partial class DodavanjeOdnosaRezerOtkzIZahtjevi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RezervacijeOtkazivanje_ZahtjevId",
                table: "RezervacijeOtkazivanje");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijeOtkazivanje_ZahtjevId",
                table: "RezervacijeOtkazivanje",
                column: "ZahtjevId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RezervacijeOtkazivanje_ZahtjevId",
                table: "RezervacijeOtkazivanje");

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijeOtkazivanje_ZahtjevId",
                table: "RezervacijeOtkazivanje",
                column: "ZahtjevId",
                unique: true);
        }
    }
}

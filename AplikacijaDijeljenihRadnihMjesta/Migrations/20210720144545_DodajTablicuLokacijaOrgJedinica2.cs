using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikacijaDijeljenihRadnihMjesta.Migrations
{
    public partial class DodajTablicuLokacijaOrgJedinica2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LokacijaOrganizacijskaJedinicas",
                columns: table => new
                {
                    LokacijeId = table.Column<int>(type: "int", nullable: false),
                    OrganizacijskeJediniceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LokacijaOrganizacijskaJedinicas", x => new { x.LokacijeId, x.OrganizacijskeJediniceId });
                    table.ForeignKey(
                        name: "FK_LokacijaOrganizacijskaJedinicas_Lokacije_LokacijeId",
                        column: x => x.LokacijeId,
                        principalTable: "Lokacije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LokacijaOrganizacijskaJedinicas_OrganizacijskeJedinice_OrganizacijskeJediniceId",
                        column: x => x.OrganizacijskeJediniceId,
                        principalTable: "OrganizacijskeJedinice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LokacijaOrganizacijskaJedinicas_OrganizacijskeJediniceId",
                table: "LokacijaOrganizacijskaJedinicas",
                column: "OrganizacijskeJediniceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LokacijaOrganizacijskaJedinicas");
        }
    }
}

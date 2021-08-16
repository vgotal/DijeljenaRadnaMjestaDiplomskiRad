using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikacijaDijeljenihRadnihMjesta.Migrations
{
    public partial class PromjenaOdnosaSobaLokacija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LokacijaSobe");

            migrationBuilder.AddColumn<int>(
                name: "SobaID",
                table: "Lokacije",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lokacije_SobaID",
                table: "Lokacije",
                column: "SobaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lokacije_Sobe_SobaID",
                table: "Lokacije",
                column: "SobaID",
                principalTable: "Sobe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lokacije_Sobe_SobaID",
                table: "Lokacije");

            migrationBuilder.DropIndex(
                name: "IX_Lokacije_SobaID",
                table: "Lokacije");

            migrationBuilder.DropColumn(
                name: "SobaID",
                table: "Lokacije");

            migrationBuilder.CreateTable(
                name: "LokacijaSobe",
                columns: table => new
                {
                    LokacijaId = table.Column<int>(type: "int", nullable: false),
                    SobeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LokacijaSobe", x => new { x.LokacijaId, x.SobeId });
                    table.ForeignKey(
                        name: "FK_LokacijaSobe_Lokacije_LokacijaId",
                        column: x => x.LokacijaId,
                        principalTable: "Lokacije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LokacijaSobe_Sobe_SobeId",
                        column: x => x.SobeId,
                        principalTable: "Sobe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LokacijaSobe_SobeId",
                table: "LokacijaSobe",
                column: "SobeId");
        }
    }
}

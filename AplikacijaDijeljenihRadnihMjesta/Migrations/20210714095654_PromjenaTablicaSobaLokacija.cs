using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikacijaDijeljenihRadnihMjesta.Migrations
{
    public partial class PromjenaTablicaSobaLokacija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "LokacijaID",
                table: "Sobe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sobe_LokacijaID",
                table: "Sobe",
                column: "LokacijaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sobe_Lokacije_LokacijaID",
                table: "Sobe",
                column: "LokacijaID",
                principalTable: "Lokacije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sobe_Lokacije_LokacijaID",
                table: "Sobe");

            migrationBuilder.DropIndex(
                name: "IX_Sobe_LokacijaID",
                table: "Sobe");

            migrationBuilder.DropColumn(
                name: "LokacijaID",
                table: "Sobe");

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
    }
}

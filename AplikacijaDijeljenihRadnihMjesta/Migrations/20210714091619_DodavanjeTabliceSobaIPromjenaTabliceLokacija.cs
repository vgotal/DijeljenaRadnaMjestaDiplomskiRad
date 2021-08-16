using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikacijaDijeljenihRadnihMjesta.Migrations
{
    public partial class DodavanjeTabliceSobaIPromjenaTabliceLokacija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrojSobe",
                table: "Lokacije");

            migrationBuilder.DropColumn(
                name: "Zgrada",
                table: "Lokacije");

          

            migrationBuilder.CreateTable(
                name: "Sobe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojSobe = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sobe", x => x.Id);
                });

         

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropTable(
                name: "Sobe");

            migrationBuilder.AddColumn<string>(
                name: "BrojSobe",
                table: "Lokacije",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zgrada",
                table: "Lokacije",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikacijaDijeljenihRadnihMjesta.Migrations
{
    public partial class PromjenaAtributaTabliceGrad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sobe");

            migrationBuilder.RenameColumn(
                name: "Inicijali",
                table: "Gradovi",
                newName: "Oznaka");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Oznaka",
                table: "Gradovi",
                newName: "Inicijali");

            migrationBuilder.CreateTable(
                name: "Sobe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojSobe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LokacijaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sobe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sobe_Lokacije_LokacijaID",
                        column: x => x.LokacijaID,
                        principalTable: "Lokacije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sobe_LokacijaID",
                table: "Sobe",
                column: "LokacijaID");
        }
    }
}

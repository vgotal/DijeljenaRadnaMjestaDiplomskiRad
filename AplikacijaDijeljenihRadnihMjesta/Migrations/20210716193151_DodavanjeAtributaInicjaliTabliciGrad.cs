using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikacijaDijeljenihRadnihMjesta.Migrations
{
    public partial class DodavanjeAtributaInicjaliTabliciGrad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Inicijali",
                table: "Gradovi",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inicijali",
                table: "Gradovi");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikacijaDijeljenihRadnihMjesta.Migrations
{
    public partial class DodavannjeAtributaObjasnjenjeUEntitetRezervacijeOtkazivanje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RazlogOtkazivanja",
                table: "RezervacijeOtkazivanje",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RazlogOtkazivanja",
                table: "RezervacijeOtkazivanje");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikacijaDijeljenihRadnihMjesta.Migrations
{
    public partial class DodavanjeEMailaDjelatnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProvjeraOtkazivanjaRezerviranja",
                table: "RezervacijeOtkazivanje",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Djelatnici",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Djelatnici");

            migrationBuilder.AlterColumn<int>(
                name: "ProvjeraOtkazivanjaRezerviranja",
                table: "RezervacijeOtkazivanje",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}

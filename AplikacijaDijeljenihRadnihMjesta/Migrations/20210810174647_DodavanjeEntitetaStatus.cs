using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikacijaDijeljenihRadnihMjesta.Migrations
{
    public partial class DodavanjeEntitetaStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "RezervacijeOtkazivanje");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "RezervacijeOtkazivanje",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Statusi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statusi", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RezervacijeOtkazivanje_StatusId",
                table: "RezervacijeOtkazivanje",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_RezervacijeOtkazivanje_Statusi_StatusId",
                table: "RezervacijeOtkazivanje",
                column: "StatusId",
                principalTable: "Statusi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RezervacijeOtkazivanje_Statusi_StatusId",
                table: "RezervacijeOtkazivanje");

            migrationBuilder.DropTable(
                name: "Statusi");

            migrationBuilder.DropIndex(
                name: "IX_RezervacijeOtkazivanje_StatusId",
                table: "RezervacijeOtkazivanje");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "RezervacijeOtkazivanje");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "RezervacijeOtkazivanje",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

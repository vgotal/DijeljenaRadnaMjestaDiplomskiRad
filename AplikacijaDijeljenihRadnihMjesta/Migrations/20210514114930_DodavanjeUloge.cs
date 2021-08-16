using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikacijaDijeljenihRadnihMjesta.Migrations
{
    public partial class DodavanjeUloge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UlogaID",
                table: "Djelatnici",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Uloge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uloge", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Djelatnici_UlogaID",
                table: "Djelatnici",
                column: "UlogaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Djelatnici_Uloge_UlogaID",
                table: "Djelatnici",
                column: "UlogaID",
                principalTable: "Uloge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Djelatnici_Uloge_UlogaID",
                table: "Djelatnici");

            migrationBuilder.DropTable(
                name: "Uloge");

            migrationBuilder.DropIndex(
                name: "IX_Djelatnici_UlogaID",
                table: "Djelatnici");

            migrationBuilder.DropColumn(
                name: "UlogaID",
                table: "Djelatnici");
        }
    }
}

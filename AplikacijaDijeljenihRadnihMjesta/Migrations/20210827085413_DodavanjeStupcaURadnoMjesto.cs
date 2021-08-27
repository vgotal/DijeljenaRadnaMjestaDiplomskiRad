using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikacijaDijeljenihRadnihMjesta.Migrations
{
    public partial class DodavanjeStupcaURadnoMjesto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropForeignKey(
                name: "FK_Djelatnici_OrganizacijskeJedinice_OrgJedinicaId",
                table: "Djelatnici");

            migrationBuilder.DropForeignKey(
                name: "FK_Djelatnici_TipoviLaptopa_TipLaptopaId",
                table: "Djelatnici");

            migrationBuilder.DropForeignKey(
                name: "FK_Djelatnici_Uloge_UlogaID",
                table: "Djelatnici");

            migrationBuilder.DropForeignKey(
                name: "FK_LokacijaOrganizacijskaJedinica_Lokacije_LokacijeId",
                table: "LokacijaOrganizacijskaJedinica");

            migrationBuilder.DropForeignKey(
                name: "FK_LokacijaOrganizacijskaJedinica_OrganizacijskeJedinice_OrganizacijskeJediniceId",
                table: "LokacijaOrganizacijskaJedinica");

            migrationBuilder.DropForeignKey(
                name: "FK_LokacijaOrganizacijskaJedinicas_Lokacije_LokacijeId",
                table: "LokacijaOrganizacijskaJedinicas");

            migrationBuilder.DropForeignKey(
                name: "FK_LokacijaOrganizacijskaJedinicas_OrganizacijskeJedinice_OrganizacijskeJediniceId",
                table: "LokacijaOrganizacijskaJedinicas");

            migrationBuilder.DropForeignKey(
                name: "FK_Lokacije_Gradovi_GradId",
                table: "Lokacije");

            migrationBuilder.DropForeignKey(
                name: "FK_RadnaMjesta_Lokacije_LokacijaId",
                table: "RadnaMjesta");

            migrationBuilder.DropForeignKey(
                name: "FK_RadnaMjesta_TipoviLaptopa_TipLaptopaId",
                table: "RadnaMjesta");

            migrationBuilder.DropForeignKey(
                name: "FK_RezervacijeOtkazivanje_RadnaMjesta_RadnoMjestoId",
                table: "RezervacijeOtkazivanje");

            migrationBuilder.DropForeignKey(
                name: "FK_RezervacijeOtkazivanje_Statusi_StatusId",
                table: "RezervacijeOtkazivanje");

            migrationBuilder.DropForeignKey(
                name: "FK_RezervacijeOtkazivanje_Zahtjevi_ZahtjevId",
                table: "RezervacijeOtkazivanje");

            migrationBuilder.DropForeignKey(
                name: "FK_Zahtjevi_Djelatnici_DjelatnikId",
                table: "Zahtjevi");

            migrationBuilder.DropForeignKey(
                name: "FK_Zahtjevi_TipoviZahtjeva_TipZahtjevaId",
                table: "Zahtjevi");

            migrationBuilder.AlterColumn<int>(
                name: "RadnoMjestoId",
                table: "RezervacijeOtkazivanje",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipLaptopaId",
                table: "RadnaMjesta",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Onemoguceno",
                table: "RadnaMjesta",
                type: "bit",
                nullable: false,
                defaultValue: false);

           

            migrationBuilder.AddForeignKey(
                name: "FK_Djelatnici_OrganizacijskeJedinice_OrgJedinicaId",
                table: "Djelatnici",
                column: "OrgJedinicaId",
                principalTable: "OrganizacijskeJedinice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Djelatnici_TipoviLaptopa_TipLaptopaId",
                table: "Djelatnici",
                column: "TipLaptopaId",
                principalTable: "TipoviLaptopa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Djelatnici_Uloge_UlogaID",
                table: "Djelatnici",
                column: "UlogaID",
                principalTable: "Uloge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LokacijaOrganizacijskaJedinica_Lokacije_LokacijeId",
                table: "LokacijaOrganizacijskaJedinica",
                column: "LokacijeId",
                principalTable: "Lokacije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LokacijaOrganizacijskaJedinica_OrganizacijskeJedinice_OrganizacijskeJediniceId",
                table: "LokacijaOrganizacijskaJedinica",
                column: "OrganizacijskeJediniceId",
                principalTable: "OrganizacijskeJedinice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LokacijaOrganizacijskaJedinicas_Lokacije_LokacijeId",
                table: "LokacijaOrganizacijskaJedinicas",
                column: "LokacijeId",
                principalTable: "Lokacije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LokacijaOrganizacijskaJedinicas_OrganizacijskeJedinice_OrganizacijskeJediniceId",
                table: "LokacijaOrganizacijskaJedinicas",
                column: "OrganizacijskeJediniceId",
                principalTable: "OrganizacijskeJedinice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lokacije_Gradovi_GradId",
                table: "Lokacije",
                column: "GradId",
                principalTable: "Gradovi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RadnaMjesta_Lokacije_LokacijaId",
                table: "RadnaMjesta",
                column: "LokacijaId",
                principalTable: "Lokacije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RadnaMjesta_TipoviLaptopa_TipLaptopaId",
                table: "RadnaMjesta",
                column: "TipLaptopaId",
                principalTable: "TipoviLaptopa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RezervacijeOtkazivanje_RadnaMjesta_RadnoMjestoId",
                table: "RezervacijeOtkazivanje",
                column: "RadnoMjestoId",
                principalTable: "RadnaMjesta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RezervacijeOtkazivanje_Statusi_StatusId",
                table: "RezervacijeOtkazivanje",
                column: "StatusId",
                principalTable: "Statusi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RezervacijeOtkazivanje_Zahtjevi_ZahtjevId",
                table: "RezervacijeOtkazivanje",
                column: "ZahtjevId",
                principalTable: "Zahtjevi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Zahtjevi_Djelatnici_DjelatnikId",
                table: "Zahtjevi",
                column: "DjelatnikId",
                principalTable: "Djelatnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Zahtjevi_TipoviZahtjeva_TipZahtjevaId",
                table: "Zahtjevi",
                column: "TipZahtjevaId",
                principalTable: "TipoviZahtjeva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Djelatnici_OrganizacijskeJedinice_OrgJedinicaId",
                table: "Djelatnici");

            migrationBuilder.DropForeignKey(
                name: "FK_Djelatnici_TipoviLaptopa_TipLaptopaId",
                table: "Djelatnici");

            migrationBuilder.DropForeignKey(
                name: "FK_Djelatnici_Uloge_UlogaID",
                table: "Djelatnici");

            migrationBuilder.DropForeignKey(
                name: "FK_LokacijaOrganizacijskaJedinica_Lokacije_LokacijeId",
                table: "LokacijaOrganizacijskaJedinica");

            migrationBuilder.DropForeignKey(
                name: "FK_LokacijaOrganizacijskaJedinica_OrganizacijskeJedinice_OrganizacijskeJediniceId",
                table: "LokacijaOrganizacijskaJedinica");

            migrationBuilder.DropForeignKey(
                name: "FK_LokacijaOrganizacijskaJedinicas_Lokacije_LokacijeId",
                table: "LokacijaOrganizacijskaJedinicas");

            migrationBuilder.DropForeignKey(
                name: "FK_LokacijaOrganizacijskaJedinicas_OrganizacijskeJedinice_OrganizacijskeJediniceId",
                table: "LokacijaOrganizacijskaJedinicas");

            migrationBuilder.DropForeignKey(
                name: "FK_Lokacije_Gradovi_GradId",
                table: "Lokacije");

            migrationBuilder.DropForeignKey(
                name: "FK_RadnaMjesta_Lokacije_LokacijaId",
                table: "RadnaMjesta");

            migrationBuilder.DropForeignKey(
                name: "FK_RadnaMjesta_TipoviLaptopa_TipLaptopaId",
                table: "RadnaMjesta");

            migrationBuilder.DropForeignKey(
                name: "FK_RezervacijeOtkazivanje_RadnaMjesta_RadnoMjestoId",
                table: "RezervacijeOtkazivanje");

            migrationBuilder.DropForeignKey(
                name: "FK_RezervacijeOtkazivanje_Statusi_StatusId",
                table: "RezervacijeOtkazivanje");

            migrationBuilder.DropForeignKey(
                name: "FK_RezervacijeOtkazivanje_Zahtjevi_ZahtjevId",
                table: "RezervacijeOtkazivanje");

            migrationBuilder.DropForeignKey(
                name: "FK_Zahtjevi_Djelatnici_DjelatnikId",
                table: "Zahtjevi");

            migrationBuilder.DropForeignKey(
                name: "FK_Zahtjevi_TipoviZahtjeva_TipZahtjevaId",
                table: "Zahtjevi");

            migrationBuilder.DropColumn(
                name: "Onemoguceno",
                table: "RadnaMjesta");

            migrationBuilder.AlterColumn<int>(
                name: "RadnoMjestoId",
                table: "RezervacijeOtkazivanje",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TipLaptopaId",
                table: "RadnaMjesta",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Djelatnici_OrganizacijskeJedinice_OrgJedinicaId",
                table: "Djelatnici",
                column: "OrgJedinicaId",
                principalTable: "OrganizacijskeJedinice",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Djelatnici_TipoviLaptopa_TipLaptopaId",
                table: "Djelatnici",
                column: "TipLaptopaId",
                principalTable: "TipoviLaptopa",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Djelatnici_Uloge_UlogaID",
                table: "Djelatnici",
                column: "UlogaID",
                principalTable: "Uloge",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LokacijaOrganizacijskaJedinica_Lokacije_LokacijeId",
                table: "LokacijaOrganizacijskaJedinica",
                column: "LokacijeId",
                principalTable: "Lokacije",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LokacijaOrganizacijskaJedinica_OrganizacijskeJedinice_OrganizacijskeJediniceId",
                table: "LokacijaOrganizacijskaJedinica",
                column: "OrganizacijskeJediniceId",
                principalTable: "OrganizacijskeJedinice",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LokacijaOrganizacijskaJedinicas_Lokacije_LokacijeId",
                table: "LokacijaOrganizacijskaJedinicas",
                column: "LokacijeId",
                principalTable: "Lokacije",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LokacijaOrganizacijskaJedinicas_OrganizacijskeJedinice_OrganizacijskeJediniceId",
                table: "LokacijaOrganizacijskaJedinicas",
                column: "OrganizacijskeJediniceId",
                principalTable: "OrganizacijskeJedinice",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lokacije_Gradovi_GradId",
                table: "Lokacije",
                column: "GradId",
                principalTable: "Gradovi",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RadnaMjesta_Lokacije_LokacijaId",
                table: "RadnaMjesta",
                column: "LokacijaId",
                principalTable: "Lokacije",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RadnaMjesta_TipoviLaptopa_TipLaptopaId",
                table: "RadnaMjesta",
                column: "TipLaptopaId",
                principalTable: "TipoviLaptopa",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RezervacijeOtkazivanje_RadnaMjesta_RadnoMjestoId",
                table: "RezervacijeOtkazivanje",
                column: "RadnoMjestoId",
                principalTable: "RadnaMjesta",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RezervacijeOtkazivanje_Statusi_StatusId",
                table: "RezervacijeOtkazivanje",
                column: "StatusId",
                principalTable: "Statusi",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RezervacijeOtkazivanje_Zahtjevi_ZahtjevId",
                table: "RezervacijeOtkazivanje",
                column: "ZahtjevId",
                principalTable: "Zahtjevi",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Zahtjevi_Djelatnici_DjelatnikId",
                table: "Zahtjevi",
                column: "DjelatnikId",
                principalTable: "Djelatnici",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Zahtjevi_TipoviZahtjeva_TipZahtjevaId",
                table: "Zahtjevi",
                column: "TipZahtjevaId",
                principalTable: "TipoviZahtjeva",
                principalColumn: "Id");
        }
    }
}

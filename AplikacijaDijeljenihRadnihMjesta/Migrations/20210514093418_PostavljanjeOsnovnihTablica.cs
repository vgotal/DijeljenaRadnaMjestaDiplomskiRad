using Microsoft.EntityFrameworkCore.Migrations;

namespace AplikacijaDijeljenihRadnihMjesta.Migrations
{
    public partial class PostavljanjeOsnovnihTablica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizacijskeJedinice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizacijskeJedinice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoviLaptopa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoviLaptopa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lokacije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zgrada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojSobe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokacije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lokacije_Gradovi_GradId",
                        column: x => x.GradId,
                        principalTable: "Gradovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Djelatnici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MBR = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrgJedinicaId = table.Column<int>(type: "int", nullable: false),
                    TipLaptopaId = table.Column<int>(type: "int", nullable: false),
                    MaxBrojDanaFirma = table.Column<int>(type: "int", nullable: false),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Djelatnici", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Djelatnici_OrganizacijskeJedinice_OrgJedinicaId",
                        column: x => x.OrgJedinicaId,
                        principalTable: "OrganizacijskeJedinice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Djelatnici_TipoviLaptopa_TipLaptopaId",
                        column: x => x.TipLaptopaId,
                        principalTable: "TipoviLaptopa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LokacijaOrganizacijskaJedinica",
                columns: table => new
                {
                    LokacijeId = table.Column<int>(type: "int", nullable: false),
                    OrganizacijskeJediniceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LokacijaOrganizacijskaJedinica", x => new { x.LokacijeId, x.OrganizacijskeJediniceId });
                    table.ForeignKey(
                        name: "FK_LokacijaOrganizacijskaJedinica_Lokacije_LokacijeId",
                        column: x => x.LokacijeId,
                        principalTable: "Lokacije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LokacijaOrganizacijskaJedinica_OrganizacijskeJedinice_OrganizacijskeJediniceId",
                        column: x => x.OrganizacijskeJediniceId,
                        principalTable: "OrganizacijskeJedinice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RadnaMjesta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sifra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipLaptopaId = table.Column<int>(type: "int", nullable: false),
                    LokacijaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadnaMjesta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RadnaMjesta_Lokacije_LokacijaId",
                        column: x => x.LokacijaId,
                        principalTable: "Lokacije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RadnaMjesta_TipoviLaptopa_TipLaptopaId",
                        column: x => x.TipLaptopaId,
                        principalTable: "TipoviLaptopa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Djelatnici_OrgJedinicaId",
                table: "Djelatnici",
                column: "OrgJedinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Djelatnici_TipLaptopaId",
                table: "Djelatnici",
                column: "TipLaptopaId");

            migrationBuilder.CreateIndex(
                name: "IX_LokacijaOrganizacijskaJedinica_OrganizacijskeJediniceId",
                table: "LokacijaOrganizacijskaJedinica",
                column: "OrganizacijskeJediniceId");

            migrationBuilder.CreateIndex(
                name: "IX_Lokacije_GradId",
                table: "Lokacije",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_RadnaMjesta_LokacijaId",
                table: "RadnaMjesta",
                column: "LokacijaId");

            migrationBuilder.CreateIndex(
                name: "IX_RadnaMjesta_TipLaptopaId",
                table: "RadnaMjesta",
                column: "TipLaptopaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Djelatnici");

            migrationBuilder.DropTable(
                name: "LokacijaOrganizacijskaJedinica");

            migrationBuilder.DropTable(
                name: "RadnaMjesta");

            migrationBuilder.DropTable(
                name: "OrganizacijskeJedinice");

            migrationBuilder.DropTable(
                name: "Lokacije");

            migrationBuilder.DropTable(
                name: "TipoviLaptopa");

            migrationBuilder.DropTable(
                name: "Gradovi");
        }
    }
}

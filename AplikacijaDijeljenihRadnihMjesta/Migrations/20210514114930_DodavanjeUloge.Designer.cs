﻿// <auto-generated />
using System;
using AplikacijaDijeljenihRadnihMjesta.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AplikacijaDijeljenihRadnihMjesta.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210514114930_DodavanjeUloge")]
    partial class DodavanjeUloge
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.Djelatnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KorisnickoIme")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lozinka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MBR")
                        .HasColumnType("int");

                    b.Property<int>("MaxBrojDanaFirma")
                        .HasColumnType("int");

                    b.Property<int>("OrgJedinicaId")
                        .HasColumnType("int");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipLaptopaId")
                        .HasColumnType("int");

                    b.Property<int>("UlogaID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrgJedinicaId");

                    b.HasIndex("TipLaptopaId");

                    b.HasIndex("UlogaID");

                    b.ToTable("Djelatnici");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.Grad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Gradovi");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.Lokacija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojSobe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GradId")
                        .HasColumnType("int");

                    b.Property<string>("Zgrada")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GradId");

                    b.ToTable("Lokacije");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.OrganizacijskaJedinica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrganizacijskeJedinice");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.RadnoMjesto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LokacijaId")
                        .HasColumnType("int");

                    b.Property<string>("Sifra")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipLaptopaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LokacijaId");

                    b.HasIndex("TipLaptopaId");

                    b.ToTable("RadnaMjesta");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.RezervacijaOtkazivanje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProvjeraOtkazivanjaRezerviranja")
                        .HasColumnType("int");

                    b.Property<int>("RadnoMjestoId")
                        .HasColumnType("int");

                    b.Property<int>("ZahtjevId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RadnoMjestoId");

                    b.HasIndex("ZahtjevId")
                        .IsUnique();

                    b.ToTable("RezervacijeOtkazivanje");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.TipLaptopa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoviLaptopa");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.TipZahtjeva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Tip")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoviZahtjeva");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.Uloga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Uloge");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.Zahtjev", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("DjelatnikId")
                        .HasColumnType("int");

                    b.Property<int>("TipZahtjevaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DjelatnikId");

                    b.HasIndex("TipZahtjevaId");

                    b.ToTable("Zahtjevi");
                });

            modelBuilder.Entity("LokacijaOrganizacijskaJedinica", b =>
                {
                    b.Property<int>("LokacijeId")
                        .HasColumnType("int");

                    b.Property<int>("OrganizacijskeJediniceId")
                        .HasColumnType("int");

                    b.HasKey("LokacijeId", "OrganizacijskeJediniceId");

                    b.HasIndex("OrganizacijskeJediniceId");

                    b.ToTable("LokacijaOrganizacijskaJedinica");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.Djelatnik", b =>
                {
                    b.HasOne("AplikacijaDijeljenihRadnihMjesta.Models.OrganizacijskaJedinica", "OrgJedinica")
                        .WithMany("Djelatnici")
                        .HasForeignKey("OrgJedinicaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AplikacijaDijeljenihRadnihMjesta.Models.TipLaptopa", "TipLaptopa")
                        .WithMany("Djelatnici")
                        .HasForeignKey("TipLaptopaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AplikacijaDijeljenihRadnihMjesta.Models.Uloga", "Uloga")
                        .WithMany("Djelatnici")
                        .HasForeignKey("UlogaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrgJedinica");

                    b.Navigation("TipLaptopa");

                    b.Navigation("Uloga");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.Lokacija", b =>
                {
                    b.HasOne("AplikacijaDijeljenihRadnihMjesta.Models.Grad", "Grad")
                        .WithMany("Lokacije")
                        .HasForeignKey("GradId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grad");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.RadnoMjesto", b =>
                {
                    b.HasOne("AplikacijaDijeljenihRadnihMjesta.Models.Lokacija", "Lokacija")
                        .WithMany("RadnaMjesta")
                        .HasForeignKey("LokacijaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AplikacijaDijeljenihRadnihMjesta.Models.TipLaptopa", "TipLaptopa")
                        .WithMany("RadnaMjesta")
                        .HasForeignKey("TipLaptopaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lokacija");

                    b.Navigation("TipLaptopa");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.RezervacijaOtkazivanje", b =>
                {
                    b.HasOne("AplikacijaDijeljenihRadnihMjesta.Models.RadnoMjesto", "RadnoMjesto")
                        .WithMany()
                        .HasForeignKey("RadnoMjestoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AplikacijaDijeljenihRadnihMjesta.Models.Zahtjev", "Zahtjev")
                        .WithOne("RezervacijaOtkazivanje")
                        .HasForeignKey("AplikacijaDijeljenihRadnihMjesta.Models.RezervacijaOtkazivanje", "ZahtjevId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RadnoMjesto");

                    b.Navigation("Zahtjev");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.Zahtjev", b =>
                {
                    b.HasOne("AplikacijaDijeljenihRadnihMjesta.Models.Djelatnik", "Djelatnik")
                        .WithMany()
                        .HasForeignKey("DjelatnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AplikacijaDijeljenihRadnihMjesta.Models.TipZahtjeva", "TipZahtjeva")
                        .WithMany("Zahtjevi")
                        .HasForeignKey("TipZahtjevaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Djelatnik");

                    b.Navigation("TipZahtjeva");
                });

            modelBuilder.Entity("LokacijaOrganizacijskaJedinica", b =>
                {
                    b.HasOne("AplikacijaDijeljenihRadnihMjesta.Models.Lokacija", null)
                        .WithMany()
                        .HasForeignKey("LokacijeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AplikacijaDijeljenihRadnihMjesta.Models.OrganizacijskaJedinica", null)
                        .WithMany()
                        .HasForeignKey("OrganizacijskeJediniceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.Grad", b =>
                {
                    b.Navigation("Lokacije");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.Lokacija", b =>
                {
                    b.Navigation("RadnaMjesta");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.OrganizacijskaJedinica", b =>
                {
                    b.Navigation("Djelatnici");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.TipLaptopa", b =>
                {
                    b.Navigation("Djelatnici");

                    b.Navigation("RadnaMjesta");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.TipZahtjeva", b =>
                {
                    b.Navigation("Zahtjevi");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.Uloga", b =>
                {
                    b.Navigation("Djelatnici");
                });

            modelBuilder.Entity("AplikacijaDijeljenihRadnihMjesta.Models.Zahtjev", b =>
                {
                    b.Navigation("RezervacijaOtkazivanje");
                });
#pragma warning restore 612, 618
        }
    }
}

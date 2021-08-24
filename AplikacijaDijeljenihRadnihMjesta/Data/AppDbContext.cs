using AplikacijaDijeljenihRadnihMjesta.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Data
{
    public class AppDbContext : IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LokacijaOrganizacijskaJedinica>()
                .HasKey(bc => new { bc.LokacijeId, bc.OrganizacijskeJediniceId });
            modelBuilder.Entity<LokacijaOrganizacijskaJedinica>()
                .HasOne(bc => bc.Lokacija)
                .WithMany(b => b.LokacijaOrganizacijskaJedinica)
                .HasForeignKey(bc => bc.LokacijeId);
            modelBuilder.Entity<LokacijaOrganizacijskaJedinica>()
                .HasOne(bc => bc.OrganizacijskaJedinica)
                .WithMany(c => c.LokacijaOrganizacijskaJedinica)
                .HasForeignKey(bc => bc.OrganizacijskeJediniceId);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Djelatnik> Djelatnici
        {
            get; set;
        }

        public DbSet<TipLaptopa> TipoviLaptopa
        {
            get; set;
        }

        public DbSet<OrganizacijskaJedinica> OrganizacijskeJedinice
        {
            get; set;
        }

        public DbSet<Grad> Gradovi
        {
            get; set;
        }

        public DbSet<Status> Statusi
        {
            get; set;
        }
        public DbSet<Lokacija> Lokacije
        {
            get; set;
        }

        public DbSet<RadnoMjesto> RadnaMjesta
        {
            get; set;
        }

        public DbSet<Zahtjev> Zahtjevi
        {
            get; set;
        }

        public DbSet<TipZahtjeva> TipoviZahtjeva
        {
            get; set;
        }

        public DbSet<RezervacijaOtkazivanje> RezervacijeOtkazivanje
        {
            get; set;
        }

        public DbSet<Uloga> Uloge
        {
            get; set;
        }

        public DbSet<LokacijaOrganizacijskaJedinica> LokacijaOrganizacijskaJedinicas
        {
            get; set;
        }

      
    }
}

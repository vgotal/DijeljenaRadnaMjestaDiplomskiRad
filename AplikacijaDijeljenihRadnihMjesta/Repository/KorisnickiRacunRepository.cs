using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Repository
{
    public class KorisnickiRacunRepository
    {
        private readonly AppDbContext db;

        private int DjelatnikID;
        public KorisnickiRacunRepository(AppDbContext db)
        {
            this.db = db;
        }

        
        public int ProvjeraDjelatnikaPriPrijavi(string korisnickoIme, string lozinka)
        {
            var md5 = new MD5CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(lozinka);
            var md5data = md5.ComputeHash(data);
            var hashedPassword = Convert.ToBase64String(md5data);
            var id = db.Djelatnici.Where(d => d.KorisnickoIme == korisnickoIme && d.Lozinka == hashedPassword).Select(d => d.Id).FirstOrDefault();
            if (id != 0)
            {
                DjelatnikID = id;
                return id;
            }
            return 0;  //vidi kako poboljšati
        }

        public string DohvatiUloguDjelatnika(int id)
        {
            var uloga = db.Djelatnici.Where(d => d.Id == id).Select(d => d.Uloga.Naziv).FirstOrDefault().ToString();
            return uloga;
        }
        public string DohvatiUloguDjelatnika()
        {
            var uloga = db.Djelatnici.Where(d => d.Id == DjelatnikID).Select(d => d.Uloga.Naziv).FirstOrDefault().ToString();
            return uloga;
        }
    }
}

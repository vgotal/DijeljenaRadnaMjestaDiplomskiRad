using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using AplikacijaDijeljenihRadnihMjesta.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Repository
{
    public class OtkazivanjeRepository
    {
        public readonly AppDbContext db;
        public OtkazivanjeRepository(AppDbContext db)
        {
            this.db = db;
        }

        public string DohvatiDjelatnikovuUlogu(int id)
        {
            var ulogaDjelatnika = (from uloga in db.Uloge
                                   join djelatnik in db.Djelatnici on uloga.Id equals djelatnik.UlogaID
                                   where djelatnik.Id.Equals(id)
                                   select uloga.Naziv).FirstOrDefault().ToString();
            return ulogaDjelatnika;
        }


        public OtkazivanjeVM DohvatiMogucaOtkazivanja(OtkazivanjeVM model, int djelatnikID)
        {
            var zahtjevOtk=0;
            try
            {
                if (model.Rezervacije == null || model.Rezervacije.Count == 0)
                {
                    model.Rezervacije = DohvatiRezervacijeOtkazivanja(djelatnikID);
                }
                else if (!model.Rezervacije.Where(rezervacija => rezervacija.OdgovorCheckBox).Any())
                {
                    model.PovratnaInfo = "Niste označili termin za otkazivanje!";
                }
                else
                {
                    var filtriraneRezervacije = model.Rezervacije.Where(rez => rez.OdgovorCheckBox).ToList();
                    List<string> listaDatuma = new List<string>();
                    foreach (RezervacijaModel rezervacija in filtriraneRezervacije)
                    {
                        try
                        {
                            db.Database.ExecuteSqlInterpolated($"exec [dbo].[OtkazivanjezahtjevaRezerviranje] {rezervacija.ZeljeniDatum:yyyy-MM-dd},{model.LokacijaID},{djelatnikID}, { rezervacija.Id}, {rezervacija.RazlogOtkazivanja}");
                            listaDatuma.Add(rezervacija.ZeljeniDatum.ToShortDateString());
                           zahtjevOtk = (from rezervacije in db.RezervacijeOtkazivanje
                                              join radnoMjesto in db.RadnaMjesta on rezervacije.RadnoMjestoId equals radnoMjesto.Id
                                              join lokacija in db.Lokacije on radnoMjesto.LokacijaId equals lokacija.Id
                                              join zahtjev in db.Zahtjevi on rezervacije.ZahtjevId equals zahtjev.Id
                                              join tipZahtjeva in db.TipoviZahtjeva on zahtjev.TipZahtjevaId equals tipZahtjeva.Id
                                              where rezervacije.ProvjeraOtkazivanjaRezerviranja == rezervacija.Id && zahtjev.DjelatnikId == djelatnikID && rezervacije.Datum == rezervacija.ZeljeniDatum
                                              select rezervacije.Id).FirstOrDefault();
                            db.SaveChanges();
                        }
                        catch (Exception)
                        {
                            model.PovratnaInfo = $"Pogreška pri unosu rezervacija u bazu za rezervaciju za datum: {rezervacija.ZeljeniDatum.ToShortDateString()}";
                        }
                        
                        if (!DohvatiOtkazivanje(rezervacija, djelatnikID))
                        {
                            var otkID = (from rezervacije in db.RezervacijeOtkazivanje
                                       join radnoMjesto in db.RadnaMjesta on rezervacije.RadnoMjestoId equals radnoMjesto.Id
                                       join lokacija in db.Lokacije on radnoMjesto.LokacijaId equals lokacija.Id
                                       join zahtjev in db.Zahtjevi on rezervacije.ZahtjevId equals zahtjev.Id
                                       join tipZahtjeva in db.TipoviZahtjeva on zahtjev.TipZahtjevaId equals tipZahtjeva.Id
                                       where rezervacije.ProvjeraOtkazivanjaRezerviranja == null && zahtjev.DjelatnikId == djelatnikID && rezervacije.Datum == rezervacija.ZeljeniDatum && rezervacije.Status == null
                                       && zahtjev.TipZahtjevaId == 2
                                       select rezervacije.Id).FirstOrDefault();
                            var otkazivanje = db.RezervacijeOtkazivanje.Find(otkID);
                        }
                    }
                    if (filtriraneRezervacije.Count > 0)
                    {
                        var zahtjvOtk = db.RezervacijeOtkazivanje.Find(zahtjevOtk);
                        model.PovratnaInfoUspjeh = $"Podnošenje zahtjeva za otkazivanje je uspješno izvršeno za datume: {String.Join(", ", listaDatuma)}";
                        model.Rezervacije = DohvatiRezervacijeOtkazivanja(djelatnikID);
                    }
                }
            }
            catch (Exception)
            {
                model.PovratnaInfo = "Pogreška pri radu s bazom";
            }
            return model;
        }
        public List<RezervacijaModel> DohvatiRezervacijeOtkazivanja(int djelatnikID)
        {
            DateTime pocetniDatum = DateTime.Now.DohvatiRadneDane(1).Date;
            DateTime krajnjiDatum = DateTime.Now.DohvatiRadneDane(5).Date;
            var dohvaceneRezervacije = (from rezervacije in db.RezervacijeOtkazivanje
                                        join radnoMjesto in db.RadnaMjesta on rezervacije.RadnoMjestoId equals radnoMjesto.Id
                                        join lokacija in db.Lokacije on radnoMjesto.LokacijaId equals lokacija.Id
                                        join zahtjev in db.Zahtjevi on rezervacije.ZahtjevId equals zahtjev.Id
                                        join tipZahtjeva in db.TipoviZahtjeva on zahtjev.TipZahtjevaId equals tipZahtjeva.Id
                                        where (zahtjev.DjelatnikId == djelatnikID &&
                                        rezervacije.Datum >= pocetniDatum &&
                                        rezervacije.Datum <= krajnjiDatum &&
                                        tipZahtjeva.Tip == "Rezervacija" && rezervacije.StatusId == 1 && rezervacije.OtkazivanjeZahtjeva==false && rezervacije.ProvjeraOtkazivanjaRezerviranja==null)
                                        orderby rezervacije.Datum ascending
                                        select new RezervacijaModel()
                                        {
                                            Id = rezervacije.Id,
                                            SifraRadnogMjesta = radnoMjesto.Sifra,
                                            ZeljeniDatum = rezervacije.Datum,
                                            Adresa = lokacija.Adresa,
                                            OdgovorCheckBox = false
                                        }).ToList();

            return dohvaceneRezervacije.OrderBy(rez => rez.ZeljeniDatum).ToList();
        }

        public bool DohvatiOtkazivanje(RezervacijaModel model, int djelatnikID)
        {
            var provjera = (from rezervacije in db.RezervacijeOtkazivanje
                            join radnoMjesto in db.RadnaMjesta on rezervacije.RadnoMjestoId equals radnoMjesto.Id
                            join lokacija in db.Lokacije on radnoMjesto.LokacijaId equals lokacija.Id
                            join zahtjev in db.Zahtjevi on rezervacije.ZahtjevId equals zahtjev.Id
                            join tipZahtjeva in db.TipoviZahtjeva on zahtjev.TipZahtjevaId equals tipZahtjeva.Id
                            where rezervacije.ProvjeraOtkazivanjaRezerviranja != null && zahtjev.DjelatnikId == djelatnikID && rezervacije.Datum == model.ZeljeniDatum && rezervacije.StatusId == 1
                            && zahtjev.TipZahtjevaId == 1
                            select rezervacije.Id).FirstOrDefault();
            if (provjera != 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}

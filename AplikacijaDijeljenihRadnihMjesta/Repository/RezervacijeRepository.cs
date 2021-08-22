using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using AplikacijaDijeljenihRadnihMjesta.Models;

namespace AplikacijaDijeljenihRadnihMjesta.Repository
{
    public class RezervacijeRepository
    {
        public readonly AppDbContext db;
        public RezervacijeRepository(AppDbContext db)
        {
            this.db = db;
        }

       
        public List<RezervacijaModel> DohvatiRezervacije(int djelatnikID, int lokacijaID) 
        {
            var pocetniDatum = DateTime.Now.DohvatiRadneDane(1).Date;
            var krajnjiDatum = DateTime.Now.DohvatiRadneDane(5).Date;
            var dohvaceneRezervacije = (from rezervacije in db.RezervacijeOtkazivanje
                                            join radnoMjesto in db.RadnaMjesta on rezervacije.RadnoMjestoId equals radnoMjesto.Id
                                            join lokacija in db.Lokacije on radnoMjesto.LokacijaId equals lokacija.Id
                                            join zahtjev in db.Zahtjevi on rezervacije.ZahtjevId equals zahtjev.Id
                                            join status in db.Statusi on rezervacije.StatusId equals status.Id
                                            where (zahtjev.DjelatnikId == djelatnikID &&
                                            rezervacije.ProvjeraOtkazivanjaRezerviranja == null &&
                                            rezervacije.Datum >= pocetniDatum &&
                                            rezervacije.Datum <= krajnjiDatum )
                                            orderby rezervacije.Datum ascending
                                            select new RezervacijaModel()
                                            {
                                                Id = rezervacije.Id,
                                                SifraRadnogMjesta = radnoMjesto.Sifra,
                                                ZeljeniDatum = rezervacije.Datum,
                                                OdgovorCheckBox = true,
                                                Rezervirano = true,
                                                LokacijaID = radnoMjesto.LokacijaId,
                                                Adresa = lokacija.Adresa,
                                                Otkazano = false,
                                                Status=status.Tip,
                                                OtkazivanjeZahtjeva=rezervacije.OtkazivanjeZahtjeva
                                            }).ToList();
            
            var adresa = db.Lokacije.Where(l => l.Id.Equals(lokacijaID)).Select(l => l.Adresa).FirstOrDefault().ToString();
            for (int i = 1;  i <= 5; i++)
            {
                var datum = DateTime.Now.DohvatiRadneDane(i).Date;
                if (dohvaceneRezervacije.FirstOrDefault(rez => rez.ZeljeniDatum.Date.Equals(datum)) == null)
                {
                    dohvaceneRezervacije.Add(new RezervacijaModel()
                    {
                        ZeljeniDatum = datum,
                        OdgovorCheckBox = false,
                        Rezervirano = false,
                        Adresa = adresa,
                        Otkazano = false
                    });
                }
            }
            return dohvaceneRezervacije.OrderBy(rez => rez.ZeljeniDatum).ToList();
        }

        public int DohvatiRezervacije(int djelatnikID, int lokacijaID, DateTime datumTrazeni)
        {

            var dohvaceneRezervacije = (from rezervacije in db.RezervacijeOtkazivanje
                                        join radnoMjesto in db.RadnaMjesta on rezervacije.RadnoMjestoId equals radnoMjesto.Id
                                        join lokacija in db.Lokacije on radnoMjesto.LokacijaId equals lokacija.Id
                                        join zahtjev in db.Zahtjevi on rezervacije.ZahtjevId equals zahtjev.Id
                                        where (zahtjev.DjelatnikId == djelatnikID &&
                                        rezervacije.ProvjeraOtkazivanjaRezerviranja == null &&
                                        rezervacije.Datum == datumTrazeni)
                                        orderby rezervacije.Datum ascending
                                        select rezervacije.Id).FirstOrDefault();
            return dohvaceneRezervacije;
        }

        public List<SelectListItem> DohvatiLokacije(RezervacijaVM rezervacije, int djelatnikID)
        {
            var dohvaceneLokacije = (from lokacija in db.Lokacije
                                     join lokOrgJed in db.LokacijaOrganizacijskaJedinicas on lokacija.Id equals lokOrgJed.LokacijeId
                                     join orgJed in db.OrganizacijskeJedinice on lokOrgJed.OrganizacijskeJediniceId equals orgJed.Id
                                     join djelatnik in db.Djelatnici on orgJed.Id equals djelatnik.OrgJedinicaId
                                     where (djelatnik.Id == djelatnikID)
                                     select new SelectListItem
                                     {
                                         Value = lokacija.Id.ToString(),
                                         Text = lokacija.Adresa
                                     }).ToList();
            return dohvaceneLokacije;
        }

        public string DohvatiOdabranuAdresu(RezervacijaVM rezervacija)
        {
            return db.Lokacije.Where(l => l.Id.Equals(Int32.Parse(rezervacija.Lokacija))).Select(l => l.Adresa).FirstOrDefault();
        }


        public RezervacijaVM DohvatiMoguceRezervacije(RezervacijaVM model, int djelatnikID, int lokacijaID)
        {
            var djelatnik = db.Djelatnici.Find(djelatnikID);
            var upjesniDatumi = new List<string>();
            Dictionary<string, string> stanjaRezervacija = new Dictionary<string, string>();
            var uspjesneRezervacije = new List<string>();
            var neuspjesneRezervacije = new List<string>();
            if (model.Rezervacije != null)
            {
                foreach (var rez in model.Rezervacije)
                {
                    rez.LokacijaID = lokacijaID;
                }
            }
            try
            {
                if (model.Rezervacije == null || model.Rezervacije.Count == 0)
                {
                    model.Rezervacije = DohvatiRezervacije(djelatnikID, lokacijaID );
                }
                else if (!model.Rezervacije.Where(rezervacija => rezervacija.OdgovorCheckBox == true && rezervacija.Rezervirano == false).Any())
                {
                   model.PovratnaInfo = "Niste označili novi termin za rezervaciju!";
                    model.Rezervacije = DohvatiRezervacije(djelatnikID, lokacijaID);

                }
                else if (model.Rezervacije.Where(r => r.OdgovorCheckBox == true).Count() > djelatnik.MaxBrojDanaFirma)
                {
                    model.PovratnaInfo = $"Maksimalan broj dana za rezervaciju: {djelatnik.MaxBrojDanaFirma}, odznačite one dane koji su višak!";
                    model.Rezervacije = DohvatiRezervacije(djelatnikID, lokacijaID); 
                }
                else
                {
                    var filtriraneRezervacije = model.Rezervacije.Where(rez => rez.OdgovorCheckBox && String.IsNullOrEmpty(rez.SifraRadnogMjesta) && rez.LokacijaID.Equals(lokacijaID)).ToList();
                    var noveRezervacije=new List<RezervacijaModel>();
                    var UspjesneRezervacija = new List<RezervacijaModel>();
                    var stareRezervacije = new List<RezervacijaModel>();
                    
                    foreach (RezervacijaModel rezervacija in filtriraneRezervacije)
                    {
                        var nesto = DohvatiRezervacije(djelatnikID, lokacijaID, rezervacija.ZeljeniDatum);
                        if (nesto == 0)
                        {
                            noveRezervacije.Add(rezervacija);
                        }
                        else
                        {
                            stareRezervacije.Add(rezervacija);
                        }
                    }
                    foreach (RezervacijaModel rezervacija in noveRezervacije)
                    {
                         try
                        {
                            db.Database.ExecuteSqlInterpolated($"exec [dbo].[DohvatiMoguceRezervacije] {rezervacija.ZeljeniDatum:yyyy-MM-dd},{lokacijaID},{djelatnikID}");
                            var provjera = DohvatiRezervacije(djelatnikID,  lokacijaID, rezervacija.ZeljeniDatum);
                                if (provjera == 0)
                                {
                                    neuspjesneRezervacije.Add(rezervacija.ZeljeniDatum.ToShortDateString());
                                }
                                else
                                    UspjesneRezervacija.Add(rezervacija);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("{0} Exception caught.", e);
                            model.PovratnaInfoNeuspjeh = $"Pogreška pri unosu rezervacija u bazu za rezervaciju za datum: {rezervacija.ZeljeniDatum.ToShortDateString()}";
                        }
                    }
                   
                    if (noveRezervacije.Count > 0)
                    {
                        if (noveRezervacije.Count > 0)
                        {
                            for (int i = 0; i < UspjesneRezervacija.Count; i++)
                               {
                                uspjesneRezervacije.Add(noveRezervacije[i].ZeljeniDatum.ToShortDateString());
                            }
                            stanjaRezervacija.Add("uspjesneRezervacije", String.Join(", ", uspjesneRezervacije));
                            stanjaRezervacija.Add("neuspjesneRezervacije", String.Join(", ", neuspjesneRezervacije));
                            if (stanjaRezervacija.TryGetValue("uspjesneRezervacije", out string uspjesneRezervacij) && uspjesneRezervacij.Length > 0)
                            {
                                model.PovratnaInfoUspjeh = $"Rezervacije su uspješno izvršene za datume: {uspjesneRezervacij}";
                                model.Datumi = uspjesneRezervacij;
                            }
                            if (stanjaRezervacija.TryGetValue("neuspjesneRezervacije", out string neuspjesneRezervacij) && neuspjesneRezervacij.Length > 0)
                            {
                                model.PovratnaInfoNeuspjeh = $"Rezervacije nisu moguće za datume: {neuspjesneRezervacij}";
                                model.Datumi = neuspjesneRezervacij;
                            }
                        }
                    }
                    model.Rezervacije = DohvatiRezervacije(djelatnikID, lokacijaID);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                model.PovratnaInfo = "Pogreška pri radu s bazom";
            }
            return model;
        }


        public string DohvatiDjelatnikovuUlogu(int id)
        {
            var ulogaDjelatnika = (from uloga in db.Uloge
                                   join djelatnik in db.Djelatnici on uloga.Id equals djelatnik.UlogaID
                                   where djelatnik.Id.Equals(id) select uloga.Naziv).FirstOrDefault().ToString();
            return ulogaDjelatnika;
        }

        public Djelatnik DohvatiDjelatnika(int id)
        {
            return db.Djelatnici.Find(id);
        }

    }

}


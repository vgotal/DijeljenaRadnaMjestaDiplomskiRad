using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models;
using AplikacijaDijeljenihRadnihMjesta.Models.Paginacija;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Repository
{
    public class OdobravanjeZahtjevaRepository
    {
        public readonly AppDbContext db;
        private MailRequest request = new MailRequest();
        private readonly IMailService mailServices;

        public OdobravanjeZahtjevaRepository(AppDbContext db, IMailService mailServices)
        {
            this.db = db;
            this.mailServices = mailServices;
        }

        public string DohvatiDjelatnikovuUlogu(int id)
        {
            var ulogaDjelatnika = (from uloga in db.Uloge
                                   join djelatnik in db.Djelatnici on uloga.Id equals djelatnik.UlogaID
                                   where djelatnik.Id.Equals(id)
                                   select uloga.Naziv).FirstOrDefault().ToString();
            return ulogaDjelatnika;
        }

        public Djelatnik DohvatiDjelatnika(int id)
        {
            return db.Djelatnici.Find(id);
        }

        public void DohvatiZahtjeveZaOdobravanjeSPaginacijom(PaginacijaOdobravanjeZahtjeva model, int pageSize, int pageNumber, int djelatnikID)
        {
            int brojZahtjevaAdmin = (from zahtjeviZaOdobravanje in db.RezervacijeOtkazivanje
                                     join zahtjev in db.Zahtjevi on zahtjeviZaOdobravanje.ZahtjevId equals zahtjev.Id
                                     join djelatnik in db.Djelatnici on zahtjev.DjelatnikId equals djelatnik.Id
                                     where zahtjev.DjelatnikId != djelatnikID
                                     select zahtjeviZaOdobravanje.Id).Count();
            if (model.odobravanjeZahtjeva.Status != null )
            {
                DohvatiZahtjeveZaOdobravanje(model, pageSize, pageNumber, djelatnikID);
                var result = new PagedResult<RezervacijeOtkazivanjeVM>
                {
                    Data = model.odobravanjeZahtjeva.RezervacijeOtkazivanje.ToList(),
                    TotalItems = db.RezervacijeOtkazivanje.Where(z=>z.StatusId.Equals(model.odobravanjeZahtjeva.Status)).Count()-brojZahtjevaAdmin,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
                model.paginationModel = result;
            }
            else
            {
                model.odobravanjeZahtjeva.RezervacijeOtkazivanje = DohvatiZahtjeveZaOdobravanje( pageSize, pageNumber, djelatnikID);
                var result = new PagedResult<RezervacijeOtkazivanjeVM>
                {
                    Data = model.odobravanjeZahtjeva.RezervacijeOtkazivanje.ToList(),
                    TotalItems = db.RezervacijeOtkazivanje.Count() - brojZahtjevaAdmin,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
                model.paginationModel = result;
            }
          
        }

        public List<RezervacijeOtkazivanjeVM> DohvatiZahtjeveZaOdobravanje( int pageSize, int pageNumber, int djelatnikID)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var zahtjevi= (from zahtjeviZaOdobravanje in db.RezervacijeOtkazivanje
                           join zahtjev in db.Zahtjevi on zahtjeviZaOdobravanje.ZahtjevId equals zahtjev.Id
                           join tip in db.TipoviZahtjeva on zahtjev.TipZahtjevaId equals tip.Id
                           join djelatnik in db.Djelatnici on zahtjev.DjelatnikId equals djelatnik.Id
                           where zahtjev.DjelatnikId != djelatnikID
                           orderby zahtjeviZaOdobravanje.Datum 
                           select new RezervacijeOtkazivanjeVM()
                           {
                               Id=zahtjeviZaOdobravanje.Id,
                              Datum=zahtjeviZaOdobravanje.Datum,
                              ProvjeraOtkazivanjaRezerviranja=zahtjeviZaOdobravanje.ProvjeraOtkazivanjaRezerviranja,
                              ZahtjevId=zahtjeviZaOdobravanje.ZahtjevId,
                              ImeIPrezime=djelatnik.Ime +" "+ djelatnik.Prezime,
                              DjelatnikEmail=djelatnik.Email,
                              TipZahtjeva=zahtjev.TipZahtjeva.Tip,
                              Status=zahtjeviZaOdobravanje.Status.Tip,
                               RazlogOtkazivanja = zahtjeviZaOdobravanje.RazlogOtkazivanja,
                               Komentar = zahtjeviZaOdobravanje.Komentar,
                               OtkazivanjeZahtjeva =zahtjeviZaOdobravanje.OtkazivanjeZahtjeva
                           }).Skip(ExcludeRecords).Take(pageSize).ToList();
            return zahtjevi;
        }

        public void DohvatiZahtjeveZaOdobravanje(PaginacijaOdobravanjeZahtjeva zahtjeviOdobravanjeSPaginacijom, int pageSize, int pageNumber, int djelatnikID)
        {
           
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            zahtjeviOdobravanjeSPaginacijom.odobravanjeZahtjeva.RezervacijeOtkazivanje = (from zahtjeviZaOdobravanje in db.RezervacijeOtkazivanje
                            join zahtjev in db.Zahtjevi on zahtjeviZaOdobravanje.ZahtjevId equals zahtjev.Id
                            join tip in db.TipoviZahtjeva on zahtjev.TipZahtjevaId equals tip.Id
                            join djelatnik in db.Djelatnici on zahtjev.DjelatnikId equals djelatnik.Id
                            join status in db.Statusi on zahtjeviZaOdobravanje.StatusId equals status.Id
                            where status.Id.Equals(zahtjeviOdobravanjeSPaginacijom.odobravanjeZahtjeva.Status) && djelatnik.Id !=djelatnikID
                            orderby zahtjeviZaOdobravanje.Datum
                            select new RezervacijeOtkazivanjeVM()
                            {
                                Id = zahtjeviZaOdobravanje.Id,
                                Datum = zahtjeviZaOdobravanje.Datum,
                                ProvjeraOtkazivanjaRezerviranja = zahtjeviZaOdobravanje.ProvjeraOtkazivanjaRezerviranja,
                                ZahtjevId = zahtjeviZaOdobravanje.ZahtjevId,
                                ImeIPrezime = djelatnik.Ime + " " + djelatnik.Prezime,
                                DjelatnikEmail = djelatnik.Email,
                                TipZahtjeva = zahtjev.TipZahtjeva.Tip,
                                Status = zahtjeviZaOdobravanje.Status.Tip,
                                RazlogOtkazivanja=zahtjeviZaOdobravanje.RazlogOtkazivanja,
                                Komentar=zahtjeviZaOdobravanje.Komentar,
                                OtkazivanjeZahtjeva = zahtjeviZaOdobravanje.OtkazivanjeZahtjeva
                            }).Skip(ExcludeRecords).Take(pageSize).ToList();
          
        }

        public string DohvatiDatumOdobravanja(OdobravanjeZahtjeva zahtjevi)
        {
            return db.RezervacijeOtkazivanje.Where(z => z.Id.Equals(zahtjevi.Id)).Select(z => z.Datum).FirstOrDefault().ToShortDateString();
        }


        public void PotvrdiZahtjev(OdobravanjeZahtjeva zahtjevi)
        {
                foreach (var zahtjev in zahtjevi.RezervacijeOtkazivanje)
                {
                    
                    if (zahtjev.OdgovorCheckBoxOdobreno == true)
                    {
                        var zah=db.RezervacijeOtkazivanje.Find(zahtjev.Id);
                        zah.StatusId = 1;
                        zah.Komentar = zahtjev.Komentar;
                        db.SaveChanges();
                        request.ToEmail = zahtjev.DjelatnikEmail;
                        request.Subject = "Potvrda rezervacije/otkazivanja";
                        request.Body = $" <b> <h3> Odobren zahtjev </h3></b> <br />   Zahtjev je odobren za dan/e:<b> {zah.Datum.ToShortDateString()}</b>";
                    mailServices.SendEmailAsync(request);
                        var zahtjevZaOtkazivanje = (from rezervacije in db.RezervacijeOtkazivanje
                                                    join radnoMjesto in db.RadnaMjesta on rezervacije.RadnoMjestoId equals radnoMjesto.Id
                                                    join lokacija in db.Lokacije on radnoMjesto.LokacijaId equals lokacija.Id
                                                    join zahtjevOtkazivanje in db.Zahtjevi on rezervacije.ZahtjevId equals zahtjevOtkazivanje.Id
                                                    join tipZahtjeva in db.TipoviZahtjeva on zahtjevOtkazivanje.TipZahtjevaId equals tipZahtjeva.Id
                                                    where rezervacije.Id == zah.Id   
                                                    && zahtjevOtkazivanje.TipZahtjevaId == 2
                                                    select tipZahtjeva.Id).FirstOrDefault();
                        var zahjtevZaOtkazivanjeLokacijaID = (from rezervacije in db.RezervacijeOtkazivanje
                                                              join radnoMjesto in db.RadnaMjesta on rezervacije.RadnoMjestoId equals radnoMjesto.Id
                                                              join lokacija in db.Lokacije on radnoMjesto.LokacijaId equals lokacija.Id
                                                              join zahtjevOtkazivanje in db.Zahtjevi on rezervacije.ZahtjevId equals zahtjevOtkazivanje.Id
                                                              join tipZahtjeva in db.TipoviZahtjeva on zahtjevOtkazivanje.TipZahtjevaId equals tipZahtjeva.Id
                                                              where rezervacije.Id == zah.Id 
                                                              && zahtjevOtkazivanje.TipZahtjevaId == 2
                                                              select lokacija.Id).FirstOrDefault();
                        var zahjtevZaOtkazivanjeDjelatnikID = (from rezervacije in db.RezervacijeOtkazivanje
                                                               join radnoMjesto in db.RadnaMjesta on rezervacije.RadnoMjestoId equals radnoMjesto.Id
                                                               join lokacija in db.Lokacije on radnoMjesto.LokacijaId equals lokacija.Id
                                                               join zahtjevOtkazivanje in db.Zahtjevi on rezervacije.ZahtjevId equals zahtjevOtkazivanje.Id
                                                               join tipZahtjeva in db.TipoviZahtjeva on zahtjevOtkazivanje.TipZahtjevaId equals tipZahtjeva.Id
                                                               where rezervacije.Id == zah.Id 
                                                               && zahtjevOtkazivanje.TipZahtjevaId == 2
                                                               select zahtjevOtkazivanje.DjelatnikId).FirstOrDefault();
                        if (zahtjevZaOtkazivanje == 2)
                        {
                            db.Database.ExecuteSqlInterpolated($"exec [dbo].[Otkazivanjezahtjeva] {zah.Datum:yyyy-MM-dd},{zahjtevZaOtkazivanjeLokacijaID},{zahjtevZaOtkazivanjeDjelatnikID}, { zah.ProvjeraOtkazivanjaRezerviranja}, {zah.Komentar}");
                            db.SaveChanges();
                        }
                }
                else if (zahtjev.OdgovorCheckBoxOtkazano == true)
                    {
                    var zahtjevRezOtk = db.RezervacijeOtkazivanje.Find(zahtjev.Id);
                    zahtjevRezOtk.StatusId = 2;
                    zahtjevRezOtk.Komentar = zahtjev.Komentar;
                    db.SaveChanges();
                    request.ToEmail = zahtjev.DjelatnikEmail;
                    request.Subject = "Potvrda rezervacije/otkazivanja";
                    request.Body = $" <b> <h3> Odbijen zahtjev </h3></b> <br />   Zahtjev je odbijen za dan:<b> {zahtjevRezOtk.Datum.ToShortDateString()}</b> uz komentar:{zahtjevRezOtk.Komentar}" ;
                    mailServices.SendEmailAsync(request);
                } 
                }
           

        }

        public List<SelectListItem> DohvatiStatuse()
        {
            var dohvaceneLokacije = (from status in db.Statusi
                                     select new SelectListItem
                                     {
                                         Value = status.Id.ToString(),
                                         Text = status.Tip
                                     }).ToList();
            return dohvaceneLokacije;
        }


    }
}

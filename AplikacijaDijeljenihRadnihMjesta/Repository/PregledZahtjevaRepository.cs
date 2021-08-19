using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using cloudscribe.Pagination.Models;
using System.Data;
using AplikacijaDijeljenihRadnihMjesta.Models.Paginacija;

namespace AplikacijaDijeljenihRadnihMjesta.Repository
{
    public class PregledZahtjevaRepository
    {
        public readonly AppDbContext db;
        public PregledZahtjevaRepository(AppDbContext db)
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
        public List<SelectListItem> DohvatiTipoveZahtjeva()
        {
            var dohvaceniTipoviZahjteva = (from tip in db.TipoviZahtjeva
                                           select new SelectListItem
                                           {
                                               Value = tip.Id.ToString(),
                                               Text = tip.Tip
                                           }).ToList();
           
            return dohvaceniTipoviZahjteva;
        }

        public PaginacijaZahtjev DohvatiZahtjeve(PregledZahtjevaVM model, int djelatnikID, int pageSize=1, int pageNumber=2)
        {
            var proba = new PaginacijaZahtjev();
           
                if ((model.PocetniDatum != null && model.KrajnjiDatum == null || model.PocetniDatum == null && model.KrajnjiDatum != null))
                {
                    model.povratnaInfo = "Morate označiti oba datuma!";
                }
                else
                {
                    if (model.TipZahtjeva != null && model.PocetniDatum != null && model.KrajnjiDatum != null)
                    {
                        if (DateTime.Compare((DateTime)model.PocetniDatum, (DateTime)model.KrajnjiDatum) > 0)
                        {
                            model.povratnaInfo = "Početni datum mora biti raniji od krajnjeg datuma!";
                        }
                        else
                        {
                            DohvatiZahtjeveZaPregled_Odabir(model, djelatnikID, (int)model.TipZahtjeva, (DateTime)model.PocetniDatum, (DateTime)model.KrajnjiDatum, pageSize, pageNumber);
                            var result = new PagedResult<ZahtjevVM>
                            {
                                Data = model.Zahtjevi.ToList(),
                                TotalItems = db.Zahtjevi.Where(z => z.Datum >= (DateTime)model.PocetniDatum && z.Datum <= (DateTime)model.KrajnjiDatum && z.TipZahtjevaId.Equals((int)model.TipZahtjeva)).Count(),
                                PageNumber = pageNumber,
                                PageSize = pageSize
                            };
                            proba.paginationModel = result;
                        }
                        
                    }
                    if (model.TipZahtjeva == null && model.PocetniDatum != null && model.KrajnjiDatum != null)
                    {
                        if (DateTime.Compare((DateTime)model.PocetniDatum, (DateTime)model.KrajnjiDatum) > 0)
                        {
                            model.povratnaInfo = "Početni datum mora biti raniji od krajnjeg datuma!";
                        }
                        else
                        {
                            DohvatiZahtjeveZaPregled(model, djelatnikID, (DateTime)model.PocetniDatum, (DateTime)model.KrajnjiDatum, pageSize, pageNumber);
                            var result = new PagedResult<ZahtjevVM>
                            {
                                Data = model.Zahtjevi.ToList(),
                                TotalItems = db.Zahtjevi.Where(z => z.Datum >= (DateTime)model.PocetniDatum && z.Datum <= (DateTime)model.KrajnjiDatum).Count(),
                                PageNumber = pageNumber,
                                PageSize = pageSize
                            };
                            proba.paginationModel = result;
                        }
                    
                    }
                    if (model.TipZahtjeva != null && model.PocetniDatum == null && model.KrajnjiDatum == null)
                    {
                        DohvatiZahtjeveZaPregled_BezDatuma(model, djelatnikID, (int)model.TipZahtjeva, pageSize, pageNumber);
                        var result = new PagedResult<ZahtjevVM>
                        {
                            Data = model.Zahtjevi.ToList(),
                            TotalItems = db.Zahtjevi.Where(t => t.TipZahtjevaId.Equals((int)model.TipZahtjeva)).Count(),
                            PageNumber = pageNumber,
                            PageSize = pageSize
                        };
                        proba.paginationModel = result;
                    }
                    if (model.TipZahtjeva == null && model.PocetniDatum == null && model.KrajnjiDatum == null)
                    {
                        DohvatiZahtjeveZaPregled(model, djelatnikID, pageSize, pageNumber);
                        var result = new PagedResult<ZahtjevVM>
                        {
                            Data = model.Zahtjevi.ToList(),
                            TotalItems = db.Zahtjevi.Count(),
                            PageNumber = pageNumber,
                            PageSize = pageSize
                        };
                        proba.paginationModel = result;

                    }


                    proba.pregledZahtjeva = model;

                
                
                }
            
            if (model.povratnaInfo != null)
            {
                var zahtjevi = new PregledZahtjevaVM();
                zahtjevi.povratnaInfo = model.povratnaInfo;
                proba.pregledZahtjeva = zahtjevi;
                DohvatiZahtjeveZaPregled(model, djelatnikID, pageSize, pageNumber);
                var result = new PagedResult<ZahtjevVM>();
                
                proba.paginationModel = result;
            }
            return proba;
        }

        public void DohvatiZahtjeveZaPregled_Odabir(PregledZahtjevaVM model,int djelatnikID, int zahtjevID, DateTime pocetniDatum, DateTime krajnjiDatum, int pageSize, int pageNumber)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            model.Zahtjevi = (from zahtjevi in db.Zahtjevi
                              join djelatnik in db.Djelatnici on zahtjevi.DjelatnikId equals djelatnik.Id
                                 join tip in db.TipoviZahtjeva on zahtjevi.TipZahtjevaId equals tip.Id
                                 where zahtjevi.Datum.Date >= pocetniDatum.Date && zahtjevi.Datum <= krajnjiDatum.Date.AddDays(1) && zahtjevi.DjelatnikId.Equals(djelatnikID)
                                 && zahtjevi.TipZahtjevaId== zahtjevID
                                 select new ZahtjevVM
                                 {
                                     ZahtjevID = zahtjevi.Id,
                                     ZahtjevDatum = zahtjevi.Datum,
                                     DjelatnikID = zahtjevi.DjelatnikId,
                                     Tip = zahtjevi.TipZahtjeva.Tip
                                 }).OrderByDescending(x => x.ZahtjevDatum).Skip(ExcludeRecords).Take(pageSize).ToList();
        }
        public void DohvatiZahtjeveZaPregled_BezDatuma(PregledZahtjevaVM model, int djelatnikID, int tipzahtjevID, int pageSize, int pageNumber)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            model.Zahtjevi = (from zahtjevi in db.Zahtjevi
                              join djelatnik in db.Djelatnici on zahtjevi.DjelatnikId equals djelatnik.Id
                              join tip in db.TipoviZahtjeva on zahtjevi.TipZahtjevaId equals tip.Id
                              where zahtjevi.DjelatnikId.Equals(djelatnikID)
                              && zahtjevi.TipZahtjevaId.Equals(tipzahtjevID)
                              select new ZahtjevVM
                              {
                                  ZahtjevID = zahtjevi.Id,
                                  ZahtjevDatum = zahtjevi.Datum,
                                  DjelatnikID = zahtjevi.DjelatnikId,
                                  Tip = zahtjevi.TipZahtjeva.Tip.ToString()
                              }).OrderByDescending(x => x.ZahtjevDatum).Skip(ExcludeRecords).Take(pageSize).ToList();
        }
        public void DohvatiZahtjeveZaPregled(PregledZahtjevaVM model, int djelatnikID, int pageSize, int pageNumber)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            model.Zahtjevi = (from zahtjevi in db.Zahtjevi
                              join djelatnik in db.Djelatnici on zahtjevi.DjelatnikId equals djelatnik.Id
                              join tip in db.TipoviZahtjeva on zahtjevi.TipZahtjevaId equals tip.Id
                              where zahtjevi.DjelatnikId.Equals(djelatnikID)
                              select new ZahtjevVM
                              {
                                  ZahtjevID = zahtjevi.Id,
                                  ZahtjevDatum = zahtjevi.Datum,
                                  DjelatnikID = zahtjevi.DjelatnikId,
                                  Tip = zahtjevi.TipZahtjeva.Tip.ToString()
                              }).OrderByDescending(x => x.ZahtjevDatum).Skip(ExcludeRecords).Take(pageSize).ToList();
        }

        public void DohvatiZahtjeveZaPregled(PregledZahtjevaVM model, int djelatnikID, DateTime pocetniDatum, DateTime krajnjiDatum, int pageSize, int pageNumber)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            model.Zahtjevi = (from zahtjevi in db.Zahtjevi

                              join djelatnik in db.Djelatnici on zahtjevi.DjelatnikId equals djelatnik.Id
                              join tip in db.TipoviZahtjeva on zahtjevi.TipZahtjevaId equals tip.Id
                              where zahtjevi.Datum.Date >= pocetniDatum.Date && zahtjevi.Datum <= krajnjiDatum.Date.AddDays(1) && zahtjevi.DjelatnikId.Equals(djelatnikID)
                              select new ZahtjevVM
                              {
                                  ZahtjevID = zahtjevi.Id,
                                  ZahtjevDatum = zahtjevi.Datum,
                                  DjelatnikID = zahtjevi.DjelatnikId,
                                  Tip = zahtjevi.TipZahtjeva.Tip
                              }).OrderByDescending(x=>x.ZahtjevDatum).Skip(ExcludeRecords).Take(pageSize).ToList();
        }

        public DetaljiVM DohvatiDetalje(int zahtjevID)
        {
            var detalji = new DetaljiVM();
            try
            {
                if (zahtjevID != 0)
                {
                    var result = (from rezervacije in db.RezervacijeOtkazivanje
                                  join radnoMjesto in db.RadnaMjesta on rezervacije.RadnoMjestoId equals radnoMjesto.Id
                                  join tipLaptopa in db.TipoviLaptopa on radnoMjesto.TipLaptopaId equals tipLaptopa.Id
                                  join lokacija in db.Lokacije on radnoMjesto.LokacijaId equals lokacija.Id
                                  join grad in db.Gradovi on lokacija.GradId equals grad.Id
                                  join status in db.Statusi on rezervacije.StatusId equals status.Id
                                  where rezervacije.ZahtjevId == zahtjevID
                                  orderby rezervacije.Datum ascending
                                  select new DetaljiVM
                                  {
                                      ID = rezervacije.Id,
                                      Adresa = lokacija.Adresa,
                                      Datum = rezervacije.Datum,
                                      Grad = grad.Naziv,
                                      Sifra = radnoMjesto.Sifra,
                                      TipLaptopa = tipLaptopa.Model,
                                      Komentar=rezervacije.Komentar,
                                      Status=rezervacije.Status.Tip
                                      
                                  }).ToList();
                    detalji.Detalji = result;
                }
                return detalji;
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}

using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using AplikacijaDijeljenihRadnihMjesta.Models.Paginacija;
using cloudscribe.Pagination.Models;

namespace AplikacijaDijeljenihRadnihMjesta.Repository
{
    public class RadnoMjestoRepository
    {
        public readonly AppDbContext db;

        public RadnoMjestoRepository(AppDbContext db)
        {
            this.db = db;
        }

        public bool DodajNovoRadnoMjesto_LokacijaID(RadnoMjestoVM radnoMjesto)
        {
            DohvatiInicijaleGrada(radnoMjesto);
            var sifraRadnogMjesta = radnoMjesto.InicijaliGrada + "-" + "K" + radnoMjesto.Kat + "-" + "P" + radnoMjesto.Prostorija + "-" + "BR" + radnoMjesto.BrojRadnogMjesta;
            if (db.RadnaMjesta.Where(r => r.Sifra.Equals(sifraRadnogMjesta)).Select(r => r.Sifra).FirstOrDefault()== null)
            {
                db.RadnaMjesta.Add(new RadnoMjesto
                {
                    Sifra = sifraRadnogMjesta,
                    TipLaptopaId = Int32.Parse(radnoMjesto.TipLaptopa),
                    LokacijaId = radnoMjesto.LokacijaId,
                    Onemoguceno = false
                }) ; 
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DodajNovoRadnoMjesto(RadnoMjestoVM radnoMjesto)
        {
            DohvatiInicijaleGrada_BezLokacijaID(radnoMjesto);
            var sifraRadnogMjesta = radnoMjesto.InicijaliGrada + "-" + "K" + radnoMjesto.Kat + "-" + "P" + radnoMjesto.Prostorija + "-" + "BR" + radnoMjesto.BrojRadnogMjesta;
            if (db.RadnaMjesta.Where(r => r.Sifra.Equals(sifraRadnogMjesta)).Select(r => r.Sifra).FirstOrDefault() == null)
            {
                if (radnoMjesto.LokacijaId == 0)
                    radnoMjesto.LokacijaId = Int32.Parse(radnoMjesto.Lokacija);
                db.RadnaMjesta.Add(new RadnoMjesto
                {
                    Sifra = sifraRadnogMjesta,
                    TipLaptopaId = Int32.Parse(radnoMjesto.TipLaptopa),
                    LokacijaId = radnoMjesto.LokacijaId,
                    Onemoguceno = false
                });
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public RadnoMjestoVM DohvatiInicijaleGrada(RadnoMjestoVM radnoMjesto)
        {
            radnoMjesto.LokacijaId = DohvatiLokacijaID(radnoMjesto);
            radnoMjesto.InicijaliGrada = (from lokacija in db.Lokacije
                                          join grad in db.Gradovi on lokacija.GradId equals grad.Id
                                          where (lokacija.Id.Equals(radnoMjesto.LokacijaId)) 
                                          select grad.Oznaka
                                        ).FirstOrDefault().ToString();
            return radnoMjesto;
        }
        public RadnoMjestoVM DohvatiInicijaleGrada_BezLokacijaID(RadnoMjestoVM radnoMjesto)
        {
            radnoMjesto.InicijaliGrada = (from lokacija in db.Lokacije
                                          join grad in db.Gradovi on lokacija.GradId equals grad.Id
                                          where (lokacija.Id.Equals(Int32.Parse(radnoMjesto.Lokacija)))
                                          select grad.Oznaka
                                        ).FirstOrDefault().ToString();
            return radnoMjesto;
        }


        public RadnoMjesto PronadjiRadnoMjesto(string sifra)
        {

            //var id = db.RadnaMjesta.Where(s => s.Sifra.Equals(sifra)).Select(s => s.Id).FirstOrDefault();
            //var radnoMjesto = db.RadnaMjesta.Find(id).;
            var radnoMjesto = (from mjesto in db.RadnaMjesta
                               join tip in db.TipoviLaptopa on mjesto.TipLaptopaId equals tip.Id
                               join lokacija in db.Lokacije on mjesto.LokacijaId equals lokacija.Id
                               where mjesto.Sifra.Equals(sifra)
                               select mjesto ).FirstOrDefault();
            db.SaveChanges();
            return radnoMjesto;

        }
        public int IzbrisiRadnoMjesto(RadnoMjesto radnoMjesto)
        {
            db.Entry<RadnoMjesto>(radnoMjesto).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            if (radnoMjesto != null)
            {
                var lokacijaID = DohvatiLokacijuPoSifri(radnoMjesto.Sifra);
             
                    db.RadnaMjesta.Update(new RadnoMjesto { Id = radnoMjesto.Id, Sifra = radnoMjesto.Sifra, TipLaptopaId = radnoMjesto.TipLaptopaId, LokacijaId = radnoMjesto.LokacijaId, Onemoguceno = true });
                    //db.RadnaMjesta.Remove(radnoMjesto);
                    db.SaveChanges();
               
         
                    return lokacijaID;
              
            }
            return 0;

        }

        public int DohvatiLokacijuPoSifri(string sifra)
        {
            var lokacijaID = (from lokacija in db.Lokacije
                              join radnaMjesta in db.RadnaMjesta on lokacija.Id equals radnaMjesta.LokacijaId
                              where (radnaMjesta.Sifra.Equals(sifra))
                              select (lokacija.Id)).FirstOrDefault();
            return lokacijaID;
        }


        public RadnoMjesto DohvatiRadnoMjestoPoSifri(string sifra)
        {

            return db.RadnaMjesta.Where(r => r.Sifra.Equals(sifra)).FirstOrDefault();
        }

        public int DohvatiModelLaptopa(RadnoMjesto radnomjesto)
        {
            return (from radnaMjesta in db.RadnaMjesta
                         join tipLaptopa in db.TipoviLaptopa on radnaMjesta.TipLaptopaId equals tipLaptopa.Id
                         where tipLaptopa.Id.Equals(radnomjesto.TipLaptopaId)
                         select tipLaptopa.Id).FirstOrDefault();
        }

        public bool EditRadnoMjesto(RadnoMjestoVM radnoMjesto)
        {
            var sifra = DohvatiInicijaleGrada(radnoMjesto).InicijaliGrada + "-" + "K" + radnoMjesto.Kat + "-" + "P" + radnoMjesto.Prostorija + "-" + "BR" + radnoMjesto.BrojRadnogMjesta;
           
                db.RadnaMjesta.Update(new RadnoMjesto { Id = radnoMjesto.Id, Sifra = sifra, TipLaptopaId = Int32.Parse(radnoMjesto.TipLaptopa), LokacijaId = Int32.Parse(radnoMjesto.Lokacija), Onemoguceno = false });
                db.SaveChanges();
         return true;
        }

        public int DohvatiLokacijaID(RadnoMjestoVM radnoMjesto)
        {
            return db.RadnaMjesta.Where(r => r.Id.Equals(radnoMjesto.Id)).Select(r => r.LokacijaId).FirstOrDefault();
        }

        public string DohvatiAdresuLokacije(int lokacijaID)
        {
            return db.Lokacije.Where(l => l.Id.Equals(lokacijaID)).Select(l=>l.Adresa).FirstOrDefault();
        }

        public PaginacijaRadnoMjesto DohvatiListuRadnihMjestapoLokacijiDI(PaginacijaRadnoMjesto pRadnaMjesta, int lokacijaID, int pageSize, int pageNumber)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var radnaMjesta = (from radnoMjesto in db.RadnaMjesta
                              join tipLaptopa in db.TipoviLaptopa on radnoMjesto.TipLaptopaId equals tipLaptopa.Id
                              join lokacija in db.Lokacije on radnoMjesto.LokacijaId equals lokacija.Id
                              join grad in db.Gradovi on lokacija.GradId equals grad.Id
                              where radnoMjesto.LokacijaId.Equals(lokacijaID) && radnoMjesto.Onemoguceno.Equals(false)
                              select new RadnoMjestoVM ()
                              {
                                  Id = radnoMjesto.Id,
                                  Sifra = radnoMjesto.Sifra,
                                  TipLaptopa = radnoMjesto.TipLaptopa.Model,
                                  Adresa = radnoMjesto.Lokacija.Adresa,
                                  Grad=radnoMjesto.Lokacija.Grad.Naziv
                              }
                              ).Skip(ExcludeRecords).Take(pageSize).ToList();
            var tipoviLaptopa = DohvatiTipoveLaptopa();
            var radnoMjestoFilter = new RadnoMjestoFilter
            {
                ListaRadnihMjesta = radnaMjesta,
                ListaTipovaLaptopa = tipoviLaptopa
            };
            var result = new PagedResult<RadnoMjestoVM>
            {
                Data = radnaMjesta.ToList(),
                TotalItems = db.RadnaMjesta.Where(r=>r.LokacijaId.Equals(lokacijaID)).Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            pRadnaMjesta.paginationModel = result;
            pRadnaMjesta.radnoMjestoFilter = radnoMjestoFilter;
            return pRadnaMjesta;
        }

        public PaginacijaRadnoMjesto DohvatiListuRadnihMjesta_LokacijiDI_TipoviLaptopa(PaginacijaRadnoMjesto pRadnaMjesta, int lokacijaID, int tipLaptopaID, int pageSize, int pageNumber)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var radnaMjesta = (from radnoMjesto in db.RadnaMjesta
                               join tipLaptopa in db.TipoviLaptopa on radnoMjesto.TipLaptopaId equals tipLaptopa.Id
                               join lokacija in db.Lokacije on radnoMjesto.LokacijaId equals lokacija.Id
                               join grad in db.Gradovi on lokacija.GradId equals grad.Id
                               where radnoMjesto.LokacijaId.Equals(lokacijaID) && radnoMjesto.TipLaptopaId.Equals(pRadnaMjesta.radnoMjestoFilter.TipLaptopaID) && radnoMjesto.Onemoguceno.Equals(false)
                               select new RadnoMjestoVM()
                               {
                                   Id = radnoMjesto.Id,
                                   Sifra = radnoMjesto.Sifra,
                                   TipLaptopa = radnoMjesto.TipLaptopa.Model,
                                   Adresa = radnoMjesto.Lokacija.Adresa,
                                   Grad = radnoMjesto.Lokacija.Grad.Naziv
                               }
                              ).Skip(ExcludeRecords).Take(pageSize).ToList();
            var tipoviLaptopa = DohvatiTipoveLaptopa();
            var radnoMjestoFilter = new RadnoMjestoFilter
            {
                ListaRadnihMjesta = radnaMjesta,
                ListaTipovaLaptopa = tipoviLaptopa
            };
            var result = new PagedResult<RadnoMjestoVM>
            {
                Data = radnaMjesta.ToList(),
                TotalItems = db.RadnaMjesta.Where(r => r.LokacijaId.Equals(lokacijaID) && r.TipLaptopaId.Equals(pRadnaMjesta.radnoMjestoFilter.TipLaptopaID)).Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            pRadnaMjesta.paginationModel = result;
            pRadnaMjesta.radnoMjestoFilter = radnoMjestoFilter;
            return pRadnaMjesta;
        }
        public PaginacijaRadnoMjesto DohvatiListuRadnihMjestailtriranihPoTipuLaptopa(PaginacijaRadnoMjesto pRadnaMjesta, int tiplaptopaID, int pageSize, int pageNumber)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var radnaMjesta = (from radnoMjesto in db.RadnaMjesta
                               join tipLaptopa in db.TipoviLaptopa on radnoMjesto.TipLaptopaId equals tipLaptopa.Id
                               join lokacija in db.Lokacije on radnoMjesto.LokacijaId equals lokacija.Id
                               join grad in db.Gradovi on lokacija.GradId equals grad.Id
                               where radnoMjesto.TipLaptopaId.Equals(pRadnaMjesta.radnoMjestoFilter.TipLaptopaID) && radnoMjesto.Onemoguceno.Equals(false)
                               select new RadnoMjestoVM()
                               {
                                   Id = radnoMjesto.Id,
                                   Sifra = radnoMjesto.Sifra,
                                   TipLaptopa = radnoMjesto.TipLaptopa.Model,
                                   Adresa = radnoMjesto.Lokacija.Adresa,
                                   Grad = radnoMjesto.Lokacija.Grad.Naziv
                               }
                              ).Skip(ExcludeRecords).Take(pageSize).ToList();
            var tipoviLaptopa = DohvatiTipoveLaptopa();
            var radnoMjestoFilter = new RadnoMjestoFilter
            {
                ListaRadnihMjesta = radnaMjesta,
                ListaTipovaLaptopa = tipoviLaptopa
            };
            var result = new PagedResult<RadnoMjestoVM>
            {
                Data = radnaMjesta.ToList(),
                TotalItems = db.RadnaMjesta.Where(r => r.TipLaptopaId.Equals(pRadnaMjesta.radnoMjestoFilter.TipLaptopaID)).Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            pRadnaMjesta.paginationModel = result;
            pRadnaMjesta.radnoMjestoFilter = radnoMjestoFilter;
            return pRadnaMjesta;
        }

        public PaginacijaRadnoMjesto DohvatiListuRadnihMjesta(PaginacijaRadnoMjesto pRadnaMjesta, int pageSize, int pageNumber)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var radnaMjesta = (from radnoMjesto in db.RadnaMjesta
                               join tipLaptopa in db.TipoviLaptopa on radnoMjesto.TipLaptopaId equals tipLaptopa.Id
                               join lokacija in db.Lokacije on radnoMjesto.LokacijaId equals lokacija.Id
                               join grad in db.Gradovi on lokacija.GradId equals grad.Id 
                               where radnoMjesto.Onemoguceno.Equals(false)
                               select new RadnoMjestoVM()
                               {
                                   Id = radnoMjesto.Id,
                                   Sifra = radnoMjesto.Sifra,
                                   TipLaptopa = radnoMjesto.TipLaptopa.Model,
                                   Adresa = radnoMjesto.Lokacija.Adresa,
                                   Grad = radnoMjesto.Lokacija.Grad.Naziv
                               }
                              ).Skip(ExcludeRecords).Take(pageSize).ToList();
            var tipoviLaptopa = DohvatiTipoveLaptopa();
            var radnoMjestoFilter = new RadnoMjestoFilter
            {
                ListaRadnihMjesta = radnaMjesta,
                ListaTipovaLaptopa = tipoviLaptopa
            };
            var result = new PagedResult<RadnoMjestoVM>
            {
                Data = radnaMjesta.ToList(),
                TotalItems = db.RadnaMjesta.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            pRadnaMjesta.paginationModel = result;
            pRadnaMjesta.radnoMjestoFilter = radnoMjestoFilter;
            return pRadnaMjesta;
        }


        public List<SelectListItem> DohvatiTipoveLaptopa()
        {
            return db.TipoviLaptopa.Select(laptop => new SelectListItem()
            {
                Value = laptop.Id.ToString(),
                Text = laptop.Model
            }).ToList();
        }

        public List<SelectListItem> DohvatiLokacije()
        {
            var dohvaceneLokacije = (from lokacija in db.Lokacije
                                     join grad in db.Gradovi on lokacija.GradId equals grad.Id
                                     select new SelectListItem
                                     {
                                         Value = lokacija.Id.ToString(),
                                         Text = lokacija.Adresa +", "+ grad.Naziv
                                     }).ToList();
            return dohvaceneLokacije;
        }

        public List<SelectListItem> DohvatiLokacije(int lokacijaID)
        {
            var dohvaceneLokacije = (from lokacija in db.Lokacije
                                     join grad in db.Gradovi on lokacija.GradId equals grad.Id
                                     where lokacija.Id.Equals(lokacijaID)
                                     select new SelectListItem
                                     {
                                         Value = lokacija.Id.ToString(),
                                         Text = lokacija.Adresa + " " + grad.Naziv
                                         
                                     }).ToList();
            return dohvaceneLokacije;
        }

    }
}

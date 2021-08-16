using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models;
using AplikacijaDijeljenihRadnihMjesta.Models.Paginacija;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace AplikacijaDijeljenihRadnihMjesta.Repository
{
    public class DjelatnikRepository
    {
        private readonly AppDbContext db;
        public DjelatnikRepository(AppDbContext db)
        {
            this.db = db;
        }

        public bool DodajNovogDjelatnika(DjelatnikVM djelatnik)
        {
            var md5 = new MD5CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(djelatnik.Lozinka);
            var md5data = md5.ComputeHash(data);
            var hashedPassword = Convert.ToBase64String(md5data);
            var djelatnici = db.Djelatnici.Where(d => d.MBR.Equals(djelatnik.MBR));
            if (djelatnici.Count() == 0)
            {
                db.Djelatnici.Add(new Djelatnik
                {
                    Id = djelatnik.Id,
                    MBR = djelatnik.MBR,
                    Ime = djelatnik.Ime,
                    Prezime = djelatnik.Prezime,
                    Email=djelatnik.Email,
                    OrgJedinicaId = Int32.Parse(djelatnik.OrganizacijskaJedinica),
                    TipLaptopaId = Int32.Parse(djelatnik.TipLaptopa),
                    MaxBrojDanaFirma = djelatnik.MaxBrojDanaFirma,
                    KorisnickoIme = djelatnik.KorisnickoIme,
                    Lozinka = hashedPassword,
                    UlogaID = Int32.Parse(djelatnik.Uloga)
                });
                db.SaveChanges();
                return true;
            }
              return false;
        }


       


        public PaginacijaDjelatnik DohvatiListuDjelatnika(PaginacijaDjelatnik pDjelatnici, int pageSize, int pageNumber)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var djelatnici = (from djelatnik in db.Djelatnici
                              join orgJedinica in db.OrganizacijskeJedinice on djelatnik.OrgJedinicaId equals orgJedinica.Id
                              join tipLaptopa in db.TipoviLaptopa on djelatnik.TipLaptopaId equals tipLaptopa.Id
                              join uloga in db.Uloge on djelatnik.UlogaID equals uloga.Id
                              select new DjelatnikVM()
                              {
                                  Id = djelatnik.Id,
                                  MBR =djelatnik.MBR,
                                  Ime = djelatnik.Ime,
                                  Prezime = djelatnik.Prezime,
                                  Email = djelatnik.Email,
                                  OrganizacijskaJedinica = djelatnik.OrgJedinica.Naziv,
                                  TipLaptopa = djelatnik.TipLaptopa.Model,
                                  Uloga = djelatnik.Uloga.Naziv
                              }
                              ).Skip(ExcludeRecords).Take(pageSize).ToList();
            var uloge = DohvatiUlogeDjelatnika();
            var djelatnikFilter = new DjelatnikFilter
            {
                ListaDjelatnika=djelatnici,
                ListaUloga=uloge
            };
            var result = new PagedResult<DjelatnikVM>
            {
                Data = djelatnici.ToList(),
                TotalItems = db.Djelatnici.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            pDjelatnici.paginationModel = result;
            pDjelatnici.djelatnikFilter = djelatnikFilter;
            return pDjelatnici;
        }
        public PaginacijaDjelatnik DohvatiListuDjelatnikaFiltriranuPoUlozi(PaginacijaDjelatnik pDjelatnici, int ulogaID, int pageSize, int pageNumber)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var djelatnici = (from djelatnik in db.Djelatnici
                              join orgJedinica in db.OrganizacijskeJedinice on djelatnik.OrgJedinicaId equals orgJedinica.Id
                              join tipLaptopa in db.TipoviLaptopa on djelatnik.TipLaptopaId equals tipLaptopa.Id
                              join uloga in db.Uloge on djelatnik.UlogaID equals uloga.Id
                              where djelatnik.UlogaID.Equals(ulogaID)
                              select new DjelatnikVM()
                              {
                                  Id = djelatnik.Id,
                                  MBR = djelatnik.MBR,
                                  Ime = djelatnik.Ime,
                                  Prezime = djelatnik.Prezime,
                                  Email = djelatnik.Email,
                                  OrganizacijskaJedinica = djelatnik.OrgJedinica.Naziv,
                                  TipLaptopa = djelatnik.TipLaptopa.Model,
                                  Uloga = djelatnik.Uloga.Naziv
                              }
                             ).Skip(ExcludeRecords).Take(pageSize).ToList();
            var uloge = DohvatiUlogeDjelatnika();
            var djelatnikFilter = new DjelatnikFilter
            {
                ListaDjelatnika = djelatnici,
                ListaUloga = uloge
            };
            var result = new PagedResult<DjelatnikVM>
            {
                Data = djelatnici.ToList(),
                TotalItems = db.Djelatnici.Where(d => d.UlogaID.Equals(ulogaID)).Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            pDjelatnici.paginationModel = result;
            pDjelatnici.djelatnikFilter = djelatnikFilter;
            return pDjelatnici;
        }
        public PaginacijaDjelatnik DohvatiListuDjelatnikaFiltriranuPoUloziOrgJed(PaginacijaDjelatnik pDjelatnici, int orgJedID, int ulogaID, int pageSize, int pageNumber)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var djelatnici = (from djelatnik in db.Djelatnici
                              join orgJedinica in db.OrganizacijskeJedinice on djelatnik.OrgJedinicaId equals orgJedinica.Id
                              join tipLaptopa in db.TipoviLaptopa on djelatnik.TipLaptopaId equals tipLaptopa.Id
                              join uloga in db.Uloge on djelatnik.UlogaID equals uloga.Id
                              where djelatnik.UlogaID.Equals(ulogaID) && djelatnik.OrgJedinicaId.Equals(orgJedID)
                              select new DjelatnikVM()
                              {
                                  Id = djelatnik.Id,
                                  MBR = djelatnik.MBR,
                                  Ime = djelatnik.Ime,
                                  Prezime = djelatnik.Prezime,
                                  Email = djelatnik.Email,
                                  OrganizacijskaJedinica = djelatnik.OrgJedinica.Naziv,
                                  TipLaptopa = djelatnik.TipLaptopa.Model,
                                  Uloga = djelatnik.Uloga.Naziv
                              }
                             ).Skip(ExcludeRecords).Take(pageSize).ToList();
            var uloge = DohvatiUlogeDjelatnika();
            var djelatnikFilter = new DjelatnikFilter
            {
                ListaDjelatnika = djelatnici,
                ListaUloga = uloge
            };
            var result = new PagedResult<DjelatnikVM>
            {
                Data = djelatnici.ToList(),
                TotalItems = db.Djelatnici.Where(d => d.OrgJedinicaId.Equals(orgJedID) && d.UlogaID.Equals(ulogaID)).Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            pDjelatnici.paginationModel = result;
            pDjelatnici.djelatnikFilter = djelatnikFilter;
            return pDjelatnici;
        }
        public List<SelectListItem> DohvatiUlogeDjelatnika()
        {
            return db.Uloge.Select(uloga => new SelectListItem()
            {
                Value = uloga.Id.ToString(),
                Text = uloga.Naziv
            }).ToList();
        }
        public PaginacijaDjelatnik DohvatiListuDjelatnikaOrgJedID(PaginacijaDjelatnik pDjelatnici, int orgJedID, int pageSize, int pageNumber)
        {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var djelatnici = (from djelatnik in db.Djelatnici
                              join orgJedinica in db.OrganizacijskeJedinice on djelatnik.OrgJedinicaId equals orgJedinica.Id
                              join tipLaptopa in db.TipoviLaptopa on djelatnik.TipLaptopaId equals tipLaptopa.Id
                              join uloga in db.Uloge on djelatnik.UlogaID equals uloga.Id
                              where djelatnik.OrgJedinicaId.Equals(orgJedID)
                              select new DjelatnikVM()
                              {
                                  Id=djelatnik.Id,
                                  MBR = djelatnik.MBR,
                                  Ime = djelatnik.Ime,
                                  Prezime = djelatnik.Prezime,
                                  Email = djelatnik.Email,
                                  OrganizacijskaJedinica = djelatnik.OrgJedinica.Naziv,
                                  TipLaptopa = djelatnik.TipLaptopa.Model,
                                  Uloga = djelatnik.Uloga.Naziv
                              }
                              ).Skip(ExcludeRecords).Take(pageSize).ToList();
            var uloge = DohvatiUlogeDjelatnika();
            var djelatnikFilter = new DjelatnikFilter
            {
                ListaDjelatnika = djelatnici,
                ListaUloga = uloge
            };
            pDjelatnici.djelatnikFilter = djelatnikFilter;
            var result = new PagedResult<DjelatnikVM>
            {
                Data = djelatnici.ToList(),
                TotalItems = db.Djelatnici.Where(d=>d.OrgJedinicaId.Equals(orgJedID)).Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            pDjelatnici.paginationModel = result;
            pDjelatnici.djelatnikFilter = djelatnikFilter;
            return pDjelatnici;
        }

        public List<SelectListItem> PopuniListuOrgJedinica()
        {
            return db.OrganizacijskeJedinice.Select(l => new SelectListItem()
            {
                Value = l.Id.ToString(),
                Text = l.Naziv
            }).ToList();
        }

        public List<SelectListItem> PopuniListuOrgJedinica( int orgJedID)
        {
            return db.OrganizacijskeJedinice.Where(l => l.Id.Equals(orgJedID)).Select(l => new SelectListItem()
            {
                Value = l.Id.ToString(),
                Text = l.Naziv
            }).ToList();
        }


        public List<SelectListItem> PopuniListuModeliLaptopa()
        {
            return db.TipoviLaptopa.Select(l => new SelectListItem()
            {
                Value = l.Id.ToString(),
                Text = l.Model
            }).ToList();
        }

        public List<SelectListItem> PopuniListuUloga()
        {
            return  db.Uloge.Select(l => new SelectListItem()
            {
                Value = l.Id.ToString(),
                Text = l.Naziv
            }).ToList();
        }

        public bool EditDjelatnik(DjelatnikVM djelatnik) 
        {
            var md5 = new MD5CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(djelatnik.Lozinka);
            var md5data = md5.ComputeHash(data);
            var hashedPassword = Convert.ToBase64String(md5data);
            db.Djelatnici.Update(new Djelatnik { Id = djelatnik.Id, MBR=djelatnik.MBR, Ime=djelatnik.Ime, Prezime=djelatnik.Prezime, Email=djelatnik.Email, KorisnickoIme=djelatnik.KorisnickoIme, Lozinka= hashedPassword, MaxBrojDanaFirma=djelatnik.MaxBrojDanaFirma ,OrgJedinicaId= Int32.Parse(djelatnik.OrganizacijskaJedinica), TipLaptopaId= Int32.Parse(djelatnik.TipLaptopa), UlogaID= Int32.Parse(djelatnik.Uloga)});
             db.SaveChanges();
             return true;
        }

        public bool IzbrisiDjelatnika(int id)
        {
            var djelatnik = db.Djelatnici.Find(id);
            if (djelatnik != null)
            {
                db.Djelatnici.Remove(djelatnik);
                db.SaveChanges();
                return true;
               
            }
            return false;
        }

        public Djelatnik DohvatiDjelatnika(int id)
        {
            return db.Djelatnici.Find(id);
        }


      

    }
}

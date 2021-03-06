using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AplikacijaDijeljenihRadnihMjesta.Repository
{
    public class LokacijaRepository
    {
        public readonly AppDbContext db;
        public LokacijaRepository(AppDbContext db)
        {
            this.db = db;
        }

        public LokacijaFilter DohvatiListuLokacija()
        {
            var lokacije =(from lokacija in db.Lokacije
                            join grad in db.Gradovi on lokacija.GradId equals grad.Id
                            select new LokacijaVM {
                                    Id = lokacija.Id,
                                    Adresa = lokacija.Adresa,
                                    Grad = lokacija.Grad.Naziv
                            }).ToList();
            var gradovi = DohvatiGradove();

            var lokacijaFilter = new LokacijaFilter
            {
                ListaLokacija = lokacije,
                ListaGradova = gradovi
            };
            return lokacijaFilter;
        }
        public List<string> DohvatiOrgJedNaLokaciji(int lokacijaID)
        {
            var lista= (from lokOrgJed in db.LokacijaOrganizacijskaJedinicas
                                                     join org in db.OrganizacijskeJedinice on lokOrgJed.OrganizacijskeJediniceId equals org.Id
                                                     join lok in db.Lokacije on lokOrgJed.LokacijeId equals lok.Id
                                                     where lok.Id.Equals(lokacijaID)
                                                     select org.Naziv).ToList();
            return lista;
        }
        public LokacijaFilter DohvatiListuLokacija(int orgJedID, int gradID)
        {
            var list = new LokacijaVM();
            var listaa = new List<LokacijaVM>();
            var lokacije = (from lokacija in db.Lokacije
                            join grad in db.Gradovi on lokacija.GradId equals grad.Id
                            join lokOrgJed in db.LokacijaOrganizacijskaJedinicas on lokacija.Id equals lokOrgJed.LokacijeId
                            where lokOrgJed.OrganizacijskeJediniceId.Equals(orgJedID) && grad.Id.Equals(gradID)
                            select new LokacijaVM
                            {
                                Id = lokacija.Id,
                                Adresa = lokacija.Adresa,
                                Grad = lokacija.Grad.Naziv
                            }).ToList();
            var gradovi = DohvatiGradove();

            var lokacijaFilter = new LokacijaFilter
            {
                ListaLokacija = lokacije,
                ListaGradova = gradovi,
                
            };

            return lokacijaFilter;
        }
        public LokacijaFilter DohvatiListuLokacijaFiltriranuPoGradu( int gradID)
        {
            var lokacije = (from lokacija in db.Lokacije
                            join grad in db.Gradovi on lokacija.GradId equals grad.Id
                            where grad.Id.Equals(gradID)
                            select new LokacijaVM
                            {
                                Id = lokacija.Id,
                                Adresa = lokacija.Adresa,
                                Grad = lokacija.Grad.Naziv
                            }).ToList();
            var gradovi = DohvatiGradove();

            var lokacijaFilter = new LokacijaFilter
            {
                ListaLokacija = lokacije,
                ListaGradova = gradovi
            };
            return lokacijaFilter;
        }
        public LokacijaFilter DohvatiListuLokacija(int orgJedID)
        {
            var lokacije = (from lokacija in db.Lokacije
                            join grad in db.Gradovi on lokacija.GradId equals grad.Id
                            join lokOrgJed in db.LokacijaOrganizacijskaJedinicas on lokacija.Id equals lokOrgJed.LokacijeId
                            where lokOrgJed.OrganizacijskeJediniceId.Equals(orgJedID)
                            select new LokacijaVM
                            {
                                Id = lokacija.Id,
                                Adresa = lokacija.Adresa,
                                Grad = lokacija.Grad.Naziv
                            }).ToList();
            var gradovi = DohvatiGradove(orgJedID);

            var lokacijaFilter = new LokacijaFilter
            {
                ListaLokacija = lokacije,
                ListaGradova = gradovi
            };
            
            return lokacijaFilter;
        }
        public List<SelectListItem> DohvatiGradove(int orgJedID)
        {
            var gradovi= (from grad in db.Gradovi
                          join lokacija in db.Lokacije on grad.Id equals lokacija.GradId
                          join lokOrgJed in db.LokacijaOrganizacijskaJedinicas on lokacija.Id equals lokOrgJed.LokacijeId
                          where lokOrgJed.OrganizacijskeJediniceId.Equals(orgJedID) 
                          select new SelectListItem()
            {
                Value = grad.Id.ToString(),
                Text = grad.Naziv
            }).Distinct().ToList();
            return gradovi;
        }

        public bool DodajNovuLokaciju(LokacijaVM lokacija)
        {
            var provjera = (from lok in db.Lokacije
                            join grad in db.Gradovi on lok.GradId equals grad.Id
                            where lok.Adresa.Equals(lokacija.Adresa) && lok.GradId.Equals(Int32.Parse(lokacija.Grad))
                            select lok.Id).ToList();
            
            if (provjera.Count()==0)
            {
                db.Lokacije.Add(new Lokacija { Adresa = lokacija.Adresa, GradId = Int32.Parse(lokacija.Grad) });
                db.SaveChanges();
                var lokacijaId = db.Lokacije.Where(l => l.Adresa.Equals(lokacija.Adresa) && l.GradId.Equals(Int32.Parse(lokacija.Grad))).Select(l => l.Id).FirstOrDefault();
                db.LokacijaOrganizacijskaJedinicas.Add(new LokacijaOrganizacijskaJedinica { LokacijeId = lokacijaId, OrganizacijskeJediniceId = Int32.Parse(lokacija.OrgJedinica) });
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool IzbrisiLokaciju(int id)
        {
            var lokacija=db.Lokacije.Find(id);
            if (lokacija != null)
            {
                if (db.RadnaMjesta.Where(rm => rm.LokacijaId.Equals(lokacija.Id)).FirstOrDefault() != null)
                {
                    return false;
                }
                else
                {
                    var orgJedID=db.LokacijaOrganizacijskaJedinicas.Where(l => l.LokacijeId.Equals(lokacija.Id)).Select(l=>l.OrganizacijskeJediniceId).FirstOrDefault();
                    if (orgJedID != 0)
                    {
                        var orgJedinicadb = db.LokacijaOrganizacijskaJedinicas.Find(lokacija.Id, orgJedID);
                        db.LokacijaOrganizacijskaJedinicas.Remove(orgJedinicadb);
                    }
                    db.Lokacije.Remove(lokacija);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool IzbrisiLokacijuUnutarOrgJedinice(int lokacijaID, int orgJedID)
        {
            var lokOrgJedID = db.LokacijaOrganizacijskaJedinicas.Where(l => l.LokacijeId.Equals(lokacijaID) && l.OrganizacijskeJediniceId.Equals(orgJedID)).FirstOrDefault();
            if (lokOrgJedID != null)
            {
                db.LokacijaOrganizacijskaJedinicas.Remove(lokOrgJedID);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public Lokacija DohvatiLokacijuPoId(int? Id) 
        {
            return db.Lokacije.Find(Id);
        }

        public string DohvatiNazivLokacije(int? Id)
        {
            var lokacija= db.Lokacije.Find(Id);
            return lokacija.Adresa;
        }
        public bool EditLokacija(LokacijaVM lokacija)
        {
            
            var provjera = (from lok in db.Lokacije
                            join grad in db.Gradovi on lok.GradId equals grad.Id
                            join lokOrgJed in db.LokacijaOrganizacijskaJedinicas on lok.Id equals lokOrgJed.LokacijeId
                            join orgJed in db.OrganizacijskeJedinice on lokOrgJed.OrganizacijskeJediniceId equals orgJed.Id
                            where lok.Adresa.Equals(lokacija.Adresa) && lokacija.Grad.Equals(grad.Naziv) && lokacija.OrgJedinica.Equals(orgJed.Id.ToString())
                            select (lok.Adresa)).ToList().Count();
            if (provjera == 0)
            {
                db.Lokacije.Update(new Lokacija { Id = lokacija.Id, Adresa = lokacija.Adresa, GradId = Int32.Parse(lokacija.Grad) });
                if (db.LokacijaOrganizacijskaJedinicas.Where(l => l.LokacijeId.Equals(lokacija.Id) && l.OrganizacijskeJediniceId.Equals(DohvatiIDOrgJedinice(lokacija.OrgJedinica))).Count() == 0)
                {
                    db.LokacijaOrganizacijskaJedinicas.Add(new LokacijaOrganizacijskaJedinica { LokacijeId = lokacija.Id, OrganizacijskeJediniceId = Int32.Parse(lokacija.OrgJedinica) });
                }
                else {
                    lokacija.povratnaInfo = $"Lokacija je već dio te organizacijske jedinice! Pogledajte pod postojeće organizacijske jedinice.";
                }
                
                db.SaveChanges();
                return true;
        }
            return false;
        }

        public LokacijaVM DohvatiPripadajuceOrgJedinice(LokacijaVM lokacija)
        {
            List<string> listaOrgJedinica = (from orgJed in db.OrganizacijskeJedinice
                                             join lokOrgJed in db.LokacijaOrganizacijskaJedinicas on orgJed.Id equals lokOrgJed.OrganizacijskeJediniceId
                                             where lokOrgJed.LokacijeId.Equals(lokacija.Id)
                                             select orgJed.Naziv).ToList();
            if (listaOrgJedinica != null)
            {
                foreach (var lok in listaOrgJedinica)
                {
                    lokacija.ListaPripadajucihOrganizacijskihJedinica+=lok;
                    lokacija.ListaPripadajucihOrganizacijskihJedinica += ", ";
                }
            }
           
            return lokacija;
        }

        public string DohvatiNazivGrada(Lokacija lokacija)
        {
            var naziv= (from lok in db.Lokacije
                        join grad in db.Gradovi on lok.GradId equals grad.Id
                        where grad.Id.Equals(lokacija.GradId)
                        select grad.Naziv).FirstOrDefault();
            return naziv;
        }
        public string DohvatiNazivOrgJedinice( int OrgJedID)
        {
            return (from orgJed in db.OrganizacijskeJedinice
                         where orgJed.Id.Equals(OrgJedID)
                         select orgJed.Naziv).FirstOrDefault();
        }

        public int DohvatiIDOrgJedinice(string naziv)
        {
            return (from orgJed in db.OrganizacijskeJedinice
                    where orgJed.Naziv.Equals(naziv)
                    select orgJed.Id).FirstOrDefault();
        }


        public string DohvatiNazivOrgJedinicePoID(int lokacijaID)
        {
            return (from lok in db.Lokacije
                    join lokOrgJed in db.LokacijaOrganizacijskaJedinicas on lok.Id equals lokOrgJed.LokacijeId
                    join orgJed in db.OrganizacijskeJedinice on lokOrgJed.OrganizacijskeJediniceId equals orgJed.Id
                    where lok.Id.Equals(lokacijaID)
                    select orgJed.Naziv).FirstOrDefault();
        }

        public int DohvatiIdGrada(LokacijaVM lokacija)
        {
            var gradID = db.Gradovi.Where(g => g.Naziv.Equals(lokacija.Grad)).Select(g => g.Id).FirstOrDefault();
            return gradID;
        }

        public LokacijaVM DohvatiGradove(LokacijaVM lokacija)
        {
            lokacija.Gradovi= db.Gradovi.Select(l => new SelectListItem()
            {
                Value = l.Id.ToString(),
                Text = l.Naziv
            }).ToList();
            return lokacija;
        }
        public List<SelectListItem> DohvatiGradove()
        {
            return db.Gradovi.Select(l => new SelectListItem()
            {
                Value = l.Id.ToString(),
                Text = l.Naziv
            }).ToList();
        }

        public LokacijaVM DohvatiOrgJedinice(LokacijaVM lokacija)
        {
             lokacija.OrgJedinice=db.OrganizacijskeJedinice.Select(l => new SelectListItem()
            {
                Value = l.Id.ToString(),
                Text = l.Naziv
            }).ToList();
            return lokacija;
        }
        public List<SelectListItem> DohvatiOrgJedinice()
        {
            return db.OrganizacijskeJedinice.Select(l => new SelectListItem()
            {
                Value = l.Id.ToString(),
                Text = l.Naziv
            }).ToList();
        }
        public List<SelectListItem> DohvatiOrgJedinice(int orgJedID)
        {
            return db.OrganizacijskeJedinice.Where(o=>o.Id.Equals(orgJedID)).Select(l => new SelectListItem()
            {
                Value = l.Id.ToString(),
                Text = l.Naziv
            }).Distinct().ToList();
           
        }

     
        public LokacijaVM DohvatiOrgJedinice(LokacijaVM lokacija, int orgJedID)
        {
            lokacija.OrgJedinice = db.OrganizacijskeJedinice.Where(o => o.Id.Equals(orgJedID)).Select(l => new SelectListItem()
            {
                Value = l.Id.ToString(),
                Text = l.Naziv
            }).Distinct().ToList();
            return lokacija;
        }

        public LokacijaVM DohvatiGradove(LokacijaVM lokacija, int orgJedID)
        {
            lokacija.Gradovi = (from grad in db.Gradovi
                                join lok in db.Lokacije on grad.Id equals lok.GradId
                                join orgJed in db.LokacijaOrganizacijskaJedinicas on lok.Id equals orgJed.LokacijeId
                                where(orgJed.OrganizacijskeJediniceId.Equals(orgJedID))
                                select new SelectListItem()
            {
                Value = grad.Id.ToString(),
                Text = grad.Naziv
            }).Distinct().ToList();
            return lokacija;
        }

    }
}

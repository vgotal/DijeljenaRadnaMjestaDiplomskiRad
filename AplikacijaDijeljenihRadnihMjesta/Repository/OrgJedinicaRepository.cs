using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Repository
{
    public class OrgJedinicaRepository
    {
        public readonly AppDbContext db;
        public OrgJedinicaRepository(AppDbContext db)
        {
            this.db = db;
        }

        public List<OrgJedinicaVM> DohvatiPopisOrgJedinica()
        {
            List<OrgJedinicaVM> orgJedinice = new List<OrgJedinicaVM>();
            var sveOrgJedinice = db.OrganizacijskeJedinice;
            foreach (var jedinica in sveOrgJedinice)
            {
                orgJedinice.Add(new OrgJedinicaVM { Id = jedinica.Id, Naziv = jedinica.Naziv, BrojDjelatnika = 3, BrojLokacija = 3 });
            }
            return orgJedinice;
        }

        public List<Djelatnik> DohvatiBrojDjelatnika(int orgJedID)
        {
            var brojDjelatnika = db.Djelatnici.Where(d => d.OrgJedinicaId.Equals(orgJedID)).ToList();
            return brojDjelatnika;
        }
        public List<LokacijaOrganizacijskaJedinica> DohvatiBrojLokacija(int orgJedID)
        {
            var listaLokacija = db.LokacijaOrganizacijskaJedinicas.Where(l => l.OrganizacijskeJediniceId.Equals(orgJedID)).ToList();
            return listaLokacija;
        }

        public bool DodajNovuOrgJedinicu(OrgJedinicaVM orgJedinica)
        {
            if (db.OrganizacijskeJedinice.Select(g => g.Naziv).Where(g => g.ToLower().Equals(orgJedinica.Naziv.ToLower())).FirstOrDefault() == null)
            {
                db.OrganizacijskeJedinice.Add(new OrganizacijskaJedinica { Naziv = orgJedinica.Naziv });
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool EditOrgJedinicu(OrgJedinicaVM orgJedinica, int orgJedID)
        {
            db.OrganizacijskeJedinice.Update(new OrganizacijskaJedinica { Id = orgJedID, Naziv = orgJedinica.Naziv });
            db.SaveChanges();
            return true;
        }
        public OrganizacijskaJedinica DohvatiOrgJedinicuPoID(int id)
        {
            return db.OrganizacijskeJedinice.Find(id);
        }

        public bool IzbrisiOrgJedinicu(int id)
        {
            var orgJedinica = db.OrganizacijskeJedinice.Find(id);
            if (orgJedinica != null)
            {
                var provjera = (from djelatnik in db.Djelatnici
                                join orgJed in db.OrganizacijskeJedinice on djelatnik.OrgJedinicaId equals orgJed.Id
                                where djelatnik.OrgJedinicaId.Equals(id)
                                select djelatnik.Id).ToList();

                if (provjera.Count() == 0)
                {
                    db.OrganizacijskeJedinice.Remove(orgJedinica);
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        public LokacijaORgJedinicaVM DohvatiListuLokacija(int orgJedID)
        {
            var orgJedLok = new LokacijaORgJedinicaVM();
            var listaLokacijaUOrgJed = (from lok in db.Lokacije
                          join grad in db.Gradovi on lok.GradId equals grad.Id
                          join lokOrgJed in db.LokacijaOrganizacijskaJedinicas on lok.Id equals lokOrgJed.LokacijeId
                         where lokOrgJed.OrganizacijskeJediniceId ==orgJedID
                          select new SelectListItem()
                          {
                              Value = lok.Id.ToString(),
                              Text = lok.Adresa + ", " + grad.Naziv
                          }).Distinct().ToList();

            orgJedLok.Adrese = (from lok in db.Lokacije
                           join grad in db.Gradovi on lok.GradId equals grad.Id
                           select new SelectListItem()
                           {
                               Value = lok.Id.ToString(),
                               Text = lok.Adresa + ", " + grad.Naziv
                           }).Distinct().ToList();
           
            foreach (var a in listaLokacijaUOrgJed)
            {
                orgJedLok.Adrese.RemoveAll(adresa=>adresa.Value==a.Value);
            }

            return orgJedLok;
        }

        

        public LokacijaVM DohvatiOrgJedinice(LokacijaVM lokacija)
        {
            lokacija.OrgJedinice = db.OrganizacijskeJedinice.Select(l => new SelectListItem()
            {
                Value = l.Id.ToString(),
                Text = l.Naziv
            }).ToList();
            return lokacija;
        }

        public bool DodajNovuLokacijuUOrgJedinicu(LokacijaORgJedinicaVM lokORgJed, int orgJedID)
        {
            if (db.LokacijaOrganizacijskaJedinicas.Where(l=>l.LokacijeId.Equals(Int32.Parse(lokORgJed.Adresa)) && l.OrganizacijskeJediniceId.Equals(orgJedID)).ToList().Count() == 0)
            {
                db.LokacijaOrganizacijskaJedinicas.Add(new LokacijaOrganizacijskaJedinica { LokacijeId= Int32.Parse(lokORgJed.Adresa), OrganizacijskeJediniceId=orgJedID });
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

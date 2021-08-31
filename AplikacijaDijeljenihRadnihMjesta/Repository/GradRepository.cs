using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using AplikacijaDijeljenihRadnihMjesta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Repository
{
    public class GradRepository
    {
        public readonly AppDbContext db;
        public GradRepository(AppDbContext db)
        {
            this.db = db;
        }

        public bool DodajNoviGrad(GradVM grad)
        {
            if(db.Gradovi.Select(g=>g.Naziv).Where(g=>g.ToLower().Equals(grad.Naziv.ToLower())).FirstOrDefault()==null)
            {
                db.Gradovi.Add(new Grad { Naziv = grad.Naziv, Oznaka = grad.Oznaka });
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<GradVM> DohvatiListuGradova()
        {
            List<GradVM> gradovi = new List<GradVM>();
            var sviGradovi = db.Gradovi;
            foreach (var grad in sviGradovi) 
            {
                gradovi.Add(new GradVM { Id=grad.Id, Naziv=grad.Naziv, Oznaka=grad.Oznaka });
            }
            return gradovi;
        }

        public bool IzbrisiGrad(int id)
        {
            var grad = db.Gradovi.Find(id);
            if (grad != null)
            {
                if (db.Lokacije.Where(l => l.GradId.Equals(id)).FirstOrDefault() == null)
                {
                    db.Gradovi.Remove(grad);
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        public Grad DohvatiGradPoId(int id)
        {
            return db.Gradovi.Find(id);
        }


        public bool EditGrad(GradVM grad, int gradID)
        {
            if (db.Gradovi.Where(g => g.Naziv.Equals(grad.Naziv) ).Count() == 0)
            {
                db.Gradovi.Update(new Grad { Id = gradID, Naziv = grad.Naziv, Oznaka = grad.Oznaka });
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}

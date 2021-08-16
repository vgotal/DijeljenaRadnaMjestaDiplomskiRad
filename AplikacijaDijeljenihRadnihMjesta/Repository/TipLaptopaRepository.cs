using AplikacijaDijeljenihRadnihMjesta.Data;
using AplikacijaDijeljenihRadnihMjesta.Models;
using AplikacijaDijeljenihRadnihMjesta.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacijaDijeljenihRadnihMjesta.Repository
{
    public class TipLaptopaRepository
    {
        public readonly AppDbContext db;
        public TipLaptopaRepository(AppDbContext db)
        {
            this.db = db;
        }

        public bool DodajNoviTipLaptopa(TipLaptopaVM tipLaptopa)
        {
            if (db.TipoviLaptopa.Select(l => l.Model).Where(l => l.ToLower().Equals(tipLaptopa.Model.ToLower())).FirstOrDefault() == null)
            {
                db.TipoviLaptopa.Add(new TipLaptopa { Model = tipLaptopa.Model }); //nema smisla da u VM držim ID jer mi ne treba 
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<TipLaptopaVM> DohvatiListuTipovaLaptopa()
        {
            List<TipLaptopaVM> tipoviLaptopa = new List<TipLaptopaVM>();
            var laptopi = db.TipoviLaptopa;
            foreach (var tipLaptopa in laptopi) //napravi novu metodu koja ce ti to raditi
            {
                tipoviLaptopa.Add(new TipLaptopaVM { Id = tipLaptopa.Id, Model = tipLaptopa.Model });
            }
            return tipoviLaptopa;
        }

        public void IzbrisiTipLaptopa(int Id)
        {
                var tip = db.TipoviLaptopa.Find(Id);
                if (tip != null)
                {
                   db.TipoviLaptopa.Remove(tip);
                   db.SaveChanges();
                 }
        }

        public TipLaptopa DohvatiTipLaptopaPoId(int? Id)
        {
            return db.TipoviLaptopa.Find(Id);
        }

        public bool EditTipLaptopa(TipLaptopaVM tipLaptopa)
        {
                db.TipoviLaptopa.Update(new TipLaptopa { Id = tipLaptopa.Id, Model = tipLaptopa.Model });
                db.SaveChanges();
                return true; 
        }



    }
}

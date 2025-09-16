using AAD.ImmoWin.Business.Classes;
using AAD.ImmoWin.Business.Interfaces;
using AAD.ImmoWin.WPF.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.WPF.Repositories
{
    static class KlantRepository
    {
        static ObservableCollection<Klant> klanten = new ObservableCollection<Klant>();

        public static void Add(Klant klant)
        {
            using (ImmoWinDBContext context = new ImmoWinDBContext())
            {
                context.Klanten.Add(klant);
                context.SaveChanges();
            }
            klanten.Add(klant);
        }

        public static void Remove(Klant klant)
        {
            using (ImmoWinDBContext context = new ImmoWinDBContext())
            {
                context.Klanten.Remove(klant);
                context.SaveChanges();
            }
            klanten.Remove(klant);
        }

        public static void Update(Klant klant)
        {
            using (ImmoWinDBContext context = new ImmoWinDBContext())
            {
                Klant? toUpdate = context.Klanten.Include(k => k.Adres).FirstOrDefault(k => k.Id == klant.Id);

                if (toUpdate == null)
                {
                    throw new ArgumentException("Klant not found.");
                }
                toUpdate.Familienaam = klant.Familienaam;
                toUpdate.Voornaam = klant.Voornaam;
                toUpdate.Adres.Straat = klant.Adres.Straat;
                toUpdate.Adres.Huisnummer = klant.Adres.Huisnummer;
                toUpdate.Adres.Postnummer = klant.Adres.Postnummer;
                toUpdate.Adres.Gemeente = klant.Adres.Gemeente;
                context.SaveChanges();
            }
        }

        public static ObservableCollection<Klant> GetAll()
        {
            using (ImmoWinDBContext context = new ImmoWinDBContext())
            {
                List<Klant> klantenLijst = context.Klanten.Include(k => k.Adres).Include(k => k.Woningen).ThenInclude(g => g.Adres).ToList();
                klanten = new ObservableCollection<Klant>(klantenLijst);
                return klanten;
            }
        }
    }
}

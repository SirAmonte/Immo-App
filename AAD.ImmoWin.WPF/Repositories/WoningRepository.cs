using AAD.ImmoWin.Business.Classes;
using AAD.ImmoWin.WPF.DBContext;
using AAD.ImmoWin.WPF.ViewModels.Woningen;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.WPF.Repositories
{
    public class WoningRepository
    {
        static ObservableCollection<Woning> woningen = new ObservableCollection<Woning>();

        public static void Add(Woning woning)
        {
            using (ImmoWinDBContext context = new ImmoWinDBContext())
            {
                context.Woningen.Add(woning);
                context.SaveChanges();
            }
            woningen.Add(woning);

        }

        public static void Update(Woning woning)
        {
            using (ImmoWinDBContext context = new ImmoWinDBContext())
            {
                Woning? toUpdate = context.Woningen.Include(w => w.Adres).FirstOrDefault(w => w.Id == woning.Id);

                if (toUpdate == null)
                {
                    throw new ArgumentException("Woning not found.");
                }
                if (toUpdate.Adres != null)
                {
                    toUpdate.Adres.Straat = woning.Adres.Straat;
                    toUpdate.Adres.Huisnummer = woning.Adres.Huisnummer;
                    toUpdate.Adres.Postnummer = woning.Adres.Postnummer;
                    toUpdate.Adres.Gemeente = woning.Adres.Gemeente;
                }
                toUpdate.KlantId = woning.KlantId;
                if (toUpdate is Huis toUpdateHuis && woning is Huis woningHuis)
                {
                    toUpdateHuis.Type = woningHuis.Type;
                }
                if (toUpdate is Appartement toUpdateApp && woning is Appartement woningApp)
                {
                    toUpdateApp.Verdieping = woningApp.Verdieping;
                }
                toUpdate.Waarde = woning.Waarde;
                toUpdate.Bouwdatum = woning.Bouwdatum;
                context.SaveChanges();
            }
        }

        public static void Remove(Woning woning)
        {
            using (ImmoWinDBContext context = new ImmoWinDBContext())
            {
                context.Woningen.Remove(woning);
                context.SaveChanges();
            }
            woningen.Remove(woning);
        }

        public static ObservableCollection<Woning> GetAll()
        {
            using (ImmoWinDBContext context = new ImmoWinDBContext())
            {
                List<Woning> woningenLijst = context.Woningen.Include(w => w.Adres).ToList();
                woningen = new ObservableCollection<Woning>(woningenLijst);
                return woningen;
            }
        }
    }
}

using AAD.ImmoWin.Business.Classes;
using AAD.ImmoWin.Business.Exceptions;
using AAD.ImmoWin.Business.Interfaces;
using System.Numerics;

namespace AAD.ImmoWin.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                IKlant teoman = new Klant("", "");
            }
            catch (FamilieNaamIsNulOfLeegException ex) {
                Console.WriteLine(ex.Message);
            }

           
            IKlant hayk = new Klant("Hayk", "Amalikyan");
            IAdres adres1 = new Adres("Stationsstraat", "10", 1000, "Brussel");
            IHuis huis1 = new Huis(adres1, 300000, HuisType.rijhuis, new DateOnly(1990, 5, 1));
            hayk.VoegWoningToe(huis1);

            
            IKlant duwé = new Klant("Duwe", "Matthias");
            IAdres adres2 = new Adres("Kerkstraat", "15B", 2000, "Antwerpen");
            IAppartement appartement1 = new Appartement(adres2, 200000, new DateOnly(2005, 8, 20), 2);
            duwé.VoegWoningToe(appartement1);

            
            IKlant pascal = new Klant("Pascal", "Smet");
            IAdres adres3 = new Adres("Dorpstraat", "20", 3000, "Leuven");
            IHuis huis2 = new Huis(adres3, 350000, HuisType.driegevel, new DateOnly(1985, 3, 15));
            IAdres adres4 = new Adres("Hoofdstraat", "30A", 1000, "Brussel");
            IAppartement appartement2 = new Appartement(adres4, 220000, new DateOnly(2010, 11, 5), 3);
            pascal.VoegWoningToe(huis2);
            pascal.VoegWoningToe(appartement2);

            
            IKlant mike = new Klant("Mike", "Ross");
            IAdres adres5 = new Adres("Rivierstraat", "8", 5000, "Namen");
            IHuis huis3 = new Huis(adres5, 400000, HuisType.alleenstaand, new DateOnly(2000, 6, 10));
            IAdres adres6 = new Adres("Bergstraat", "12", 7000, "Mons");
            IHuis huis4 = new Huis(adres6, 320000, HuisType.rijhuis, new DateOnly(1995, 9, 25));

            IAdres adres7 = new Adres("Parklaan", "5C", 3000, "Leuven");
            IAppartement appartement3 = new Appartement(adres7, 180000, new DateOnly(2012, 7, 13), 1);

            IAdres adres8 = new Adres("Zeeweg", "23A", 8400, "Oostende");
            IAppartement appartement4 = new Appartement(adres8, 210000, new DateOnly(2018, 4, 2), 5);

            IAdres adres9 = new Adres("Mechelsesteenweg", "14", 2800, "Mechelen");
            IAppartement appartement5 = new Appartement(adres9, 190000, new DateOnly(2015, 2, 17), 2);

            IAdres adres10 = new Adres("Vrijheidslaan", "45", 1000, "Brussel");
            IAppartement appartement6 = new Appartement(adres10, 250000, new DateOnly(2020, 9, 30), 6);

            mike.VoegWoningToe(huis3);
            mike.VoegWoningToe(huis4);
            mike.VoegWoningToe(appartement3);
            mike.VoegWoningToe(appartement4);
            mike.VoegWoningToe(appartement5);
            mike.VoegWoningToe(appartement6);

            
            List<IKlant> klanten = new List<IKlant> {  hayk, pascal, duwé, mike };

            
            foreach (IKlant klant in klanten)
            {
                Console.WriteLine(klant.ToString() + Environment.NewLine);
            }

            Console.ReadLine();
        }
    }
}

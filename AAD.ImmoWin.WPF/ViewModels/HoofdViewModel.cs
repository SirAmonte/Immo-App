using AAD.ImmoWin.Business.Classes;
using AAD.ImmoWin.WPF.DBContext;
using AAD.ImmoWin.WPF.Repositories;
using AAD.ImmoWin.WPF.ViewModels.Klanten;
using AAD.ImmoWin.WPF.Views;
using AAD.ImmoWin.WPF.Views.Woningen;
using Odisee.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AAD.ImmoWin.WPF.ViewModels
{
    public class HoofdViewModel : BaseViewModel
    {
        private UserControl currentview;
        public KlantenModuleView KlantenModuleView { get; private set; }
        public WoningenModuleView WoningenModuleView { get; private set; }
        public UserControl CurrentView { get => currentview; set => SetProperty(ref currentview, value); }

        public RelayCommand OpenWoningenModuleCommand { get; set; }
        public RelayCommand OpenKlantenModuleCommand { get; set; }
        public RelayCommand CloseAppCommand { get; set; }
        public RelayCommand EmptyDBCommand { get; set; }
        public RelayCommand DemoDataCommand { get; set; }

        public string Titel { get; set; }

        public HoofdViewModel()
        {
            createDB();
            Titel = "WPF - MVVM";
            CurrentView = new KlantenModuleView();
            OpenWoningenModuleCommand = new RelayCommand(OpenWoningenModule);
            OpenKlantenModuleCommand = new RelayCommand(OpenKlantenModule);
            CloseAppCommand = new RelayCommand(CloseApp);
            EmptyDBCommand = new RelayCommand(EmptyDB);
            DemoDataCommand = new RelayCommand(AddDemoData);
        }

        private void createDB()
        {
            ImmoWinDBContext context = new ImmoWinDBContext();
            context.Database.EnsureCreated();

            int klantCount = context.Klanten.Count();
            if (klantCount == 0)
            {
                AddDemoData();
            }
        }

        private void EmptyDB()
        {
            ImmoWinDBContext context = new ImmoWinDBContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public static void AddDemoData()
        {
            // demo klanten
            ImmoWinDBContext context = new ImmoWinDBContext();

            if (context.Klanten.Any())
            {
                MessageBox.Show("Er is al demo data aanwezig.");
                return;
            }

            Adres adres1 = new Adres("Kerkstraat", "1", "Gent", 9000);
            Adres adres2 = new Adres("Herbergstraat", "52", "Brussel", 1800);
            Adres adres3 = new Adres("Koningstraat", "20", "Anderlecht", 1070);

            Klant Teoman = new Klant("Teoman", "Liman", adres1);
            KlantRepository.Add(Teoman);
            Klant Jeroen = new Klant("Jeroen", "DeHaan", adres2);
            KlantRepository.Add(Jeroen);
            Klant John = new Klant("John", "Doe", adres3);
            KlantRepository.Add(John);

            // demo woningen
            Adres adresHuis = new Adres("Dorpstraat", "10", "Aalst", 9300);
            Woning huis1 = new Huis(adresHuis, 250000, HuisType.driegevel, DateOnly.FromDateTime(new DateTime(1990, 5, 1)), Teoman.Id);
            Teoman.VoegWoningToe(huis1);
            WoningRepository.Add(huis1);

            Adres adresApp = new Adres("Lindestraat", "15", "Antwerpen", 2000);
            Woning appartement1 = new Appartement(adresApp, 180000, DateOnly.FromDateTime(new DateTime(2005, 10, 15)), 2, Jeroen.Id);
            Jeroen.VoegWoningToe(appartement1);
            WoningRepository.Add(appartement1);

            Adres adresHuis2 = new Adres("Kasteelstraat", "23", "Mechelen", 2800);
            Woning huis2 = new Huis(adresHuis2, 350000, HuisType.alleenstaand, DateOnly.FromDateTime(new DateTime(2000, 8, 10)), John.Id);
            John.VoegWoningToe(huis2);
            WoningRepository.Add(huis2);

            context.SaveChanges();
        }

        public void CloseApp()
        {
            Application.Current.Shutdown();
        }

        public void OpenWoningenModule()
        {
            CurrentView = new WoningenModuleView();
        }
        public void OpenKlantenModule()
        {
            CurrentView = new KlantenModuleView();
        }
    }
}

using AAD.ImmoWin.Business.Classes;
using AAD.ImmoWin.Business.Interfaces;
using AAD.ImmoWin.WPF.Repositories;
using AAD.ImmoWin.WPF.Views;
using AAD.ImmoWin.WPF.Views.Woningen;
using AAD.ImmoWin.WPF.ViewState;
using Odisee.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.WPF.ViewModels.Woningen
{
    public class WoningDetailCommandViewModel : BaseViewModel
    {
        private string straat;
        private string gemeente;
        private string huisnummer;
        private int postnummer;
        private int verdieping;
        private Adres adres;
        private Huis huis;
        private Appartement appartement;
        private DateTime bouwdatum;
        private double waarde;
        private bool isEnabled;
        private WoningDetailViewState? status;
        private Woning selectedWoning;
        private Klant selectedKlant;
        private HuisType selectedType;
        private bool ishuis;
        private bool isappartement;
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public ObservableCollection<Klant> Klanten { get; set; }

        public ObservableCollection<HuisType> HuisTypen { get; set; }

        public List<Woning> Woningen { get; set; }

        public HuisType SelectedType { get => selectedType; set => SetProperty(ref selectedType, value); }

        public Klant SelectedKlant { get => selectedKlant; set => SetProperty(ref selectedKlant, value); }
        public string Gemeente { get => gemeente; set => SetProperty(ref gemeente, value); }
        public string Straat { get => straat; set => SetProperty(ref straat, value); }
        public string Huisnummer { get => huisnummer; set => SetProperty(ref huisnummer, value); }
        public int Postnummer { get => postnummer; set => SetProperty(ref postnummer, value); }
        public Adres Adres { get => adres; set => SetProperty(ref adres, value); }
        public Appartement Appartement { get => appartement; set => SetProperty(ref appartement, value); }
        public bool IsAppartement { get => isappartement; set => SetProperty(ref isappartement, value); }
        public Huis Huis { get => huis; set => SetProperty(ref huis, value); }
        public bool IsHuis { get => ishuis; set => SetProperty(ref ishuis, value); }
        public int Verdieping { get => verdieping; set => SetProperty(ref verdieping, value); }
        public DateTime Bouwdatum { get => bouwdatum; set => SetProperty(ref bouwdatum, value); }
        public double Waarde { get => waarde; set => SetProperty(ref waarde, value); }

        public bool IsEnabled { get => isEnabled; set => SetProperty(ref isEnabled, value); }

        public WoningDetailViewState? Status { get => status; set => SetProperty(ref status, value); }

        public Woning SelectedWoning { get => selectedWoning; set => SetProperty(ref selectedWoning, value); }
        public WoningDetailView WoningDetailView { get; }

        public WoningDetailCommandViewModel()
        {
            WoningDetailView = new WoningDetailView();
            SaveCommand = new RelayCommand(Save, CanSave);
            CancelCommand = new RelayCommand(Cancel);
            Klanten = KlantRepository.GetAll();
            HuisTypen = new ObservableCollection<HuisType>(Enum.GetValues<HuisType>());
            Bouwdatum = DateTime.Today;
        }

        private void Cancel()
        {
            IsEnabled = true;
            Straat = string.Empty;
            Gemeente = string.Empty;
            Huisnummer = string.Empty;
            Postnummer = 0;
            Verdieping = 0;
            Bouwdatum = DateTime.Today;
            Waarde = 0.0;
            SelectedType = default;
            SelectedKlant = null;
            IsHuis = false;
            IsAppartement = false;
            Huis = null;
            Appartement = null;
            Adres = null;
            Status = WoningDetailViewState.Cancelled;
        }

        private bool CanSave()
        {
            return !string.IsNullOrEmpty(Straat) && !string.IsNullOrEmpty(Huisnummer) && !string.IsNullOrEmpty(Gemeente) 
            && SelectedKlant != null && Waarde > 0 && (IsHuis || IsAppartement);
        }

        private void Save()
        {
            if (SelectedWoning != null)
            {
                if (IsHuis)
                {
                    Adres adres = new Adres(Straat, Huisnummer, Gemeente, Postnummer);
                    Huis huis = new Huis(adres, Waarde, SelectedType, DateOnly.FromDateTime(Bouwdatum), SelectedWoning.Id, SelectedKlant.Id);
                    WoningRepository.Update(huis);
                    Status = WoningDetailViewState.ItemEdited;
                }
                else if (IsAppartement)
                {
                    Adres adres = new Adres(Straat, Huisnummer, Gemeente, Postnummer);
                    Appartement appartement = new Appartement(adres, Waarde, DateOnly.FromDateTime(Bouwdatum), Verdieping, SelectedWoning.Id, SelectedKlant.Id);
                    WoningRepository.Update(appartement);
                    Status = WoningDetailViewState.ItemEdited;
                }
            }
            else if (SelectedWoning == null)
            {
                if (IsHuis)
                {
                    Adres adres = new Adres(Straat, Huisnummer, Gemeente, Postnummer);
                    Woning huis = new Huis(adres, Waarde, SelectedType, DateOnly.FromDateTime(Bouwdatum), SelectedKlant.Id);
                    SelectedKlant.VoegWoningToe(huis);
                    WoningRepository.Add(huis);
                    Status = WoningDetailViewState.ItemCreated;
                }
                else if (IsAppartement)
                {
                    Adres adres = new Adres(Straat, Huisnummer, Gemeente, Postnummer);
                    Woning appartement = new Appartement(adres, Waarde, DateOnly.FromDateTime(Bouwdatum), Verdieping, SelectedKlant.Id);
                    SelectedKlant.VoegWoningToe(appartement);
                    WoningRepository.Add(appartement);
                    Status = WoningDetailViewState.ItemCreated;
                }
            }

            Straat = string.Empty;
            Gemeente = string.Empty;
            Huisnummer = string.Empty;
            Postnummer = 0;
            Verdieping = 0;
            Bouwdatum = DateTime.Today;
            Waarde = 0.0;
            SelectedType = default;
            SelectedKlant = null;
            IsHuis = false;
            IsAppartement = false;
            Huis = null;
            Appartement = null;
            Adres = null;
        }
    }
}

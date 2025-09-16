using AAD.ImmoWin.Business.Classes;
using AAD.ImmoWin.WPF.Repositories;
using AAD.ImmoWin.WPF.Views;
using AAD.ImmoWin.WPF.ViewState;
using Microsoft.VisualBasic;
using Odisee.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.WPF.ViewModels.Klanten
{
    public class KlantDetailCommandViewModel : BaseViewModel
    {
        private string voornaam;
        private string familienaam;
        private string straat;
        private string gemeente;
        private string huisnummer;
        private int postnummer;
        private Adres adres;
        private bool isEnabled;
        private KlantDetailViewState? status;
        private Klant? selectedKlant;
        public KlantDetailView KlantDetailView { get; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public Adres Adres { get => adres; set => SetProperty(ref adres, value); }
        public string Gemeente { get => gemeente; set => SetProperty(ref gemeente, value); }
        public string Straat { get => straat; set => SetProperty(ref straat, value); }
        public string Huisnummer { get => huisnummer; set => SetProperty(ref huisnummer, value); }
        public int Postnummer { get => postnummer; set => SetProperty(ref postnummer, value); }

        public string Voornaam { get => voornaam; set => SetProperty(ref voornaam, value); }
        public string Familienaam { get => familienaam; set => SetProperty(ref familienaam, value); }

        public bool IsEnabled { get => isEnabled; set => SetProperty(ref isEnabled, value); }

        public KlantDetailViewState? Status { get => status; set => SetProperty(ref status, value); }

        public Klant? SelectedKlant { get => selectedKlant; set => SetProperty(ref selectedKlant, value); }

        public KlantDetailCommandViewModel()
        {
            KlantDetailView = new KlantDetailView();
            SaveCommand = new RelayCommand(Save, CanSave);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Cancel()
        {
            Voornaam = string.Empty;
            Familienaam = string.Empty;
            Straat = string.Empty;
            Huisnummer = string.Empty;
            Gemeente = string.Empty;
            Postnummer = 0;
            IsEnabled = true;
            Status = KlantDetailViewState.Cancelled;
        }

        private bool CanSave()
        {
            return !string.IsNullOrEmpty(Voornaam) && !string.IsNullOrEmpty(Familienaam);
        }

        private void Save()
        {
            if (SelectedKlant != null)
            {
                Adres adres = new Adres(Straat, Huisnummer, Gemeente, Postnummer);
                Klant klant = new Klant(Voornaam, Familienaam, adres) { Id = SelectedKlant.Id };
                KlantRepository.Update(klant);
                Status = KlantDetailViewState.ItemEdited;
            }
            else if (SelectedKlant == null)
            {
                Klant klant = new Klant(Voornaam, Familienaam);
                klant.Adres = new Adres(Straat, Huisnummer, Gemeente, Postnummer);
                KlantRepository.Add(klant);
                Status = KlantDetailViewState.ItemCreated;
            }
            Voornaam = string.Empty;
            Familienaam = string.Empty;
            Straat = string.Empty;
            Huisnummer = string.Empty;
            Gemeente = string.Empty;
            Postnummer = 0;
        }
    }
}

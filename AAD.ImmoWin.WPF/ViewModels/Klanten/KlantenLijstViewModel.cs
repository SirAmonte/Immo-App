using AAD.ImmoWin.Business.Classes;
using AAD.ImmoWin.Business.Exceptions;
using AAD.ImmoWin.Business.Interfaces;
using AAD.ImmoWin.WPF.Repositories;
using AAD.ImmoWin.WPF.ViewState;
using Odisee.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.WPF.ViewModels.Klanten
{
    public class KlantenLijstViewModel : BaseViewModel
    {
        private bool isEnabled;
        private KlantenLijstViewState? status;
        private Klant selectedKlant;
        private string voornaam;
        private string familienaam;
        private string searchklant;
        public RelayCommand CreateCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public string SearchKlant { get => searchklant; set => SetProperty(ref searchklant, value); }
        public ObservableCollection<Klant> Klanten { get; set; }
        public ObservableCollection<Klant> FilteredKlanten { get; set; }
        public KlantenLijstViewState? Status { get => status; set => SetProperty(ref status, value); }
        public Klant? SelectedKlant { get => selectedKlant; set => SetProperty(ref selectedKlant, value); }
        public string Voornaam { get => voornaam; set => SetProperty(ref voornaam, value); }
        public string Familienaam { get => familienaam; set => SetProperty(ref familienaam, value); }
        public bool IsEnabled { get => isEnabled; set => SetProperty(ref isEnabled, value); }

        public KlantenLijstViewModel()
        {
            CreateCommand = new RelayCommand(CreateClicked);
            EditCommand = new RelayCommand(Edit, CanEdit);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            Klanten = KlantRepository.GetAll();
            FilteredKlanten = new ObservableCollection<Klant>(Klanten);
        }

        public void SearchKlanten()
        {
            string searchQuery = SearchKlant?.ToLower() ?? string.Empty;
            var filtered = string.IsNullOrWhiteSpace(searchQuery)
                ? Klanten
                : new ObservableCollection<Klant>(Klanten.Where(
                    k => k.Voornaam.ToLower().Contains(searchQuery) ||
                         k.Familienaam.ToLower().Contains(searchQuery) ||
                         $"{k.Voornaam.ToLower()} {k.Familienaam.ToLower()}".Contains(searchQuery) ||
                         $"{k.Familienaam.ToLower()} {k.Voornaam.ToLower()}".Contains(searchQuery)));

            FilteredKlanten.Clear();
            foreach (Klant klant in filtered)
            {
                FilteredKlanten.Add(klant);
            }
        }

        public void ReloadData()
        {
            ObservableCollection<Klant> klanten = KlantRepository.GetAll();
            Klanten.Clear();
            foreach (Klant klant in klanten)
            {
                Klanten.Add(klant);
            }
            SearchKlanten();
        }

        private bool CanDelete()
        {
            return SelectedKlant != null;
        }

        private void Delete()
        {
            KlantRepository.Remove(SelectedKlant);
            ReloadData();
        }

        private void CreateClicked()
        {
            Status = KlantenLijstViewState.Create;
        }

        private bool CanEdit()
        {
            return SelectedKlant != null;
        }

        private void Edit()
        {
            Status = KlantenLijstViewState.Edit;
        }
    }
}

using AAD.ImmoWin.Business.Classes;
using AAD.ImmoWin.WPF.Repositories;
using AAD.ImmoWin.WPF.Views;
using Odisee.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AAD.ImmoWin.WPF.ViewModels.Klanten
{
    public class KlantenModuleViewModel : BaseViewModel
    {
        public KlantenLijstViewModel KlantenLijstViewModel { get; set; }
        public KlantDetailCommandViewModel KlantDetailCommandViewModel { get; set; }

        public KlantenModuleViewModel()
        {
            KlantenLijstViewModel = new KlantenLijstViewModel();
            KlantDetailCommandViewModel = new KlantDetailCommandViewModel();
            KlantenLijstViewModel.PropertyChanged += KlantenLijstViewModel_PropertyChanged;
            KlantenLijstViewModel.IsEnabled = true;
            KlantDetailCommandViewModel.PropertyChanged += KlantDetailCommandViewModel_PropertyChanged;
        }

        private void KlantDetailCommandViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Status":
                    if (KlantDetailCommandViewModel.Status == ViewState.KlantDetailViewState.ItemCreated)
                    {
                        KlantDetailCommandViewModel.IsEnabled = false;
                        KlantenLijstViewModel.IsEnabled = true;
                        KlantenLijstViewModel.Status = null;
                        KlantenLijstViewModel.ReloadData();
                    }
                    else if (KlantDetailCommandViewModel.Status == ViewState.KlantDetailViewState.ItemEdited)
                    {
                        KlantDetailCommandViewModel.IsEnabled = false;
                        KlantenLijstViewModel.IsEnabled = true;
                        KlantenLijstViewModel.Status = null;
                        KlantenLijstViewModel.ReloadData();
                    }
                    else if (KlantDetailCommandViewModel.Status == ViewState.KlantDetailViewState.Cancelled)
                    {
                        KlantDetailCommandViewModel.IsEnabled = false;
                        KlantenLijstViewModel.IsEnabled = true;
                        KlantenLijstViewModel.Status = null;
                        KlantenLijstViewModel.ReloadData();
                    }
                    break;
            }
        }

        private void KlantenLijstViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Status":
                    if (KlantenLijstViewModel.Status == ViewState.KlantenLijstViewState.Create)
                    {
                        KlantDetailCommandViewModel.IsEnabled = true;
                        KlantenLijstViewModel.IsEnabled = false;
                        KlantDetailCommandViewModel.Status = ViewState.KlantDetailViewState.Create;
                    }
                    break;
                case "SelectedKlant":
                    KlantDetailCommandViewModel.SelectedKlant = KlantenLijstViewModel.SelectedKlant;
                    KlantDetailCommandViewModel.Voornaam = KlantenLijstViewModel.SelectedKlant?.Voornaam;
                    KlantDetailCommandViewModel.Familienaam = KlantenLijstViewModel.SelectedKlant?.Familienaam;
                    KlantDetailCommandViewModel.Straat = KlantenLijstViewModel.SelectedKlant?.Adres.Straat;
                    KlantDetailCommandViewModel.Huisnummer = KlantenLijstViewModel.SelectedKlant?.Adres.Huisnummer;
                    KlantDetailCommandViewModel.Gemeente = KlantenLijstViewModel.SelectedKlant?.Adres.Gemeente;
                    KlantDetailCommandViewModel.Postnummer = KlantenLijstViewModel.SelectedKlant?.Adres.Postnummer ?? 0;
                    break;
                case "SearchKlant":
                    KlantenLijstViewModel.SearchKlanten();
                    break;
            }
        }
    }
}

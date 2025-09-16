using AAD.ImmoWin.Business.Classes;
using AAD.ImmoWin.WPF.ViewModels.Klanten;
using Odisee.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.WPF.ViewModels.Woningen
{
    public class WoningenModuleViewModel: BaseViewModel
    {
        public WoningenLijstViewModel WoningenLijstViewModel { get; set; }
        public WoningDetailCommandViewModel WoningDetailCommandViewModel { get; set; }

        public WoningenModuleViewModel()
        {
            WoningenLijstViewModel = new WoningenLijstViewModel();
            WoningDetailCommandViewModel = new WoningDetailCommandViewModel();
            WoningenLijstViewModel.IsEnabled = true;
            WoningenLijstViewModel.PropertyChanged += WoningenLijstViewModel_PropertyChanged;
            WoningDetailCommandViewModel.PropertyChanged += WoningDetailCommandViewModel_PropertyChanged;
        }

        private void WoningDetailCommandViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Status":
                    if (WoningDetailCommandViewModel.Status == ViewState.WoningDetailViewState.ItemCreated)
                    {
                        WoningDetailCommandViewModel.IsEnabled = false;
                        WoningenLijstViewModel.IsEnabled = true;
                        WoningenLijstViewModel.Status = null;
                        WoningenLijstViewModel.ReloadData();
                    }
                    else if (WoningDetailCommandViewModel.Status == ViewState.WoningDetailViewState.ItemEdited)
                    {
                        WoningDetailCommandViewModel.IsEnabled = false;
                        WoningenLijstViewModel.IsEnabled = true;
                        WoningenLijstViewModel.Status = null;
                        WoningenLijstViewModel.ReloadData();
                    }
                    else if (WoningDetailCommandViewModel.Status == ViewState.WoningDetailViewState.Cancelled)
                    {
                        WoningDetailCommandViewModel.IsEnabled = false;
                        WoningenLijstViewModel.IsEnabled = true;
                        WoningenLijstViewModel.Status = null;
                        WoningenLijstViewModel.ReloadData();
                    }
                    break;
            }
        }

        private void WoningenLijstViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Status":
                    if (WoningenLijstViewModel.Status == ViewState.WoningenLijstViewState.Create)
                    {
                        WoningDetailCommandViewModel.IsEnabled = true;
                        WoningenLijstViewModel.IsEnabled = false;
                        WoningDetailCommandViewModel.Status = ViewState.WoningDetailViewState.Create;
                    }
                    break;
                case "SelectedWoning":
                    if (WoningenLijstViewModel.SelectedWoning != null)
                    {
                        WoningDetailCommandViewModel.SelectedWoning = WoningenLijstViewModel.SelectedWoning;
                        if (WoningenLijstViewModel.SelectedWoning.Adres != null)
                        {
                            WoningDetailCommandViewModel.Gemeente = WoningenLijstViewModel.SelectedWoning.Adres.Gemeente;
                            WoningDetailCommandViewModel.Straat = WoningenLijstViewModel.SelectedWoning.Adres.Straat;
                            WoningDetailCommandViewModel.Huisnummer = WoningenLijstViewModel.SelectedWoning.Adres.Huisnummer;
                            WoningDetailCommandViewModel.Postnummer = WoningenLijstViewModel.SelectedWoning.Adres.Postnummer;
                        }
                        WoningDetailCommandViewModel.SelectedKlant = WoningDetailCommandViewModel.Klanten.FirstOrDefault(k => k.Id == WoningenLijstViewModel.SelectedWoning.KlantId);
                        if (WoningenLijstViewModel.SelectedWoning is Huis huis)
                        {
                            WoningDetailCommandViewModel.SelectedType = huis.Type;
                            WoningDetailCommandViewModel.IsHuis = true;
                            WoningDetailCommandViewModel.Verdieping = 0;
                        }
                        else if (WoningenLijstViewModel.SelectedWoning is Appartement appartement)
                        {
                            WoningDetailCommandViewModel.Verdieping = appartement.Verdieping;
                            WoningDetailCommandViewModel.IsAppartement = true;
                            WoningDetailCommandViewModel.SelectedType = default;
                        }
                        WoningDetailCommandViewModel.Bouwdatum = WoningenLijstViewModel.SelectedWoning.Bouwdatum.ToDateTime(TimeOnly.MinValue);
                        WoningDetailCommandViewModel.Waarde = WoningenLijstViewModel.SelectedWoning.Waarde;
                    }
                    else
                    {
                        WoningDetailCommandViewModel.SelectedWoning = null;
                        WoningDetailCommandViewModel.Gemeente = string.Empty;
                        WoningDetailCommandViewModel.Straat = string.Empty;
                        WoningDetailCommandViewModel.Huisnummer = string.Empty;
                        WoningDetailCommandViewModel.Postnummer = 0;
                        WoningDetailCommandViewModel.SelectedKlant = null;
                        WoningDetailCommandViewModel.SelectedType = default;
                        WoningDetailCommandViewModel.Verdieping = 0;
                        WoningDetailCommandViewModel.Bouwdatum = DateTime.Today;
                        WoningDetailCommandViewModel.Waarde = 0;
                        WoningDetailCommandViewModel.IsHuis = false;
                        WoningDetailCommandViewModel.IsAppartement = false;
                    }
                    break;
                case "SelectedFilter":
                    WoningenLijstViewModel.FilterWoningen();
                    break;
                case "IsNewest":
                    WoningenLijstViewModel.FilterWoningen();
                    break;
                case "IsValuable":
                    WoningenLijstViewModel.FilterWoningen();
                    break;
            }
        }
    }
}

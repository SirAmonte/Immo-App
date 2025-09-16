using AAD.ImmoWin.Business.Classes;
using AAD.ImmoWin.WPF.Repositories;
using AAD.ImmoWin.WPF.ViewState;
using Odisee.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.WPF.ViewModels.Woningen
{
    public class WoningenLijstViewModel : BaseViewModel
    {
        private bool isEnabled;
        private WoningenLijstViewState? status;
        private Woning? selectedWoning;
        private string straat;
        private string gemeente;
        private string huisnummer;
        private int postnummer;
        private DateTime bouwdatum;
        private double waarde;
        private Adres adres;
        private string selectedFilter;
        private bool isnewest;
        private bool isvaluable;
        public RelayCommand CreateCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public ObservableCollection<Woning> FilteredWoningen { get; set; }
        public ObservableCollection<Woning> Woningen { get; set; }
        public ObservableCollection<string> Filters { get; set; }
        public WoningenLijstViewState? Status { get => status; set => SetProperty(ref status, value); }
        public Woning? SelectedWoning { get => selectedWoning; set => SetProperty(ref selectedWoning, value); }
        public string SelectedFilter { get => selectedFilter; set => SetProperty(ref selectedFilter, value); }
        public string Gemeente { get => gemeente; set => SetProperty(ref gemeente, value); }
        public string Straat { get => straat; set => SetProperty(ref straat, value); }
        public string Huisnummer { get => huisnummer; set => SetProperty(ref huisnummer, value); }
        public int Postnummer { get => postnummer; set => SetProperty(ref postnummer, value); }
        public Adres Adres { get => adres; set => SetProperty(ref adres, value); }
        public DateTime Bouwdatum { get => bouwdatum; set => SetProperty(ref bouwdatum, value); }
        public double Waarde { get => waarde; set => SetProperty(ref waarde, value); }
        public bool IsNewest { get => isnewest; set => SetProperty(ref isnewest, value); }
        public bool IsValuable { get => isvaluable; set => SetProperty(ref isvaluable, value); }
        public bool IsEnabled { get => isEnabled; set => SetProperty(ref isEnabled, value); }

        public WoningenLijstViewModel()
        {
            CreateCommand = new RelayCommand(CreateClicked);
            EditCommand = new RelayCommand(Edit, CanEdit);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            Woningen = WoningRepository.GetAll();
            FilteredWoningen = new ObservableCollection<Woning>(Woningen);
            Filters = new ObservableCollection<string> { "Huis", "Appartement" };
        }

        public void FilterWoningen()
        {
            List<Woning> filtered = new List<Woning>(Woningen);

            if (!string.IsNullOrEmpty(SelectedFilter))
            {
                filtered = filtered.Where(woning =>
                    (SelectedFilter == "Huis" && woning is Huis) ||
                    (SelectedFilter == "Appartement" && woning is Appartement)).ToList();
            }

            if (IsNewest)
            {
                filtered = filtered.OrderByDescending(woning => woning.Bouwdatum).ToList();
            }

            if (IsValuable)
            {
                filtered = filtered.OrderByDescending(woning => woning.Waarde).ToList();
            }

            FilteredWoningen.Clear();
            foreach (Woning woning in filtered)
            {
                FilteredWoningen.Add(woning);
            }
        }

        public void ReloadData()
        {
            ObservableCollection<Woning> woningen = WoningRepository.GetAll();
            Woningen.Clear();
            foreach (Woning woning in woningen)
            {
                Woningen.Add(woning);
            }
            FilterWoningen();
        }

        private bool CanDelete()
        {
            return SelectedWoning != null;
        }

        private void Delete()
        {
            WoningRepository.Remove(SelectedWoning);
            ReloadData();
            SelectedWoning = null;
        }

        private void CreateClicked()
        {
            Status = WoningenLijstViewState.Create;
        }

        private bool CanEdit()
        {
            return SelectedWoning != null;
        }

        private void Edit()
        {
            Status = WoningenLijstViewState.Edit;
        }
    }
}

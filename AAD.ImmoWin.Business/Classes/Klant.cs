using AAD.ImmoWin.Business.Exceptions;
using AAD.ImmoWin.Business.Interfaces;
using Odisee.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Classes
{
    public class Klant : ObservableObject, IKlant, IFormattable
    {
        private string voornaam;
        private string familienaam;
        private List<Woning> woningen;
        private int id;
        private Adres adres;

        public string Voornaam { get => voornaam; set => SetProperty(ref voornaam, value); }
        public string Familienaam { get => familienaam; set => SetProperty(ref familienaam, value); }

        public List<Woning> Woningen { get => woningen; set => SetProperty(ref woningen, value); }

        public Adres Adres { get => adres; set => SetProperty(ref adres, value); }

        [Key]
        public int Id { get => id; set => id = value; }

        public Klant()
        {
            Woningen = new List<Woning>();
        }

        public Klant(string voornaam, string familienaam)
        {
            if (string.IsNullOrWhiteSpace(familienaam))
            {
                throw new FamilieNaamIsNulOfLeegException();
            }
            Familienaam = familienaam;
            Voornaam = voornaam;
            Woningen = new List<Woning>();
        }

        public Klant(string voornaam, string familienaam, Adres adres)
        {
            if (string.IsNullOrWhiteSpace(familienaam))
            {
                throw new FamilieNaamIsNulOfLeegException();
            }
            Familienaam = familienaam;
            Voornaam = voornaam;
            Woningen = new List<Woning>();
            Adres = adres;
        }

        public Klant(Klant selectedKlant)
        {
            if (string.IsNullOrWhiteSpace(selectedKlant.Familienaam))
            {
                throw new FamilieNaamIsNulOfLeegException();
            }
            Id = selectedKlant.Id;
            Familienaam = selectedKlant.Familienaam;
            Voornaam = selectedKlant.Voornaam;
            Woningen = new List<Woning>(selectedKlant.Woningen);
            Adres = selectedKlant.Adres;
        }

        public void VoegWoningToe(Woning woning)
        {
            Woningen.Add(woning);
        }

        public int CompareTo(object? obj)
        {
            if (obj is IKlant)
            {
                return CompareTo(obj as IKlant);
            }
            throw new ArgumentException("Object is geen klant");
        }

        public int CompareTo(IKlant? other)
        {
            if (other == null)
            {
                throw new ArgumentNullException();
            }

            int result = string.Compare(Familienaam, other.Familienaam, StringComparison.Ordinal);
            if (result != 0) return result;

            return string.Compare(Voornaam, other.Voornaam, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return ToString("T", null);
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            switch (format)
            {
                case "VN":
                    return $"{Voornaam} {Familienaam}";
                case "T":
                default:
                    return $"{Familienaam} {Voornaam} #eigendommen: {Woningen?.Count ?? 0}";
            }
        }
    }
}

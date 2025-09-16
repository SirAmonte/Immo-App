using AAD.ImmoWin.Business.Exceptions;
using AAD.ImmoWin.Business.Interfaces;
using Odisee.Common;
using System.ComponentModel.DataAnnotations;

namespace AAD.ImmoWin.Business.Classes
{
    public class Adres : ObservableObject, IAdres, IFormattable
    {
        private string straat;
        private string huisnummer;
        private int postnummer;
        private string gemeente;
        private int id;

        public string Straat { get => straat; set => SetProperty(ref straat, value); }
        public string Huisnummer { get => huisnummer; set => SetProperty(ref huisnummer, value); }
        public int Postnummer { get => postnummer; set => SetProperty(ref postnummer, value); }
        public string Gemeente { get => gemeente; set => SetProperty(ref gemeente, value); }

        [Key]
        public int Id { get => id; set => id = value; }

        public Adres()
        {
            Id = id;
        }

        public Adres(string straat, string huisnummer, string gemeente, int postnummer)
        {
            if (string.IsNullOrWhiteSpace(straat))
            {
                throw new StraatGemeenteException();
            }
            if (string.IsNullOrWhiteSpace(gemeente))
            {
                throw new StraatGemeenteException();
            }
            if (string.IsNullOrWhiteSpace(huisnummer) || postnummer <= 0)
            {
                throw new PostnummerIsNegatiefOfOException();
            }
            Straat = straat;
            Huisnummer = huisnummer;
            Postnummer = postnummer;
            Gemeente = gemeente;
        }
        public int CompareTo(object? obj)
        {
            if (obj is IAdres)
            {
                return CompareTo(obj as IAdres);
            }
            throw new ArgumentException("Object is geen adres");
        }

        public int CompareTo(IAdres? other)
        {
            if (other == null)
            {
                throw new ArgumentException();
            }

            int result = Postnummer.CompareTo(other.Postnummer);
            if (result != 0) return result;

            result = string.Compare(Gemeente, other.Gemeente, StringComparison.Ordinal);
            if (result != 0) return result;

            result = string.Compare(Straat, other.Straat, StringComparison.Ordinal);
            if (result != 0) return result;

            return string.Compare(Huisnummer, other.Huisnummer, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return ToString("T", null);
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            switch (format)
            {
                case "S":
                    return $"{Postnummer} {Gemeente} {Straat} {Huisnummer}";
                case "L":
                    return $"{Straat} {Huisnummer}";
                case "T":
                default:
                    return $"{Straat} {Huisnummer}, {Postnummer} {Gemeente}";
            }
        }
    }
}

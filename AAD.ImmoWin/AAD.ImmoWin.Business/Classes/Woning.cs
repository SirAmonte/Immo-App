using AAD.ImmoWin.Business.Exceptions;
using AAD.ImmoWin.Business.Interfaces;
using Odisee.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AAD.ImmoWin.Business.Exceptions.WaardeIsNegatiefOfOException;

namespace AAD.ImmoWin.Business.Classes
{
    public class Woning : ObservableObject, IWoning
    {
        private Adres adres;
        private DateOnly bouwdatum;
        private double waarde;
        private int id;
        private int klantid;

        public Adres Adres { get => adres; set => SetProperty(ref adres, value); }

        public DateOnly Bouwdatum { get => bouwdatum; set => SetProperty(ref bouwdatum, value); }
        public double Waarde { get => waarde; set => SetProperty(ref waarde, value); }
        [Key]
        public int Id { get => id; set => id = value; }

        public int KlantId { get => klantid; set => klantid = value; }

        public Woning()
        {
            Id = id;
            KlantId = klantid;
        }

        public Woning(Woning selectedWoning)
        {
            DateOnly huidigedatum = DateOnly.FromDateTime(DateTime.Now);
            if (waarde < 0)
            {
                throw new WaardeIsNegatiefOfOException();
            }
            if (bouwdatum > huidigedatum)
            {
                throw new BouwDatumInToekomstException();
            }
            Id = selectedWoning.Id;
            KlantId = selectedWoning.KlantId;
            Adres = selectedWoning.Adres;
            Waarde = selectedWoning.Waarde;
            Bouwdatum = selectedWoning.Bouwdatum;
        }

        public Woning(double waarde, Adres adres, DateOnly bouwdatum)
        {
            DateOnly huidigedatum = DateOnly.FromDateTime(DateTime.Now); 
            if (waarde < 0)
            {
                throw new WaardeIsNegatiefOfOException();
            }
            if (bouwdatum > huidigedatum)
            {
                throw new BouwDatumInToekomstException();
            }
            Adres = adres;
            Waarde = waarde;
            Bouwdatum = bouwdatum;
        }

        public Woning(double waarde, Adres adres, DateOnly bouwdatum, int id)
        {
            DateOnly huidigedatum = DateOnly.FromDateTime(DateTime.Now);
            if (waarde < 0)
            {
                throw new WaardeIsNegatiefOfOException();
            }
            if (bouwdatum > huidigedatum)
            {
                throw new BouwDatumInToekomstException();
            }
            Adres = adres;
            Waarde = waarde;
            Bouwdatum = bouwdatum;
            Id = id;
        }

        public int CompareTo(object? obj)
        {
            return CompareTo(obj as IWoning);
        }

        public int CompareTo(IWoning? other)
        {
            if (other == null) return 1;

            return Adres.ToString().CompareTo(other.Adres?.ToString() ?? string.Empty);
        }

        public override string ToString()
        {
            return Adres?.ToString() ?? string.Empty;
        }
    }
}

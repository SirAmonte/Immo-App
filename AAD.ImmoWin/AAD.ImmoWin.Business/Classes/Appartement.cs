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
    public class Appartement : Woning, IAppartement, IFormattable
    {
        private int verdieping;
        private int id;

        [Key]
        public int Id { get => id; set => id = value; }

        public int Verdieping { get => verdieping; set => SetProperty(ref verdieping, value); }

        public Appartement() { }

        public Appartement(Adres adres, double waarde, DateOnly bouwdatum, int verdieping, int klantId)
        : base(waarde, adres, bouwdatum)
        {
            Verdieping = verdieping;
            KlantId = klantId;
        }

        public Appartement(Adres adres, double waarde, DateOnly bouwdatum, int verdieping, int id, int klantId)
            : base(waarde, adres, bouwdatum, id)
        {
            Verdieping = verdieping;
            Id = id;
            KlantId = klantId;
        }

        public override string ToString()
        {
            return ToString("T", null);
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            switch (format)
            {
                case "T":
                default:
                    return $"Appartement: verd.: {Verdieping} €{Waarde} - {Adres}";
            }
        }

        public new int CompareTo(object? obj)
        {
            if (obj is IAppartement)
            {
                return CompareTo(obj as IAppartement);
            }
            return base.CompareTo(obj as Woning);
        }

        public int CompareTo(IAppartement? other)
        {
            if (other == null)
            {
                throw new ArgumentNullException();
            }
            if (base.CompareTo(other) != 0)
            {
                return base.CompareTo(other);
            }
            return Verdieping.CompareTo(other?.Verdieping);
        }
    }
}

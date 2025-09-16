using AAD.ImmoWin.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Classes
{
    public enum HuisType
    {
        alleenstaand = 0,
        driegevel = 1,
        rijhuis = 2,
    }
    public class Huis : Woning, IHuis
    {
        private HuisType type;
        private int id;

        [Key]
        public int Id { get => id; set => id = value; }

        public HuisType Type { get => type; set => SetProperty(ref type, value); }

        public Huis() { }

        public Huis(Adres adres, double waarde, HuisType type, DateOnly bouwdatum, int klantId)
        : base(waarde, adres, bouwdatum)
        {
            Type = type;
            KlantId = klantId;
        }

        public Huis(Adres adres, double waarde, HuisType type, DateOnly bouwdatum, int id, int klantId)
            : base(waarde, adres, bouwdatum, id)
        {
            Type = type;
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
                    return $"Huis: {Type} €{Waarde} - {Adres}";
            }
        }
    }
}

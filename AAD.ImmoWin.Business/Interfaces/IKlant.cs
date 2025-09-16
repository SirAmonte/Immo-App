using AAD.ImmoWin.Business.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Interfaces
{
    public interface IKlant : IComparable, IComparable<IKlant>
    {
        string Voornaam { get; }
        string Familienaam { get; }
        void VoegWoningToe(Woning woning);
        public List<Woning> Woningen { get; set; }
    }
}

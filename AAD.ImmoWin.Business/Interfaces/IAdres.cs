using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Interfaces
{
    public interface IAdres : IComparable, IComparable<IAdres>
    {
        string Straat { get; }
        string Huisnummer { get; }
        int Postnummer { get; }
        string Gemeente { get; }
    }
}

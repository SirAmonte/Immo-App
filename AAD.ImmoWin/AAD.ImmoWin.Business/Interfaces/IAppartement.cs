using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Interfaces
{
    public interface IAppartement : IWoning, IComparable, IComparable<IAppartement>
    {
        int Verdieping { get; }
    }
}

using AAD.ImmoWin.Business.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Interfaces
{
    public interface IHuis : IWoning
    {
        HuisType Type { get; }
    }
}

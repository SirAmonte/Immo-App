using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Exceptions
{
    public class FamilieNaamIsNulOfLeegException : Exception
    {
        public FamilieNaamIsNulOfLeegException() : base("Familienaam is leeg of null") { }
    }
}

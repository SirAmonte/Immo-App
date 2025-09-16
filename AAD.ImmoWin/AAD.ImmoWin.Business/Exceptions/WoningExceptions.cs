using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Exceptions
{
    public class WaardeIsNegatiefOfOException : Exception
    {
        public WaardeIsNegatiefOfOException() : base("Waarde is negatief of nul")
        {
        }

        public class BouwDatumInToekomstException : Exception
        {
            public BouwDatumInToekomstException() : base("Bouwdatum is in de toekomst") { }
        }
    }
}

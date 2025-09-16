using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Exceptions
{
    public class PostnummerIsNegatiefOfOException : Exception
    {
        public PostnummerIsNegatiefOfOException() : base("Postnummer is negatief of 0") { }
    }

    public class StraatGemeenteException : Exception
    {
        public StraatGemeenteException() : base("straat en gemeente mag niet leeg of null zijn.") { }
    }
}

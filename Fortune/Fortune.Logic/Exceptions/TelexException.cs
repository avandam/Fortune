using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Logic.Exceptions
{
    public class TelexException : Exception
    {
        public TelexException(string message) : base(message)
        {

        }
    }
}

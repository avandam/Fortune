using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Logic.Exceptions
{
    public class JokerException : Exception
    {
        public JokerException(string message) : base(message)
        {

        }
    }
}

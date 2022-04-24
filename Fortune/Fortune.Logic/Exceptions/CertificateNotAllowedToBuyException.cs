using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Logic.Exceptions
{
    public class CertificateNotAllowedToBuyException : Exception
    {
        public CertificateNotAllowedToBuyException(string message) : base(message)
        {

        }
    }
}

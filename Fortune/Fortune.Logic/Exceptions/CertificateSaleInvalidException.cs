using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Logic.Exceptions
{
    public class CertificateSaleInvalidException : Exception
    {
        public CertificateSaleInvalidException(string message) : base(message)
        {

        }
    }
}

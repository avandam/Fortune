using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Logic
{
    public class Certificate
    {
        public Resource Resource { get; private set; }
        public int Percentage { get; private set; }
        public int Price { get; private set; }
        public Area Area { get; private set; }
    }
}

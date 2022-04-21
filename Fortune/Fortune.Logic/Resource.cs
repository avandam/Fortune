using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;

namespace Fortune.Logic
{
    public class Resource
    {
        public string Name { get; set; }
        public int RoyaltyPrice { get; set; }

        public Resource(string name, int royaltyPrice)
        {
            Name = name;
            RoyaltyPrice = royaltyPrice;
        }

        public int GetFee(int percentage)
        {
            if (percentage < 30)
            {
                return 0;
            }
            else if (percentage < 50)
            {
                return RoyaltyPrice;
            }
            else if (percentage < 70)
            {
                return RoyaltyPrice * 5;
            }
            else if (percentage < 90)
            {
                return RoyaltyPrice * 5 * 2;
            }
            else
            {
                return RoyaltyPrice * 5 * 2 * 2;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

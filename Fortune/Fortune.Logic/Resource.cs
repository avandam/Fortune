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
        public ResourceType Name { get; set; }
        public int RoyaltyFee { get; set; }

        public Resource(ResourceType name, int royaltyFee)
        {
            Name = name;
            RoyaltyFee = royaltyFee;
        }

        public int GetFee(int percentage)
        {
            if (percentage < 30)
            {
                return 0;
            }
            else if (percentage < 50)
            {
                return RoyaltyFee;
            }
            else if (percentage < 70)
            {
                return RoyaltyFee * 5;
            }
            else if (percentage < 90)
            {
                return RoyaltyFee * 5 * 2;
            }
            else
            {
                return RoyaltyFee * 5 * 2 * 2;
            }
        }

        public override string ToString()
        {
            return Name.ToText();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Logic
{
    public class Certificate
    {
        public Resource Resource { get; }
        public int Percentage { get; }
        public int Price { get; }
        public Zone Zone { get; }
        public RegionType Region { get; }

        public Certificate(Resource resource, int percentage, int price, Zone zone) : this(resource, percentage, price, zone, RegionType.None)
        {

        }

        public Certificate(Resource resource, int percentage, int price, Zone zone, RegionType region)
        {
            Resource = resource;
            Percentage = percentage;
            Price = price;
            Zone = zone;
            Region = region;
        }
    }
}

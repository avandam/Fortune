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
        public string Region { get; }

        public Certificate(Resource resource, int percentage, int price, Zone zone, string region)
        {
            Resource = resource;
            Percentage = percentage;
            Price = price;
            Zone = zone;
            Region = region;
        }
    }
}

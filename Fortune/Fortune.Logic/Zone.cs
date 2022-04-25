using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Logic
{
    public class Zone
    {
        public CountryType Country { get; private set; }
        public ContinentType Continent { get; private set; }
        public Color Color { get; private set; }

        public Zone(CountryType country, ContinentType continent, Color color)
        {
            Country = country;
            Continent = continent;
            Color = color;
        }

        public override string ToString()
        {
            return $"{Country.ToText()} - {Continent.ToText()}";
        }
    }
}

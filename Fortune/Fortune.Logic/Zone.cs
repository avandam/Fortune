using System.Drawing;

namespace Fortune.Logic
{
    public class Zone
    {
        public CountryType Country { get; }
        public ContinentType Continent { get; }
        public Color Color { get; }

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

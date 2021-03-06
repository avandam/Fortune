using Fortune.Logic.Exceptions;
// ReSharper disable StringLiteralTypo

namespace Fortune.Logic
{
    public class TelexCard
    {
        public List<Resource> Resources { get; }
        public string FlavorText { get; }
        public TelexType Type { get; }
        public int MaxFee { get; }
        public int MinFee { get; }

        public TelexCard(string flavorText, TelexType type, int fixedFee) : this(flavorText, type, fixedFee, int.MinValue, new List<Resource>())
        {

        }

        public TelexCard(string flavorText, TelexType type, int fixedFee, List<Resource> resources) : this(flavorText, type, fixedFee, int.MinValue, resources)
        {
        }

        public TelexCard(string flavorText, TelexType type, int maxFee, int minFee, List<Resource> resources)
        {
            if (type == TelexType.GainFromPlayers && resources.Count > 0)
            {
                throw new TelexException("TelexType GainFromPlayers can not have resources.");
            }

            Resources = resources;
            FlavorText = flavorText;
            Type = type;
            MaxFee = maxFee;
            MinFee = minFee;
        }

        public string CreateText()
        {
            if (Resources.Count == 0)
            {
                return string.Format(FlavorText, SimplifyCurrency(MaxFee));
            }

            string resourcesText = string.Join(" of ", Resources.Select(resource => resource.ToString()).ToList());

            if (MinFee == int.MinValue)
            {
                return string.Format(FlavorText, SimplifyCurrency(MaxFee), resourcesText);
            }

            return string.Format(FlavorText, SimplifyCurrency(MaxFee), resourcesText, SimplifyCurrency(MinFee));
        }

        private static string SimplifyCurrency(int fee)
        {
            return (fee / 1000000) + " miljoen";
        }
    }
}

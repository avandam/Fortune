namespace Fortune.Logic
{
    public class TelexCard
    {
        public List<Resource> Resources { get; }
        public string FlavorText { get; }
        public bool IsGain { get; }
        public int MaxFee { get; }
        public int MinFee { get; }

        public TelexCard(string flavorText, bool isGain, int fixedFee) : this(flavorText, isGain, fixedFee, int.MinValue, new List<Resource>())
        {

        }

        public TelexCard(string flavorText, bool isGain, int fixedFee, List<Resource> resources) : this(flavorText, isGain, fixedFee, int.MinValue, resources)
        {

        }

        public TelexCard(string flavorText, bool isGain, int maxFee, int minFee, List<Resource> resources)
        {
            this.Resources = resources;
            this.FlavorText = flavorText;
            this.IsGain = isGain;
            this.MaxFee = maxFee;
            this.MinFee = minFee;
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
                return String.Format(FlavorText, SimplifyCurrency(MaxFee), resourcesText);
            }

            return string.Format(FlavorText, SimplifyCurrency(MaxFee), resourcesText, SimplifyCurrency(MinFee));
        }

        private string SimplifyCurrency(int fee)
        {
            return (fee / 1000000) + " million";
        }
    }
}

namespace Fortune.Logic.Fields
{
    // TODO: Telex has an issue: DoAction should select a telex, instead of hardcoding one in the constructor like done now
    public class Telex : Field
    {
        private readonly List<Resource> resources;
        private readonly int maxFee;
        private readonly int minFee;
        private readonly string flavorText;
        private readonly bool isGain;

        public Telex(Game game, int number, List<Resource> resources, int maxFee, int minFee, bool isGain, string flavorText) : this(game, number)
        {
            this.resources = resources;
            this.maxFee = maxFee;
            this.minFee = minFee;
            this.isGain = isGain;
            this.flavorText = flavorText;
        }

        public Telex(Game game, int number, List<Resource> resources, int fixedFee, bool isGain, string flavorText) : this(game, number, resources, fixedFee, int.MinValue, isGain, flavorText)
        {
        }


        public Telex(Game game, int number, int fixedFee, bool isGain, string flavorText) : this(game, number, new List<Resource>(), fixedFee, int.MinValue, isGain, flavorText)
        {
        }

        public Telex(Game game, int number) : base(game, number, "Telex")
        {

        }

        public override void DoAction(int redDiceValue, int whiteDiceValue)
        {
            Game.HandleTelex(CreateText(), resources, maxFee, minFee, isGain );
        }

        public string CreateText()
        {
            if (resources.Count == 0)
            {
                return string.Format(flavorText, SimplifyCurrency(maxFee));
            }

            string resourcesText = string.Join(" of ", resources.Select(resource => resource.ToString()).ToList());

            if (minFee == int.MinValue)
            {
                return String.Format(flavorText, SimplifyCurrency(maxFee), resourcesText);
            }

            return string.Format(flavorText, SimplifyCurrency(maxFee), resourcesText, SimplifyCurrency(minFee));
        }

        private string SimplifyCurrency(int fee)
        {
            return (fee / 1000000) + " million";
        }
    }
}

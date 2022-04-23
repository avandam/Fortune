namespace Fortune.Logic.Fields
{
    public class ChoiceContinent : Field
    {
        private readonly List<string> continents;

        public ChoiceContinent(Game game, int number, Resource resource, List<string> continents) : base(game, number, "Choice ", resource)
        {
            this.continents = continents;
        }

        public override void DoAction(int redDiceValue, int whiteDiceValue)
        {
            Game.HandleChoiceContinent(continents);
        }

        public override string ToString()
        {
            string continentsText = string.Join(" and ", this.continents);
            return $"{Name} {continentsText}";
        }
    }
}

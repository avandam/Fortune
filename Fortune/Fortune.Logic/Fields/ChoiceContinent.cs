namespace Fortune.Logic.Fields
{
    public class ChoiceContinent : Field
    {
        public Resource Resource { get; }

        private readonly List<ContinentType> continents;

        public ChoiceContinent(Game game, int number, Resource resource, List<ContinentType> continents) : base(game, number, "Keuze " + string.Join(" en ", continents.Select(continent => continent.ToText())))
        {
            this.continents = continents;
            Resource = resource;
        }

        public override void OnEntry(int redDiceValue, int whiteDiceValue)
        {
            Game.HandleResourceFee(Resource);
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

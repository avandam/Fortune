namespace Fortune.Logic.Fields
{
    public class Bonus : Field
    {
        public Resource Resource { get; }
        public Bonus(Game game, int number, Resource resource) : base (game, number, "Bonus")
        {
            Resource = resource;
        }

        public override void OnEntry(int redDiceValue, int whiteDiceValue)
        {
            Game.HandleBonus(redDiceValue + whiteDiceValue);
            Game.HandleResourceFee(Resource);
        }
    }
}

namespace Fortune.Logic.Fields
{
    public class Bonus : Field
    {
        public Bonus(Game game, int number, Resource resource) : base (game, number, "Bonus", resource)
        {
        }

        public override void OnEntry(int redDiceValue, int whiteDiceValue)
        {
            Game.HandleBonus(redDiceValue + whiteDiceValue);
            Game.HandleResourceFee(Resource);
        }
    }
}

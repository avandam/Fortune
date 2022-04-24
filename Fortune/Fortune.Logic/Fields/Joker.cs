namespace Fortune.Logic.Fields
{
    public class Joker : Field
    {
        public Joker(Game game, int number) : base(game, number, "Joker")
        {
        }

        public override void DoAction(int redDiceValue, int whiteDiceValue)
        {
            Game.HandleBuyJoker();
        }
    }
}

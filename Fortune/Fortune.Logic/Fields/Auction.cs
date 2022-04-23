namespace Fortune.Logic.Fields
{
    public class Auction : Field
    {
        public Auction(Game game, int number) : base(game, number, "Auction")
        {
        }

        public override void DoAction(int redDiceValue, int whiteDiceValue)
        {
            Game.HandleAuction(redDiceValue);
        }
    }
}

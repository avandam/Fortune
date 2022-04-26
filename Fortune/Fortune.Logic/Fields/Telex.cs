namespace Fortune.Logic.Fields
{
    public class Telex : Field
    {
        public Telex(Game game, int number) : base(game, number, "Telex")
        {

        }

        public override void DoAction(int redDiceValue, int whiteDiceValue)
        {
            Game.HandleTelex(GameData.GetTelexCard());
        }
    }
}

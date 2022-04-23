namespace Fortune.Logic.Fields
{
    public class Start : Field
    {
        public Start(Game game) : base(game, -1, "Start")
        {

        }

        public override void DoAction(int redDiceValue, int whiteDiceValue)
        {
            // DoNothing
        }
    }
}

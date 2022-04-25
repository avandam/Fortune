namespace Fortune.Logic.Fields
{
    public class ChoiceWorld : Field
    {
        public ChoiceWorld(Game game, int number) : base(game, number, "Keuze Wereld")
        {
        }
        
        public override void DoAction(int redDiceValue, int whiteDiceValue)
        {
            Game.HandleChoiceWorld();
        }
    }
}

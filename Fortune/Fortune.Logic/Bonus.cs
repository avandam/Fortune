using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Logic
{
    public class Bonus : Field
    {
        public Bonus(Game game, int number, Resource resource) : base (game, number, "Bonus", resource)
        {
        }

        public override void DoAction(Player currentPlayer, int redDiceValue, int whiteDiceValue)
        {
            // TODO: Notify UI that a bonus has been provided
            currentPlayer.UpdateCash((redDiceValue + whiteDiceValue) * 500000);
        }
    }
}

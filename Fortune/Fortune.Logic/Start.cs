using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Logic
{
    public class Start : Field
    {
        public Start(Game game) : base(game, -1, "Start")
        {

        }

        public override void DoAction(Player currentPlayer, int redDiceValue, int whiteDiceValue)
        {
            // DoNothing
        }
    }
}

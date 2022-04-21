using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Logic
{
    public class Joker : Field
    {
        public Joker(Game game, int number) : base(game, number, "Joker")
        {
        }

        public override void DoAction(Player currentPlayer, int redDiceValue, int whiteDiceValue)
        {
            // TODO: Notify UI that Joker cannot be bought if Cash is not enough
            if (currentPlayer.Cash >= 3000000)
            {
                currentPlayer.OfferJoker();
            }
        }
    }
}

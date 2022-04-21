using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Logic
{
    public class Auction : Field
    {
        public Auction(Game game, int number) : base(game, number, "Auction")
        {
        }

        public override void DoAction(Player currentPlayer, int redDiceValue, int whiteDiceValue)
        {
            // Notify the UI that a Auction has been triggered
            currentPlayer.Auction(redDiceValue);
        }
    }
}

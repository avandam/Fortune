using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Logic
{
    public class ChoiceWorld : Field
    {
        public ChoiceWorld(Game game, int number) : base(game, number, "Choice World")
        {
        }
        
        public override void DoAction(Player currentPlayer, int redDiceValue, int whiteDiceValue)
        {
            currentPlayer.OfferCertificates(Game.GetPossibleCertificates(currentPlayer));
        }
    }
}

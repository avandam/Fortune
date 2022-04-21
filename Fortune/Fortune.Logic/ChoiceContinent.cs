using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Logic
{
    public class ChoiceContinent : Field
    {
        private readonly List<Continent> continents;

        public ChoiceContinent(Game game, int number, string name, Resource resource, List<Continent> continents) : base(game, number, name, resource)
        {
            this.continents = continents;
        }

        public override void DoAction(Player currentPlayer, int redDiceValue, int whiteDiceValue)
        {
            currentPlayer.OfferCertificates(Game.GetPossibleCertificates(continents, currentPlayer));
        }

        public override string ToString()
        {
            string continentsText = string.Join(" and ", this.continents);
            return $"{Name} {continentsText}";
        }
    }
}

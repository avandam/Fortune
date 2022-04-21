using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Logic
{
    public class Telex : Field
    {
        private readonly List<Resource> resources = new List<Resource>();
        private readonly int maxFee;
        private readonly int minFee;
        private readonly string flavorText;
        private readonly int fixedFee;
        private readonly bool isGain;

        private Telex(Game game, int number) : base(game, number ,"Telex")
        {

        }

        public Telex(Game game, int number, List<Resource> resources, int maxFee, int minFee, bool isGain, string flavorText) : this(game, number)
        {
            this.resources = resources;
            this.maxFee = maxFee;
            this.minFee = minFee;
            this.isGain = isGain;
            this.flavorText = flavorText;
            this.fixedFee = int.MinValue;
        }

        public Telex(Game game, int number, int fixedFee, bool isGain, string flavorText) : this(game, number)
        {
            this.fixedFee = fixedFee;
            this.isGain = isGain;
            this.flavorText = flavorText;
            this.maxFee = int.MinValue;
            this.minFee = int.MinValue;

        }

        public override void DoAction(Player currentPlayer, int redDiceValue, int whiteDiceValue)
        {
            // TODO: Push Telext text to UI
            int fee;
            if (fixedFee == 0)
            {
                fee = isGain ? fixedFee : 0 - fixedFee;
            }
            else
            {
                if (resources.Any(resource => currentPlayer.HasResource(resource)))
                {
                    fee = isGain ? maxFee : 0 - maxFee;
                }
                else
                {
                    fee = isGain ? minFee : 0 - minFee;
                }
            }

            currentPlayer.UpdateCash(fee);
        }

        private string CreateText()
        {
            if (fixedFee == int.MinValue)
            {
                string resourcesText = string.Join(" of ", resources.Select(resource => resource.ToString()).ToList());

                return string.Format(flavorText, maxFee, resourcesText, minFee);
            }

            return string.Format(flavorText, fixedFee);
        }
    }
}

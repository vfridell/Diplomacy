using DiplomacyLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyLib.AI
{
    public class Alliance
    {
        public readonly double Sentiment;
        public readonly Powers TargetPower;

        public Alliance(Powers targetPower, double sentiment)
        {
            TargetPower = targetPower;
            Sentiment = sentiment;
        }

    }
}

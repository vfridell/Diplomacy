using System;
using DiplomacyLib.Models;

namespace DiplomacyLib.Analysis.Features
{
    public class FeatureTool
    {
        internal virtual void MeasureBoard(Board board, FeatureMeasurementCollection result)
        {
        }

        internal virtual void MeasureGame(Game game, FeatureMeasurementCollection result)
        {
        }
    }
}
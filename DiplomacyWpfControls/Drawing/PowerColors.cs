using DiplomacyLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DiplomacyWpfControls.Drawing
{
    public static class ColorPicker
    {
        private static List<Brush> _powerBrushes = new List<Brush>()
        {
            Brushes.SlateGray, Brushes.Black, Brushes.Pink, Brushes.Red, Brushes.Blue, Brushes.LightBlue, Brushes.Yellow, Brushes.Green
        };

        private static List<Brush> _territoryTypeBrushes = new List<Brush>()
        {
            Brushes.LightGray, Brushes.LightGray, Brushes.LightGray
        };

        public static Brush GetBrushForPower(Powers power)
        {
            return _powerBrushes[(int)power];
        }

        public static Brush GetBrushForMapNode(MapNode mapNode)
        {
            if (mapNode.Territory.IsSupplyCenter) return Brushes.LightSteelBlue;
            return _territoryTypeBrushes[(int)mapNode.Territory.TerritoryType];
        }
    }
}

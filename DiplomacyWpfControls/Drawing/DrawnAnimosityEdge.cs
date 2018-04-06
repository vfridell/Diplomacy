using GraphX.PCL.Common.Models;
using System.Windows.Media;

namespace DiplomacyWpfControls.Drawing
{
    public class DrawnAnimosityEdge : EdgeBase<DrawnPowerNode>
    {
        public Brush Color { get; protected set; }

        public DrawnAnimosityEdge(DrawnPowerNode source, DrawnPowerNode target, double weight = 1) : base(source, target, weight)
        {
            Color color = new Color();
            color.A = (byte)(256 - (256d * weight));
            Color = new SolidColorBrush(color);
        }
    }
}

using DiplomacyLib.Models;
using GraphX.PCL.Common.Models;

namespace DiplomacyWpfControls.Drawing
{
    public class DrawnPowerNode : VertexBase
    {
        public Powers Power { get; set; }

        public DrawnPowerNode(Powers power)
        {
            ID = (long)power;
            Power = power;
        }

        public override bool Equals(object obj)
        {
            DrawnPowerNode node = obj as DrawnPowerNode;
            if (node == null) return false;
            return Equals(node);
        }

        public bool Equals(DrawnPowerNode other)
        {
            return other.Power == Power;
        }

        public override int GetHashCode()
        {
            return Power.GetHashCode();
        }
    }
}

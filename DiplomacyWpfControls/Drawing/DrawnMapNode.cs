using DiplomacyLib.Models;
using DiplomacyLib.Visualize;
using GraphX.PCL.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomacyWpfControls.Drawing
{
    public class DrawnMapNode : VertexBase
    {
        public readonly MapNode MapNode;
        public readonly MapNodeRenderStyle RenderStyle;

        public DrawnMapNode(MapNode source)
        {
            MapNode = source;
            RenderStyle = MapNodeStyles.Get(source);
            ID = MapNode.SequenceNumber;
        }

        public override bool Equals(object obj)
        {
            DrawnMapNode otherNode = obj as DrawnMapNode;
            if (null == obj) return false;
            return Equals(otherNode);
        }

        public bool Equals(DrawnMapNode other)
        {
            return MapNode.Equals(other.MapNode);
        }

        public override int GetHashCode()
        {
            return MapNode.GetHashCode();
        }

        public override string ToString()
        {
            return MapNode.ToString();
        }

    }
}

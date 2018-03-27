using DiplomacyLib.Models;
using DiplomacyLib.Visualize;
using GraphX.Controls;
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DiplomacyWpfControls.Drawing
{
    public class DrawnMapNode : VertexBase
    {
        public readonly MapNode MapNode;
        public readonly MapNodeRenderStyle RenderStyle;
        public readonly GraphX.PCL.Common.Enums.VertexShape Shape;
        public Shape GeometryShape { get; protected set; }
        public string Text { get; protected set; }

        public DrawnMapNode(MapNode source)
        {
            MapNode = source;
            RenderStyle = MapNodeStyles.Get(source);
            ID = MapNode.SequenceNumber;
            Shape = (GraphX.PCL.Common.Enums.VertexShape)RenderStyle.Shape;
            Text = MapNode.ShortName;
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

    public class DrawnSeaNode : DrawnMapNode
    {
        public DrawnSeaNode(MapNode source) : base(source) { GeometryShape = new Ellipse() { Width = 100, Height = 50, Fill = Brushes.AliceBlue, Stroke = Brushes.Black };
    }
    }

    public class DrawnInlandNode : DrawnMapNode
    {
        public DrawnInlandNode(MapNode source) : base(source) { GeometryShape = new Rectangle() { Width=100, Height=50, Fill=Brushes.LightPink, Stroke=Brushes.Black }; }
    }

    public class DrawnCoastNode : DrawnMapNode
    {
        public DrawnCoastNode(MapNode source) : base(source) { GeometryShape = new Rectangle() { Width = 100, Height = 50, Fill = Brushes.LightSeaGreen, Stroke = Brushes.Black }; }
    }
}

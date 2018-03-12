using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph.Graphviz.Dot;
using System.Windows.Media;
using DiplomacyLib.Models;

namespace DiplomacyLib.Visualize
{
    public class MapNodeRenderStyle
    {
        public Color Color;
        public GraphvizColor GVColor;
        public GraphvizVertexShape Shape;
        public GraphvizPoint Point;

        private MapNodeRenderStyle(Color color, GraphvizVertexShape shape, int x, int y)
        {
            Shape = shape;
            Color = color;
            GVColor = new GraphvizColor(color.A, color.R, color.G, color.B);
            Point = new GraphvizPoint(x, y);
        }

        public static MapNodeRenderStyle Get(string mapNodeShortName, double xd, double yd)
        {
            MapNode node = MapNodes.Get(mapNodeShortName);
            int x = (int)xd;
            int y = (int)yd;
            switch (node.Territory.TerritoryType)
            {
                case TerritoryType.Sea:
                    return new MapNodeRenderStyle(Colors.Blue, GraphvizVertexShape.Ellipse, x, y);
                case TerritoryType.Coast:
                    return new MapNodeRenderStyle(Colors.Green, GraphvizVertexShape.Box, x, y);
                case TerritoryType.Inland:
                    return new MapNodeRenderStyle(Colors.Brown, GraphvizVertexShape.Box, x, y);
                default:
                    throw new Exception($"Unknown TerritoryType: {node.Territory.TerritoryType}");
            }

        }
    }
}

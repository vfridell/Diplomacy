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
        public readonly Color Color;
        public readonly GraphvizVertexShape Shape;
        public readonly double X;
        public readonly double Y;

        private MapNodeRenderStyle(Color color, GraphvizVertexShape shape, double x, double y)
        {
            Shape = shape;
            Color = color;
            X = x;
            Y = y;
        }

        public static MapNodeRenderStyle Get(string mapNodeShortName, double x, double y)
        {
            MapNode node = MapNodes.Get(mapNodeShortName);
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

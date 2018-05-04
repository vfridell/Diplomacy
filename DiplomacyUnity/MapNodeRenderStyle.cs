using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.Models;
using DiplomacyLib;

namespace DiplomacyUnity
{

    public enum VertexShape { Ellipse, Rectangle};

    public class MapNodeRenderStyle
    {
        public readonly VertexShape Shape;
        public readonly double X;
        public readonly double Y;

        private MapNodeRenderStyle(VertexShape shape, double x, double y)
        {
            Shape = shape;
            X = x;
            Y = y;
        }

        public static MapNodeRenderStyle Get(string mapNodeShortName, double x, double y)
        {
            MapNode node = MapNodes.Get(mapNodeShortName);
            switch (node.Territory.TerritoryType)
            {
                case TerritoryType.Sea:
                    return new MapNodeRenderStyle(VertexShape.Ellipse, x, y);
                case TerritoryType.Coast:
                    return new MapNodeRenderStyle(VertexShape.Rectangle, x, y);
                case TerritoryType.Inland:
                    return new MapNodeRenderStyle(VertexShape.Rectangle, x, y);
                default:
                    throw new Exception($"Unknown TerritoryType: {node.Territory.TerritoryType}");
            }

        }
    }
}

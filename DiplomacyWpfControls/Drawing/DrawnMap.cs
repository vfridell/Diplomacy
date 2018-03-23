using DiplomacyLib;
using DiplomacyLib.Models;
using QuickGraph;
using System;

namespace DiplomacyWpfControls.Drawing
{
    public class DrawnMap : BidirectionalGraph<DrawnMapNode, DrawnEdge>
    {
        public void Populate(Board board)
        {
            foreach(var edge in Maps.Full.Edges)
            {
                DrawnMapNode from = GetDrawnMapNode(edge.Source);
                DrawnMapNode to = GetDrawnMapNode(edge.Target);
                DrawnEdge drawnEdge = new DrawnEdge(from, to);
                if (!ContainsVertex(from)) AddVertex(from);
                if (!ContainsVertex(to)) AddVertex(to);
                if (!ContainsEdge(drawnEdge)) AddEdge(drawnEdge);
            }
        }

        private DrawnMapNode GetDrawnMapNode(MapNode mapNode)
        {
            switch(mapNode.Territory.TerritoryType)
            {
                case TerritoryType.Coast:
                    return new DrawnCoastNode(mapNode);
                case TerritoryType.Inland:
                    return new DrawnInlandNode(mapNode);
                case TerritoryType.Sea:
                    return new DrawnSeaNode(mapNode);
                default:
                    throw new ArgumentException($"Unknown TerritoryType: {mapNode.Territory.TerritoryType}");
            }
        }
    }
}
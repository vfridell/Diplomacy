using DiplomacyLib.Models;
using QuickGraph;

namespace DiplomacyWpfControls.Drawing
{
    public class DrawnMap : BidirectionalGraph<DrawnMapNode, DrawnEdge>
    {
        public void Populate(Map map)
        {
            foreach(var edge in map.Edges)
            {
                DrawnMapNode from = new DrawnMapNode(edge.Source);
                DrawnMapNode from2 = new DrawnMapNode(edge.Source);
                DrawnMapNode to = new DrawnMapNode(edge.Target);
                DrawnEdge drawnEdge = new DrawnEdge(from, to);
                if (!ContainsVertex(from))
                {
                    bool added = AddVertex(from);
                }
                if (!ContainsVertex(to))
                {
                    bool added = AddVertex(to);
                }
                var foo = ContainsVertex(from);
                var bar = ContainsVertex(to);
                if (!ContainsEdge(drawnEdge)) AddEdge(drawnEdge);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomacyLib.Models;
using QuickGraph;
using QuickGraph.Graphviz;

namespace DiplomacyLib.Visualize
{
    public class Renderer
    {
        public void Render(Map g)
        {
            var graphvizAlg = new GraphvizAlgorithm<MapNode, UndirectedEdge<MapNode>>(g);
            graphvizAlg.FormatVertex += GraphvizAlg_FormatVertex;
            
            string foo = graphvizAlg.Generate();
        }

        private void GraphvizAlg_FormatVertex(object sender, FormatVertexEventArgs<MapNode> e)
        {
            MapNodeRenderStyle style = MapNodeStyles.Get(e.Vertex);
            e.VertexFormatter.Position = style.Point;
            e.VertexFormatter.Shape = style.Shape;
            e.VertexFormatter.StrokeColor = style.GVColor;
        }
    }
}

using GraphX.Measure;
using GraphX.PCL.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiplomacyWpfControls.Drawing
{
    public class AbsoluteLayoutAlgorithm : IExternalLayout<DrawnMapNode, DrawnMapEdge>
    {
        private DrawnMap _map;
        private Dictionary<DrawnMapNode, Point> _vertexPositions;

        public AbsoluteLayoutAlgorithm(DrawnMap map)
        {
            _map = map;
            _vertexPositions = new Dictionary<DrawnMapNode, Point>();
        }

        public void Compute(CancellationToken cancellationToken)
        {
            foreach (var mapNode in _map.Vertices)
            {
                _vertexPositions[mapNode] = new Point(mapNode.RenderStyle.X, mapNode.RenderStyle.Y);
            }
        }

        public void ResetGraph(IEnumerable<DrawnMapNode> vertices, IEnumerable<DrawnMapEdge> edges)
        {
            // ??
        }

        public IDictionary<DrawnMapNode, Point> VertexPositions => _vertexPositions;
        public IDictionary<DrawnMapNode, Size> VertexSizes { get; set; }
        public bool NeedVertexSizes => false;
        public bool SupportsObjectFreeze => false;
    }
}

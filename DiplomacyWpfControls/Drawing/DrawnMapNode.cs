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
using System.Windows;
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
        public Shape OccupationShape { get; protected set; }
        public string Text { get; protected set; }
        public double OccupationSize { get; protected set; }
        public double GeometrySize { get; protected set; }

        public DrawnMapNode(MapNode source, Unit occupyingUnit, Powers owningPower)
        {
            OccupationSize = 40;
            GeometrySize = 200;
            MapNode = source;
            RenderStyle = MapNodeStyles.Get(source);
            ID = MapNode.SequenceNumber;
            Shape = (GraphX.PCL.Common.Enums.VertexShape)RenderStyle.Shape;
            Text = MapNode.ShortName;
            if(occupyingUnit != null)
            {
                AddOccupyingUnit(occupyingUnit);
            }

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

        private void AddOccupyingUnit(Unit unit)
        {
            if (unit == null || unit.Power == Powers.None)
            {
                OccupationShape = new Rectangle() { Width = 0, Height = 0 };
            }
            else
            {
                Brush powerBrush = ColorPicker.GetBrushForPower(unit.Power);
                if (unit.UnitType == UnitType.Army)
                    OccupationShape = new Ellipse() { Width = OccupationSize, Height = OccupationSize, Fill = powerBrush, Stroke = powerBrush };
                else
                {
                    List<Point> points = new List<Point>() { new Point(0,0), new Point(OccupationSize, 0), new Point(OccupationSize/2, OccupationSize) };
                    OccupationShape = new Polygon { Points = new PointCollection(points), Width = OccupationSize, Height = OccupationSize, Fill = powerBrush, Stroke = powerBrush };
                }
            }
        }
    }

    public class DrawnSeaNode : DrawnMapNode
    {
        public DrawnSeaNode(MapNode source, Unit unit, Powers owningPower) : base(source, unit, owningPower)
        {
            GeometryShape = new Ellipse()
            {
                StrokeThickness = MapNode.Territory.IsSupplyCenter ? Math.Max(GeometrySize / 10, 5) : 1,
                Width = GeometrySize,
                Height = GeometrySize / 2,
                Fill = ColorPicker.GetBrushForMapNode(source),
                Stroke = ColorPicker.GetBrushForPower(owningPower)
            };
        }
    }

    public class DrawnInlandNode : DrawnMapNode
    {
        public DrawnInlandNode(MapNode source, Unit unit, Powers owningPower) : base(source, unit, owningPower)
        {
            GeometryShape = new Rectangle()
            {
                StrokeThickness = MapNode.Territory.IsSupplyCenter ? Math.Max(GeometrySize / 10, 5) : 1,
                Width = GeometrySize,
                Height = GeometrySize / 2,
                Fill = ColorPicker.GetBrushForMapNode(source),
                Stroke = ColorPicker.GetBrushForPower(owningPower)
            };
        }
    }

    public class DrawnCoastNode : DrawnMapNode
    {
        public DrawnCoastNode(MapNode source, Unit unit, Powers owningPower) : base(source, unit, owningPower)
        {
            GeometryShape = new Rectangle()
            {
                StrokeThickness = MapNode.Territory.IsSupplyCenter ? Math.Max(GeometrySize / 10, 5) : 1,
                Width = GeometrySize,
                Height = GeometrySize / 2,
                Fill = ColorPicker.GetBrushForMapNode(source),
                Stroke = ColorPicker.GetBrushForPower(owningPower)
            };
        }
    }
}

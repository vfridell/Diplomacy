using DiplomacyLib.AI;
using DiplomacyWpfControls.Drawing;
using DiplomacyLib.Models;
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Common.Models;
using GraphX.PCL.Logic.Algorithms.OverlapRemoval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GraphX.PCL.Logic.Algorithms.LayoutAlgorithms;

namespace DiplomacyWpfControls
{
    /// <summary>
    /// Interaction logic for AllianceScenarioGraphControl.xaml
    /// </summary>
    public partial class AllianceScenarioGraphControl : UserControl
    {
        private bool _executing;

        public AllianceScenarioGraphControl()
        {
            InitializeComponent();
        }

        public void Draw(AllianceScenario allianceScenario, Powers focus)
        {
            try
            {
                _executing = true;

                var logicCore = new AllianceScenarioGXLogicCore();
                var drawnAllianceScenario = new DrawnAllianceScenario();
                drawnAllianceScenario.FocusPower = focus;
                drawnAllianceScenario.Populate(allianceScenario);
                logicCore.Graph = drawnAllianceScenario;

                /*
                logicCore.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.FR;
                logicCore.DefaultLayoutAlgorithmParams = logicCore.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.FR);
                ((FreeFRLayoutParameters)logicCore.DefaultLayoutAlgorithmParams).IdealEdgeLength = 100;
                ((FreeFRLayoutParameters)logicCore.DefaultLayoutAlgorithmParams).RepulsiveMultiplier = 1.5;
                ((FreeFRLayoutParameters)logicCore.DefaultLayoutAlgorithmParams).Seed = 23423423;
                */
                
                
                logicCore.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.KK;
                logicCore.DefaultLayoutAlgorithmParams = logicCore.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.KK);
                ((KKLayoutParameters)logicCore.DefaultLayoutAlgorithmParams).Width = 1000;
                ((KKLayoutParameters)logicCore.DefaultLayoutAlgorithmParams).Height = 1000;
                ((KKLayoutParameters)logicCore.DefaultLayoutAlgorithmParams).AdjustForGravity = true;
                

                ////Setup optional params
                logicCore.DefaultOverlapRemovalAlgorithmParams =
                    logicCore.AlgorithmFactory.CreateOverlapRemovalParameters(OverlapRemovalAlgorithmTypeEnum.FSA);
                ((OverlapRemovalParameters)logicCore.DefaultOverlapRemovalAlgorithmParams).HorizontalGap = 50;
                ((OverlapRemovalParameters)logicCore.DefaultOverlapRemovalAlgorithmParams).VerticalGap = 50;

                //This property sets edge routing algorithm that is used to build route paths according to algorithm logic.
                //For ex., SimpleER algorithm will try to set edge paths around vertices so no edge will intersect any vertex.
                logicCore.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.SimpleER;
                //This property sets async algorithms computation so methods like: Area.RelayoutGraph() and Area.GenerateGraph()
                //will run async with the UI thread. Completion of the specified methods can be catched by corresponding events:
                //Area.RelayoutFinished and Area.GenerateGraphFinished.
                logicCore.AsyncAlgorithmCompute = false;

                logicCore.EdgeCurvingEnabled = true;
                logicCore.EnableParallelEdges = true;
                logicCore.ParallelEdgeDistance = 50;

                //Finally assign logic core to GraphArea object
                GraphArea.LogicCore = logicCore;
                GraphArea.SetVerticesDrag(true);
                GraphArea.GenerateGraph();

                ZoomControl.ZoomToFill();
            }
            finally
            {
                _executing = false;
            }
        }

    }
}

﻿using DiplomacyLib;
using DiplomacyLib.Models;
using DiplomacyWpfControls.Drawing;
using GraphX.PCL.Common.Enums;
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

namespace DiplomacyWpfControls
{
    /// <summary>
    /// Interaction logic for BoardViewer.xaml
    /// </summary>
    public partial class BoardViewer : UserControl
    {
        private bool _executing = false;

        public BoardViewer()
        {
            InitializeComponent();
        }

        public void Draw(Board board)
        {
            try
            {
                _executing = true;
                DrawnMap drawnMap = new DrawnMap();
                drawnMap.Populate(board);

                var logicCore = new DiplomacyGXLogicCore();
                logicCore.Graph = drawnMap;
                logicCore.ExternalLayoutAlgorithm = new AbsoluteLayoutAlgorithm(drawnMap);

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
                logicCore.EnableParallelEdges = false;

                //Finally assign logic core to GraphArea object
                GraphArea.LogicCore = logicCore;
                GraphArea.SetVerticesDrag(false);
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

using DiplomacyLib.AI;
using DiplomacyLib.Models;
using QuickGraph;
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

namespace GameExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Board board = Board.GetInitialBoard();
            AllianceScenario allianceScenario = new AllianceScenario();
            IEnumerable<UnitMove> allMoves = board.GetUnitMoves();
            BoardMove boardMove = new BoardMove();
            foreach(var kvp in board.OccupiedMapNodes)
            {
                UnitTargetCalculator targetCalculator = new UnitTargetCalculator();
                List<MapNode> path = targetCalculator.GetUnitTargetPath(board, kvp.Key, allianceScenario);
                MapNode moveTarget = path[1];
                UnitMove currentMove = allMoves.FirstOrDefault(um => um.Edge.Source == kvp.Key && um.Edge.Target == moveTarget);
                if (currentMove != null)
                {
                    boardMove.Add(currentMove);
                }
            }
            boardMove.FillHolds(board);
            

        }
    }
}

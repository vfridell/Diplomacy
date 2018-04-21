using DiplomacyLib;
using DiplomacyLib.Analysis;
using DiplomacyLib.Analysis.Features;
using DiplomacyLib.Models;
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

namespace DiplomacyMapViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            Board board = Board.GetInitialBoard();
            BoardMove moves = new BoardMove();
            //moves.Add(board.GetMove("tri", "ven"));
            //moves.Add(board.GetMove("ven", "pie"));
            //moves.Add(board.GetMove("ber", "kie"));
            //moves.Add(board.GetMove("kie", "den"));
            //moves.Add(board.GetMove("mun", "ruh"));
            //moves.Add(board.GetMove("stp_sc", "bot"));
            //moves.Add(board.GetMove("sev", "rum"));
            //moves.FillHolds(board);
            //board.ApplyMoves(moves);
            //board.EndTurn();


            //moves.Clear();
            //moves.Add(board.GetMove("bot", "swe"));
            //moves.Add(board.GetMove("kie", "hol"));
            //moves.Add(board.GetMove("ruh", "bel"));
            //moves.FillHolds(board);
            //board.ApplyMoves(moves);
            //board.EndTurn();

            FeatureToolCollection toolCollection = new FeatureToolCollection();
            toolCollection.Add(new RelativeTerritoryStrengths());
            FeatureMeasurementCollection measurements = toolCollection.GetMeasurements(board);

            BoardViewer.Draw(board, measurements);
        }


        private void SaveGraphButton_Click(object sender, RoutedEventArgs e)
        {
            BoardViewer.SaveGraph();

        }
    }
}

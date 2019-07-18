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
            //1901 Spring
            BoardMove moves = new BoardMove();            moves.Add(board.GetMove("bud", "ser"));            moves.Add(board.GetMove("edi", "nth"));            moves.Add(board.GetMove("lvp", "wal"));            moves.Add(board.GetMove("mar", "spa"));            moves.Add(board.GetMove("par", "bur"));            moves.Add(board.GetMove("ber", "kie"));            moves.Add(board.GetMove("kie", "den"));            moves.Add(board.GetMove("mun", "ruh"));            moves.Add(board.GetMove("nap", "ion"));            moves.Add(board.GetMove("rom", "apu"));            //A Ven H ? SUCCEEDS            moves.Add(board.GetMove("mos", "stp"));            moves.Add(board.GetMove("sev", "rum"));            moves.Add(board.GetMove("stp_sc", "fin"));            moves.Add(board.GetMove("war", "lvn"));            moves.Add(board.GetMove("ank", "con"));            moves.Add(board.GetMove("con", "bul"));            //A Smy H ? SUCCEEDS            moves.FillHolds(board);            board.ApplyMoves(moves, true);            board.EndTurn();

            //1901 Fall
            moves = new BoardMove();
            moves.Add(board.GetMove("bul", "gre"));
            moves.Add(board.GetMove("tri", "adr"));
            moves.Add(board.GetMove("vie", "tri"));
            moves.Add(board.GetMove("lon", "nth"));
            moves.Add(board.GetMove("nth", "nwg"));
            moves.Add(board.GetMove("wal", "yor"));
            moves.Add(board.GetMove("bre", "mao"));
            moves.Add(board.GetMove("bur", "bel"));
            //A Spa H ? SUCCEEDS
            moves.Add(board.GetMove("den", "swe"));
            moves.Add(board.GetMove("kie", "mun"));
            moves.Add(board.GetMove("ruh", "hol"));
            moves.Add(board.GetConvoyMove("apu", "tun", "ion"));
            //A Ven H ? SUCCEEDS
            moves.Add(board.GetMove("fin", "bot"));
            //A Lvn H ? SUCCEEDS
            //F Rum H ? SUCCEEDS
            moves.Add(board.GetMove("stp", "nwy"));
            moves.Add(board.GetMove("con", "bul_sc"));
            moves.Add(board.GetMove("smy", "ank"));
            moves.FillHolds(board);
            board.ApplyMoves(moves, true);
            board.EndTurn();

            // 1901 Winter
            moves = new BoardMove();
            moves.Add(board.GetBuildMove("vie", UnitType.Army));
            moves.Add(board.GetBuildMove("bre", UnitType.Fleet));
            moves.Add(board.GetBuildMove("mar", UnitType.Fleet));
            moves.Add(board.GetBuildMove("ber", UnitType.Fleet));
            moves.Add(board.GetBuildMove("kie", UnitType.Fleet));
            moves.Add(board.GetBuildMove("nap", UnitType.Fleet));
            moves.Add(board.GetBuildMove("mos", UnitType.Army));
            moves.Add(board.GetBuildMove("stp_nc", UnitType.Fleet));
            moves.Add(board.GetBuildMove("smy", UnitType.Fleet));
            moves.Add(board.GetBuildMove("con", UnitType.Fleet));
            moves.FillHolds(board);
            board.ApplyMoves(moves, true);
            board.EndTurn();

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

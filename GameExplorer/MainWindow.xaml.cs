using DiplomacyLib.AI;
using DiplomacyLib.AI.Algorithms;
using DiplomacyLib.AI.Targeting;
using DiplomacyLib.Analysis;
using DiplomacyLib.Models;
using QuickGraph;
using System;
using System.Collections;
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

        private void SetupAllianceScenario()
        {
            allianceScenario = new AllianceScenario();
            allianceScenario.AddRelationship(Powers.Germany, Powers.Russia, .7d, .6d);
            allianceScenario.AddRelationship(Powers.Germany, Powers.Austria, .5d, .5d);
            allianceScenario.AddRelationship(Powers.Germany, Powers.England, .6d, .5d);
            allianceScenario.AddRelationship(Powers.Germany, Powers.France, .3d, .3d);
            allianceScenario.AddRelationship(Powers.Germany, Powers.Turkey, .5d, .5d);
            allianceScenario.AddRelationship(Powers.Germany, Powers.Italy, .5d, .5d);

            allianceScenario.AddRelationship(Powers.Russia, Powers.Austria, .5d, .5d);
            allianceScenario.AddRelationship(Powers.Russia, Powers.England, .7d, .7d);
            allianceScenario.AddRelationship(Powers.Russia, Powers.France, .5d, .5d);
            allianceScenario.AddRelationship(Powers.Russia, Powers.Turkey, .3d, .3d);
            allianceScenario.AddRelationship(Powers.Russia, Powers.Italy, .6d, .6d);

            allianceScenario.AddRelationship(Powers.Austria, Powers.England, .5d, .5d);
            allianceScenario.AddRelationship(Powers.Austria, Powers.France, .5d, .5d);
            allianceScenario.AddRelationship(Powers.Austria, Powers.Turkey, .5d, .6d);
            allianceScenario.AddRelationship(Powers.Austria, Powers.Italy, .5d, .5d);

            allianceScenario.AddRelationship(Powers.England, Powers.France, .6d, .6d);
            allianceScenario.AddRelationship(Powers.England, Powers.Turkey, .5d, .5d);
            allianceScenario.AddRelationship(Powers.England, Powers.Italy, .5d, .5d);

            allianceScenario.AddRelationship(Powers.France, Powers.Turkey, .5d, .5d);
            allianceScenario.AddRelationship(Powers.France, Powers.Italy, .5d, .5d);

            allianceScenario.AddRelationship(Powers.Turkey, Powers.Italy, .6d, .6d);
        }

        private void SetupBoard()
        {

            initialBoard = Board.GetInitialBoard();
            //1901 Spring
            BoardMove moves = new BoardMove();            moves.Add(initialBoard.GetMove("bud", "ser"));            moves.Add(initialBoard.GetMove("edi", "nth"));            moves.Add(initialBoard.GetMove("lvp", "wal"));            moves.Add(initialBoard.GetMove("mar", "spa"));            moves.Add(initialBoard.GetMove("par", "bur"));            moves.Add(initialBoard.GetMove("ber", "kie"));            moves.Add(initialBoard.GetMove("kie", "den"));            moves.Add(initialBoard.GetMove("mun", "ruh"));            moves.Add(initialBoard.GetMove("nap", "ion"));            moves.Add(initialBoard.GetMove("rom", "apu"));
            //A Ven H ? SUCCEEDS
            moves.Add(initialBoard.GetMove("mos", "stp"));            moves.Add(initialBoard.GetMove("sev", "rum"));            moves.Add(initialBoard.GetMove("stp_sc", "fin"));            moves.Add(initialBoard.GetMove("war", "lvn"));            moves.Add(initialBoard.GetMove("ank", "con"));            moves.Add(initialBoard.GetMove("con", "bul"));
            //A Smy H ? SUCCEEDS
            moves.FillHolds(initialBoard);            initialBoard.ApplyMoves(moves, true);            initialBoard.EndTurn();

            //1901 Fall
            moves = new BoardMove();
            moves.Add(initialBoard.GetMove("bul", "gre"));
            moves.Add(initialBoard.GetMove("tri", "adr"));
            moves.Add(initialBoard.GetMove("vie", "tri"));
            moves.Add(initialBoard.GetMove("lon", "nth"));
            moves.Add(initialBoard.GetMove("nth", "nwg"));
            moves.Add(initialBoard.GetMove("wal", "yor"));
            moves.Add(initialBoard.GetMove("bre", "mao"));
            moves.Add(initialBoard.GetMove("bur", "bel"));
            //A Spa H ? SUCCEEDS
            moves.Add(initialBoard.GetMove("den", "swe"));
            moves.Add(initialBoard.GetMove("kie", "mun"));
            moves.Add(initialBoard.GetMove("ruh", "hol"));
            moves.Add(initialBoard.GetConvoyMove("apu", "tun", "ion"));
            //A Ven H ? SUCCEEDS
            moves.Add(initialBoard.GetMove("fin", "bot"));
            //A Lvn H ? SUCCEEDS
            //F Rum H ? SUCCEEDS
            moves.Add(initialBoard.GetMove("stp", "nwy"));
            moves.Add(initialBoard.GetMove("con", "bul_sc"));
            moves.Add(initialBoard.GetMove("smy", "ank"));
            moves.FillHolds(initialBoard);
            initialBoard.ApplyMoves(moves, true);
            initialBoard.EndTurn();

            // 1901 Winter
            moves = new BoardMove();
            moves.Add(initialBoard.GetBuildMove("vie", UnitType.Army));
            moves.Add(initialBoard.GetBuildMove("bre", UnitType.Fleet));
            moves.Add(initialBoard.GetBuildMove("mar", UnitType.Fleet));
            moves.Add(initialBoard.GetBuildMove("ber", UnitType.Fleet));
            moves.Add(initialBoard.GetBuildMove("kie", UnitType.Fleet));
            moves.Add(initialBoard.GetBuildMove("nap", UnitType.Fleet));
            moves.Add(initialBoard.GetBuildMove("mos", UnitType.Army));
            moves.Add(initialBoard.GetBuildMove("stp_nc", UnitType.Fleet));
            moves.Add(initialBoard.GetBuildMove("smy", UnitType.Fleet));
            moves.Add(initialBoard.GetBuildMove("con", UnitType.Fleet));
            moves.FillHolds(initialBoard);
            initialBoard.ApplyMoves(moves, true);
            initialBoard.EndTurn();

        }

        Board initialBoard;
        List<Board> allFutureBoards;
        AllianceScenario allianceScenario;
        ProbabilisticFuturesAlgorithm futuresAlgorithm = new ProbabilisticFuturesAlgorithm();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetupBoard();
            SetupAllianceScenario();
            //initialBoard = Board.GetInitialBoard();
            allFutureBoards = new List<Board>() { initialBoard };
            allFutureBoards.AddRange(initialBoard.GetFutures(allianceScenario, futuresAlgorithm));

            BoardViewer.Draw(initialBoard);
            UpdateDetailsTextBlock(initialBoard);
            AllianceScenarioGraphControl.Draw(allianceScenario, Powers.None);
            BoardsListView.ItemsSource = allFutureBoards;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (BoardsListView.SelectedItem != null)
            {
                Board board = (Board)BoardsListView.SelectedItem;
                allFutureBoards = new List<Board>() { board };
                allFutureBoards.AddRange( board.GetFutures(allianceScenario, futuresAlgorithm));
                
                BoardViewer.Draw(board);
                UpdateDetailsTextBlock(board);
                BoardsListView.ItemsSource = allFutureBoards;
            }
        }

        private void UpdateDetailsTextBlock(Board board)
        {
            BasicScorer basicScorer = new BasicScorer();
            PowersDictionary<double> scores = basicScorer.GetScore(board);

            DetailsTextBlock.Text = $"{board.Season} {board.Year}\t({board.GetHashCode()})\n";
            foreach(var kvp in scores)
            {
                DetailsTextBlock.Text += $"{kvp.Key}\t\t: {kvp.Value:.###}\n";
            }
        }

        private void BoardsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(BoardsListView.SelectedItem != null)
            {
                Board board = (Board)BoardsListView.SelectedItem;
                BoardViewer.Draw(board);
                UpdateDetailsTextBlock(board);
            }
        }
    }

}

using DiplomacyLib.AI;
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

        Board initialBoard;
        List<Board> allFutureBoards;
        AllianceScenario allianceScenario = new AllianceScenario();
        UnitTargetCalculator unitTargetCalculator = new UnitTargetCalculator();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            initialBoard = Board.GetInitialBoard();
            allFutureBoards = new List<Board>() { initialBoard };
            allFutureBoards.AddRange(initialBoard.GetFutures(allianceScenario, unitTargetCalculator));

            BoardViewer.Draw(initialBoard);
            UpdateDetailsTextBlock(initialBoard);
            AllianceScenarioGraphControl.Draw(allianceScenario, Powers.None);
            BoardsListView.ItemsSource = allFutureBoards;
        }

        int _num = 1;
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (BoardsListView.SelectedItem != null)
            {
                Board board = (Board)BoardsListView.SelectedItem;
                allFutureBoards = new List<Board>() { board };
                allFutureBoards.AddRange( board.GetFutures(allianceScenario, unitTargetCalculator));
                
                BoardViewer.Draw(board);
                UpdateDetailsTextBlock(board);
                BoardsListView.ItemsSource = allFutureBoards;
            }
        }

        private void UpdateDetailsTextBlock(Board board)
        {
            DetailsTextBlock.Text = $"{board.Season} {board.Year}\n";
            DetailsTextBlock.Text += $"{board.GetHashCode()}";
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

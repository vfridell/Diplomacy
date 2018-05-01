﻿using DiplomacyLib.AI;
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

        Board initialBoard;
        List<Board> allFutureBoards;
        AllianceScenario allianceScenario = new AllianceScenario();
        ProbabilisticFuturesAlgorithm futuresAlgorithm = new ProbabilisticFuturesAlgorithm();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            initialBoard = Board.GetInitialBoard();
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

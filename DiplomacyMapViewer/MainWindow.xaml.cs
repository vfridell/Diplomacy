﻿using DiplomacyLib;
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
            Board initialBoard = Board.GetInitialBoard();
            BoardViewer.Draw(initialBoard);
        }


        private void SaveGraphButton_Click(object sender, RoutedEventArgs e)
        {
            BoardViewer.SaveGraph();

        }
    }
}

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
            FeatureToolCollection toolCollection = new FeatureToolCollection();
            toolCollection.Add(new TerritoryStrengths());
            FeatureMeasurementCollection measurements = toolCollection.GetMeasurements(board);

            BoardViewer.Draw(board, measurements);
        }


        private void SaveGraphButton_Click(object sender, RoutedEventArgs e)
        {
            BoardViewer.SaveGraph();

        }
    }
}

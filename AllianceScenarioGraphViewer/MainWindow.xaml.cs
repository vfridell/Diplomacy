using System.Windows;
using DiplomacyLib.AI;

namespace AllianceScenarioGraphViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AllianceScenario allianceScenario = AllianceScenario.GetRandomAllianceScenario();
            AllianceScenarioGraphControl.Draw(allianceScenario, DiplomacyLib.Models.Powers.Italy);
        }
    }
}

using DiplomacyLib.AI;
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
    /// Interaction logic for AllianceScenarioControl.xaml
    /// </summary>
    public partial class AllianceScenarioControl : UserControl
    {
        public AllianceScenarioControl()
        {
            InitializeComponent();
        }

        private AllianceScenario _allianceScenario;
        public AllianceScenario AllianceScenario
        {
            get { return _allianceScenario; }
            set { _allianceScenario = value; }
        }

        public void Refresh()
        {
            if (null == _allianceScenario) return;


        }
    }
}

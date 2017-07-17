using ITHelpDesk.Modules.Hardware.ViewModels;
using System.Windows.Controls;

namespace ITHelpDesk.Modules.Hardware.Views
{
    /// <summary>
    /// Interaction logic for HardwareDetail.xaml
    /// </summary>
    public partial class HardwareDetail : UserControl
    {
        public HardwareDetail(HardwareViewModel hvm)
        {
            InitializeComponent();
            DataContext = hvm;
        }
    }
}

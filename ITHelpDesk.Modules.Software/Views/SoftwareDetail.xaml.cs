using ITHelpDesk.Modules.Software.ViewModels;
using System.Windows.Controls;

namespace ITHelpDesk.Modules.Software.Views
{
    /// <summary>
    /// Interaction logic for SoftwareDetail.xaml
    /// </summary>
    public partial class SoftwareDetail : UserControl
    {
        public SoftwareDetail(SoftwareViewModel svm)
        {
            InitializeComponent();
            DataContext = svm;
        }
    }
}

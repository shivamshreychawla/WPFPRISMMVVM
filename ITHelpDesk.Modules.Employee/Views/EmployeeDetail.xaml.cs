using ITHelpDesk.Modules.Employee.ViewModels;
using System.Windows.Controls;

namespace ITHelpDesk.Modules.Employee.Views
{
    /// <summary>
    /// Interaction logic for EmployeeDetail.xaml
    /// </summary>
    public partial class EmployeeDetail : UserControl
    {
        public EmployeeDetail(EmployeeViewModel evm)
        {
            InitializeComponent();
            DataContext = evm;
        }
    }
}

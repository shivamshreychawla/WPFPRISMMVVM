using ITHelpDesk.Module.Navigation.ViewModels;
using System.Windows.Controls;

namespace ITHelpDesk.Module.Navigation.Views
{
    
    public partial class NavigationBar : UserControl
    {
        public NavigationBar(NavigationViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}

using ITHelpDesk.Module.RequestM.ViewModels;
using System.Windows.Controls;

namespace ITHelpDesk.Module.RequestM.Views
{

    public partial class RequestDetail : UserControl
    {
        public RequestDetail(RequestViewModel svm)
        {
            InitializeComponent();
            DataContext = svm;
        }
    }
}

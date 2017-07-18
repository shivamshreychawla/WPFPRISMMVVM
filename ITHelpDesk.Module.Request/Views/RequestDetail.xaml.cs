using ITHelpDesk.Module.Request.ViewModels;
using System.Windows.Controls;

namespace ITHelpDesk.Module.Request.Views
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

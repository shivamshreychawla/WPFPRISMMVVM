using System.Collections.Generic;
using System.Collections.ObjectModel;
using ITHelpDesk.Common.Infrastructure.Models;
using ITHelpDesk.Common.Infrastructure.Services;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Events;
using ITHelpDesk.Common.Infrastructure.Events;
using ITHelpDesk.Common.Infrastructure;

namespace ITHelpDesk.Module.Request.ViewModels
{
    public class RequestViewModel : ViewModelBase
    {
        #region Private Fields
        private ObservableCollection<ITRequest> allRequest;
        private IEmployeeService employeeService;
        private ITRequest selectedRequest;
        private bool isDetailVisible = false;
        IEventAggregator eventAggregator;       
        #endregion

        public RequestViewModel(IEmployeeService employeeService,IEventAggregator eventAggregator)
        {
            Title = "All Requests";
            this.employeeService = employeeService;
            this.eventAggregator = eventAggregator;
            eventAggregator.GetEvent<EmployeeUpdatedEvent>().Subscribe(refereshModuleHandler);
            
            fetchAllRequest();
        }

        private void refereshModuleHandler(int empId)
        {
            fetchAllRequest();
        }

       #region Public Properties
        public ObservableCollection<ITRequest> AllRequests
        {
            get { return allRequest; }
            set
            {
                allRequest = value;
                RaisePropertyChanged("AllRequests");
            }
        }
        public ITRequest SelectedRequest
        {
            get { return selectedRequest; }
            set
            {
              
                selectedRequest = value;
                IsDetailVisible = true;
                RaisePropertyChanged("SelectedRequest");
                
            }
        }
        public bool IsDetailVisible
        {
            get { return isDetailVisible; }
            set
            {
                isDetailVisible = value;
                RaisePropertyChanged("IsDetailVisible");
            }
        }

        #endregion

       #region Private Method
        private void fetchAllRequest()
        {
            List<ITRequest> listOfRequest = employeeService.GetAllRequest();
            AllRequests = new ObservableCollection<ITRequest>(listOfRequest);
           
        }
        #endregion



        
    }
}

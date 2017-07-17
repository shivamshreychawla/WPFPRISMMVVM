using System.Collections.Generic;
using System.Collections.ObjectModel;
using ITHelpDesk.Common.Infrastructure.Models;
using ITHelpDesk.Common.Infrastructure.Services;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Events;
using ITHelpDesk.Common.Infrastructure.Events;

namespace ITHelpDesk.Module.RequestM.ViewModels
{
    public class RequestViewModel : NotificationObject 
    {
        #region Private Fields
        private ObservableCollection<Request> allRequest;
        private IEmployeeService employeeService;
        private Request selectedRequest;
        private bool isDetailVisible = false;
        IEventAggregator eventAggregator;       
        #endregion

        public RequestViewModel(IEmployeeService employeeService,IEventAggregator eventAggregator)
        {
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
        public ObservableCollection<Request> AllRequests
        {
            get { return allRequest; }
            set
            {
                allRequest = value;
                RaisePropertyChanged("AllRequests");
            }
        }
        public Request SelectedRequest
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
            List<Request> listOfRequest = employeeService.GetAllRequest();
            AllRequests = new ObservableCollection<Request>(listOfRequest);
           
        }
        #endregion



        
    }
}

using ITHelpDesk.Common.Infrastructure.Events;
using ITHelpDesk.Common.Infrastructure.Services;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace ITHelpDesk.Modules.Employee.ViewModels
{
    public class EmployeeViewModel : NotificationObject
    {
        #region Private Fields

        private ITHelpDesk.Common.Infrastructure.Models.Employee employee;
        private int employeeId;
        private string employeeName;
        private string managerName;
        private string workStation;
        IEmployeeService employeeService;
        private IEventAggregator eventAggregator;
        #endregion

        public EmployeeViewModel(IEmployeeService employeeService, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.employeeService = employeeService;
        }

        #region Public Properties
        public int EmployeeId
        {
            get { return employeeId; }

            set
            {
                if (value != 0)
                {
                    employeeId = value;
                    fetchEmployeeDetail(employeeId);
                    RaisePropertyChanged("EmployeeId");


                }
                eventAggregator.GetEvent<EmployeeUpdatedEvent>().Publish(value);       

            }
        }
        public string EmployeeName
        {
            get { return employeeName; }

            set
            {
                employeeName = value;
                RaisePropertyChanged("EmployeeName");
            }
        }
        public string ManagerName
        {
            get { return managerName; }

            set
            {
                managerName = value;
                RaisePropertyChanged("ManagerName");
            }
        }
        public string WorkStation
        {
            get { return workStation; }

            set
            {
                workStation = value;
                RaisePropertyChanged("WorkStation");
            }
        }
        #endregion

        #region Private Method
        private void fetchEmployeeDetail(int employeeId)
        {
            employee = employeeService.GetCurrentEmployeeDetail(employeeId);
            //employee = employeeService.GetCurrentEmployeeDetail();           
            if (employee == null)
            {
                EmployeeName = string.Empty;
                ManagerName = string.Empty;
                WorkStation = string.Empty;
            }
            else
            {
                string mgrName = employeeService.GetManagerNameOfCurrentEmp();
                ManagerName = mgrName;
                EmployeeName = employee.EmpName;
                WorkStation = employee.WorkStation;
            }
           
        }
        #endregion
    }
}

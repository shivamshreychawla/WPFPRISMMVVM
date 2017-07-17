using System.Collections.Generic;
using ITHelpDesk.Common.Infrastructure.Models;
using ITHelpDesk.Common.Infrastructure.Repository;
using Microsoft.Practices.Prism.Events;
using ITHelpDesk.Common.Infrastructure.Events;

namespace ITHelpDesk.Common.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IHelpDeskRepository dataSource;
        private IEventAggregator eventAggregator;
        public int CurrentEmployeeId { get; set; }
        public EmployeeService(IHelpDeskRepository repository, IEventAggregator eventAggregator)
        {
            this.dataSource = repository;
            this.eventAggregator = eventAggregator;
        }

        private void employeeUpdatedHandler(int empId)
        {
            CurrentEmployeeId = empId;
        }       
        public Employee GetCurrentEmployeeDetail(int currentEmployeeId)
        {
            CurrentEmployeeId = currentEmployeeId;
            return dataSource.GetEmployeeById(CurrentEmployeeId);
        }
        public string GetManagerNameOfCurrentEmp()
        {
            return dataSource.GetManagerNameById(CurrentEmployeeId);
        }
        public List<SoftwareCategory> GetSoftwareCategories() 
        { return dataSource.GetSoftwareCategories(); }
        public List<SoftwareItems> GetSoftwareItemsByCategory(string softwareCategoryName) 
        { return dataSource.GetSoftwareItemsByCategory(softwareCategoryName); }
        public List<HardwareCategory> GetHardwareCategories()
        { return dataSource.GetHardwareCategories(); }
        public List<HardwareItems> GetHardwareItemsByCategory(string hardwareCategoryName) 
        { return dataSource.GetHardwareItemsByCategory(hardwareCategoryName); }
        public bool SaveSoftwareRequest(string selectedCategory, string selectedSoftware, string comment)
        {
            if (CurrentEmployeeId <= 0 || string.IsNullOrEmpty(selectedCategory) ||
                string.IsNullOrEmpty(selectedSoftware) || string.IsNullOrEmpty(comment))
            {
                return false;
            }
            else
                return dataSource.SaveSoftwareRequest(CurrentEmployeeId, selectedCategory, selectedSoftware, comment);
        }
        public bool SaveHardwareRequest(string selectedCategory, string selectedSoftware, string comment)
        {
            if (CurrentEmployeeId <= 0 || string.IsNullOrEmpty(selectedCategory) ||
               string.IsNullOrEmpty(selectedSoftware) || string.IsNullOrEmpty(comment))
            {
                return false;
            }
            else
                return dataSource.SaveHardwareRequest(CurrentEmployeeId, selectedCategory, selectedSoftware, comment);
        }
        public List<Request> GetAllRequest()
        {
            return dataSource.GetAllRequest(CurrentEmployeeId);
        }
    }
}

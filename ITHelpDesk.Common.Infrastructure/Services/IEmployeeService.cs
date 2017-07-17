using ITHelpDesk.Common.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITHelpDesk.Common.Infrastructure.Services
{
    public interface IEmployeeService
    {

        int CurrentEmployeeId { get; set; }
        Employee GetCurrentEmployeeDetail(int employeeId);
        string GetManagerNameOfCurrentEmp();
        List<SoftwareCategory> GetSoftwareCategories();
        List<SoftwareItems> GetSoftwareItemsByCategory(string softwareCategoryName);
        List<HardwareCategory> GetHardwareCategories();
        List<HardwareItems> GetHardwareItemsByCategory(string hardwareCategoryName);
        bool SaveSoftwareRequest(string selectedCategory, string selectedSoftware, string comment);
        bool SaveHardwareRequest(string selectedCategory, string selectedSoftware, string comment);
        List<Request> GetAllRequest();
    }
}

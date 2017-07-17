using ITHelpDesk.Common.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITHelpDesk.Common.Infrastructure.Repository
{
    public interface IHelpDeskRepository
    {
        Employee GetEmployeeById(int empId);
        string GetManagerNameById(int empId);
        List<SoftwareCategory> GetSoftwareCategories();
        List<SoftwareItems> GetSoftwareItemsByCategory(string softwareCategoryName);
        List<HardwareCategory> GetHardwareCategories();
        List<HardwareItems> GetHardwareItemsByCategory(string hardwareCategoryName);
        bool SaveSoftwareRequest(int empId, string selectedCategory, string selectedSoftware, string comment);
        bool SaveHardwareRequest(int empId, string selectedCategory, string selectedSoftware, string comment);
        List<Request> GetAllRequest(int empId);
    }
}

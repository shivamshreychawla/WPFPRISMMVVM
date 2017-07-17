using ITHelpDesk.Common.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ITHelpDesk.Common.Infrastructure.Repository
{
    public class HelpDeskXMLRepository : IHelpDeskRepository
    {

        private XDocument itHeplData;

        private string filePath = "../../../ITHelpDesk.Common.Infrastructure/App_Data/ITHelpDeskDB.xml";
        XElement ex;
        public HelpDeskXMLRepository()
        {
            ex = XElement.Load(filePath);
        }
        public Employee GetEmployeeById(int empId)
        {
            bool ifFileExist = File.Exists(filePath);
            itHeplData = XDocument.Load(filePath);

            var currentEmployee = (from t in itHeplData.Descendants("Employee")
                                   where ((int)t.Element("id") == empId)
                                   select new Employee
                                   {
                                       EmpID = (int)t.Element("id"),
                                       EmpName = t.Element("name").Value,
                                       ManagerID = (int)t.Element("managerId"),
                                       WorkStation = t.Element("workStation").Value
                                   }).FirstOrDefault();
            return currentEmployee;
        }
        public string GetManagerNameById(int empId)
        {
            bool ifFileExist = File.Exists(filePath);

            itHeplData = XDocument.Load(filePath);


            var employeeManagerId = (from t in itHeplData.Descendants("Employee")
                                     where ((int)t.Element("id") == empId)
                                     select
                                         (int)t.Element("managerId")
                                      ).FirstOrDefault();

            var managerName = (from t in itHeplData.Descendants("Employee")
                               where ((int)t.Element("id") == employeeManagerId)
                               select t.Element("name").Value).FirstOrDefault(); ;
            return managerName;
        }
        public List<SoftwareCategory> GetSoftwareCategories()
        {

            IEnumerable<XElement> items = from el in ex.Descendants("Softwares") select el;

            var allsoftwareCategory = (from t in items.Descendants("Category")
                                       select new SoftwareCategory
                                       {
                                           Id = (int)t.Element("categoryId"),
                                           Name = t.Element("categoryName").Value
                                       }).ToList();
            return allsoftwareCategory;
        }
        public List<SoftwareItems> GetSoftwareItemsByCategory(string softwareCategoryName)
        {


            IEnumerable<XElement> items = from el in ex.Descendants("Softwares") select el;

            var softwareList = from d in items.Descendants("Category")
                               where d.Element("categoryName").Value == softwareCategoryName
                               select new SoftwareCategory
                               {
                                   Id = (int)d.Element("categoryId"),
                                   Name = d.Element("categoryName").Value,
                                   Items = (from i in d.Descendants("Item")
                                            select new SoftwareItems
                                            {
                                                ItemId = (int)i.Element("itemId"),
                                                ItemName = i.Element("itemName").Value
                                            }).ToList() //or Array as you prefer
                               };


            return softwareList.FirstOrDefault().Items;
        }
        public List<HardwareCategory> GetHardwareCategories()
        {
            IEnumerable<XElement> items = from el in ex.Descendants("Hardwares") select el;

            var allHardwareCategory = (from t in items.Descendants("Category")
                                       select new HardwareCategory
                                       {
                                           Id = (int)t.Element("categoryId"),
                                           Name = t.Element("categoryName").Value
                                       }
                                      ).ToList();

            return allHardwareCategory;
        }
        public List<HardwareItems> GetHardwareItemsByCategory(string hardwareCategoryName)
        {
            IEnumerable<XElement> items = from el in ex.Descendants("Hardwares") select el;

            var hardwareList = from d in items.Descendants("Category")
                               where d.Element("categoryName").Value == hardwareCategoryName
                               select new HardwareCategory
                               {
                                   Id = (int)d.Element("categoryId"),
                                   Name = d.Element("categoryName").Value,

                                   Items = (from i in d.Descendants("Item")
                                            select new HardwareItems
                                            {
                                                ItemId = (int)i.Element("itemId"),
                                                ItemName = i.Element("itemName").Value
                                            }).ToList()
                               };


            return hardwareList.FirstOrDefault().Items;
        }
        public bool SaveSoftwareRequest(int empId, string selectedCategory, string selectedSoftware, string comment)
        {
            bool isSaveSuccessful;
            int newRequestId;
            try
            {
                IEnumerable<XElement> items = from el in ex.Descendants("RequestInfo") select el;

                if (items.Descendants("Request").FirstOrDefault() != null)
                {
                    var maxID = (from d in items.Descendants("Request")
                                 select (int)d.Element("Id")).Max();

                    newRequestId = maxID + 1;
                }
                else
                {
                    newRequestId = 0;
                }

                var insertPoint = from ip in ex.Descendants("RequestInfo") select ip;
                ex.Element("RequestInfo").Add(new XElement("Request",
                    new XElement("Id", newRequestId),
                    new XElement("employeeId", empId),
                    new XElement("type", "Software"),
                    new XElement("category", selectedCategory),
                    new XElement("item", selectedSoftware),
                    new XElement("comment", comment)
                    ));
                ex.Save(filePath);

                isSaveSuccessful = true;
            }
            catch (Exception ex)
            {
                isSaveSuccessful = false;
                throw ex;
            }

            return isSaveSuccessful;
        }
        public bool SaveHardwareRequest(int empId, string selectedCategory, string selectedSoftware, string comment)
        {
            bool isSaveSuccessful;
            int newRequestId;
            try
            {
                IEnumerable<XElement> items = from el in ex.Descendants("RequestInfo") select el;
                if (items.Descendants("Request").FirstOrDefault() != null)
                {
                    var maxID = (from d in items.Descendants("Request")
                                 select (int)d.Element("Id")).Max();

                    newRequestId = maxID + 1;
                }
                else
                {
                    newRequestId = 0;
                }

                var insertPoint = from ip in ex.Descendants("RequestInfo") select ip;
                ex.Element("RequestInfo").Add(new XElement("Request",
                    new XElement("Id", newRequestId),
                    new XElement("employeeId", empId),
                    new XElement("type", "Hardawre"),
                    new XElement("category", selectedCategory),
                    new XElement("item", selectedSoftware),
                    new XElement("comment", comment)
                    ));
                ex.Save(filePath);

                isSaveSuccessful = true;
            }
            catch (Exception ex)
            {
                isSaveSuccessful = false;
                throw ex;
            }
            return isSaveSuccessful;
        }
        public List<Request> GetAllRequest(int empId)
        {
            IEnumerable<XElement> items = from el in ex.Descendants("RequestInfo") select el;
            var allrequest = (from t in items.Descendants("Request")
                              where ((int)t.Element("employeeId") == empId)
                              select new Request
                              {
                                  Id = (int)t.Element("Id"),
                                  Type = t.Element("type").Value,
                                  Category = t.Element("category").Value,
                                  Item = t.Element("item").Value,
                                  Comment = t.Element("comment").Value
                              }).ToList();
            return allrequest;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITHelpDesk.Common.Infrastructure.Models
{
    public class Employee
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public int ManagerID { get; set; }
        public string WorkStation { get; set; }

    }
}

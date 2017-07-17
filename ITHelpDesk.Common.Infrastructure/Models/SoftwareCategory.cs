using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITHelpDesk.Common.Infrastructure.Models
{
    public class SoftwareCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SoftwareItems> Items { get; set; }
    }

    public class SoftwareItems
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
    }
}

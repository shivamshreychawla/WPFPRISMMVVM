using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITHelpDesk.Common.Infrastructure.Models
{
    public class HardwareCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<HardwareItems> Items { get; set; }
    }
    public class HardwareItems
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }

    }
}

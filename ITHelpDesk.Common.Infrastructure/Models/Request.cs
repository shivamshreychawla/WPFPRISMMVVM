using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITHelpDesk.Common.Infrastructure.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Item { get; set; }
        public string Comment { get; set; }

    }
}

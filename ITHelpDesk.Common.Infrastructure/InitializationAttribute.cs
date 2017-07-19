using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITHelpDesk.Common.Infrastructure
{
    public class InitializationAttribute : Attribute
    {

    }

    public class ShivamModuleAttribute : Attribute
    {
        public ShivamModuleAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMessages.HelperClass
{
    public class TypeHelper
    {

        public Type theType { get; set; }
        public string path { get; set; }

        public override string ToString()
        {
            return theType.Name;
        }
    }
}

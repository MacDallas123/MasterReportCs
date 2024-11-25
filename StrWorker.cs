using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterReport
{
    internal class StrWorker
    {
        public StrWorker() { }

        public static String Capitalize(String str)
        {
            if(String.IsNullOrEmpty(str)) return str;
            return char.ToUpper(str[0]) + str.Substring(1); 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 图书借还管理系统.Lib
{
    public static class StringChecker
    {
        public static String AddBookTitle(String input)
        {
            if (input == null)
                return null;
            String output = input;
            if (output.Substring(0,1)!="《")
            {
                output = "《" + output;
            }
            if (output.Substring(output.Length - 1,1)!="》")
            {
                output = output + "》";
            }
            return output;
        }
    }
}

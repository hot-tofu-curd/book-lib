using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 图书借还管理系统.Models.User
{
    public enum AccessEnum
    {
        Administrator,
        Normal

    }
    public class Student
    {
        public int StudentNumber { get; set; }
        public String Name { get; set; }
        public AccessEnum Access { get; set; }
        public String Password { get; set; }
    }
}

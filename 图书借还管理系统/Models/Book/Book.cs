using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 图书借还管理系统.Models.Book
{
    public class Book
    {
        public String Name { get; set; }
        public Guid GUID { get; set; }
        public int TotalNumber { get; set; }
        public int RemainNumber { get; set; }
    }
}

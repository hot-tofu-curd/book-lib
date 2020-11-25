using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 图书借还管理系统.Models.Record
{
    public class Record
    {
        public Guid RcdID { get; set; }
        public int StudentNumber { get; set; }
        public Guid BookID { get; set; }
        public DateTime BorrowTime { get; set; }
        public DateTime ReturnTime { get; set; }
        public bool Returned { get; set; }
    }
}

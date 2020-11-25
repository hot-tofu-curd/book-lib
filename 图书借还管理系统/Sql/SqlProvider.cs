using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace 图书借还管理系统.Sql
{
    public static class SqlProvider
    {
        private static String CoonStr = "Server = 127.0.0.1; Database = booklib; Uid = root; Pwd = TOOR; ";

        public static MySqlConnection GetMySqlConnection()
        {
            return new MySqlConnection(CoonStr);
        }


    }
}

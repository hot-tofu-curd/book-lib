using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using 图书借还管理系统.Models.Book;
using 图书借还管理系统.Sql;

namespace 图书借还管理系统.Lib
{
    public static class BookChecker
    {
        //判断是否存在此书籍
        public static bool existBook(String titledBookName)
        {
            using (MySqlConnection conn = SqlProvider.GetMySqlConnection())
            {
                conn.Open();
                var cmdstr = $"SELECT * from books WHERE Name='{titledBookName}';";
                MySqlCommand cmd1 = new MySqlCommand(cmdstr, conn);
                var reader = cmd1.ExecuteReader();
                if (reader.HasRows)
                {
                    conn.Close();
                    return true;
                }
                conn.Close();
                return false;
            }
        }
        public static bool existBook(BookQuery bookQuery)
        {
            using (MySqlConnection conn = SqlProvider.GetMySqlConnection())
            {
                String titledBookName = StringChecker.AddBookTitle(bookQuery.Name);
                conn.Open();
                var cmdstr = $"SELECT * from books WHERE Name='{titledBookName}'OR GUID='{bookQuery.guid}';";
                MySqlCommand cmd1 = new MySqlCommand(cmdstr, conn);
                var reader = cmd1.ExecuteReader();
                if (reader.HasRows)
                {
                    conn.Close();
                    return true;
                }
                conn.Close();
                return false;
            }
        }
    }
}

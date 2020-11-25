using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using 图书借还管理系统.Models.Book;
using 图书借还管理系统.Sql;
using MySql.Data.MySqlClient;
using 图书借还管理系统.Models.User;
using System.Data;
using 图书借还管理系统.Lib;

//这个控制器用来管理图书，分权限
namespace 图书借还管理系统.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        //判断是否存在此书籍
        private bool existBook(String titledBookName)
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

        #region 创建Book
        [HttpPost("createBook")]
        public ActionResult Post([FromBody] Book book)
        {
            var titledBookName = StringChecker.AddBookTitle(book.Name);
            String guid = null;

            if (existBook(titledBookName))
                return BadRequest(new { message = "已有重复书籍" });
            using (MySqlConnection conn = SqlProvider.GetMySqlConnection())
            {

                MySqlCommand cmd = new MySqlCommand("SELECT * from books;", conn);
                //int row = cmd.ExecuteNonQuery();
                //if(row==0)
                //{
                //    return BadRequest(new { message="插入失败"});
                //}
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                //创建DataSet类的对象
                DataSet dataset = new DataSet();

                //使用SQLDataAdapter对象sda将查询结果填充到DataTable对象ds中
                adapter.Fill(dataset);

                //创建SqlCommandBuilder类的对象
                MySqlCommandBuilder cmdBuilder = new MySqlCommandBuilder(adapter);


                DataTable dataTable = dataset.Tables[0];

                DataRow dataRow = dataTable.NewRow();
                guid= Guid.NewGuid().ToString();
                dataRow["GUID"] = guid;
                dataRow["Name"] = titledBookName;

                dataRow["TotalNumber"] = book.TotalNumber;
                dataRow["RemainNumber"] = book.RemainNumber;
                //向DataTable对象中添加一行
                dataset.Tables[0].Rows.Add(dataRow);
                //更新数据库
                if( adapter.Update(dataset)==0)
                {
                    conn.Close();
                    return BadRequest(new { message = "插入失败" });
                }

            }
            return Ok(new { message = "插入成功" ,guid=guid});
        }
        #endregion

        #region 删除图书
        [HttpPost("deleteBook")]
        public ActionResult deleteBook(BookQuery bookQuery)
        {
            String titledBookName = StringChecker.AddBookTitle(bookQuery.Name);
            using (MySqlConnection conn = SqlProvider.GetMySqlConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"DELETE  from books WHERE name='{titledBookName}'OR GUID='{bookQuery.guid}';", conn);
                var rowsCount=cmd.ExecuteNonQuery();
                if (rowsCount != 0)
                {
                    conn.Close();
                    return Ok(new { message = "删除成功" });
                }
                conn.Close();
                return BadRequest(new { message = "删除失败" });
            }

        }
        #endregion

        #region 获取所有图书
        //这个方法没用DataSet
        [HttpPost("getBooks")]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            Console.WriteLine("进入GetBook方法");

            List<Book> books = new List<Book>();
            MySqlDataReader reader;
            using (MySqlConnection conn = SqlProvider.GetMySqlConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from books;", conn);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString("GUID"));
                        var bookInList = new Book()
                        {
                            GUID = Guid.Parse(reader.GetString("GUID")),
                            Name = reader.GetString("Name"),
                            TotalNumber = reader.GetInt32("TotalNumber"),
                            RemainNumber = reader.GetInt32("RemainNumber")
                        };
                        books.Add(bookInList);
                    }
                }
            }


            return Ok(books);
        }
        #endregion

        #region 根据书名或者书的id查询书
        [HttpPost("queryBook")]
        public ActionResult QueryBook([FromBody] BookQuery bookQuery)
        {
            String titledBookName = StringChecker.AddBookTitle(bookQuery.Name);
            using (MySqlConnection conn = SqlProvider.GetMySqlConnection())
            {
                MySqlCommand cmd = new MySqlCommand($"SELECT * from books WHERE name='{titledBookName}'OR GUID='{bookQuery.guid}';", conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                //创建DataSet类的对象
                DataSet dataset = new DataSet();

                //使用SQLDataAdapter对象sda将查询结果填充到DataTable对象ds中
                adapter.Fill(dataset);

                MySqlCommandBuilder cmdBuilder = new MySqlCommandBuilder(adapter);

                DataRow dataRow = dataset.Tables[0].Rows[0];

                Book book = new Book()
                {
                    GUID = Guid.Parse(dataRow["GUID"].ToString()),
                    Name = dataRow["Name"].ToString(),
                    TotalNumber = (int)dataRow["TotalNumber"],
                    RemainNumber = (int)dataRow["RemainNumber"]
                };
                return Ok(book);
            }
        }
        #endregion
    }
}

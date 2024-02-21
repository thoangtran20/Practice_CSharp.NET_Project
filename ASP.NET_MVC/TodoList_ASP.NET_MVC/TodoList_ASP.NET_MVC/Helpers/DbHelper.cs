using System;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace TodoList_ASP.NET_MVC.Helpers
{
    public class DbHelper
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source =(localdb)\ProjectModels;Initial Catalog=TodoList;");
        }
    }
}

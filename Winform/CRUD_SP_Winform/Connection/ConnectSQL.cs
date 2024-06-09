using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public class ConnectSQL
    {
        public static string GetConnectionString()
        {
            return "Data Source=DESKTOP-11P8S1B\\SQLEXPRESS;Initial Catalog=TestSP_DB;Integrated Security=True";
        }
    }
}

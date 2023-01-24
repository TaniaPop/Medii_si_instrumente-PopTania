using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Medii_si_instrumente
{
    class MyConnection
    {
        public SqlConnection GetConnection()
        {
            return new SqlConnection { ConnectionString = @"Data Source = DESKTOP - 7ESVVP9\SQLEXPRESS; Initial Catalog = bij_shop; Integrated Security = True" };
        }
    }
}

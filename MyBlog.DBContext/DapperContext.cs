using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyBlog.DBContext
{
    public class DapperContext
    {
        private readonly string _connectionString;
        public DapperContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(_connectionString);
            }
        }
    }
}

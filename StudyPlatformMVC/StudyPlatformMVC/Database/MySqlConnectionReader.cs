using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace StudyPlatformMVC.Database
{
    public class MySqlConnectionReader
    {
        public MySqlConnection Connection;
        public MySqlDataReader Reader;
        public MySqlConnectionReader(MySqlConnection connection)
        {
            Connection = connection;
        }
    }
}
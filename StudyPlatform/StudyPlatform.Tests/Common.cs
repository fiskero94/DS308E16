using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyPlatform.Classes.Database;
using System.Configuration;
using StudyPlatformSQLSetup;

namespace StudyPlatform.Tests
{
    static class Common
    {
        public static void ResetTables()
        {
            Query query = new Query("SHOW TABLES;");
            MySqlConnectionReader connectionReader = query.Execute();
            List<string> tables = new List<string>();
            while (connectionReader.Reader.Read())
                tables.Add(connectionReader.Reader.GetString(0));
            foreach (string table in tables)
                Query.ExecuteQueryString("DROP TABLE IF EXISTS " + table + ";");
            Program.SetupTables(ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString());
            Program.SetupAdmin(ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString());
        }
    }
}

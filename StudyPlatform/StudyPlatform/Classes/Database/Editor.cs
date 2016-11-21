using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Database
{
    public static class Editor
    {
        public static void SetValue(string table, uint id, string variable, string value)
        {
            if (table == null || variable == null || value == null)
                throw new ArgumentNullException();

            Query query = new Query("UPDATE " + table + " SET " + variable + "='" + value + "' WHERE id='" + id + "';");
            query.Execute();
        }
    }
}
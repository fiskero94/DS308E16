using System;

namespace StudyPlatform.Classes.Database
{
    public static class Editor
    {
        public static void SetValue(string table, uint id, string variable, string value)
        {
            if (table == null || variable == null || value == null)
                throw new ArgumentNullException();


            if (value == "TRUE" || value == "FALSE")
            {
                Query.ExecuteQueryString("UPDATE " + table + " SET " + variable + "=" + value + " WHERE id='" + id + "';");
            }
            else
            {
                Query.ExecuteQueryString("UPDATE " + table + " SET " + variable + "='" + value + "' WHERE id='" + id + "';");
            }
        }
    }
}
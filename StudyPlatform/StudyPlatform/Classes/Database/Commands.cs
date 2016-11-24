using System;
using System.Linq;
using StudyPlatform.Classes;

namespace StudyPlatform.Classes.Database
{
    public static class Commands
    {
        private static string[] _keywords = { "NULL", "TRUE", "FALSE", "NOW()" };
        private static string[] AddApostrophes(params string[] strings)
        {
            for (int i = 0; i < strings.Length; i++)
                if (!_keywords.Contains(strings[i]))
                    strings[i] = string.Concat("'", strings[i], "'");
            return strings;
        }

        public static void SetValue(string table, uint id, string variable, string value)
        {
            if (table == null || variable == null || value == null)
                throw new ArgumentNullException();
            value = AddApostrophes(value)[0];
            Query.ExecuteQueryString("UPDATE " + table + " SET " + variable + "=" + value + " WHERE id='" + id + "';");
        }
        public static void CreateTable(string tableName, params string[] variables)
        {
            string queryString = "USE studyplatform; CREATE TABLE " + tableName + " (";
            Common.AppendStringArray(ref queryString, ", ", variables);
            queryString += ");";
            Query.ExecuteQueryString(queryString);
        }
        public static void InsertInto(string tableName, params string[] values)
        {
            values = AddApostrophes(values);
            string queryString = "INSERT INTO studyplatform." + tableName + " VALUES(";
            Common.AppendStringArray(ref queryString, ", ", values);
            queryString += ");";
            Query.ExecuteQueryString(queryString);
        }
        public static void DeleteFrom(string tableName, string condition)
        {
            Query.ExecuteQueryString("DELETE FROM studyplatform." + tableName + " WHERE " + condition + ";");
        }
        public static void DropTable(string tableName)
        {
            Query.ExecuteQueryString("DROP TABLE studyplatform." + tableName + ";");
        }
        public static MySqlConnectionReader GetLatestRows(string tableName, uint count)
        {
            Query query = new Query("SELECT * FROM studyplatform." + tableName + " ORDER BY id DESC LIMIT " + count + ";");
            return query.Execute();
        }
    }
}
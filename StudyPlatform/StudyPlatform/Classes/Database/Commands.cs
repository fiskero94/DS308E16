using System;
using System.Linq;

namespace StudyPlatform.Classes.Database
{
    public static class Commands
    {
        private static string[] _keywords = { "NULL", "TRUE", "FALSE", "NOW()" };
        public static void SetValue(string table, uint id, string variable, string value)
        {
            if (table == null || variable == null || value == null)
                throw new ArgumentNullException();
            AddApostrophes(value);
            Query.ExecuteQueryString("UPDATE " + table + " SET " + variable + "=" + value + " WHERE id='" + id + "';");
        }
        public static void CreateTable(string tableName, params string[] variables)
        {
            string queryString = "USE studyplatform; CREATE TABLE " + tableName + " (";
            AppendStringArray(ref queryString, ", ", variables);
            queryString += ");";
            Query.ExecuteQueryString(queryString);
        }
        public static void InsertInto(string tableName, params string[] values)
        {
            AddApostrophes(values);
            string queryString = "INSERT INTO studyplatform." + tableName + " VALUES(";
            AppendStringArray(ref queryString, ", ", values);
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
        public static void AppendStringArray(ref string stringToAppendOn, string seperator, string[] strings)
        {
            foreach (string item in strings)
            {
                stringToAppendOn += item;
                stringToAppendOn += seperator;
            }
            stringToAppendOn = stringToAppendOn.TrimEnd(seperator.ToCharArray());
        }
        public static MySqlConnectionReader GetLatestRows(string tableName, int count)
        {
            Query query = new Query("SELECT * FROM studyplatform." + tableName + " ORDER BY id DESC LIMIT " + count + ";");
            return query.Execute();
        }
        private static string[] AddApostrophes(params string[] strings)
        {
            for (int i = 0; i < strings.Length; i++)
                if (!_keywords.Contains(strings[i]))
                    strings[i] = string.Concat("'", strings[i], "'");
            return strings;
        }
    }
}
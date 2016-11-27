using System;
using System.Linq;
using StudyPlatform.Classes;
using MySql.Data.MySqlClient;
using StudyPlatform.Classes.Exceptions;

namespace StudyPlatform.Classes.Database
{
    public static class Commands
    {
        private static readonly string[] Keywords = { "NULL", "TRUE", "FALSE", "NOW()" };
        private static string[] AddApostrophes(params string[] strings)
        {
            for (int i = 0; i < strings.Length; i++)
                if (!Keywords.Contains(strings[i]))
                    strings[i] = string.Concat("'", strings[i], "'");
            return strings;
        }

        public static void SetValue(string tableName, uint id, string variable, string value)
        {
            Common.EnsureNotNull(tableName, variable, value);
            value = AddApostrophes(value)[0];
            Query.ExecuteQueryString("UPDATE " + tableName + " SET " + variable + "=" + value + " WHERE ID='" + id + "';");
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
        public static MySqlConnectionReader GetLatestRows(string tableName, uint count)
        {
            Query query = new Query("SELECT * FROM studyplatform." + tableName + " ORDER BY ID DESC LIMIT " + count + ";");
            return query.Execute();
        }
        public static bool CheckNull(string tableName, uint id, string variable)
        {
            Common.EnsureNotNull(tableName, variable);
            Query query = new Query("SELECT * FROM " + tableName + " WHERE ID='" + id + "';");
            MySqlConnectionReader connectionReader = query.Execute();
            MySqlDataReader reader = connectionReader.Reader;
            if (reader.HasRows && reader.Read())
            {
                bool isNull = reader.IsDBNull(reader.GetOrdinal(variable));
                connectionReader.Connection.Close();
                return isNull;
            }
            connectionReader.Connection.Close();
            throw new InvalidIDException();
        }
    }
}
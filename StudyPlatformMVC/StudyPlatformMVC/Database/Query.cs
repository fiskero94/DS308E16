﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace StudyPlatformMVC.Database
{
    public class Query
    {
        private static string ConnectionString => ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString();
        private string _queryString;
        public string QueryString
        {
            get
            {
                return _queryString;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                _queryString = value;
            }
        }
        public Query(string queryString)
        {
            QueryString = queryString;
        }
        public MySqlConnectionReader Execute()
        {
            MySqlConnectionReader connectionReader = new MySqlConnectionReader(new MySqlConnection(ConnectionString));
            using (MySqlCommand command = connectionReader.Connection.CreateCommand())
            {
                connectionReader.Connection.Open();
                command.CommandText = QueryString;
                connectionReader.Reader = command.ExecuteReader();
            }
            return connectionReader;
        }
        public void ExecuteNonReader()
        {
            MySqlConnectionReader connectionReader = new MySqlConnectionReader(new MySqlConnection(ConnectionString));
            using (MySqlCommand command = connectionReader.Connection.CreateCommand())
            {
                connectionReader.Connection.Open();
                command.CommandText = QueryString;
                command.ExecuteNonQuery();
                connectionReader.Connection.Close();
            }
        }
        public static void ExecuteQueryString(string queryString)
        {
            Query query = new Query(queryString);
            query.ExecuteNonReader();
        }
    }
}
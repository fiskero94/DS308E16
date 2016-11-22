﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace StudyPlatform.Classes.Database
{
    public class Query
    {
        private string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ToString(); }
        }
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
                else
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
        public static MySqlConnectionReader ExecuteQueryString(string queryString)
        {
            Query query = new Query(queryString);
            return query.Execute();
        }
    }
}
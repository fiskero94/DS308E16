using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Secretary : Person
    {
        public Secretary(uint id, string name) : base(id, name)
        {

        }

        public static Secretary New(string name, string username, string password)
        {
            Creator.CreateSecretary(name, username, password);
            return GetLatest(1).Single();
        }

        public new static Secretary GetByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.Person WHERE id=" + id + " AND type='Secretary';");
            return Extractor.ExtractPersons(query.Execute()).Single() as Secretary;
        }
        public new static List<Secretary> GetAll()
        {
            Query query = new Query("SELECT * FROM studyplatform.Person WHERE type='Secretary';");
            return Extractor.ExtractPersons(query.Execute()).Cast<Secretary>().ToList();
        }
        public new static List<Secretary> GetByConditions(params string[] conditions)
        {
            string queryString = "SELECT * FROM studyplatform.Person WHERE type='Secretary' AND ";
            Common.AppendStringArray(ref queryString, " AND ", conditions);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractPersons(query.Execute()).Cast<Secretary>().ToList();
        }
        public new static List<Secretary> GetLatest(uint count) =>
            Extractor.ExtractPersons(Commands.GetLatestRows("Person WHERE type='Secretary'", count)).Cast<Secretary>().ToList();
    }
}
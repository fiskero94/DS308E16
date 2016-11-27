using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Teacher : Person
    {
        public Teacher(uint id, string name) : base(id, name)
        {

        }

        public List<Course> Courses
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public static Teacher New(string name, string username, string password)
        {
            Creator.CreateTeacher(name, username, password);
            return GetLatest(1).Single();
        }

        public new static Teacher GetByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.Person WHERE id=" + id + " AND type='Teacher';");
            return Extractor.ExtractPersons(query.Execute()).Single() as Teacher;
        }
        public new static List<Teacher> GetAll()
        {
            Query query = new Query("SELECT * FROM studyplatform.Person WHERE type='Teacher';");
            return Extractor.ExtractPersons(query.Execute()).Cast<Teacher>().ToList();
        }
        public new static List<Teacher> GetByConditions(params string[] conditions)
        {
            string queryString = "SELECT * FROM studyplatform.Person WHERE type='Teacher' AND ";
            Common.AppendStringArray(ref queryString, " AND ", conditions);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractPersons(query.Execute()).Cast<Teacher>().ToList();
        }
        public new static List<Teacher> GetLatest(uint count) =>
            Extractor.ExtractPersons(Commands.GetLatestRows("Person WHERE type='Teacher'", count)).Cast<Teacher>().ToList();
    }
}
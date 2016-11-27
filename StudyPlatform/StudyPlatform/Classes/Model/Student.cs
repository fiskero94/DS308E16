using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Student : Person
    {
        public Student(uint id, string name) : base(id, name)
        {

        }

        public List<Course> Courses
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public List<Assignment> Assignments
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public List<Lesson> Absences
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public static Student New(string name, string username, string password)
        {
            Creator.CreateStudent(name, username, password);
            return GetLatest(1).Single();
        }

        public new static Student GetByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.Person WHERE id=" + id + " AND type='Student';");
            return Extractor.ExtractPersons(query.Execute()).Single() as Student;
        }
        public new static List<Student> GetAll()
        {
            Query query = new Query("SELECT * FROM studyplatform.Person WHERE type='Student';");
            return Extractor.ExtractPersons(query.Execute()).Cast<Student>().ToList();
        }
        public new static List<Student> GetByConditions(params string[] conditions)
        {
            string queryString = "SELECT * FROM studyplatform.Person WHERE type='Student' AND ";
            Common.AppendStringArray(ref queryString, " AND ", conditions);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractPersons(query.Execute()).Cast<Student>().ToList();
        }
        public new static List<Student> GetLatest(uint count) =>
            Extractor.ExtractPersons(Commands.GetLatestRows("Person WHERE type='Student'", count)).Cast<Student>().ToList();
    }
}
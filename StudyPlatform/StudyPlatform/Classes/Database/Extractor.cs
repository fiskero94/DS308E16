using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;
using StudyPlatform.Classes.Exceptions;

namespace StudyPlatform.Classes.Database
{
    public static class Extractor
    {
        public static uint[] ExtractIDs(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<uint> ids = new List<uint>();
            while (reader.HasRows && reader.Read())
                ids.Add(reader.GetUInt32(0));
            return ids.ToArray();
        }
        public static List<Person> ExtractPersons(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<Person> persons = new List<Person>();
            while (reader.HasRows && reader.Read())
            {
                uint id = reader.GetUInt32(reader.GetOrdinal("id"));
                string name = reader.GetString(reader.GetOrdinal("name"));
                string type = reader.GetString(reader.GetOrdinal("type"));
                switch (type)
                {
                    case "student":
                        persons.Add(new Student(id, name));
                        break;
                    case "teacher":
                        persons.Add(new Teacher(id, name));
                        break;
                    case "secretary":
                        persons.Add(new Secretary(id, name));
                        break;
                    default:
                        throw new InvalidPersonTypeException();
                }
            }
            return persons;
        }
        public static List<Message> ExtractMessages(MySqlConnectionReader connectionReader)
        {
            throw new NotImplementedException();
        }
        public static List<News> ExtractNews(MySqlConnectionReader connectionReader)
        {

            throw new NotImplementedException();
        }
        public static List<Course> ExtractCourses(MySqlConnectionReader connectionReader)
        {
            throw new NotImplementedException();
        }
        public static List<Lesson> ExtractLessons(MySqlConnectionReader connectionReader)
        {
            throw new NotImplementedException();
        }
        public static List<Room> ExtractRooms(MySqlConnectionReader connectionReader)
        {
            throw new NotImplementedException();
        }
        public static List<AssignmentDescription> ExtractAssignmentDescriptions(MySqlConnectionReader connectionReader)
        {
            throw new NotImplementedException();
        }
        public static List<Assignment> ExtractAssignments(MySqlConnectionReader connectionReader)
        {
            throw new NotImplementedException();
        }
        public static List<AssignmentGrade> ExtractAssignmentGrades(MySqlConnectionReader connectionReader)
        {
            throw new NotImplementedException();
        }
        public static List<CourseGrade> ExtractCourseGrades(MySqlConnectionReader connectionReader)
        {
            throw new NotImplementedException();
        }
    }
}
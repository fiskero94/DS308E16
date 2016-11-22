using MySql.Data.MySqlClient;
using StudyPlatform.Classes.Exceptions;
using StudyPlatform.Classes.Model;
using System;
using System.Collections.Generic;

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
        public static string[] ExtractFilepaths(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<string> filepaths = new List<string>();
            while (reader.HasRows && reader.Read())
                filepaths.Add(reader.GetString(0));
            return filepaths.ToArray();
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
            MySqlDataReader reader = connectionReader.Reader;
            List<Message> messages = new List<Message>();
            while (reader.HasRows && reader.Read())
            {
                Person sender = Getters.GetPersonByID(reader.GetUInt32(reader.GetOrdinal("id")));
                string title = reader.GetString(reader.GetOrdinal("title"));
                string text = reader.GetString(reader.GetOrdinal("text"));    
                messages.Add(new Message(sender.ID, title, text));
            }
            return messages;
        }

        public static List<News> ExtractNews(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<News> news = new List<News>();
            while (reader.HasRows && reader.Read())
            {
                uint id = reader.GetUInt32(reader.GetOrdinal("id"));
                uint authorid = reader.GetUInt32(reader.GetOrdinal("authorid"));
                string title = reader.GetString(reader.GetOrdinal("title"));
                string text = reader.GetString(reader.GetOrdinal("text"));
                news.Add(new News(id, authorid, title, text));
            }
            return news;
        }
        public static List<Course> ExtractCourses(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<Course> courses = new List<Course>();
            while (reader.HasRows && reader.Read())
            {
                uint id = reader.GetUInt32(reader.GetOrdinal("id"));
                string name = reader.GetString(reader.GetOrdinal("name"));
                string description = reader.GetString(reader.GetOrdinal("description"));
                courses.Add(new Course(id, name, description));
            }
            return courses;
        }
        public static List<Lesson> ExtractLessons(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<Lesson> lessons = new List<Lesson>();
            while (reader.HasRows && reader.Read())
            {
                uint id = reader.GetUInt32(reader.GetOrdinal("id"));
                uint courseid = reader.GetUInt32("courseid");
                string description = reader.GetString(reader.GetOrdinal("description"));
                DateTime date = reader.GetDateTime(reader.GetOrdinal("date"));
                bool online = reader.GetBoolean(reader.GetOrdinal("online"));
                bool active = reader.GetBoolean(reader.GetOrdinal("active"));
                lessons.Add(new Lesson(id, courseid, date, description, online, active));
            }
            return lessons;
        }
        public static List<Room> ExtractRooms(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<Room> rooms = new List<Room>();
            while (reader.HasRows && reader.Read())
            {
                uint id = reader.GetUInt32(reader.GetOrdinal("id"));
                string name = reader.GetString(reader.GetOrdinal("name"));
                rooms.Add(new Room(id, name));
            }
            return rooms;
        }
        public static List<AssignmentDescription> ExtractAssignmentDescriptions(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<AssignmentDescription> assignmentdescriptions = new List<AssignmentDescription>();
            while (reader.HasRows && reader.Read())
            {
                uint id = reader.GetUInt32(reader.GetOrdinal("id"));
                uint courseid = reader.GetUInt32(reader.GetOrdinal("courseid"));
                string description = reader.GetString(reader.GetOrdinal("description"));
                DateTime date = reader.GetDateTime(reader.GetOrdinal("date"));
                assignmentdescriptions.Add(new AssignmentDescription(id, courseid, description, date));
            }
            return assignmentdescriptions;
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
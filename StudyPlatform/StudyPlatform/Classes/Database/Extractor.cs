using MySql.Data.MySqlClient;
using StudyPlatform.Classes.Exceptions;
using StudyPlatform.Classes.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudyPlatform.Classes.Database
{
    public static class Extractor
    {
        public static uint[] ExtractIDs(MySqlConnectionReader connectionReader, string field)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<uint> ids = new List<uint>();
            while (reader.HasRows && reader.Read())
                ids.Add(reader.GetUInt32(reader.GetOrdinal(field)));
            connectionReader.Connection.Close();
            return ids.ToArray();
        }
        public static List<string> ExtractFilepaths(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<string> filepaths = new List<string>();
            while (reader.HasRows && reader.Read())
                filepaths.Add(reader.GetString(reader.GetOrdinal("Filepath")));
            connectionReader.Connection.Close();
            return filepaths;
        }
        public static List<Person> ExtractPersons(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<Person> persons = new List<Person>();
            while (reader.HasRows && reader.Read())
            {
                uint id = reader.GetUInt32(reader.GetOrdinal("ID"));
                string name = reader.GetString(reader.GetOrdinal("Name"));
                string type = reader.GetString(reader.GetOrdinal("Type"));
                switch (type)
                {
                    case "Student":
                        persons.Add(new Student(id, name));
                        break;
                    case "Teacher":
                        persons.Add(new Teacher(id, name));
                        break;
                    case "Secretary":
                        persons.Add(new Secretary(id, name));
                        break;
                    default:
                        throw new InvalidPersonTypeException();
                }
            }
            connectionReader.Connection.Close();
            return persons;
        }
        public static List<Student> ExtractStudents(MySqlConnectionReader connectionReader) =>
            ExtractPersons(connectionReader).Cast<Student>().ToList();
        public static List<Teacher> ExtractTeachers(MySqlConnectionReader connectionReader) =>
            ExtractPersons(connectionReader).Cast<Teacher>().ToList();
        public static List<Secretary> ExtractSecretaries(MySqlConnectionReader connectionReader) =>
            ExtractPersons(connectionReader).Cast<Secretary>().ToList();
        public static List<Message> ExtractMessages(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<Message> messages = new List<Message>();
            while (reader.HasRows && reader.Read())
            {
                uint id = reader.GetUInt32(reader.GetOrdinal("ID"));
                uint senderID = reader.GetUInt32(reader.GetOrdinal("SenderID"));
                string title = reader.GetString(reader.GetOrdinal("Title"));
                string text = reader.GetString(reader.GetOrdinal("Text"));
                DateTime dateTime = reader.GetDateTime(reader.GetOrdinal("DateTime"));
                messages.Add(new Message(id, senderID, title, text, dateTime));
            }
            connectionReader.Connection.Close();
            return messages;
        }

        public static List<News> ExtractNews(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<News> news = new List<News>();
            while (reader.HasRows && reader.Read())
            {
                uint id = reader.GetUInt32(reader.GetOrdinal("ID"));
                uint authorID = reader.GetUInt32(reader.GetOrdinal("AuthorID"));
                string title = reader.GetString(reader.GetOrdinal("Title"));
                string text = reader.GetString(reader.GetOrdinal("Text"));
                DateTime dateTime = reader.GetDateTime(reader.GetOrdinal("DateTime"));
                news.Add(new News(id, authorID, title, text, dateTime));
            }
            connectionReader.Connection.Close();
            return news;
        }
        public static List<Course> ExtractCourses(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<Course> courses = new List<Course>();
            while (reader.HasRows && reader.Read())
            {
                uint id = reader.GetUInt32(reader.GetOrdinal("ID"));
                string name = reader.GetString(reader.GetOrdinal("Name"));
                string description = reader.GetString(reader.GetOrdinal("Description"));
                courses.Add(new Course(id, name, description));
            }
            connectionReader.Connection.Close();
            return courses;
        }
        public static List<Lesson> ExtractLessons(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<Lesson> lessons = new List<Lesson>();
            while (reader.HasRows && reader.Read())
            {
                uint id = reader.GetUInt32(reader.GetOrdinal("ID"));
                uint courseID = reader.GetUInt32("CourseID");
                string description = reader.GetString(reader.GetOrdinal("Description"));
                bool online = reader.GetBoolean(reader.GetOrdinal("Online"));
                bool cancelled = reader.GetBoolean(reader.GetOrdinal("Cancelled"));
                DateTime dateTime = reader.GetDateTime(reader.GetOrdinal("DateTime"));
                lessons.Add(new Lesson(id, courseID, description, online, cancelled, dateTime));
            }
            connectionReader.Connection.Close();
            return lessons;
        }
        public static List<Room> ExtractRooms(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<Room> rooms = new List<Room>();
            while (reader.HasRows && reader.Read())
            {
                uint id = reader.GetUInt32(reader.GetOrdinal("ID"));
                string name = reader.GetString(reader.GetOrdinal("Name"));
                rooms.Add(new Room(id, name));
            }
            connectionReader.Connection.Close();
            return rooms;
        }
        public static List<AssignmentDescription> ExtractAssignmentDescriptions(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<AssignmentDescription> assignmentdescriptions = new List<AssignmentDescription>();
            while (reader.HasRows && reader.Read())
            {
                uint id = reader.GetUInt32(reader.GetOrdinal("ID"));
                uint courseID = reader.GetUInt32(reader.GetOrdinal("CourseID"));
                string description = reader.GetString(reader.GetOrdinal("Description"));
                bool cancelled = reader.GetBoolean(reader.GetOrdinal("Cancelled"));
                DateTime deadline = reader.GetDateTime(reader.GetOrdinal("Deadline"));
                assignmentdescriptions.Add(new AssignmentDescription(id, courseID, description, cancelled, deadline));
            }
            connectionReader.Connection.Close();
            return assignmentdescriptions;
        }
        public static List<Assignment> ExtractAssignments(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<Assignment> assignments = new List<Assignment>();
            while (reader.HasRows && reader.Read())
            {
                uint id = reader.GetUInt32(reader.GetOrdinal("ID"));
                uint assignmentdescriptionID = reader.GetUInt32(reader.GetOrdinal("AssignmentDescriptionID"));
                uint studentid = reader.GetUInt32(reader.GetOrdinal("StudentID"));
                string comment = reader.GetString(reader.GetOrdinal("Comment"));
                DateTime date = reader.GetDateTime(reader.GetOrdinal("DateTime"));
                if (Commands.CheckNull("Assignment", id, "GradeID"))
                    assignments.Add(new Assignment(id, assignmentdescriptionID, studentid, comment, date));
                else
                {
                    uint gradeid = reader.GetUInt32(reader.GetOrdinal("GradeID"));
                    assignments.Add(new Assignment(id, assignmentdescriptionID, studentid, comment, gradeid, date));
                }
            }
            connectionReader.Connection.Close();
            return assignments;
        }
        public static List<AssignmentGrade> ExtractAssignmentGrades(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<AssignmentGrade> assignmentgrades = new List<AssignmentGrade>();
            while (reader.HasRows && reader.Read())
            {
                uint id = reader.GetUInt32(reader.GetOrdinal("ID"));
                uint assignmentID = reader.GetUInt32(reader.GetOrdinal("AssignmentID"));
                string grade = reader.GetString(reader.GetOrdinal("Grade"));
                string comment = reader.GetString(reader.GetOrdinal("Comment"));
                assignmentgrades.Add(new AssignmentGrade(id, assignmentID, grade, comment));
            }
            connectionReader.Connection.Close();
            return assignmentgrades;
        }
        public static List<CourseGrade> ExtractCourseGrades(MySqlConnectionReader connectionReader)
        {
            MySqlDataReader reader = connectionReader.Reader;
            List<CourseGrade> coursegrades = new List<CourseGrade>();
            while (reader.HasRows && reader.Read())
            {
                uint id = reader.GetUInt32(reader.GetOrdinal("ID"));
                uint courseID = reader.GetUInt32(reader.GetOrdinal("CourseID"));
                uint studentID = reader.GetUInt32(reader.GetOrdinal("StudentID"));
                string grade = reader.GetString(reader.GetOrdinal("Grade"));
                string comment = reader.GetString(reader.GetOrdinal("Comment"));
                coursegrades.Add(new CourseGrade(id, courseID, studentID, grade, comment));
            }
            connectionReader.Connection.Close();
            return coursegrades;
        }
    }
}
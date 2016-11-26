using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Model;

namespace StudyPlatform.Classes.Database
{
    public static class Lists
    {
        public static List<Person> Persons
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.Person;");
                return Extractor.ExtractPersons(query.Execute());
            }
        }
        public static List<Student> Students
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.Person WHERE Type='Student';");
                return Extractor.ExtractPersons(query.Execute()).Cast<Student>().ToList();
            }
        }
        public static List<Teacher> Teachers
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.Person WHERE Type='Teacher';");
                return Extractor.ExtractPersons(query.Execute()).Cast<Teacher>().ToList();
            }
        }
        public static List<Secretary> Secretaries
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.Person WHERE Type='Secretary';");
                return Extractor.ExtractPersons(query.Execute()).Cast<Secretary>().ToList();
            }
        }
        public static List<Message> Messages
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.Message;");
                return Extractor.ExtractMessages(query.Execute());
            }
        }
        public static List<News> News
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.News;");
                return Extractor.ExtractNews(query.Execute());
            }
        }
        public static List<Course> Courses
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.Course;");
                return Extractor.ExtractCourses(query.Execute());
            }
        }
        public static List<Lesson> Lessons
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.Lesson;");
                return Extractor.ExtractLessons(query.Execute());
            }
        }
        public static List<Room> Rooms
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.Room;");
                return Extractor.ExtractRooms(query.Execute());
            }
        }
        public static List<AssignmentDescription> AssignmentDescriptions
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.AssignmentDescription;");
                return Extractor.ExtractAssignmentDescriptions(query.Execute());
            }
        }
        public static List<Assignment> Assignments
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.Assignments;");
                return Extractor.ExtractAssignments(query.Execute());
            }
        }
        public static List<AssignmentGrade> AssignmentGrades
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.AssignmentGrade;");
                return Extractor.ExtractAssignmentGrades(query.Execute());
            }
        }
        public static List<CourseGrade> CourseGrades
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.CourseGrade;");
                return Extractor.ExtractCourseGrades(query.Execute());
            }
        }
    }
}
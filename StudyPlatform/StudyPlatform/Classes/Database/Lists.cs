using StudyPlatform.Classes.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public static class Lists
    {
        public static List<Person> Persons
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.persons;");
                return Extractor.ExtractPersons(query.Execute());
            }
        }
        public static List<Student> Students
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.persons WHERE type='student';");
                return Extractor.ExtractPersons(query.Execute()).Cast<Student>().ToList();
            }
        }
        public static List<Teacher> Teachers
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.persons WHERE type='teacher';");
                return Extractor.ExtractPersons(query.Execute()).Cast<Teacher>().ToList();
            }
        }
        public static List<Secretary> Secretaries
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.persons WHERE type='secretary';");
                return Extractor.ExtractPersons(query.Execute()).Cast<Secretary>().ToList();
            }
        }
        public static List<Message> Messages
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.messages;");
                return Extractor.ExtractMessages(query.Execute());
            }
        }
        public static List<News> News
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.news;");
                return Extractor.ExtractNews(query.Execute());
            }
        }
        public static List<AssignmentDescription> AssignmentDescriptions
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.assignmentdescriptions;");
                return Extractor.ExtractAssignmentDescriptions(query.Execute());
            }
        }

        public static List<Course> Courses
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.courses;");
                return Extractor.ExtractCourses(query.Execute());
            }
        }
        public static List<Room> Rooms
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.rooms;");
                return Extractor.ExtractRooms(query.Execute());
            }
        }
        public static List<Lesson> Lessons
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.lessons;");
                return Extractor.ExtractLessons(query.Execute());
            }
        }
        public static List<AssignmentGrade> AssignmentGrades
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.assignmentgrades;");
                return Extractor.ExtractAssignmentGrades(query.Execute());
            }
        }
        public static List<CourseGrade> CourseGrades
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.coursegrades;");
                return Extractor.ExtractCourseGrades(query.Execute());
            }
        }
        public static List<Assignment> Assignments
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.assignments;");
                return Extractor.ExtractAssignments(query.Execute());
            }
        }
    }
}
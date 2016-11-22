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
                return Persons.Where(person => person.GetType() == typeof(Student)).Cast<Student>().ToList();
            }
        }
        public static List<Teacher> Teachers
        {
            get
            {
                return Persons.Where(person => person.GetType() == typeof(Teacher)).Cast<Teacher>().ToList();
            }
        }
        public static List<Secretary> Secretaries
        {
            get
            {
                return Persons.Where(person => person.GetType() == typeof(Secretary)).Cast<Secretary>().ToList();
            }
        }
        public static List<Message> Messages
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public static List<News> News
        {
            get
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
        public static List<Lesson> lessons
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
                throw new NotImplementedException();
            }
        }
        public static List<CourseGrade> CourseGrades
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public static List<Assignment> Assignments
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
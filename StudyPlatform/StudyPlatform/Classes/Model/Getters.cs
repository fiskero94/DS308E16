using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public static class Getters
    {
        public static Person GetPersonByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.persons WHERE id=" + id + ";");
            return Extractor.ExtractPersons(query.Execute()).First();
        }
        public static List<Person> GetPersonsByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.persons WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractPersons(query.Execute());
        }
        public static Message GetMessageByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.messages WHERE id=" + id + ";");
            return Extractor.ExtractMessages(query.Execute()).First();
        }
        public static List<Message> GetMessagesByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.messages WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractMessages(query.Execute());
        }
        public static News GetNewsByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.news WHERE id=" + id + ";");
            return Extractor.ExtractNews(query.Execute()).First();
        }
        public static List<News> GetNewsByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.news WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractNews(query.Execute());
        }
        public static Course GetCourseByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.courses WHERE id=" + id + ";");
            return Extractor.ExtractCourses(query.Execute()).First();
        }
        public static List<Course> GetCoursesByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.courses WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractCourses(query.Execute());
        }
        public static Lesson GetLessonByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.lessons WHERE id=" + id + ";");
            return Extractor.ExtractLessons(query.Execute()).First();
        }
        public static List<Lesson> GetLessonsByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.lessons WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractLessons(query.Execute());
        }
        public static Room GetRoomByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.rooms WHERE id=" + id + ";");
            return Extractor.ExtractRooms(query.Execute()).First();
        }
        public static List<Room> GetRoomsByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.rooms WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractRooms(query.Execute());
        }
        public static AssignmentDescription GetAssignmentDescriptionByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.assignmentsdescriptions WHERE id=" + id + ";");
            return Extractor.ExtractAssignmentDescriptions(query.Execute()).First();
        }
        public static List<AssignmentDescription> GetAssignmentDescriptionsByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.assignmentsdescriptions WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractAssignmentDescriptions(query.Execute());
        }
        public static Assignment GetAssignmentByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.assignments WHERE id=" + id + ";");
            return Extractor.ExtractAssignments(query.Execute()).First();
        }
        public static List<Assignment> GetAssignmentsByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.assignments WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractAssignments(query.Execute());
        }
        public static AssignmentGrade GetAssignmentGradeByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.assignmentgrades WHERE id=" + id + ";");
            return Extractor.ExtractAssignmentGrades(query.Execute()).First();
        }
        public static List<AssignmentGrade> GetAssignmentGradesByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.assignmentgrades WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractAssignmentGrades(query.Execute());
        }
        public static CourseGrade GetCourseGradeByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.coursegrades WHERE id=" + id + ";");
            return Extractor.ExtractCourseGrades(query.Execute()).First();
        }
        public static List<CourseGrade> GetCourseGradesByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.coursegrades WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractCourseGrades(query.Execute());
        }
    }
}
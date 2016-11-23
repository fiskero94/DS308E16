using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Model;
using MySql.Data.MySqlClient;
using StudyPlatform.Classes.Exceptions;

namespace StudyPlatform.Classes.Database
{
    public static class Getters
    {
        public static Person GetPersonByID(uint id)
        {
            try
            {
                Query query = new Query("SELECT * FROM studyplatform.persons WHERE id=" + id + ";");
                return Extractor.ExtractPersons(query.Execute()).Single();
            }
            catch (MySqlException ex)
            {
                if(ex.Number == 0)
                {
                    throw new NoConnectionException();
                }
                else
                {
                    throw new InvalidIDException();
                }
            }
            catch (InvalidOperationException)
            {
                throw new InvalidIDException();
            }
        }
        public static List<Person> GetPersonsByPredicates(params string[] predicates)
        {
            try
            {
                string queryString = "SELECT * FROM studyplatform.persons WHERE ";
                Commands.AppendStringArray(ref queryString, " AND ", predicates);
                queryString += ";";
                Query query = new Query(queryString);
                return Extractor.ExtractPersons(query.Execute());
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 0)
                {
                    throw new NoConnectionException();
                }
                else
                {
                    throw new InvalidIDException();
                }
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException();
            }
        }
        public static List<Person> GetLatestPersons(int count)
        {
            return Extractor.ExtractPersons(Commands.GetLatestRows("persons", count));
        }
        public static Message GetMessageByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.messages WHERE id=" + id + ";");
            return Extractor.ExtractMessages(query.Execute()).Single();
        }
        public static List<Message> GetMessagesByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.messages WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractMessages(query.Execute());
        }
        public static List<Message> GetLatestMessages(int count)
        {
            return Extractor.ExtractMessages(Commands.GetLatestRows("messages", count));
        }
        public static News GetNewsByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.news WHERE id=" + id + ";");
            return Extractor.ExtractNews(query.Execute()).Single();
        }
        public static List<News> GetNewsByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.news WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractNews(query.Execute());
        }
        public static List<News> GetLatestNews(int count)
        {
            return Extractor.ExtractNews(Commands.GetLatestRows("news", count));
        }
        public static Course GetCourseByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.courses WHERE id=" + id + ";");
            return Extractor.ExtractCourses(query.Execute()).Single();
        }
        public static List<Course> GetCoursesByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.courses WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractCourses(query.Execute());
        }
        public static List<Course> GetLatestCourses(int count)
        {
            return Extractor.ExtractCourses(Commands.GetLatestRows("courses", count));
        }
        public static Lesson GetLessonByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.lessons WHERE id=" + id + ";");
            return Extractor.ExtractLessons(query.Execute()).Single();
        }
        public static List<Lesson> GetLessonsByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.lessons WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractLessons(query.Execute());
        }
        public static List<Lesson> GetLatestLessons(int count)
        {
            return Extractor.ExtractLessons(Commands.GetLatestRows("lessons", count));
        }
        public static Room GetRoomByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.rooms WHERE id=" + id + ";");
            return Extractor.ExtractRooms(query.Execute()).Single();
        }
        public static List<Room> GetRoomsByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.rooms WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractRooms(query.Execute());
        }
        public static List<Room> GetLatestRooms(int count)
        {
            return Extractor.ExtractRooms(Commands.GetLatestRows("rooms", count));
        }
        public static AssignmentDescription GetAssignmentDescriptionByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.assignmentsdescriptions WHERE id=" + id + ";");
            return Extractor.ExtractAssignmentDescriptions(query.Execute()).Single();
        }
        public static List<AssignmentDescription> GetAssignmentDescriptionsByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.assignmentsdescriptions WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractAssignmentDescriptions(query.Execute());
        }
        public static List<AssignmentDescription> GetLatestAssignmentDescriptions(int count)
        {
            return Extractor.ExtractAssignmentDescriptions(Commands.GetLatestRows("assignmentdescriptions", count));
        }
        public static Assignment GetAssignmentByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.assignments WHERE id=" + id + ";");
            return Extractor.ExtractAssignments(query.Execute()).Single();
        }
        public static List<Assignment> GetAssignmentsByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.assignments WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractAssignments(query.Execute());
        }
        public static List<Assignment> GetLatestAssignments(int count)
        {
            return Extractor.ExtractAssignments(Commands.GetLatestRows("assignments", count));
        }
        public static AssignmentGrade GetAssignmentGradeByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.assignmentgrades WHERE id=" + id + ";");
            return Extractor.ExtractAssignmentGrades(query.Execute()).Single();
        }
        public static List<AssignmentGrade> GetAssignmentGradesByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.assignmentgrades WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractAssignmentGrades(query.Execute());
        }
        public static List<AssignmentGrade> GetLatestAssignmentGrades(int count)
        {
            return Extractor.ExtractAssignmentGrades(Commands.GetLatestRows("assignmentgrades", count));
        }
        public static CourseGrade GetCourseGradeByID(uint id)
        {
            Query query = new Query("SELECT * FROM studyplatform.coursegrades WHERE id=" + id + ";");
            return Extractor.ExtractCourseGrades(query.Execute()).Single();
        }
        public static List<CourseGrade> GetCourseGradesByPredicates(params string[] predicates)
        {
            string queryString = "SELECT * FROM studyplatform.coursegrades WHERE ";
            Commands.AppendStringArray(ref queryString, " AND ", predicates);
            queryString += ";";
            Query query = new Query(queryString);
            return Extractor.ExtractCourseGrades(query.Execute());
        }
        public static List<CourseGrade> GetLatestCourseGrades(int count)
        {
            return Extractor.ExtractCourseGrades(Commands.GetLatestRows("coursegrades", count));
        }
    }
}
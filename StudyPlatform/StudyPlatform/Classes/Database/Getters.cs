using System;
using System.Collections.Generic;
using System.Linq;
using StudyPlatform.Classes.Exceptions;
using StudyPlatform.Classes.Model;

namespace StudyPlatform.Classes.Database
{
    public static class Getters
    {
        private static Dictionary<Type, string> TablesByType = new Dictionary<Type, string>
        {
            { typeof(Person), "Person" },
            { typeof(Message), "Message" },
            { typeof(News), "News" },
            { typeof(Course), "Course" },
            { typeof(Lesson), "Lesson" },
            { typeof(Room), "Room" },
            { typeof(AssignmentDescription), "AssignmentDescription" },
            { typeof(Assignment), "Assignment" },
            { typeof(AssignmentGrade), "AssignmentGrade" },
            { typeof(CourseGrade), "CourseGrade" }
        };
        private static Dictionary<Type, Func<MySqlConnectionReader, object>> ExtractMethodsByType = 
            new Dictionary<Type, Func<MySqlConnectionReader, object>>
            {
            { typeof(Person), Extractor.ExtractPersons },
            { typeof(Message), Extractor.ExtractMessages },
            { typeof(News), Extractor.ExtractNews },
            { typeof(Course), Extractor.ExtractCourses },
            { typeof(Lesson), Extractor.ExtractLessons },
            { typeof(Room), Extractor.ExtractRooms },
            { typeof(AssignmentDescription), Extractor.ExtractAssignmentDescriptions },
            { typeof(Assignment), Extractor.ExtractAssignments },
            { typeof(AssignmentGrade), Extractor.ExtractAssignmentGrades },
            { typeof(CourseGrade), Extractor.ExtractCourseGrades }
        };

        private static T GetRecordByID<T>(uint id)
        {
            try
            {
                Query query = new Query("SELECT * FROM studyplatform." + TablesByType[typeof(T)] + " WHERE id=" + id + ";");
                return ((List<T>)ExtractMethodsByType[typeof(T)].Invoke(query.Execute())).Single();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidIDException();
            }
        }
        private static List<T> GetRecordsByPredicates<T>(params string[] conditions)
        {
            try
            {
                string queryString = "SELECT * FROM studyplatform." + TablesByType[typeof(T)] + " WHERE ";
                Common.AppendStringArray(ref queryString, " AND ", conditions);
                queryString += ";";
                Query query = new Query(queryString);
                return (List<T>)ExtractMethodsByType[typeof(T)].Invoke(query.Execute());
            }
            catch (InvalidOperationException)
            {
                throw new InvalidIDException();
            }
        }
        private static List<T> GetLatestRecords<T>(uint count)
        {
            return (List<T>)ExtractMethodsByType[typeof(T)].Invoke(Commands.GetLatestRows(TablesByType[typeof(T)],count));
        }

        public static Person GetPersonByID(uint id) => GetRecordByID<Person>(id);
        public static Message GetMessageByID(uint id) => GetRecordByID<Message>(id);
        public static News GetNewsByID(uint id) => GetRecordByID<News>(id);
        public static Course GetCourseByID(uint id) => GetRecordByID<Course>(id);
        public static Lesson GetLessonByID(uint id) => GetRecordByID<Lesson>(id);
        public static Room GetRoomByID(uint id) => GetRecordByID<Room>(id);
        public static AssignmentDescription GetAssignmentDescriptionByID(uint id) => GetRecordByID<AssignmentDescription>(id);
        public static Assignment GetAssignmentByID(uint id) => GetRecordByID<Assignment>(id);
        public static AssignmentGrade GetAssignmentGradeByID(uint id) => GetRecordByID<AssignmentGrade>(id);
        public static CourseGrade GetCourseGradeByID(uint id) => GetRecordByID<CourseGrade>(id);
        
        public static List<Person> GetPersonsByConditions(params string[] conditions) => GetRecordsByPredicates<Person>(conditions);
        public static List<Message> GetMessagesByConditions(params string[] conditions) => GetRecordsByPredicates<Message>(conditions);
        public static List<News> GetNewsByConditions(params string[] conditions) => GetRecordsByPredicates<News>(conditions);
        public static List<Course> GetCoursesByConditions(params string[] conditions) => GetRecordsByPredicates<Course>(conditions);
        public static List<Lesson> GetLessonsByConditions(params string[] conditions) => GetRecordsByPredicates<Lesson>(conditions);
        public static List<Room> GetRoomsByConditions(params string[] conditions) => GetRecordsByPredicates<Room>(conditions);
        public static List<AssignmentDescription> GetAssignmentDescriptionsByConditions(params string[] conditions) => GetRecordsByPredicates<AssignmentDescription>(conditions);
        public static List<Assignment> GetAssignmentsByConditions(params string[] conditions) => GetRecordsByPredicates<Assignment>(conditions);
        public static List<AssignmentGrade> GetAssignmentGradesByConditions(params string[] conditions) => GetRecordsByPredicates<AssignmentGrade>(conditions);
        public static List<CourseGrade> GetCourseGradesByConditions(params string[] conditions) => GetRecordsByPredicates<CourseGrade>(conditions);

        public static List<Person> GetLatestPersons(uint count) => GetLatestRecords<Person>(count);
        public static List<Message> GetLatestMessages(uint count) => GetLatestRecords<Message>(count);
        public static List<News> GetLatestNews(uint count) => GetLatestRecords<News>(count);
        public static List<Course> GetLatestCourses(uint count) => GetLatestRecords<Course>(count);
        public static List<Lesson> GetLatestLessons(uint count) => GetLatestRecords<Lesson>(count);
        public static List<Room> GetLatestRooms(uint count) => GetLatestRecords<Room>(count);
        public static List<AssignmentDescription> GetLatestAssignmentDescriptions(uint count) => GetLatestRecords<AssignmentDescription>(count);
        public static List<Assignment> GetLatestAssignments(uint count) => GetLatestRecords<Assignment>(count);
        public static List<AssignmentGrade> GetLatestAssignmentGrades(uint count) => GetLatestRecords<AssignmentGrade>(count);
        public static List<CourseGrade> GetLatestCourseGrades(uint count) => GetLatestRecords<CourseGrade>(count);
    } 
}
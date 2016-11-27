using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Exceptions;

namespace StudyPlatform.Classes.Model
{
    public abstract class Entity<ClassT>
    {
        protected Entity(uint id)
        {
            ID = id;
        }
        
        public uint ID { get; }
        public static ClassT GetByID(uint id) => GetRecordByID<ClassT>(id);
        public static List<ClassT> GetByConditions(params string[] conditions) => GetRecordsByConditions<ClassT>(conditions);
        public static List<ClassT> GetLatest(uint count) => GetLatestRecords<ClassT>(count);
        public static List<ClassT> GetAll() => GetAll<ClassT>();

        private static readonly Dictionary<Type, string> TablesByType = new Dictionary<Type, string>
        {
            { typeof(Person), "Person" },
            { typeof(Student), "Person WHERE Type='Student'" },
            { typeof(Teacher), "Person WHERE Type='Teacher'" },
            { typeof(Secretary), "Person WHERE Type='Secretary'" },
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
        private static readonly Dictionary<Type, Func<MySqlConnectionReader, object>> ExtractMethodsByType =
            new Dictionary<Type, Func<MySqlConnectionReader, object>>
            {
            { typeof(Person), Extractor.ExtractPersons },
            { typeof(Student), Extractor.ExtractStudents },
            { typeof(Teacher), Extractor.ExtractTeachers },
            { typeof(Secretary), Extractor.ExtractSecretaries },
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

        protected static T GetRecordByID<T>(uint id)
        {
            try
            {
                Query query;
                if (typeof(T) == typeof(Teacher) || typeof(T) == typeof(Student) || typeof(T) == typeof(Secretary))
                    query = new Query("SELECT * FROM studyplatform." + TablesByType[typeof(T)] + " AND ID=" + id + ";");

                else
                    query = new Query("SELECT * FROM studyplatform." + TablesByType[typeof(T)] + " WHERE ID=" + id + ";");

                return ((List<T>)ExtractMethodsByType[typeof(T)].Invoke(query.Execute())).Single();
            }
            catch (InvalidOperationException)
            {
                throw new InvalidIDException();
            }
        }
        protected static List<T> GetRecordsByConditions<T>(params string[] conditions)
        {
            string queryString = "SELECT * FROM studyplatform." + TablesByType[typeof(T)] + " WHERE ";
            if (typeof(T) == typeof(Teacher) || typeof(T) == typeof(Student) || typeof(T) == typeof(Secretary))
                queryString = "SELECT * FROM studyplatform." + TablesByType[typeof(T)] + " AND ";

            Common.AppendStringArray(ref queryString, " AND ", conditions);
            queryString += ";";
            Query query = new Query(queryString);
            return (List<T>)ExtractMethodsByType[typeof(T)].Invoke(query.Execute());
        }
        protected static List<T> GetLatestRecords<T>(uint count) =>
            (List<T>)ExtractMethodsByType[typeof(T)].Invoke(Commands.GetLatestRows(TablesByType[typeof(T)], count));
        protected static List<T> GetAll<T>()
        {
            Query query = new Query("SELECT * FROM studyplatform." + TablesByType[typeof(T)] + ";");
            return (List<T>)ExtractMethodsByType[typeof(T)].Invoke(query.Execute());
        }

        protected static List<T> GetRelations<T>(string relationTable, string TIdentifier, string relatedIdentifier, uint relatedID)
        {
            Query query = new Query("SELECT * FROM studyplatform." + relationTable + " WHERE " + relatedIdentifier + "=" + relatedID + ";");
            uint[] ids = Extractor.ExtractIDs(query.Execute(), TIdentifier);
            return ids.Select(GetRecordByID<T>).ToList();
        }
    }
}
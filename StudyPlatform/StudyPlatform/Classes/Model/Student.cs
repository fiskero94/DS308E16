using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Student : Person
    {
        public Student(uint id, string name) : base(id, name)
        {

        }

        public List<Course> Courses => GetRelations<Course>("PersonCourse", "CourseID", "PersonID", ID);
        public List<Assignment> Assignments
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public List<Lesson> Absences
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.StudentAbsence WHERE StudentID=" + ID + ";");
                uint[] absenceList = Extractor.ExtractIDs(query.Execute(), "LessonID");
                List<Lesson> studentAbsences = new List<Lesson>();
                foreach (uint lessonID in absenceList)
                {
                    studentAbsences.Add(Lesson.GetByID(lessonID));
                }
                return studentAbsences;
            }
        }

        public static Student New(string name, string username, string password)
        {
            Creator.CreateStudent(name, username, password);
            return GetLatest(1).Single();
        }

        public new static Student GetByID(uint id) => GetRecordByID<Student>(id);
        public new static List<Student> GetAll() => GetAll<Student>();
        public new static List<Student> GetByConditions(params string[] conditions)
            => GetRecordsByConditions<Student>(conditions);
        public new static List<Student> GetLatest(uint count) => GetLatestRecords<Student>(count);
    }
}
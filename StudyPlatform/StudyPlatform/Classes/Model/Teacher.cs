using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Teacher : Person
    {
        public Teacher(uint id, string name) : base(id, name)
        {

        }

        public List<Course> Courses
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.PersonCourse WHERE PersonID=" + ID + ";");
                uint[] courseList = Extractor.ExtractIDs(query.Execute(),"CourseID");
                List<Course> personCourses = new List<Course>();
                foreach (uint course in courseList)
                {
                    personCourses.Add(Course.GetByID(course));
                }
                return personCourses;
            }
        }

        public static Teacher New(string name, string username, string password)
        {
            Creator.CreateTeacher(name, username, password);
            return GetLatest(1).Single();
        }

        public new static Teacher GetByID(uint id) => GetRecordByID<Teacher>(id);
        public new static List<Teacher> GetAll() => GetAll<Teacher>();
        public new static List<Teacher> GetByConditions(params string[] conditions)
            => GetRecordsByConditions<Teacher>(conditions);
        public new static List<Teacher> GetLatest(uint count) => GetLatestRecords<Teacher>(count);
    }
}
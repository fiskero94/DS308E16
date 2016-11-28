using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatformMVC.Database;

namespace StudyPlatformMVC.Models
{
    public class Teacher : Person
    {
        public Teacher(uint id, string name) : base(id, name)
        {

        }

        public List<Course> Courses => GetRelations<Course>("PersonCourse", "CourseID", "PersonID", ID);

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
        public new static Teacher GetLatest() => GetLatestRecord<Teacher>();
    }
}
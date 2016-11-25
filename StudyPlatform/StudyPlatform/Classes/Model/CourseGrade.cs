using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class CourseGrade : Grade
    {
        private uint _courseid;
        private uint _studentid;

        public CourseGrade(uint id, string grade, string comment, uint courseid, uint studentid) : base(id, grade, comment)
        {
            _courseid = courseid;
            _studentid = studentid;
        }

        public Course Course => Getters.GetCourseByID(_courseid);

        public Student Student => Getters.GetPersonByID(_studentid) as Student;

        public static CourseGrade New(Course course, Student student, string grade, string comment)
        {
            Creator.CreateCourseGrade(course, student, grade, comment);
            return Getters.GetLatestCourseGrades(1).Single();
        }
        public void Remove() => Remover.RemoveCourseGrade(this);
    }
}
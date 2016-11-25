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

        public Course Course
        {
            get
            {
                return Getters.GetCourseByID(_courseid);
            }
        }
        public Student Student
        {
            get
            {
                return Getters.GetPersonByID(_studentid) as Student;
            }
        }

        public static CourseGrade New(string grade, string comment, Course course, Student student)
        {
            Creator.CreateCourseGrade(grade, comment, course, student);
            return Getters.GetLatestCourseGrades(1).Single();
        }
        public void Remove() => Remover.RemoveCourseGrade(this);
    }
}
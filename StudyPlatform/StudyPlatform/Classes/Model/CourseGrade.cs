using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}
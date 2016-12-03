using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Exceptions;

namespace StudyPlatform.Classes.Model
{
    public class CourseGrade : Entity<CourseGrade>
    {
        private readonly uint _courseid;
        private readonly uint _studentid;
        private string _grade;
        private string _comment;

        public CourseGrade(uint id, uint courseid, uint studentid, string grade, string comment) : base(id)
        {
            _courseid = courseid;
            _studentid = studentid;
            _grade = grade;
            _comment = comment;
        }

        public Course Course => Course.GetByID(_courseid);
        public Student Student => Student.GetByID(_studentid);
        public string Grade
        {
            get
            {
                return _grade;
            }
            set
            {
                if (!Common.ValidGrades.Contains(value))
                    throw new InvalidGradeException();
                Commands.SetValue("CourseGrade", ID, "Grade", value);
                _grade = value;
            }
        }
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                Commands.SetValue("CourseGrade", ID, "Comment", value);
                _comment = value;
            }
        }

        public void Remove() => Remover.RemoveCourseGrade(this);
        public static CourseGrade New(Course course, Student student, string grade, string comment)
        {
            Creator.CreateCourseGrade(course, student, grade, comment);
            return GetLatest(1).Single();
        }
    }
}
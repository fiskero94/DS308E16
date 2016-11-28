using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatformMVC.Database;
using StudyPlatformMVC.Exceptions;

namespace StudyPlatformMVC.Models
{
    public class AssignmentGrade : Entity<AssignmentGrade>
    {
        private readonly uint _assignmentid;
        private string _grade;
        private string _comment;

        public AssignmentGrade(uint id, uint assignmentid, string grade, string comment) : base(id)
        {
            _assignmentid = assignmentid;
            _grade = grade;
            _comment = comment;
        }

        public Assignment Assignment => Assignment.GetByID(_assignmentid);
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
                Commands.SetValue("AssignmentGrade", ID, "Grade", value);
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
                Commands.SetValue("AssignmentGrade", ID, "Comment", value);
                _comment = value;
            }
        }

        public void Remove() => Remover.RemoveAssignmentGrade(this);
        public static AssignmentGrade New(string grade, string comment, Assignment assignment)
        {
            Creator.CreateAssignmentGrade(grade, comment, assignment);
            return GetLatest(1).Single();
        }
    }
}
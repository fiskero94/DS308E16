using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Assignment
    {
        private uint _id;
        private uint _assignmentdescriptionid;
        private uint _studentid;
        private string _comment;
        private uint _gradeid;
        private DateTime _date;

        public Assignment(uint id, uint assignmentdescriptionid, uint studentid, string comment, uint gradeid, DateTime date)
        {
            _id = id;
            _assignmentdescriptionid = assignmentdescriptionid;
            _studentid = studentid;
            _comment = comment;
            _gradeid = gradeid;
            _date = date;
        }

        public uint ID
        {
            get
            {
                return _id;
            }
        }
        public AssignmentDescription AssignmentDescription
        {
            get
            {
                return Getters.GetAssignmentDescriptionByID(_assignmentdescriptionid);
            }
        }
        public Student Student
        {
            get
            {
                return Getters.GetPersonByID(_studentid) as Student;
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
                if (value == null)
                    throw new ArgumentNullException();
                else
                {
                    Commands.SetValue("assignments", ID, "comment", value);
                    _comment = value;
                }
            }
        }
        public AssignmentGrade Grade
        {
            get
            {
                return Getters.GetAssignmentGradeByID(_gradeid);
            }
        }
        public DateTime Date
        {
            get
            {
                return _date;
            }
        }
    }
}
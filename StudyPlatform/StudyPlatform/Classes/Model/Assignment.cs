using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Assignment : Entity<Assignment>
    {
        private readonly uint _assignmentdescriptionid;
        private readonly uint _studentid;
        private string _comment;
        private uint _gradeid;

        public Assignment(uint id, uint assignmentdescriptionid, uint studentid, string comment, uint gradeid,
            DateTime dateTimeSubmitted) : base(id)
        {
            _assignmentdescriptionid = assignmentdescriptionid;
            _studentid = studentid;
            _comment = comment;
            _gradeid = gradeid;
            DateTimeSubmitted = dateTimeSubmitted;
        }
        public Assignment(uint id, uint assignmentdescriptionid, uint studentid, string comment, DateTime dateTimeSubmitted)
            : this(id, assignmentdescriptionid, studentid, comment, 0, dateTimeSubmitted) { }

        public AssignmentDescription AssignmentDescription => AssignmentDescription.GetByID(_assignmentdescriptionid);
        public Student Student => Student.GetByID(_studentid);
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                Commands.SetValue("Assignment", ID, "Comment", value);
                _comment = value;
            }
        }
        public AssignmentGrade Grade
        {
            get
            {
                return _gradeid == 0 ? null : AssignmentGrade.GetByID(_gradeid);
            }
            set
            {
                if (value == null)
                {
                    Commands.SetValue("Assignment", ID, "GradeID", "NULL");
                    _gradeid = 0;
                }
                else
                {
                    Commands.SetValue("Assignment", ID, "GradeID", value.ID.ToString());
                    _gradeid = value.ID;
                }
            }
        }
        public DateTime DateTimeSubmitted { get; }
        public List<string> Documents => GetDocuments("AssignmentFile", "AssignmentID", ID);

        public void Remove() => Remover.RemoveAssignment(this);
        public static Assignment New(AssignmentDescription assignmentDescription, Student student, string comment, List<string> filepaths)
        {
            Creator.CreateAssignment(assignmentDescription, student, comment, filepaths);
            return GetLatest(1).Single();
        }
    }
}
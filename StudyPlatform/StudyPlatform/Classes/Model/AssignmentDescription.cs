using StudyPlatform.Classes.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public class AssignmentDescription : Entity<AssignmentDescription>
    {
        private readonly uint _courseid;
        private string _description;
        private bool _cancelled;
        private DateTime _deadline;

        public AssignmentDescription(uint id, uint courseid, string description, bool cancelled, DateTime deadline)
            : base(id)
        {
            _courseid = courseid;
            _description = description;
            _cancelled = cancelled;
            _deadline = deadline;
        }   
        
        public Course Course => Course.GetByID(_courseid);
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                Commands.SetValue("AssignmentDescription", ID, "Description", value);
                _description = value;
            }
        }
        public bool Cancelled
        {
            get { return _cancelled; }
            set
            {
                Commands.SetValue("AssignmentDescription", ID, "Cancelled", value.ToString().ToUpper());
                _cancelled = value;
            }
        }
        public DateTime Deadline
        {
            get
            {
                return _deadline;
            }
            set
            {
                Commands.SetValue("AssignmentDescription", ID, "Deadline", value.ToString("yyyy-MM-dd HH:mm:ss"));
                _deadline = value;
            }
        }
        public List<Assignment> Assignments => Assignment.GetByConditions("AssignmentDescriptionID=" + ID);
        public List<string> Documents => GetDocuments("AssignmentDescriptionFile", "AssignmentDescriptionID", ID);
        public void Remove() => Remover.RemoveAssignmentDescription(this);

        public static AssignmentDescription New(Course course, string description, DateTime deadline, List<string> filepaths)
        {
            Creator.CreateAssignmentDescription(course, description, deadline, filepaths);
            return GetLatest(1).Single();
        }
    }
}
using StudyPlatform.Classes.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public class AssignmentDescription
    {
        private readonly uint _courseid;
        private string _description;
        private bool _cancelled;
        private DateTime _date;

        public AssignmentDescription(uint id, uint courseid, string description, bool cancelled, DateTime date)
        {
            ID = id;
            _courseid = courseid;
            _description = description;
            _cancelled = cancelled;
            _date = date;
        }   
        
        public uint ID { get; }
        public Course Course => Getters.GetCourseByID(_courseid);
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                Commands.SetValue("assignmentdescriptions", ID, "description", value);
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
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                Commands.SetValue("AssignmentDescription", ID, "Deadline", value.ToString("yyyy-MM-dd HH:mm:ss"));
                _date = value;
            }
        }
        public List<Assignment> Assignments
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.assignmentdescriptionassignments" + ID);
                uint[] ids = Extractor.ExtractIDs(query.Execute(), "field");
                List<Assignment> assignments = new List<Assignment>();
                foreach (uint id in ids)
                    assignments.Add(Getters.GetAssignmentByID(id));
                return assignments;
            }
        }
        public List<string> Documents
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.assignmentdescriptiondocuments" + ID);
                string[] filepaths = Extractor.ExtractFilepaths(query.Execute());
                return filepaths.ToList();
            }
        }

        public static AssignmentDescription New(Course course, string description, DateTime deadline, List<string> filepaths)
        {
            Creator.CreateAssignmentDescription(course, description, deadline, filepaths);
            return Getters.GetLatestAssignmentDescriptions(1).Single();
        }
        public void Remove() => Remover.RemoveAssignmentDescription(this);
    }
}
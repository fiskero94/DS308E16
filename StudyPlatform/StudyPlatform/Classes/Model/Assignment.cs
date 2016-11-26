using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Assignment
    {
        private readonly uint _assignmentdescriptionid;
        private readonly uint _studentid;
        private string _comment;
        private uint _gradeid;
        private DateTime _deadline;

        public Assignment(uint id, uint assignmentdescriptionid, uint studentid, string comment, uint gradeid, DateTime deadline)
        {
            ID = id;
            _assignmentdescriptionid = assignmentdescriptionid;
            _studentid = studentid;
            _comment = comment;
            _gradeid = gradeid;
            _deadline = deadline;
        }
        public Assignment(uint id, uint assignmentdescriptionid, uint studentid, string comment, DateTime deadline) 
            : this(id, assignmentdescriptionid, studentid, comment, 0, deadline)
        {

        }

        public uint ID { get; }

        public AssignmentDescription AssignmentDescription => Getters.GetAssignmentDescriptionByID(_assignmentdescriptionid);

        public Student Student => Getters.GetPersonByID(_studentid) as Student;

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
                if (Commands.CheckNull("assignments", ID, "gradeid"))
                    return null;
                else
                    return Getters.GetAssignmentGradeByID(_gradeid);
            }
        }
        public DateTime Deadline => _deadline;

        public List<string> Documents
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.assignmentdocuments" + ID);
                string[] filepaths = Extractor.ExtractFilepaths(query.Execute());
                return filepaths.ToList();
            }
        }

        public static Assignment New(AssignmentDescription assignmentDescription, Student student, string comment, List<string> filepaths)
        {
            Creator.CreateAssignment(assignmentDescription, student, comment, filepaths);
            return Getters.GetLatestAssignments(1).Single();
        }
        public void Remove() => Remover.RemoveAssignment(this);
    }
}
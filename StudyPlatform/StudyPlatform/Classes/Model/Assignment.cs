using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public class Assignment
    {
        private uint _assignmentdescriptionid;
        private string _comment;
        private DateTime _date;
        private uint _gradeid;
        private uint _studentid;
        private uint _id;

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
    }
}
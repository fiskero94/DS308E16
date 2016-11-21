using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public class AssignmentDescription
    {
        private uint id;
        public uint ID
        {
            get
            {
                return id;
            }
        }

        private uint courseid;
        public uint CourseID
        {
            get
            {
                return courseid
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get
            {
                return date;
            }
        }

        public AssignmentDescription(uint id, uint courseid, string description, DateTime date)
        {

        }
    }
}
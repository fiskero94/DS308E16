using StudyPlatform.Classes.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public class AssignmentDescription
    {
        public AssignmentDescription(uint id, uint courseid, string description, DateTime date)
        {
            id = _id;
            courseid = _courseid;
            description = _description;
            date = _date;
        }

        private uint _id;
        public uint ID
        {
            get
            {
                return _id;
            }
        }

        private uint _courseid;
        public uint CourseID
        {
            get
            {
                return _courseid;
            }
        }

        private string _description;
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
                else
                {
                    Editor.SetValue("assignmentdescriptions", ID, "description", value);
                    _description = value;
                }
            }
        }

        private DateTime _date;
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
                else
                {
                    Editor.SetValue("assignmentdescriptions", ID, "date", value.ToString("yyyy-MM-dd HH:mm:ss"));
                    _date = value;
                }
            }
        }
    }
}
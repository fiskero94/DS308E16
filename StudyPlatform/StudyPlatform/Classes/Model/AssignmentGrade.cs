using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class AssignmentGrade : Grade
    {
        private uint _assignmentid;

        public AssignmentGrade(uint id, string grade, string comment, uint assignmentid) : base(id, grade, comment)
        {
            _assignmentid = assignmentid;
        }
        
        public Assignment Assignment
        {
            get
            {
                return Getters.GetAssignmentByID(_assignmentid);
            }
        }  
    }
}
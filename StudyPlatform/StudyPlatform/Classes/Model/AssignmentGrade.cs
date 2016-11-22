using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public class AssignmentGrade : Grade
    {
        
        public AssignmentGrade(uint id, string grade, string comment, uint assignmentid) : base(id, grade, comment)
        {

            _assignmentid = assignmentid;
        }
        private uint _assignmentid;
        public Assignment Assignment
        {
            get
            {
                return Getters.GetAssignmentByID(_assignmentid);
            }
        }
            
    }
}
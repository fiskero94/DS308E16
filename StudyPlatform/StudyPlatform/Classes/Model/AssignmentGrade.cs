using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public class AssignmentGrade : Grade
    {
        AssignmentGrade(uint id, string grade, string comment, uint assignmentid) : base(id, grade, comment)
        {
            _id = id;
            _grade = grade;
            _comment = comment;
            _assignmentid = assignmentid;
        }

            
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public class AssignmentGrade : Grade
    {
        private uint _id;
        public uint ID
        {
            get
            {
                return _id;
            }
        }
    }
}
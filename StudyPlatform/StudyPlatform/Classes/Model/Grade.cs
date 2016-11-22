using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public abstract class Grade
    {
        public static string[] ValidGrades = { "12", "10", "7", "4", "02", "00", "-3" };
        private string comment;
        private string grade;
        private uint _id;

        public Grade(uint id, string grade, string comment)
        {
            ID = id;
            this.grade = grade;
            this.comment = comment;
        }

        public uint ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
    }
}
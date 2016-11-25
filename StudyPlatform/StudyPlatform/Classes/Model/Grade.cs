using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public abstract class Grade
    {
        public static string[] ValidGrades = { "12", "10", "7", "4", "02", "00", "-3" };
        private string _comment;
        private string _grade;
        private uint _id;

        public Grade(uint id, string grade, string comment)
        {
            _id = id;
            _grade = grade;
            _comment = comment;
        }

        public uint ID => _id;
        public string Value => _grade;
        public string Comment => _comment;
    }
}
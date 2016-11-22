using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Exceptions
{
    public class InvalidGradeException : Exception
    {
        public InvalidGradeException(string message) : base(message)
        {

        }
        public InvalidGradeException()
        {

        }
    }
}
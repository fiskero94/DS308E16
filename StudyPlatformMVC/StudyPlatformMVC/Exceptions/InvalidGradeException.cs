using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatformMVC.Exceptions
{
    [Serializable]
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
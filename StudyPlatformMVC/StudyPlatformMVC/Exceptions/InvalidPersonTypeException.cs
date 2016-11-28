using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatformMVC.Exceptions
{
    [Serializable]
    public class InvalidPersonTypeException : Exception
    {
        public InvalidPersonTypeException(string message) : base(message)
        {

        }
        public InvalidPersonTypeException()
        {

        }
    }
}
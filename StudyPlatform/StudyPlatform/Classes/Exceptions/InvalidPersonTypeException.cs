using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Exceptions
{
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
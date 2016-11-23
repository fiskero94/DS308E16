using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Exceptions
{
    public class NoConnectionException : Exception
    {
        public NoConnectionException(string message) : base(message)
        {

        }
        public NoConnectionException()
        {

        }
    }
}
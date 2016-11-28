using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatformMVC.Exceptions
{
    [Serializable]
    public class RoomUnavailableException : Exception
    {
        public RoomUnavailableException(string message) : base(message)
        {

        }
        public RoomUnavailableException()
        {

        }
    }
}
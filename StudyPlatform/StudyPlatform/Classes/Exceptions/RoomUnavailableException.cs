using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Exceptions
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
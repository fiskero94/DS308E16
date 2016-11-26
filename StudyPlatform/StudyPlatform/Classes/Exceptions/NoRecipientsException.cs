﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Exceptions
{
    [Serializable]
    public class NoRecipientsException : Exception
    {
        public NoRecipientsException(string message) : base(message)
        {

        }
        public NoRecipientsException()
        {

        }
    }
}
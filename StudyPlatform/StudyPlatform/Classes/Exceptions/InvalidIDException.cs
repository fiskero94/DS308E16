﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Exceptions
{
    public class InvalidIDException : Exception
    {
        public InvalidIDException(string message) : base(message)
        {

        }
        public InvalidIDException()
        {

        }
    }
}
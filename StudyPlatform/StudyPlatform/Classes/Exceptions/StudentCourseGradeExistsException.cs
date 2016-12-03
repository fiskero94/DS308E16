﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Exceptions
{
    [Serializable]
    public class StudentCourseGradeExistsException : Exception
    {
        public StudentCourseGradeExistsException(string message) : base(message)
        {

        }
        public StudentCourseGradeExistsException()
        {

        }
    }
}
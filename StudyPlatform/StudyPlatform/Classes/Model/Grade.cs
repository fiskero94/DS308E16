using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public abstract class Grade
    {
        public static string[] ValidGrades = { "12", "10", "7", "4", "02", "00", "-3" };
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes
{
    public static class Common
    {
        public static string[] ValidGrades = { "12", "10", "7", "4", "02", "00", "-3" };
        public static void AppendStringArray(ref string stringToAppendOn, string seperator, string[] strings)
        {
            foreach (string item in strings)
            {
                stringToAppendOn += item;
                stringToAppendOn += seperator;
            }
            stringToAppendOn = stringToAppendOn.TrimEnd(seperator.ToCharArray());
        }
        public static void EnsureNotNull(params object[] objects)
        {
            foreach (object obj in objects)
                if (obj == null)
                    throw new ArgumentNullException();
        }
        public static void EnsureNotEmpty(params string[] strings)
        {
            foreach (string str in strings)
            {
                if (str == "")
                    throw new ArgumentException();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using StudyPlatform.Classes.Model;

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
            if (objects.Any(obj => obj == null))
                throw new ArgumentNullException();
        }
        public static void EnsureNotEmpty(params string[] strings)
        {
            if (strings.Any(str => str == ""))
                throw new ArgumentException();
        }
    }
}
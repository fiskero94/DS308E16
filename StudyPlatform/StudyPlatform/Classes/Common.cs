using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

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
        // http://stackoverflow.com/a/8605204 edited to support WebControls instead of HtmlControls
        public static T FindWebControlByAttribute<T>(Control controlToSearch, string attributeName, string attributeValue) where T : WebControl
        {
            foreach (Control control in controlToSearch.Controls)
            {
                if (control.GetType() == typeof(T) && ((T)control).Attributes[attributeName] == attributeValue)
                {
                    return (T)control;
                }
                var cb = FindWebControlByAttribute<T>(control, attributeName, attributeValue);
                if (cb != null)
                    return cb;
            }
            return null;
        }
        public static T FindHtmlControlByAttribute<T>(Control controlToSearch, string attributeName, string attributeValue) where T : HtmlControl
        {
            foreach (Control control in controlToSearch.Controls)
            {
                if (control.GetType() == typeof(T) && ((T)control).Attributes[attributeName] == attributeValue)
                {
                    return (T)control;
                }
                var cb = FindHtmlControlByAttribute<T>(control, attributeName, attributeValue);
                if (cb != null)
                    return cb;
            }
            return null;
        }

        public static TableCell CreateTextCell(string text, string size)
        {
            TableCell cell = new TableCell {Text = text};
            cell.Attributes["class"] = size;
            return cell;
        }
    }
}
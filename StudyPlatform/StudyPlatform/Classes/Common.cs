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
        public static HtmlGenericControl CreateIconControl(string icon)
        {
            HtmlGenericControl iconControl = new HtmlGenericControl("i");
            iconControl.Attributes.Add("class", "fa " + icon);
            return iconControl;
        }
        public static HtmlGenericControl CreateTextControl(string text) => 
            new HtmlGenericControl { InnerText = text };
        public static LinkButton CreateLinkButtonWithIcon(string cssClass, string icon, string text)
        {
            LinkButton button = new LinkButton { CssClass = cssClass };
            button.Controls.Add(CreateIconControl(icon));
            button.Controls.Add(CreateTextControl(" " + text));
            return button;
        }
        public static LinkButton CreateDownloadButton(string filepath, EventHandler onClickEventHandler)
        {
            LinkButton downloadButton = CreateLinkButtonWithIcon("btn btn-warning", "fa-file",
                Path.GetFileName(filepath));
            downloadButton.Attributes.Add("path", filepath);
            downloadButton.Click += onClickEventHandler;
            return downloadButton;
        }
        public static void DownloadFile(LinkButton button, Page page)
        {
            string path = button.Attributes["path"];
            string name = Path.GetFileName(path);
            try
            {
                page.Response.ContentType = "application/octet-stream";
                page.Response.AppendHeader("Content-Disposition", "attachment; filename=" + name);
                page.Response.TransmitFile(page.Server.MapPath(path));
                page.Response.End();
            }
            catch (Exception)
            {
                button.Text = "Fil forsvundet";
                button.Attributes.Add("class", "btn btn-danger disabled");
                Assignment.RemoveDocument(path);
            }
        }

        public static TableCell CreateAccordionToggleCell(string target, string text)
        {
            TableCell cell = new TableCell { Text = text };
            cell.Attributes.Add("data-toggle", "collapse");
            cell.Attributes.Add("data-target", target);
            cell.Attributes.Add("class", "clickable");
            return cell;
        }

        public static HtmlGenericControl CreateAlertDiv(string message)
        {
            HtmlGenericControl alert = new HtmlGenericControl("div");
            alert.Attributes.Add("role", "alert");
            alert.Attributes.Add("class", "alert alert-info");
            alert.InnerText = message;
            return alert;
        }
        public static TableCell CreateCellWithControls(params Control[] controls)
        {
            TableCell cell = new TableCell();
            foreach(Control control in controls)
                cell.Controls.Add(control);
            return cell;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using StudyPlatform.Classes.Model;

namespace StudyPlatform
{
    public partial class Karakterer : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["user"] is Student))
                Response.Redirect("login.aspx");
            Master.TitelLabelText = "Karakterer";
            Student student = (Student)Session["user"];
            List<CourseGrade> courseGrades = CourseGrade.GetByConditions("StudentID=" + student.ID);
            int gradeSum = 0;
            foreach (CourseGrade grade in courseGrades)
            {
                gradeSum += Convert.ToInt32(grade.Grade);
                TableRow gradeRow = new TableRow();
                gradeRow.Cells.Add(new TableCell { Text = grade.Course.Name });
                gradeRow.Cells.Add(new TableCell { Text = grade.Grade });
                GradesTable.Rows.Add(gradeRow);
                if (grade.Comment == string.Empty) continue;
                TableRow commentRow = new TableRow();
                commentRow.Cells.Add(new TableCell { Text = grade.Course.Name });
                commentRow.Cells.Add(new TableCell { Text = grade.Comment });
                CommentsTable.Rows.Add(commentRow);
            }
            if (courseGrades.Count > 0) // Avoid DevideByZero
            {
                TableRow averageRow = new TableRow();
                TableCell textCell = new TableCell {Text = "Gennemsnit"};
                textCell.Attributes["class"] = "average-grade";
                averageRow.Cells.Add(textCell);
                TableCell averageCell = new TableCell {Text = Convert.ToString((double)gradeSum/(double)courseGrades.Count)};
                averageCell.Attributes["class"] = "average-grade";
                averageRow.Cells.Add(averageCell);
                GradesTable.Rows.Add(averageRow);
            }
            if (GradesTable.Rows.Count == 1)
                AddAlertRow(GradesTable, "Du har ikke fået nogen kursus karakterer");
            if (CommentsTable.Rows.Count == 1)
                AddAlertRow(CommentsTable, "Du har ikke fået nogen karakter kommentarer");
        }

        private static void AddAlertRow(Table table, string message)
        {
            HtmlGenericControl alert = new HtmlGenericControl("div") {InnerText = message};
            alert.Attributes["role"] = "alert";
            alert.Attributes["class"] = "alert alert-info";
            TableCell alertCell = new TableCell { ColumnSpan = 2 };
            alertCell.Controls.Add(alert);
            TableRow alertRow = new TableRow();
            alertRow.Cells.Add(alertCell);
            table.Rows.Add(alertRow);
        }
    }
}
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
            foreach (CourseGrade grade in courseGrades)
            {
                TableRow gradeRow = new TableRow();
                gradeRow.Cells.Add(new TableCell { Text = grade.Course.Name });
                gradeRow.Cells.Add(new TableCell { Text = grade.Grade });
                GradesTable.Rows.Add(gradeRow);
                
                if (grade.Comment != string.Empty)
                {
                    TableRow commentRow = new TableRow();
                    commentRow.Cells.Add(new TableCell { Text = grade.Course.Name });
                    commentRow.Cells.Add(new TableCell { Text = grade.Comment });
                    CommentsTable.Rows.Add(commentRow);
                }
            }
        }
    }
}
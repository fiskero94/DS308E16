using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudyPlatform.Classes.Model;

namespace StudyPlatform
{
    public partial class Fravaer : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["user"] is Student))
                Response.Redirect("login.aspx");
            Master.TitelLabelText = "Fravær";
            Student student = (Student)Session["user"];
            int totalNumberOfLessons = 0;
            int totalNumberOfCurrentLessons = 0;
            int totalNumberOfAssignments = 0;
            int totalNumberOfCurrentAssignments = 0;
            foreach (Course course in student.Courses)
            {
                totalNumberOfLessons += course.Lessons.Count;
                totalNumberOfCurrentLessons += course.CurrentLessons.Count;
                totalNumberOfAssignments += course.AssignmentDescriptions.Count;
                totalNumberOfCurrentAssignments += course.CurrentAssignmentDescriptions.Count;
                TableRow courseRow = new TableRow();
                courseRow.Cells.Add(new TableCell { Text = course.Name, ColumnSpan = 2});
                if (course.CurrentLessons.Count == 0)
                    courseRow.Cells.Add(new TableCell {Text = "0 %"});
                else
                    courseRow.Cells.Add(new TableCell { Text = Convert.ToString(100 * student.CourseLessons(course).Count / course.CurrentLessons.Count) + "%" });
                courseRow.Cells.Add(new TableCell { Text = Convert.ToString(student.CourseLessons(course).Count) +  "/" + Convert.ToString(course.CurrentLessons.Count) });
                if (course.Lessons.Count == 0)
                    courseRow.Cells.Add(new TableCell { Text = "0 %" });
                else
                    courseRow.Cells.Add(new TableCell { Text = Convert.ToString(100 * student.CourseLessons(course).Count / course.Lessons.Count) + "%" });
                courseRow.Cells.Add(new TableCell { Text = Convert.ToString(student.CourseLessons(course).Count) + "/" + Convert.ToString(course.Lessons.Count) });
                if (course.CurrentAssignmentDescriptions.Count == 0)
                    courseRow.Cells.Add(new TableCell { Text = "0 %" });
                else
                    courseRow.Cells.Add(new TableCell { Text = Convert.ToString(((course.CurrentAssignmentDescriptions.Count - student.GetActiveAssignmentsByCourse(course).Count) * 100) / course.CurrentAssignmentDescriptions.Count) + "%" });
                courseRow.Cells.Add(new TableCell { Text = Convert.ToString(course.CurrentAssignmentDescriptions.Count - student.GetActiveAssignmentsByCourse(course).Count) + "/" + Convert.ToString(course.CurrentAssignmentDescriptions.Count)});
                if (course.AssignmentDescriptions.Count == 0)
                    courseRow.Cells.Add(new TableCell { Text = "0 %" });
                else
                    courseRow.Cells.Add(new TableCell { Text = Convert.ToString(((course.CurrentAssignmentDescriptions.Count - student.GetActiveAssignmentsByCourse(course).Count) * 100) / course.AssignmentDescriptions.Count) + "%" });
                courseRow.Cells.Add(new TableCell { Text = Convert.ToString(course.CurrentAssignmentDescriptions.Count - student.GetActiveAssignmentsByCourse(course).Count) + "/" + Convert.ToString(course.AssignmentDescriptions.Count) });
                AbsenceTable.Rows.Add(courseRow);
            }
            TableRow totalCoursesRow = new TableRow();
            totalCoursesRow.Attributes["style"] = "font-weight: bolder;";
            totalCoursesRow.Cells.Add(new TableCell { Text = "Samlet", ColumnSpan = 2});
            if (totalNumberOfCurrentLessons == 0)
                totalCoursesRow.Cells.Add(new TableCell { Text = "0 %" });
            else
                totalCoursesRow.Cells.Add(new TableCell { Text = Convert.ToString(100 * student.Absences.Count / totalNumberOfCurrentLessons) + "%" });
            totalCoursesRow.Cells.Add(new TableCell { Text = Convert.ToString(student.Absences.Count + "/" + Convert.ToString(totalNumberOfCurrentLessons))});
            if (totalNumberOfLessons == 0)
                totalCoursesRow.Cells.Add(new TableCell { Text = "0 %" });
            else
                totalCoursesRow.Cells.Add(new TableCell { Text = Convert.ToString(100 * student.Absences.Count / totalNumberOfLessons) + "%" });
            totalCoursesRow.Cells.Add(new TableCell { Text = Convert.ToString(student.Absences.Count) + "/" + Convert.ToString(totalNumberOfLessons)});
            if (totalNumberOfCurrentAssignments == 0)
                totalCoursesRow.Cells.Add(new TableCell { Text = "0 %" });
            else
                totalCoursesRow.Cells.Add(new TableCell { Text = Convert.ToString(((totalNumberOfCurrentAssignments - student.ActiveAssignments.Count) * 100) / totalNumberOfCurrentAssignments) + "%" });
            totalCoursesRow.Cells.Add(new TableCell { Text = Convert.ToString(totalNumberOfCurrentAssignments - student.ActiveAssignments.Count) + "/" + Convert.ToString(totalNumberOfCurrentAssignments)});
            if (totalNumberOfAssignments == 0)
                totalCoursesRow.Cells.Add(new TableCell { Text = "0 %" });
            else
                totalCoursesRow.Cells.Add(new TableCell { Text = Convert.ToString(((totalNumberOfCurrentAssignments - student.ActiveAssignments.Count) * 100) / totalNumberOfAssignments) + "%" });
            totalCoursesRow.Cells.Add(new TableCell { Text = Convert.ToString(totalNumberOfCurrentAssignments - student.ActiveAssignments.Count) + "/" + Convert.ToString(totalNumberOfAssignments) });
            AbsenceTable.Rows.Add(totalCoursesRow);
            fag.Attributes["style"] = "line-height: 100px;";

            string filePathName = "~/Content/Images/absenceGraph.jpg";
            const string customChartDesign = @"<Chart BackColor=""#F8F8F8"" BorderColor=""#158CBA"" BorderlineDashStyle=""Solid"" BorderWidth=""5"" Palette=""None""> <ChartAreas> <ChartArea Name=""Default"" BackColor=""#F8F8F8"" BorderColor=""#158CBA"" BorderDashStyle=""Solid""/> </ChartAreas> </Chart>";
            Chart chartImage = new Chart(2500, 500, customChartDesign);
            List<Lesson> allLessons = new List<Lesson>();
            foreach (Course studentCourse in student.Courses)
            {
                foreach (Lesson modelCourseLesson in studentCourse.Lessons)
                {
                    allLessons.Add(modelCourseLesson);
                }
            }
            List<Lesson> sortedAllLessons = allLessons.OrderBy(o => o.DateTime).ToList();

            int lessonCount = 0;
            List<double> yValueList = new List<double>();
            List<string> xValueList = new List<string>();

            Lesson temp = sortedAllLessons.First();
            DateTime date = temp.DateTime.Date;

            foreach (Lesson sortedAllLesson in sortedAllLessons)
            {
                lessonCount++;
                if (sortedAllLesson.DateTime.Date > date.Date)
                {
                    int absenceCount = 0;
                    foreach (Lesson studentAbsence in student.Absences)
                    {
                        if (date.Date > studentAbsence.DateTime.Date)
                        {
                            absenceCount++;
                        }
                    }
                    double currentAbsence = (100 * Convert.ToDouble(absenceCount)) / Convert.ToDouble(lessonCount);
                    yValueList.Add(currentAbsence);
                    xValueList.Add(date.Date.ToShortDateString());
                }
                date = sortedAllLesson.DateTime;
            }
            chartImage.AddSeries(
                chartType: "line",
                xField: "Dato",
                yFields: "Fravær",
            xValue: xValueList,
            yValues: yValueList);
            chartImage.Save(filePathName);
            AbsenceGraph.ImageUrl = filePathName;
        }
    }
}
using StudyPlatformMVC.Models;
using System;

namespace StudyPlatformMVC.Database
{
    public static class Remover
    {
        public static void RemovePerson(Person person)
        {
            Commands.DeleteFrom("MessageRecipient", "PersonID=" + person.ID);
            foreach (Message message in person.SentMessages)
                RemoveMessage(message);
            if (person is Teacher)
                foreach (Course course in ((Teacher)person).Courses)
                    course.RemoveTeacher((Teacher)person);
            else if (person is Student)
                foreach (Course course in ((Student)person).Courses)
                    course.RemoveStudent((Student)person);
            else if (person is Secretary)
                foreach (News news in News.GetAll())
                    if (news.Author.ID == person.ID)
                        RemoveNews(news);
            Commands.DeleteFrom("Person", "ID=" + person.ID);
            person = null;
        }
        public static void RemoveMessage(Message message)
        {
            Commands.DeleteFrom("MessageRecipient", "MessageID=" + message.ID);
            Commands.DeleteFrom("MessageFile", "MessageID=" + message.ID);
            Commands.DeleteFrom("Message", "ID=" + message.ID);
            message = null;
        }
        public static void RemoveNews(News news)
        {
            Commands.DeleteFrom("News", "ID=" + news.ID);
            news = null;
        }
        public static void RemoveCourse(Course course)
        {
            Commands.DeleteFrom("StudentCourse", "CourseID=" + course.ID);
            Commands.DeleteFrom("TeacherCourse", "CourseID=" + course.ID);
            Commands.DeleteFrom("CourseFile", "CourseID=" + course.ID);
            foreach (Lesson lesson in course.Lessons)
                RemoveLesson(lesson);
            foreach (AssignmentDescription assignmentDescription in course.AssignmentDescriptions)
                RemoveAssignmentDescription(assignmentDescription);
            foreach (CourseGrade courseGrade in course.CourseGrades)
                RemoveCourseGrade(courseGrade);
            Commands.DeleteFrom("Course", "ID=" + course.ID);
            course = null;
        }
        public static void RemoveLesson(Lesson lesson)
        {
            Commands.DeleteFrom("StudentAbsence", "LessonID=" + lesson.ID);
            Commands.DeleteFrom("LessonRoom", "LessonID=" + lesson.ID);
            Commands.DeleteFrom("LessonFile", "LessonID=" + lesson.ID);
            Commands.DeleteFrom("Lesson", "ID=" + lesson.ID);
            lesson = null;
        }
        public static void RemoveRoom(Room room)
        {
            Commands.DeleteFrom("LessonRoom", "RoomID=" + room.ID);
            Commands.DeleteFrom("Room", "ID=" + room.ID);
            room = null;
        }
        public static void RemoveAssignmentDescription(AssignmentDescription assignmentDescription)
        {
            foreach (Assignment assignmentDescriptionAssignment in assignmentDescription.Assignments)
                RemoveAssignment(assignmentDescriptionAssignment);
            Commands.DeleteFrom("AssignmentDescriptionFile", "AssignmentDescriptionID=" + assignmentDescription.ID);
            Commands.DeleteFrom("AssignmentDescription", "ID=" + assignmentDescription.ID);
            assignmentDescription = null;
        }
        public static void RemoveAssignment(Assignment assignment)
        {
            if (assignment.Grade != null)
                RemoveAssignmentGrade(assignment.Grade);

            Commands.DeleteFrom("AssignmentFile", "AssignmentID=" + assignment.ID);
            Commands.DeleteFrom("Assignment", "ID=" + assignment.ID);
            assignment = null;
        }
        public static void RemoveAssignmentGrade(AssignmentGrade grade)
        {
            Commands.SetValue("Assignment", grade.Assignment.ID, "GradeID", "NULL");
            Commands.DeleteFrom("AssignmentGrade", "ID=" + grade.ID);
            grade = null;
        }
        public static void RemoveCourseGrade(CourseGrade grade)
        {
            Commands.DeleteFrom("CourseGrade", "ID=" + grade.ID);
            grade = null;
        }
    }
}
using StudyPlatform.Classes.Model;
using System;

namespace StudyPlatform.Classes.Database
{
    public static class Remover
    {
        public static void RemovePerson(Person person)
        {
            foreach (Message message in person.RecievedMessages)
                Commands.DeleteFrom("messagerecipients" + message.ID, "personid=" + person.ID);
            foreach (Message message in person.SentMessages)
                RemoveMessage(message);

            Commands.DropTable("personsentmessages" + person.ID);
            Commands.DropTable("personrecievedmessages" + person.ID);
            
            if (person is Student)
            {
                foreach (Course course in ((Student)person).Courses)
                    Commands.DeleteFrom("coursestudents" + course.ID, "studentid=" + person.ID);

                foreach (Lesson lesson in ((Student)person).Absences)
                    Commands.DeleteFrom("lessonabsences" + lesson.ID, "studentid=" + person.ID);

                Commands.DropTable("personcourses" + person.ID);
                Commands.DropTable("personassignments" + person.ID);
                Commands.DropTable("personabsences" + person.ID);
            }
            else if (person is Teacher)
            {
                foreach (Course course in ((Teacher)person).Courses)
                    Commands.DeleteFrom("courseteachers" + course.ID, "teacherid=" + person.ID);

                Commands.DropTable("personcourses" + person.ID);
            }
            else if(person is Secretary)
            {
                foreach (News item in Lists.News)
                {
                    if(item.Author.ID == person.ID)
                    {
                        RemoveNews(item);
                    }
                }
            }
            Commands.DeleteFrom("persons", "id=" + person.ID);
            person = null;
        }
        public static void RemoveMessage(Message message)
        {
            Commands.DeleteFrom("messages", "id=" + message.ID);

            foreach (Person recipient in message.Recipients)
                Commands.DeleteFrom("personrecievedmessages" + recipient.ID, "messageid=" + message.ID);

            Commands.DropTable("messagerecipients" + message.ID);
            Commands.DropTable("messageattachments" + message.ID);
            Commands.DeleteFrom("personsentmessages" + message.Sender.ID, "messageid=" + message.ID);
            Commands.DeleteFrom("personrecievedmessages" + message.Sender.ID, "messageid=" + message.ID);
        }
        public static void RemoveNews(News news)
        {
            Commands.DeleteFrom("news", "id=" + news.ID);
            news = null;
        }
        public static void RemoveCourse(Course course)
        {
            Commands.DeleteFrom("courses", "id=" + course.ID);
            
            foreach (Student student in course.Students)
                RemovePerson(student);
            foreach (Teacher teacher in course.Teachers)
                RemovePerson(teacher);
            foreach (Lesson lesson in course.Lessons)
                RemoveLesson(lesson);
            foreach (AssignmentDescription assignmentDescription in course.AssignmentDescriptions)
                RemoveAssignmentDescription(assignmentDescription);
            foreach (CourseGrade courseGrade in course.CourseGrades)
                RemoveCourseGrade(courseGrade);

            Commands.DropTable("coursestudents" + course.ID);
            Commands.DropTable("courseteachers" + course.ID);
            Commands.DropTable("courselessons" + course.ID);
            Commands.DropTable("courseassignmentdescriptions" + course.ID);
            Commands.DropTable("coursegrades" + course.ID);
            Commands.DropTable("coursedocuments" + course.ID);
            course = null;
        }
        public static void RemoveLesson(Lesson lesson)
        {
            // Deleting the different tabels lessonrooms, lessonabsences and lessondocuments.
            Commands.DropTable("lessonrooms" + lesson.ID);
            Commands.DropTable("lessonabsences" + lesson.ID);
            Commands.DropTable("lessondocuments" + lesson.ID);

            // Deleting the ID from lessons.
            Commands.DeleteFrom("lessons", "id=" + lesson.ID);

            // Deleting the lesson.ID from different classes/tabels.
            foreach (Person person in Lists.Persons)
                Commands.DeleteFrom("personabsences" + person.ID, "lessonid=" + lesson.ID);
            foreach (Room room in Lists.Rooms)
                Commands.DeleteFrom("roomreservations" + room.ID, "lessonid=" + lesson.ID);
            foreach (Course course in Lists.Courses)
                Commands.DeleteFrom("courselessons" + course.ID, "lessonid=" + lesson.ID);

            lesson = null;
        }
        public static void RemoveRoom(Room room)
        {
            foreach (Lesson lesson in room.Reservations)
                Commands.DeleteFrom("lessonrooms" + lesson.ID, "roomid=" + room.ID);

            Commands.DeleteFrom("rooms", "id=" + room.ID);
            Commands.DropTable("roomreservations" + room.ID);
            room = null;
        }
        public static void RemoveAssignmentDescription(AssignmentDescription assignmentDescription)
        {
            Commands.DeleteFrom("assignmentdescriptions", "id=" + assignmentDescription.ID);
            Commands.DropTable("assignmentdescriptionassignments" + assignmentDescription.ID);
            Commands.DropTable("assignmentdescriptiondocuments" + assignmentDescription.ID);
            foreach (Assignment assignment in Lists.Assignments)
                if (assignment.AssignmentDescription.ID == assignmentDescription.ID)
                    RemoveAssignment(assignment);
            assignmentDescription = null;
        }
        public static void RemoveAssignment(Assignment assignment)
        {
            Commands.DeleteFrom("assignments", "id=" + assignment.ID);
            Commands.DropTable("assignmentdocuments" + assignment.ID);
            foreach (AssignmentGrade assignmentGrade in Lists.AssignmentGrades)
                if (assignmentGrade.Assignment.ID == assignment.ID)
                    RemoveAssignmentGrade(assignmentGrade);
            foreach (AssignmentDescription assignmentDescription in Lists.AssignmentDescriptions)
                Commands.DeleteFrom("assignmentdescriptionassignments" + assignmentDescription.ID, "assignmentid=" + assignment.ID);
            foreach (Student student in Lists.Students)
                Commands.DeleteFrom("personassignments" + student.ID, "assignmentid=" + assignment.ID);
            assignment = null;
        }
        public static void RemoveAssignmentGrade(AssignmentGrade grade)
        {
            Commands.SetValue("assignments", grade.Assignment.ID, "gradeid", "NULL");
            Commands.DeleteFrom("assignmentgrades", "id=" + grade.ID);
            grade = null;
        }
        public static void RemoveCourseGrade(CourseGrade grade)
        {
            Commands.DeleteFrom("coursegrades" + grade.Course.ID, "gradeid=" + grade.ID);
            Commands.DeleteFrom("coursegrades", "id=" + grade.ID);
            grade = null;
        }
    }
}
using StudyPlatform.Classes.Model;
using System;

namespace StudyPlatform.Classes.Database
{
    public static class Remover
    {
        public static void RemovePerson(Person person)
        {
            throw new NotImplementedException();
        }
        public static void RemoveMessage(Message message)
        {
            Commands.DeleteFrom("messages", "id=" + message.ID);
            Commands.DropTable("messagerecipients" + message.ID);
            Commands.DropTable("messageattachments" + message.ID);

            foreach (Person recipient in message.Recipients)
            {
                Commands.DeleteFrom("personrecievedmessages" + recipient.ID, "id=" + message.ID);
            }

            Commands.DeleteFrom("personsentmessages" + message.SenderId, "id=" + message.ID);
            Commands.DeleteFrom("personrecievedmessages" + message.SenderId, "id=" + message.ID);
        }
        public static void RemoveNews(News news)
        {
            Commands.DeleteFrom("news", "id=" + news.ID);
        }
        public static void RemoveCourse(Course course)
        {
            //course
            Commands.DeleteFrom("courses", "id=" + course.ID);
            //people
            Commands.DropTable("courseteachers" + course.ID);
            Commands.DropTable("coursestudents" + course.ID);
            foreach (Person person in Lists.Persons)
                Commands.DeleteFrom("personcourses" + person.ID, "courseid=" + course.ID);
            //lessons
            //Commands.DropTable("courselessons" + course.ID); ---- metoden burde gerne fjerne dette?
            foreach (Lesson lesson in Lists.Lessons)
                if (lesson.Course.ID == course.ID)
                    RemoveLesson(lesson);
            //assignmentdescription
            //Commands.DropTable("courseassignmentdescriptions" + course.ID); ---- metoden burde gerne fjerne dette?
            foreach (AssignmentDescription assignmentdescription in Lists.AssignmentDescriptions)
                if (assignmentdescription.Course.ID == course.ID)
                    RemoveAssignmentDescription(assignmentdescription);
            //coursegrades
            Commands.DropTable("coursegrades" + course.ID);
            Commands.DeleteFrom("coursegrades", "courseid=" + course.ID);
            //coursedocuments
            Commands.DropTable("coursedocuments" + course.ID);
            //brug for måde at fjerne selve dokumentet. generisk metode som tager filepath som vi kan bruge til alle slags dokumenter?
        }
        public static void RemoveLesson(Lesson lesson)
        {
            Commands.DeleteFrom("lessons", "id=" + lesson.ID);

        }
        public static void RemoveRoom(Room room)
        {
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
                if (assignment. == assignmentDescription.ID)
                    RemoveAssignment(assignment);
        }
        public static void RemoveAssignment(Assignment assignment)
        {

            throw new NotImplementedException();
        }
        public static void RemoveAssignmentGrade(AssignmentGrade grade)
        {
            throw new NotImplementedException();
        }
        public static void RemoveCourseGrade(CourseGrade grade)
        {
            Commands.DeleteFrom("coursegrades", "id=" + grade.ID);
        }
    }
}
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
            throw new NotImplementedException();
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
            Commands.DropTable("courselessons" + course.ID);
            //assignmentdescription
            Commands.DropTable("courseassignmentdescriptions" + course.ID);
            foreach (AssignmentDescription assignmentdescription in Lists.AssignmentDescriptions)
                if (assignmentdescription.Course.ID == course.ID)
                    RemoveAssignmentDescription(assignmentdescription);
            //coursegrades
            Commands.DropTable("coursegrades" + course.ID);
            //coursedocuments
            Commands.DropTable("coursedocuments" + course.ID);
            Commands.DeleteFrom("coursegrades", "courseid=" + course.ID);
            foreach (Person person in Lists.Persons)
                Commands.DeleteFrom("personcourses" + person.ID, "courseid=" + course.ID);
            foreach (AssignmentDescription assigmentdescription in Lists.AssignmentDescriptions)
                if (true)
                    Commands.DeleteFrom("assignments", "assignmentdescriptionid=" + assigmentdescription.ID);
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
            {
                Commands.DeleteFrom("assignments", "assignmentid=" + assignment.ID);
            }

            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

    }
}
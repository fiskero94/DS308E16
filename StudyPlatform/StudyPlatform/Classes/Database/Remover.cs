using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Model;

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
            Commands.DeleteFrom("courses", "id=" + course.ID);
            Commands.DropTable("courseteachers" + course.ID);
            Commands.DropTable("coursestudents" + course.ID);
            Commands.DropTable("courselessons" + course.ID);
            Commands.DropTable("courseassignmentdescriptions" + course.ID);
            Commands.DropTable("coursegrades" + course.ID);
            Commands.DropTable("coursedocuments" + course.ID);
            Commands.DeleteFrom("assignmentdescriptions", "courseid=" + course.ID);
            Commands.DeleteFrom("coursegrades", "courseid=" + course.ID);
            foreach (Person person in Lists.Persons)
                Commands.DeleteFrom("personcourses" + person.ID, "courseid=" + course.ID);
            foreach (AssignmentDescription assigmentdescription in Lists.AssignmentDescriptions)
                if (true) // Why tho?
                    Commands.DeleteFrom("assignments", "assignmentdescriptionid=" + assigmentdescription.ID);
            //Delete lessons som var tilknyttet course
        }
        public static void RemoveLesson(Lesson lesson)
        {
            throw new NotImplementedException();
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
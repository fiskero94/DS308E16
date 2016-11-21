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
            Query.ExecuteQueryString("DELETE FROM studyplatform.news WHERE id='" + news.ID + "';");
            //throw new NotImplementedException();
        }
        public static void RemoveCourse(Course course)
        {

            throw new NotImplementedException();
        }
        public static void RemoveLesson(Lesson lesson)
        {
            throw new NotImplementedException();
        }
        public static void RemoveRoom(Room room)
        {
            Query.ExecuteQueryString("DELETE FROM studieplatform.rooms WHERE id='" + room.ID + "';");
            Query.ExecuteQueryString("DROP TABLE studieplatform.rooms WHERE id='" + room.ID + "';");
        }
        public static void RemoveAssignmentDescription(AssignmentDescription assignmentDescription)
        {

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
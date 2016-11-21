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
            Query.ExecuteQueryString("DELETE FROM studyplatform.courses WHERE id='" + course.ID + "';");
            Query.ExecuteQueryString("DROP TABLE studyplatform.courseteachers" + course.ID + ";");
            Query.ExecuteQueryString("DROP TABLE studyplatform.coursestudents" + course.ID + ";");
            Query.ExecuteQueryString("DROP TABLE studyplatform.courselessons" + course.ID + ";");
            Query.ExecuteQueryString("DROP TABLE studyplatform.courseassignmentdescriptions" + course.ID + ";");
            Query.ExecuteQueryString("DROP TABLE studyplatform.coursegrades" + course.ID + ";");
            Query.ExecuteQueryString("DROP TABLE studyplatform.coursedocuments" + course.ID + ";");
            foreach (Person person in Lists.Persons)
            {
                Query.ExecuteQueryString("DELETE FROM studyplatform.personcourses" + person.ID + " WHERE courseid='" + course.ID + "';");
            }
            Query.ExecuteQueryString("DELETE FROM studyplatform.assignmentdescriptions WHERE courseid='" + course.ID + "';");
            Query.ExecuteQueryString("DELETE FROM studyplatform.coursegrades WHERE courseid='" + course.ID + "';");
            foreach (AssignmentDescription assigmentdescription in Lists.AssignmentDescriptions)
            {
                if()
                Query.ExecuteQueryString("DELETE FROM studyplatform.assignments WHERE assignmentdescriptionid='" + assigmentdescription.ID + "';");
            }

            //throw new NotImplementedException();
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
            Query.ExecuteQueryString("DELETE FROM studieplatform.assignmentdescriptions WHERE id='" + assignmentDescription.ID + "';");
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
            DateTime.Parse("")
            throw new NotImplementedException();
        }

    }
}
﻿using StudyPlatform.Classes.Model;
using System;

namespace StudyPlatform.Classes.Database
{
    public static class Remover
    {
        public static void RemovePerson(Person person)
        {
            Commands.DeleteFrom("persons", "id=" + person.ID);
            Commands.DropTable("personsentmessages" + person.ID);
            Commands.DropTable("personrecievedmessages" + person.ID);

            foreach (Message message in Lists.Messages)
            {
                Commands.DeleteFrom("messagerecipients" + message.ID, "personid=" + person.ID);
            }

            if (person is Student)
            {
                foreach (Course course in Lists.Courses)
                {
                    Commands.DeleteFrom("coursestudents" + course.ID, "studentid=" + person.ID);
                }
                foreach (Lesson lesson in Lists.lessons)
                {
                    Commands.DeleteFrom("lessonabsences" + lesson.ID, "studentid=" + person.ID);
                }
                Commands.DropTable("personcourses" + person.ID);
                Commands.DropTable("personassignments" + person.ID);
                Commands.DropTable("personabsences" + person.ID);
            }
            if (person is Teacher)
            {
                foreach (Course course in Lists.Courses)
                {
                    Commands.DeleteFrom("courseteachers" + course.ID, "teacherid=" + person.ID);
                }
                Commands.DropTable("personcourses" + person.ID);
            }
            person = null;

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
            news = null;
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
            course = null;
            //brug for måde at fjerne selve dokumentet. generisk metode som tager filepath som vi kan bruge til alle slags dokumenter?
        }
        public static void RemoveLesson(Lesson lesson)
        {
            //Deleting the different tabels lessonrooms, lessonabsences and lessondocuments.
            Commands.DropTable("lessonrooms" + lesson.ID);
            Commands.DropTable("lessonabsences" + lesson.ID);
            Commands.DropTable("lessondocuments" + lesson.ID);

            //deleting the ID from lessons.
            Commands.DeleteFrom("lessons", "id=" + lesson.ID);

            // Deleting the lesson.ID from different classes/tabels.
            foreach (Person person in Lists.Persons)
                Commands.DeleteFrom("personabsences" + person.ID, "lessonid=" + lesson.ID);
            foreach (Room room in Lists.Rooms)
            {
                Commands.DeleteFrom("roomreservations" + room.ID, "lessonid=" + lesson.ID);
            }
            foreach (Course course in Lists.Courses)
            {
                Commands.DeleteFrom("courselessons" + course.ID, "lessonid=" + lesson.ID);
            }
            lesson = null;
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
            Commands.DeleteFrom("assignments", "id=" + assignment.ID);
            Commands.DropTable("assignmentdocuments" + assignment.ID);
            throw new NotImplementedException();
        }
        public static void RemoveAssignmentGrade(AssignmentGrade grade)
        {
            Commands.DeleteFrom("assignmentgrades", "id=" + grade.ID);
        }
        public static void RemoveCourseGrade(CourseGrade grade)
        {
            Commands.DeleteFrom("coursegrades", "id=" + grade.ID);
        }
    }
}
using StudyPlatform.Classes.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudyPlatform.Classes.Database
{
    public static class Creator
    {
        private static void EnsureNotNull(params object[] objects)
        {
            foreach (object obj in objects)
                if (obj == null)
                    throw new ArgumentNullException();
        }
        static void CreateTable(string tableName, params string[] variables)
        {
            string query = "USE studyplatform; CREATE TABLE " + tableName + " (";
            foreach (string variable in variables)
            {
                query += variable;
                query += ", ";
            }
            query = query.TrimEnd(' ');
            query = query.TrimEnd(',');
            query += ");";
            Query.ExecuteQueryString(query);
        }
        private static void CreatePerson(string name, string username, string password, string type) =>
            Query.ExecuteQueryString("INSERT INTO studyplatform.persons VALUES(NULL,'" +
                                     username + "','" + password + "','" + name + "','" + type + "');");
        public static void CreateStudent(string name, string username, string password)
        {
            EnsureNotNull(name, username, password);
            CreatePerson(name, username, password, "student");
            Student student = Lists.Students.Last();
            CreateTable("personmessages" + student.ID, "messageid INT UNSIGNED NOT NULL");
            CreateTable("personcourses" + student.ID, "courseid INT UNSIGNED NOT NULL");
            CreateTable("personabscences" + student.ID, "lessonid INT UNSIGNED NOT NULL");
        }
        public static void CreateTeacher(string name, string username, string password)
        {
            EnsureNotNull(name, username, password);
            CreatePerson(name, username, password, "teacher");
            Teacher teacher = Lists.Teachers.Last();
            CreateTable("personmessages" + teacher.ID, "messageid INT UNSIGNED NOT NULL");
            CreateTable("personcourses" + teacher.ID, "courseid INT UNSIGNED NOT NULL");
        }
        public static void CreateSecretary(string name, string username, string password)
        {
            EnsureNotNull(name, username, password);
            CreatePerson(name, username, password, "secretary");
            Secretary secretary = Lists.Secretaries.Last();
            CreateTable("personmessages" + secretary.ID, "messageid INT UNSIGNED NOT NULL");
        }
        public static void CreateMessage(Person sender, string title, string text, List<Person> recipients, List<string> filepaths)
        {
            // Ensure input is not null, throw ArgumentNullException (Use EnsureNotNull method)
            // Add new Message to the studyplatform.messages table
            // Get the ID of the newly created Message
            // Create new table messagerecipientsN where N is the ID of the Message
            // Add the ID's of the recipients to the table
            // Create new table messageattachmentsN where N is the ID of the Message
            // Add the filepaths to the table
            // Input the ID of the Message into the personsentmessagesN tables for the sender.
            // Input the ID of the Message into the personrecievedmessagesN tables foreach of the recipients
            throw new NotImplementedException();
        }
        public static void CreateNews(Person author, string title, string text)
        {
            // Ensure input is not null, throw ArgumentNullException (Use EnsureNotNull method)
            EnsureNotNull(author, title, text);
            // Add new News to the studyplatform.news table
            Query.ExecuteQueryString("INSERT INTO studyplatform.news VALUES(NULL,'" +
                                     author.ID + "','" + title + "','" + text + "','" + "NOW()" + "');"); // tilføjet now() 
            throw new NotImplementedException();
        }
        public static void CreateCourse(string name, string description)
        {
            // Ensure input is not null, throw ArgumentNullException(Use EnsureNotNull method)
            EnsureNotNull(name, description);
            // Add new Course to the studyplatform.courses table
            Query.ExecuteQueryString("INSERT INTO studyplatform.courses VALUES(NULL,'" +
                                     name + "','" + description + "');");
            // Get the ID of the newly created Course
            Course course = Lists.Courses.Last();
            // Create new table courseteachersN where N is the ID of the Course
            CreateTable("courseteachers" + course.ID, "teacherid INT UNSIGNED NOT NULL");
            // Create new table coursestudentsN where N is the ID of the Course
            CreateTable("coursestudents" + course.ID, "studentid INT UNSIGNED NOT NULL");
            // Create new table courselessonsN where N is the ID of the Course
            CreateTable("courselessons" + course.ID, "lessonid INT UNSIGNED NOT NULL");
            // Create new table courseassignmentdescriptionsN where N is the ID of the Course
            CreateTable("courseassignmentdescriptions" + course.ID, "assignmentdescriptionid INT UNSIGNED NOT NULL");
            // Create new table coursegradesN where N is the ID of the Course
            CreateTable("coursegrades" + course.ID, "gradeid INT UNSIGNED NOT NULL");
            // Create new table messagerecipientsN where N is the ID of the Course - teacher mente coursedocuments istedet
            CreateTable("coursedocuments" + course.ID, "filepath TEXT NOT NULL");
            //throw new NotImplementedException();
        }
        public static void CreateLesson(DateTime date, string description, bool online, bool active, List<Room> rooms, List<string> filepaths, Course course)
        {
            EnsureNotNull(date, description, online, active, rooms, filepaths, course);
            Query.ExecuteQueryString("INSERT INTO studyplatform.lessons VALUES(NULL,'" +
                             date + "','" + description + "','" + online.ToString().ToUpper() + "','" + active.ToString().ToUpper() + "');");
            Lesson lesson = Lists.lessons.Last();
            CreateTable("lessonrooms" + lesson.ID, "roomid INT UNSIGNED NOT NULL");
            CreateTable("lessonabsences" + lesson.ID, "absenceid INT UNSIGNED NOT NULL");
            CreateTable("lessondocuments" + lesson.ID, "TEXT NOT NULL");

            foreach (Room room in rooms)
            {
                Query.ExecuteQueryString("INSERT INTO studyplatform.lessonrooms" + lesson.ID + " VALUES(" + room.ID + ");");
                Query.ExecuteQueryString("INSERT INTO studyplatform.roomreservations" + room.ID + " VALUES(" + lesson.ID + ");");
            }

            foreach (string filepath in filepaths)
            {
                Query.ExecuteQueryString("INSERT INTO studyplatform.lessondocuments" + lesson.ID + " VALUES(" + filepath + ");");
            }

            Query.ExecuteQueryString("INSERT INTO studyplatform.courselessons" + course.ID + " VALUES(" + lesson.ID + ");");

            // Add new Lesson to the studyplatform.lessons table (enter bool values as TRUE or FALSE, not 'TRUE' or 'FALSE')
            // Get the ID of the newly created Lesson
            // Create new table lessonroomsN where N is the ID of the Lesson
            // Add the ID's of the rooms to the table
            // Create new table lessonabsencesN where N is the ID of the Lesson
            // Create new table lessondocumentsN where N is the ID of the Lesson
            // Add the filepaths to the table
            // Input the ID of the Lesson into the courselessonsN tables for the Course.
            // Input the ID of the Lesson into the roomreservationsN tables foreach of the Rooms.
        }
        public static void CreateRoom(string name)
        {
            EnsureNotNull(name);
            Query.ExecuteQueryString("INSERT INTO studyplatform.rooms VALUES(NULL,'" + name + "');");
            Room room = Lists.Rooms.Last();
            CreateTable("roomreservations" + room.ID, "lessonid INT UNSIGNED NOT NULL");
        }

        public static void CreateAssignmentDescription(Course course, string description, DateTime deadline, List<string> filepaths)
        {
            // Ensure input is not null, throw ArgumentNullException (Use EnsureNotNull method)
            EnsureNotNull(course, description, deadline, filepaths);

            // Add new AssignmentDescription to the studyplatform.assignmentdescription table
            Query.ExecuteQueryString("INSERT INTO studyplatform.assignmentdescriptions VALUES(NULL, '" + course +
                                    "','" + description + "','" + deadline + "');");

            // Get the ID of the newly created AssignmentDescription
            AssignmentDescription assignmentDescription = Lists.AssignmentDescriptions.Last();

            // Create new table assignmentdescriptionassignmentsN where N is the ID of the AssignmentDescription
            CreateTable("assignmentdescriptionassignments" + assignmentDescription.ID, "assignmentid INT UNSIGNED NOT NULL");

            // Create new table assignmentdescriptiondocumentsN where N is the ID of the AssignmentDescription
            CreateTable("assignmentdescriptiondocuments" + assignmentDescription.ID, "filepath TEXT NOT NULL");

            // Add the filepaths to the table
            foreach (string filepath in filepaths)
            {
                Query.ExecuteQueryString("INSERT INTO studyplatform.assignentdescriptiondocuments " +
                    assignmentDescription.ID + " VALUES(NULL, '" + filepath + "');");
            }

            // Input the ID of the AssignmentDescription into the courseassignmentdescriptionsN table for the Course
            Query.ExecuteQueryString("INSERT INTO studyplatform.courseassignmentdescription" + course.ID +
                                    " VALUES(NULL, '" + assignmentDescription.ID + "');");
        }
        public static void CreateAssignment(AssignmentDescription assignmentDescription, Student student, string comment, List<string> filepaths)
        {
            // Ensure input is not null, throw ArgumentNullException (Use EnsureNotNull method)
            // Add new Assignment to the studyplatform.assignments table
            // Get the ID of the newly created Assignment
            // Create new table assignmentdocumentsN where N is the ID of the Assignment
            // Add the filepaths to the table
            // Input the ID of the Assignment into the assignmentdescriptionassignmentsN table for the AssignmentDescription
            // Input the ID of the Student into the personassignmentsN table for the Student
            throw new NotImplementedException();
        }
        public static void CreateAssignmentGrade(string grade, Assignment assignment)
        {
            // Ensure input is not null, throw ArgumentNullException (Use EnsureNotNull method)
            EnsureNotNull(grade, assignment);
            // Ensure that grade is a part of the set Grade.ValidGrades, throw exception if not.
            if (!Grade.ValidGrades.Contains(grade))
            {
                throw new Exception();
            }
            // Add new AssignmentGrade to the studyplatform.assignmentgrades table
            Query.ExecuteQueryString("INSERT INTO studyplatform.assignmentgrade VALUES(NULL,'" +
                                     grade + "','" + assignment.ID + "');");
            // Get the ID of the newly created AssignmentGrade
            AssignmentGrade assignmentGrade = Lists.AssignmentGrades.Last();
            // Input the ID into the gradeid variable of the corrosponding assigment.
            Editor.SetValue("assignments", assignment.ID, "gradeid", assignmentGrade.ID.ToString());

            throw new NotImplementedException();
        }
        public static void CreateCourseGrade(string grade, Course course, Student student)
        {
            // Ensure input is not null, throw ArgumentNullException (Use EnsureNotNull method)
            // Ensure that grade is a part of the set Grade.ValidGrades, throw exception if not.
            // Add new CourseGrade to the studyplatform.coursegrades table
            // Get the ID of the newly created CourseGrade
            // Input the ID into the coursegrades table of the corrosponding course.
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyPlatform.Classes.Model;

namespace StudyPlatform.Tests
{
    static class Instances
    {
        public static Assignment Assignment
        {
            get
            {
                return new Assignment(ID, ID, ID, Comment, ID, Date);
            }
        }
        public static AssignmentDescription AssignmentDescription
        {
            get
            {
                return new AssignmentDescription(ID, ID, Description, Date);
            }
        }
        public static AssignmentGrade AssignmentGrade
        {
            get
            {
                return new AssignmentGrade(ID, Grade, Comment, ID);
            }
        }
        public static Course Course
        {
            get
            {
                return new Course(ID, Name, Description);
            }
        }
        public static CourseGrade CourseGrade
        {
            get
            {
                return new CourseGrade(ID, Grade, Comment, ID, ID);
            }
        }
        public static Lesson Lesson
        {
            get
            {
                return new Lesson(ID, ID, Date, Description, Online, Active);
            }
        }
        public static Message Message
        {
            get
            {
                return new Message(ID, ID, Title, Text);
            }
        }
        public static News News
        {
            get
            {
                return new News(ID, ID, Title, Text, Date);
            }
        }
        public static Room Room
        {
            get
            {
                return new Room(ID, Name);
            }
        }
        public static Secretary Secretary
        {
            get
            {
                return new Secretary(ID, Name);
            }
        }
        public static Teacher Teacher
        {
            get
            {
                return new Teacher(ID, Name);
            }
        }
        public static Student Student
        {
            get
            {
                return new Student(ID, Name);
            }
        }
        public static uint ID
        {
            get
            {
                return 2;
            }
        }
        public static DateTime Date
        {
            get
            {
                return DateTime.Now;
            }
        }
        public static string Comment
        {
            get
            {
                return "Comment";
            }
        }
        public static string Description
        {
            get
            {
                return "Description";
            }
        }
        public static string Grade
        {
            get
            {
                return "12";
            }
        }
        public static string Name
        {
            get
            {
                return "Name";
            }
        }
        public static string Title
        {
            get
            {
                return "Title";
            }
        }
        public static string Text
        {
            get
            {
                return "Text";
            }
        }
        public static string Username
        {
            get
            {
                return "Username";
            }
        }
        public static string Password
        {
            get
            {
                return "Password";
            }
        }
        public static string EmptyString
        {
            get
            {
                return "";
            }
        }
        public static bool Online
        {
            get
            {
                return true;
            }
        }
        public static bool Active
        {
            get
            {
                return true;
            }
        }
    }
}
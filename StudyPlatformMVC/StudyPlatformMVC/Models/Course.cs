﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatformMVC.Database;

namespace StudyPlatformMVC.Models
{
    public class Course : Entity<Course>
    {
        private string _name;
        private string _description;

        public Course(uint id, string name, string description) : base(id)
        {
            _name = name;
            _description = description;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                Commands.SetValue("Course", ID, "Name", value);
                _name = value;
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                Commands.SetValue("Course", ID, "Description", value);
                _description = value;
            }
        }
        public List<Teacher> Teachers => GetRelations<Teacher>("TeacherCourse", "TeacherID", "CourseID", ID);
        public List<Student> Students => GetRelations<Student>("StudentCourse", "StudentID", "CourseID", ID);
        public List<Lesson> Lessons => Lesson.GetByConditions("CourseID=" + ID);
        public List<AssignmentDescription> AssignmentDescriptions => AssignmentDescription.GetByConditions("CourseID=" + ID);

        public List<AssignmentDescription> CurrentAssignmentDescriptions
        {
            get
            {
                List<AssignmentDescription> currentList = new List<AssignmentDescription>();
                foreach (AssignmentDescription assignmentDescription in AssignmentDescriptions)
                {
                    if(assignmentDescription.Deadline < DateTime.Now)
                        currentList.Add(assignmentDescription);
                }
                return currentList;
            }
        }
        public List<Lesson> CurrentLessons
        {
            get
            {
                List<Lesson> currentList = new List<Lesson>();
                foreach (Lesson lesson in Lessons)
                {
                    if(lesson.DateTime < DateTime.Now)
                        currentList.Add(lesson);
                }
                return currentList;
            }
        }

        public List<CourseGrade> CourseGrades => CourseGrade.GetByConditions("CourseID=" + ID);
        public List<string> Documents => GetDocuments("CourseFile", "CourseID", ID);
        public void AddTeacher(Teacher teacher) => Commands.InsertInto("TeacherCourse", teacher.ID.ToString(), ID.ToString());
        public void RemoveTeacher(Teacher teacher) => Commands.DeleteFrom("TeacherCourse", "TeacherID=" + teacher.ID + " AND CourseID=" + ID);
        public void AddStudent(Student student) => Commands.InsertInto("StudentCourse", student.ID.ToString(), ID.ToString());
        public void RemoveStudent(Student student)
        {
            foreach (Assignment assignment in student.Assignments)
                if (assignment.AssignmentDescription.Course.ID == ID)
                    Remover.RemoveAssignment(assignment);
            foreach (Lesson lesson in Lessons)
                lesson.RemoveAbsence(student);
            Commands.DeleteFrom("StudentCourse", "StudentID=" + student.ID + " AND CourseID=" + ID);
        }

        public void AddDocument(string filepath) =>
            Commands.InsertInto("CourseFile", ID.ToString(), filepath);
        public void RemoveDocument(string filepath) =>
            Commands.DeleteFrom("CourseFile", "CourseID=" + ID + " AND Filepath=" + filepath);

        public void Remove() => Remover.RemoveCourse(this);
        public static Course New(string name, string description)
        {
            Creator.CreateCourse(name, description);
            return GetLatest(1).Single();
        }
    }
}
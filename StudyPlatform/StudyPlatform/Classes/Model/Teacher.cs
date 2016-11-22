﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Teacher : Person
    {
        public List<Course> Courses
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.personcourses" + ID);
                uint[] ids = Extractor.ExtractIDs(query.Execute());
                List<Course> courses = new List<Course>();
                foreach (uint id in ids)
                    courses.Add(Getters.GetCourseByID(id));
                return courses;
            }
        }
        public Teacher(uint id, string name) : base(id, name)
        {

        }
    }
}
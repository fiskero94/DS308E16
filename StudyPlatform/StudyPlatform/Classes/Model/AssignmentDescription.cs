﻿using StudyPlatform.Classes.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public class AssignmentDescription
    {
        private uint _id;
        private uint _courseid;
        private string _description;
        private DateTime _date;

        public AssignmentDescription(uint id, uint courseid, string description, DateTime date)
        {
            _id = id;
            _courseid = courseid;
            _description = description;
            _date = date;
        }   
        
        public uint ID
        {
            get
            {
                return _id;
            }
        }
        public Course Course
        {
            get
            {
                return Getters.GetCourseByID(_courseid);
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
                if (value == null)
                    throw new ArgumentNullException();
                else
                {
                    Commands.SetValue("assignmentdescriptions", ID, "description", value);
                    _description = value;
                }
            }
        }
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                else
                {
                    Commands.SetValue("assignmentdescriptions", ID, "date", value.ToString("yyyy-MM-dd HH:mm:ss"));
                    _date = value;
                }
            }
        }
        public List<Assignment> Assignments
        {
            get
            {

            }
        }
        public List<string> Documents
        {
            get
            {

            }
        }
    }
}
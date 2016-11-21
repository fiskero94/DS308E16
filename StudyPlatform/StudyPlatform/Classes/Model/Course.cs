using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Course
    {
        public Course(uint id, string name, string description)
        {
            _id = id;
            _name = name;
            _description = description;
        }
        private uint _id;
        public uint ID
        {
            get
            {
                return _id;
            }
        }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                else
                {
                    Editor.SetValue("courses", ID, "name", value);
                    _name = value;
                }
            }
        }
        private string _description;
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
                    Editor.SetValue("courses", ID, "description", value);
                    _description = value;
                }
            }
        }
    }
}
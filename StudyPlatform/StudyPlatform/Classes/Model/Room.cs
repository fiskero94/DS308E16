using StudyPlatform.Classes.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public class Room
    {
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
                if(value == null)
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    Commands.SetValue("rooms", ID, "name", value);
                    _name = value;
                }
            }
        }

        public Room(uint id, string name)
        {
            _id = id;
            Name = name;
        }
    }
}
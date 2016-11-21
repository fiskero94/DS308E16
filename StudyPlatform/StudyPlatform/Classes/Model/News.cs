using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class News
    {
        public News(uint id, uint authorid, string title, string text)
        {
            _id = id;
            _authorid = authorid;
            _title = title;
            _text = text;
            // mangler date
        }

        private uint _id;
        public uint ID
        {
            get
            {
                return _id;
            }
        }
        private uint _authorid;
        public uint AuthorID
        {
            get
            {
                return _authorid;
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                else
                {
                    Editor.SetValue("news", ID, "title", value);
                    _title = value;
                }
            }
        }
        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                else
                {
                    Editor.SetValue("news", ID, "text", value);
                    _text = value;
                }
            }
        }
        

        /*
         HALLOOOOOOOOOOOOO Mangler date - convert fra mysql til datetime
        */

    }

}
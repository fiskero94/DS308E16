using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class News : Entity<News>
    {
        private readonly uint _authorid;
        private string _title;
        private string _text;

        public News(uint id, uint authorid, string title, string text, DateTime dateTimePublished) : base(id)
        {
            _authorid = authorid;
            _title = title;
            _text = text;
            DateTimePublished = dateTimePublished;
        }

        public Secretary Author => Secretary.GetByID(_authorid);
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value == string.Empty)
                    throw new ArgumentException();
                Commands.SetValue("News", ID, "Title", value);
                _title = value;
            }
        }
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (value == string.Empty)
                    throw new ArgumentException();
                Commands.SetValue("News", ID, "Text", value);
                _text = value;
            }
        }
        public DateTime DateTimePublished { get; }

        public void Remove() => Remover.RemoveNews(this);
        public static News New(Secretary author, string title, string text)
        {
            Creator.CreateNews(author, title, text);
            return GetLatest(1).Single();
        }
    }
}
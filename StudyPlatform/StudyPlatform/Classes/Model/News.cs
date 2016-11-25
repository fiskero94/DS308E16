﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class News
    {
        private uint _id;
        private uint _authorid;
        private string _title;
        private string _text;
        private DateTime _date;

        public News(uint id, uint authorid, string title, string text, DateTime date)
        {
            _id = id;
            _authorid = authorid;
            _title = title;
            _text = text;
            _date = date;
        }
        
        public uint ID => _id;

        public Secretary Author => Getters.GetPersonByID(_authorid) as Secretary;

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
                    Commands.SetValue("news", ID, "title", value);
                    _title = value;
                }
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
                if (value == null)
                    throw new ArgumentNullException();
                else
                {
                    Commands.SetValue("news", ID, "text", value);
                    _text = value;
                }
            }
        }
        public DateTime Date => _date;

        public static News New(Secretary author, string title, string text)
        {
            Creator.CreateNews(author, title, text);
            return Getters.GetLatestNews(1).Single();
        }
        public void Remove() => Remover.RemoveNews(this);
    }
}
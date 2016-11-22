using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Model
{
    public class Message
    {
        private uint _id;
        public uint ID
        {
            get
            {
                return _id;
            }
        }

        private uint _senderid;
        public Person Sender
        {
            get
            {
                return Getters.GetPersonByID(_senderid);
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
        }

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get
            {
                return _date;
            }
        }

        private List<Person> _recipients;
        public List<Person> Recipients
        {
            get
            {
                return _recipients;
            }
        }

        private List<string> _attachments;
        public List<string> Attachments
        {
            get
            {
                return _attachments;
            }
        }

        public Message(uint senderid, string title, string text)
        {
            _senderid = senderid;
            _title = title;
            _text = text;
            _date = DateTime.Now;
        }

        //public Message(uint id, uint senderid, string title, string text, List<Person> recipients, List<string> attachments)
        //{
        //    _id = id;
        //    _senderid = senderid;
        //    _title = title;
        //    _text = text;
        //    Recipients = recipients;
        //    Attachments = attachments;
        //    Date = DateTime.Now;
        //}

    }
}
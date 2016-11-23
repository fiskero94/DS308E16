﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;

namespace StudyPlatform.Classes.Model
{
    public abstract class Person
    {
        private uint _id;
        private string _name;

        protected Person(uint id, string name)
        {
            _id = id;
            Name = name;
        }

        public uint ID
        {
            get
            {
                return _id;
            }
        }
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
                    Commands.SetValue("persons", ID, "name", value);
                    _name = value;
                }
            }
        }
        public List<Message> SentMessages
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.personsentmessages" + ID);
                uint[] ids = Extractor.ExtractIDs(query.Execute());
                List<Message> sentMessages = new List<Message>();
                foreach (uint id in ids)
                    sentMessages.Add(Getters.GetMessageByID(id));
                return sentMessages;
            }
        }
        public List<Message> RecievedMessages
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.personrecievedmessages" + ID);
                uint[] ids = Extractor.ExtractIDs(query.Execute());
                List<Message> recievedMessages = new List<Message>();
                foreach (uint id in ids)
                    recievedMessages.Add(Getters.GetMessageByID(id));
                return recievedMessages;
            }
        }


        public override bool Equals(object obj)
        {
            if(obj == null || !(obj is Person))
            {
                return false;
            }

            var item = obj as Person;

            this.ID.Equals(item.ID);
            return this.ID.Equals(item.ID);
        }



    }
}
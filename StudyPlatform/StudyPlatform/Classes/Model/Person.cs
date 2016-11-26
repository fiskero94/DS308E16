using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;

namespace StudyPlatform.Classes.Model
{
    public abstract class Person
    {
        private string _name;

        protected Person(uint id, string name)
        {
            ID = id;
            _name = name;
        }

        public uint ID { get; }
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
                Commands.SetValue("Person", ID, "name", value);
                _name = value;
            }
        }
        public List<Message> SentMessages => Getters.GetMessagesByConditions("SenderID=" + ID);
        public List<Message> RecievedMessages
        {
            get
            {
                Query query = new Query("SELECT * FROM studyplatform.MessageRecipient WHERE PersonID=" + ID);
                uint[] ids = Extractor.ExtractIDs(query.Execute(), "MessageID");
                List<Message> recievedMessages = new List<Message>();
                foreach (uint id in ids)
                    recievedMessages.Add(Getters.GetMessageByID(id));
                return recievedMessages;
            }
        }

        public void Remove() => Remover.RemovePerson(this);
    }
}
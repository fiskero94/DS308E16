using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;
using StudyPlatform.Classes.Model;

namespace StudyPlatform.Classes.Model
{
    public abstract class Person : Entity<Person>
    {
        private string _name;

        protected Person(uint id, string name) : base(id)
        {
            _name = name;
        }
        
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                Commands.SetValue("Person", ID, "name", value);
                _name = value;
            }
        }
        public List<Message> SentMessages => Message.GetByConditions("SenderID=" + ID);
        public List<Message> RecievedMessages
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Remove() => Remover.RemovePerson(this);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyPlatform.Classes.Database;

namespace StudyPlatform.Classes.Model
{
    public class Secretary : Person
    {
        public Secretary(uint id, string name) : base(id, name)
        {

        }

        public static Secretary New(string name, string username, string password)
        {
            Creator.CreateSecretary(name, username, password);
            return Getters.GetLatestPersons(1).Single() as Secretary;
        }
    }
}
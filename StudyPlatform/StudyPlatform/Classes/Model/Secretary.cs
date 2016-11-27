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
            return GetLatest(1).Single();
        }

        public new static Secretary GetByID(uint id) => GetRecordByID<Secretary>(id);
        public new static List<Secretary> GetAll() => GetAll<Secretary>();
        public new static List<Secretary> GetByConditions(params string[] conditions)
            => GetRecordsByConditions<Secretary>(conditions);
        public new static List<Secretary> GetLatest(uint count) => GetLatestRecords<Secretary>(count);
    }
}
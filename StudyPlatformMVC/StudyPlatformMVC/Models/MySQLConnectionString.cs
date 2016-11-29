using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudyPlatformMVC.Models
{
    public class MySQLConnectionString : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Student> Students { get; set; }
        //public DbSet<PersonCourse> PersonCourse { get; set; }


    }
}
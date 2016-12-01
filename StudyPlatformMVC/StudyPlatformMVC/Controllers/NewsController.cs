using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyPlatformMVC.Database;
using StudyPlatformMVC.Exceptions;
using StudyPlatformMVC.Models;
using StudyPlatformMVC.ViewModels;

namespace StudyPlatformMVC.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        [Route("News/Index")]
        public ActionResult Index()
        {

            //Testing
            Session["user"] = Student.GetByConditions("name='Iver Clausen'").Single();
            Creator.CreateNews(Person.GetByID(1) as Secretary, "Nyhed2", "Text");
            Creator.CreateNews(Person.GetByID(1) as Secretary, "Nyhed3", "Textffdsfddsfs");

            // Actual
            Person person = (Person)Session["user"];
            if (person == null)
                return RedirectToAction("Index", "Login");
            return View(new NewsViewModel()
            {
                News = News.GetAll()
            });
        }
        [Route("News/Index/{id}")]
        public ActionResult Index(int id)
        {
            //Testing
            Session["user"] = Student.GetByConditions("name='Iver Clausen'").Single();

            // Actual
            Person person = (Person)Session["user"];
            News currentNews;
            if (person == null)
                return RedirectToAction("Index", "Login");
            try { currentNews = News.GetByID(Convert.ToUInt32(id)); }
            catch (InvalidIDException) { return RedirectToAction("Index", "News"); }

            return View(new NewsViewModel()
            {
                News = News.GetAll(),
                CurrentNews = currentNews
            });
        }
    }
}
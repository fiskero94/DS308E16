using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyPlatformMVC.Database;
using StudyPlatformMVC.Models;

namespace StudyPlatformMVC.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        
        public ActionResult Index()
        {
            Creator.CreateNews(Person.GetByID(1) as Secretary, "Nyhed2", "Text");
            Creator.CreateNews(Person.GetByID(1) as Secretary, "Nyhed3", "Textffdsfddsfs");
            List<News> news = News.GetAll();

            return View(news);
        }

    }
}
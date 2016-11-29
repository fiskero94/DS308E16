using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyPlatformMVC.Models;

namespace StudyPlatformMVC.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        
        public ActionResult Index()
        {
            List<News> news = News.GetAll();

            return View();
        }

    }
}
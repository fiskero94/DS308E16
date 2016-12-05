using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace StudyPlatformMVC.Models
{
    public class CustomChart : Chart
    {
        public CustomChart(int width, int height, string theme = null, string themePath = null) : base(width, height, theme, themePath)
        {

        }
    }
}
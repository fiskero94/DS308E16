﻿using System.Web;
using System.Web.Optimization;

namespace StudyPlatformMVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jasny-bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-lumen.css",
                      "~/Content/bootstrap-lumen-font.css",
                      "~/Content/navigation.css",
                      "~/Content/simple-sidebar.css",                                                    
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Grades").Include(
                      "~/Content/Grades.css"));
            bundles.Add(new StyleBundle("~/Content/Course").Include(
                      "~/Content/Course.css"));
            bundles.Add(new StyleBundle("~/Content/news").Include(
                      "~/Content/news.css"));
            bundles.Add(new StyleBundle("~/Content/Absence").Include(
                      "~/Content/Absence.css"));
            bundles.Add(new StyleBundle("~/Content/messages").Include(
                      "~/Content/messages.css"));
        }
    }
}

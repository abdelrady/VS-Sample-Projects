using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult UnorderedList()
        {
            var array = new string[] { "John", "Doe", "Foo", "Bar" };
            return View("UnorderedList", array);
        }

        public ActionResult Index()
        {
            var obj = new MyClass
            {
                Number = 15
            };

            return View(obj);
        }


        [ActionName("me")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View("About");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
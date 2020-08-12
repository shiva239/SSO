using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class HtmlHelperController : Controller
    {
        // GET: HtmlHelper
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HtmlHelperExample()
        {
            RegisterEntities db = new RegisterEntities();
            ViewBag.Register = new SelectList(db.Registers, "Id", "Name", "Select User");
            return View();
        }
    }
}
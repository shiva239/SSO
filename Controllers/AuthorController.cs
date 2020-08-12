using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class AuthorController : Controller
    {

        AuthorModel authDB = new AuthorModel();

        [Authorize]
        public ActionResult Index()
        {
            return View(authDB.GetAuthorsList());
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.author = new SelectList((IEnumerable<Author>)authDB.GetAuthorsList(), "AuthorId", "AuthorName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Author authorObj)
        {
            int i = authDB.SaveAuthor(authorObj);
            if (i > 0)
            {
                ViewBag.message = true;
                ViewBag.msg = "Display";
                return View();
                
            }
            else
            {
                return View(authorObj);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Author authorObj = authDB.GetAuthorById(id);
            return View(authorObj);
        }

        [HttpPost]
        public ActionResult Edit(Author authorObj)
        {
            int i = authDB.EditAuthorById(authorObj);
            if (i > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(authorObj);
            }
        }

        [HttpGet]
        public RedirectToRouteResult Delete(int? id)
        {
            int i  = authDB.DeleteAuthorById(id);
            return RedirectToAction("index");
        }

    }
}
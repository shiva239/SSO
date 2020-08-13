using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        BookModel bookDB = new BookModel();
        AuthorModel authDB = new AuthorModel();

        [Authorize]
        public ActionResult Index()
        {
            return View(bookDB.GetBooksList());
        }

        public JsonResult IndexGrid()
        {
            return Json(bookDB.GetBooksList(), JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.author = new SelectList((IEnumerable<Author>)authDB.GetAuthorsList(), "AuthorId", "AuthorName");
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Book bookObj, HttpPostedFileBase FilePath)
        {
            ViewBag.author = new SelectList((IEnumerable<Author>)authDB.GetAuthorsList(), "AuthorId", "AuthorName");
            string filename = System.IO.Path.GetFileName(FilePath.FileName);
            string path = System.IO.Path.Combine(Server.MapPath("~/Images"), filename);
            bookObj.FilePath = path.Replace("C:\\Users\\x-spagidal\\source\\repos\\SSO\\Images\\","~/Images/");
            FilePath.SaveAs(path);

            int i = bookDB.SaveBook(bookObj);

            if (i > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Book bookObj = bookDB.GetBookById(id);
            ViewBag.author = new SelectList((IEnumerable<Author>)authDB.GetAuthorsList(), "AuthorId", "AuthorName");
            return View(bookObj);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Book bookObj)
        {
            int i = bookDB.EditBookById(bookObj);
            if (i > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(bookObj);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            int i = bookDB.DeleteBookById(id);
            return RedirectToAction("index");
        }

        public JsonResult GetBookCategoryList()
        {
            List<String> bookCategoryList = new List<string>();
            bookCategoryList.Add("Action");
            bookCategoryList.Add("Drama");
            bookCategoryList.Add("Fantacy");
            bookCategoryList.Add("Fiction");
            bookCategoryList.Add("Thriller");
            return Json(bookCategoryList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadFile(HttpPostedFileBase postedFileBase)
        {
            return View("Success");
        }

        public JsonResult IsDateValid(DateTime BookPublishDate)
        {
            if (BookPublishDate >= Convert.ToDateTime("01/01/1900") && BookPublishDate <= DateTime.Now)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
/*
        [HttpPost]
        public string FileUpload(HttpPostedFileBase FilePath)
        {
            if (FilePath != null)
            {
                string pic = System.IO.Path.GetFileName(FilePath.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Images"), pic);
                // file is uploaded
                FilePath.SaveAs(path);

                using (MemoryStream ms = new MemoryStream())
                {
                    FilePath.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
                return path;
            }
            else
            {
                return null;
            }
        }

        public ActionResult Index(HttpPostedFileBase fileupload)
        {
            string filename = Path.GetFileName(fileupload.FileName);
            string path = Server.MapPath("~/Images");
            fileupload.SaveAs(Path.Combine(path, filename));
            ViewBag.msg = "uploaded successfully";

            return View();
        }
 */

    }
}
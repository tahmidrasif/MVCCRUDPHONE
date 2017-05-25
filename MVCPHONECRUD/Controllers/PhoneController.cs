using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MVCPHONECRUD.Models;
using System.Linq.Dynamic;

namespace MVCPHONECRUD.Controllers
{
    public class PhoneController : Controller
    {
        private TestDbEntities db = new TestDbEntities();

        // GET: /Phone/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string filter = null, int page = 1,
         int pageSize = 5, string sort = "PhoneId", string sortdir = "DESC")
        {
            var records = new PagedList<Phone>();


            ViewBag.Sort = sort;
            ViewBag.SortDir = sortdir;
            ViewBag.Filter = filter;

            records.Content = db.Phone.Where(x => filter == null || (x.Model.Contains(filter)) || x.Company.Contains(filter))
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            records.TotalRecords = db.Phone.Count(x => filter == null ||
                                                        (x.Model.Contains(filter)) || x.Company.Contains(filter));

            records.CurrentPage = page;
            records.PageSize = pageSize;


            return PartialView("_List", records);
        }


        public ActionResult Details(int id = 0)
        {
            Phone phone = db.Phone.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return PartialView("Details", phone);
        }


        // GET: /Phone/Create
        [HttpGet]
        public ActionResult Create()
        {
            var phone = new Phone();
            return PartialView("Create", phone);
        }


        // POST: /Phone/Create
        [HttpPost]
        public ActionResult Create(Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Phone.Add(phone);
                db.SaveChanges();
                return RedirectToAction("List");
                //  return Json(new { success = true });
            }
            return RedirectToAction("List");
        }


        // GET: /Phone/Edit/5
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var phone = db.Phone.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }

            return PartialView("Edit", phone);
        }


        // POST: /Phone/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Phone phone)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(phone).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("List");
                    return Json(new { success = true });
                }
                // throw new HttpException(404, "Not Found");
                return Json(new { status = 1 });
                //   return Json(new {status=1},JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                Response.StatusCode = 400;
                return Json(new { status = 2 });
            }

        }

        //
        // GET: /Phone/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Phone phone = db.Phone.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return PartialView("Delete", phone);
        }


        //
        // POST: /Phone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var phone = db.Phone.Find(id);
            db.Phone.Remove(phone);
            db.SaveChanges();
            return Json(new { success = true });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
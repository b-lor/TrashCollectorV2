using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollectorV2.Models;

namespace TrashCollectorV2.Controllers
{
    public class DateAddController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: DateAdd
        public ActionResult Index()
        {
            return View(db.DateAdd.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            DateAdd editDate = db.DateAdd.Where(d => d.ID == id).First();
            DateAdd dateAdd = new DateAdd();

            dateAdd.CurrentDate = editDate.CurrentDate;
            dateAdd.FutureDate = editDate.FutureDate;

            return View(dateAdd);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(DateAdd dateAdd)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            DateAdd editDate = db.DateAdd.Where(e => e.ID == dateAdd.ID).First();
            //editDate.CurrentDate = DateTime.Now;
            editDate.CurrentDate =dateAdd.CurrentDate;
            editDate.FutureDate = dateAdd.FutureDate;
            db.SaveChanges();
            return RedirectToAction("EditDate", "Customer");

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
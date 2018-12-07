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

        //[HttpPost]
        //public ActionResult Create([Bind(Include = "Id,CurrentDate,FutureDate")] DateAdd dateAdd)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.DateAdd.Add(dateAdd);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(dateAdd);
        //}

        //public ActionResult Edit()
        //{
        //    //ApplicationDbContext db = new ApplicationDbContext();

        //    //DateAdd dateAdd = new DateAdd();
        //    //dateAdd.CurrentDate = DateTime.Now;
        //    //dateAdd.FutureDate = DateTime.Now.AddDays(15);

        //    //return View(dateAdd);
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Edit(DateAdd id)
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();

        //    // Update fields
        //    DateAdd dateAdd = new DateAdd();
        //    //dateAdd.CurrentDate = DateTime.Now();
        //    var changeDate = DateTime.Now;
        //    dateAdd.FutureDate = changeDate;

        //    //db.Entry(dateAdd).State = EntityState.Modified;

        //    db.SaveChanges();
        //    return RedirectToAction("Index");

        //}
        public ActionResult Edit(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var editDate = db.DateAdd.Where(d => d.ID == id).First();
            return View(editDate);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(DateAdd dateAdd)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            DateAdd editDate = db.DateAdd.Where(e => e.ID == dateAdd.ID).First();
            editDate.CurrentDate = DateTime.Now;
            //DateTime changeDate = DateTime.Now;
            editDate.FutureDate = dateAdd.FutureDate;
            db.SaveChanges();
            return RedirectToAction("Index");

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
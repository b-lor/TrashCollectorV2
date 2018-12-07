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
    public class DayController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Days
        public ActionResult Index()
        {
            return View(db.Days.ToList());
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Day")] Days days)
        {
            if (ModelState.IsValid)
            {
                db.Days.Add(days);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(days);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Days days = db.Days.Find(id);
            if (days == null)
            {
                return HttpNotFound();
            }
            return View(days);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Day")] Days days)
        {
            if (ModelState.IsValid)
            {
                db.Entry(days).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(days);
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
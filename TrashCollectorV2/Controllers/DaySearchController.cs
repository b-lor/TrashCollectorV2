using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrashCollectorV2.Controllers
{
    public class DaySearchController : Controller
    {
        // GET: DaySearch
        public ActionResult Index()
        {
            //using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return View();
            }
        }
    }
}
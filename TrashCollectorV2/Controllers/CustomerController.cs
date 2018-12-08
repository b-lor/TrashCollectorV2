using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrashCollectorV2.Models;

namespace TrashCollectorV2.Controllers
{
    public class CustomerController : Controller
    {

        // GET: Customer
        public ActionResult Index()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return View(db.Customer.ToList());
            }

        }
        public ViewResult Detail()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string username = User.Identity.Name;

            Customer customer = db.Customer.FirstOrDefault(u => u.Username.Equals(username));
            return View(customer);

        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Customer customer)
        {
           
            if (ModelState.IsValid)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())

                {
                    db.Customer.Add(customer);
                    db.SaveChanges();
                }
                AddCustomerToRole(customer);
                ModelState.Clear();
                ViewBag.MessageCustomer = customer.FirstName + " " + customer.LastName + " successfully registered.";
            }
            return View();
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Customer user)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var usr = db.Customer.Single(u => u.Username == user.Username && u.Password == user.Password);
                if (usr != null)
                {
                    Session["Id"] = user.Id.ToString();
                    Session["Username"] = usr.Username.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is wrong");
                }
            }
            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["Id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public void AddCustomerToRole(Customer customer)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = new ApplicationUser();
            user.UserName = customer.Username;
            user.Email = customer.Email;

            string userPWD = customer.Password;

            var chkUser = UserManager.Create(user, userPWD);

            //Add default User to Role Customer   
            if (chkUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(user.Id, "Customer");

            }

        }

        public ActionResult Edit()
        {
            ApplicationDbContext db = new ApplicationDbContext();


            string username = User.Identity.Name;

            Customer customer = db.Customer.FirstOrDefault(u => u.Username.Equals(username));

            Customer updateCustomer = new Customer();
            updateCustomer.FirstName = customer.FirstName;
            updateCustomer.LastName = customer.LastName;
            updateCustomer.Address1 = customer.Address1;
            updateCustomer.Address2 = customer.Address2;
            updateCustomer.City = customer.City;
            updateCustomer.State = customer.State;
            updateCustomer.ZipCode = customer.ZipCode;

            return View(updateCustomer);
        }
        [HttpPost]
        public ActionResult Edit(Customer userprofile)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (ModelState.IsValid)
            {
                string username = User.Identity.Name;
                // Get the userprofile
                Customer customer = db.Customer.FirstOrDefault(u => u.Username.Equals(username));

                // Update fields
                customer.FirstName = userprofile.FirstName;
                customer.LastName = userprofile.LastName;
                customer.Address1 = userprofile.Address1;
                customer.Address2 = userprofile.Address2;
                customer.City = userprofile.City;
                customer.State = userprofile.State;
                customer.ZipCode = userprofile.ZipCode;

                db.Entry(customer).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index", "Home"); // or whatever
            }

            return View(userprofile);
        }

        public ActionResult MyInfo()
        {
            ApplicationDbContext db = new ApplicationDbContext();


            string username = User.Identity.Name;

            Customer customer = db.Customer.FirstOrDefault(u => u.Username.Equals(username));

            Customer updateCustomer = new Customer();
            updateCustomer.Username = customer.Username;
            updateCustomer.Email = customer.Email;
            updateCustomer.Password = customer.Password;
            updateCustomer.ConfirmPassword = customer.ConfirmPassword;

            return View(updateCustomer);
        }
        [HttpPost]
        public ActionResult MyInfo(Customer userprofile)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (ModelState.IsValid)
            {
                string username = User.Identity.Name;
                // Get the userprofile
                Customer customer = db.Customer.FirstOrDefault(u => u.Username.Equals(username));

                // Update fields
                customer.Username = userprofile.Username;
                customer.Email = userprofile.Email;
                customer.Password = userprofile.Password;
                customer.ConfirmPassword = userprofile.ConfirmPassword;

                db.Entry(customer).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index", "Home"); // or whatever
            }

            return View(userprofile);
        }

        public ActionResult EditDate()
        {
            ApplicationDbContext db = new ApplicationDbContext();


            string username = User.Identity.Name;

            Customer customer = db.Customer.FirstOrDefault(u => u.Username.Equals(username));

            Customer updateCustomer = new Customer();
            updateCustomer.DayOfWeek = customer.DayOfWeek;
            updateCustomer.ExtraPickUp = customer.ExtraPickUp;


            DateAdd dateAdd = db.DateAdd.Take(1).FirstOrDefault();

            DateTime StartDate = (DateTime)dateAdd.CurrentDate;
            DateTime EndDate = (DateTime)dateAdd.FutureDate;


            DayOfWeek? day = customer.DayOfWeek;

            List<DateTime> dates =
            Enumerable.Range(0, (int)((EndDate - StartDate).TotalDays) + 1)
            .Select(n => StartDate.AddDays(n))
            .ToList();

            var dayCount = dates.Count(x => x.DayOfWeek == day);
            ///
            double extraDayCost;
            DateTime extraPickUp = (DateTime)customer.ExtraPickUp;
            if (extraPickUp >= StartDate && extraPickUp < EndDate)
            {
                extraDayCost = 25;
            }
            extraDayCost = 0;

            double balance = dayCount * 20 + extraDayCost;




            //double balance = dayCount;

            updateCustomer.Balance = balance;
            updateCustomer.StartDate = customer.StartDate;
            updateCustomer.EndDate = customer.EndDate;

            return View(updateCustomer);
        }
        [HttpPost]
        public ActionResult EditDate(Customer userprofile)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (ModelState.IsValid)
            {
                string username = User.Identity.Name;
                // Get the userprofile
                Customer customer = db.Customer.FirstOrDefault(u => u.Username.Equals(username));

                customer.DayOfWeek = userprofile.DayOfWeek;
                customer.ExtraPickUp = userprofile.ExtraPickUp;

                DateAdd dateAdd = db.DateAdd.Take(1).FirstOrDefault();

                DateTime StartDate = (DateTime)dateAdd.CurrentDate;
                DateTime EndDate = (DateTime)dateAdd.FutureDate;

                DayOfWeek day = DayOfWeek.Monday;

                List<DateTime> dates =
                Enumerable.Range(0, (int)((EndDate - StartDate).TotalDays) + 1)
                .Select(n => StartDate.AddDays(n))
                .ToList();

                var MondayCount = dates.Count(x => x.DayOfWeek == day);
                double balance = MondayCount;

                customer.Balance = balance;
                customer.StartDate = userprofile.StartDate;
                customer.EndDate = userprofile.EndDate;


                db.Entry(customer).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index", "Home"); // or whatever
            }

            return View(userprofile);
        }

        public ActionResult Invoice()
        {
            ApplicationDbContext db = new ApplicationDbContext();


            string username = User.Identity.Name;

            Customer customer = db.Customer.FirstOrDefault(u => u.Username.Equals(username));

            Customer updateCustomer = new Customer();

            updateCustomer.Balance = customer.Balance;


            return View(updateCustomer);
        }
        [HttpPost]
        public ActionResult Invoice(Customer userprofile)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (ModelState.IsValid)
            {
                string username = User.Identity.Name;
                // Get the userprofile
                Customer customer = db.Customer.FirstOrDefault(u => u.Username.Equals(username));

                customer.Balance = customer.Balance;

                db.Entry(customer).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index", "Home"); // or whatever
            }

            return View(userprofile);
        }


    }
}
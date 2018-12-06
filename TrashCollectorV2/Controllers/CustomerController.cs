using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ApplicationDbContext context = new ApplicationDbContext();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

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
    }
}
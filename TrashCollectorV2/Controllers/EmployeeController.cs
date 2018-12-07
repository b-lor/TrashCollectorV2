using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrashCollectorV2.Models;

namespace TrashCollectorV2.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return View(db.Employee.ToList());
            }

        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Employee employee)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    db.Employee.Add(employee);
                    db.SaveChanges();
                }
                AddEmployeeToRole(employee);
                ModelState.Clear();
                ViewBag.MessageEmployee = employee.FirstName + " " + employee.LastName + " successfully registered.";
            }
            return View();
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Employee user)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var usr = db.Employee.Single(u => u.Username == user.Username && u.Password == user.Password);
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

        public void AddEmployeeToRole(Employee employee)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = new ApplicationUser();
            user.UserName = employee.Username;
            user.Email = employee.Email;

            string userPWD = employee.Password;

            var chkUser = UserManager.Create(user, userPWD);

            //Add default User to Role Employee   
            if (chkUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(user.Id, "Employee");

            }

        }

        public ActionResult Edit()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string username = User.Identity.Name;

            Employee employee = db.Employee.FirstOrDefault(u => u.Username.Equals(username));

            Employee updateEmployee = new Employee();
            //updateEmployee.FirstName = employee.FirstName;
            //updateEmployee.LastName = employee.LastName;
            //updateEmployee.ZipCode = employee.ZipCode;
            updateEmployee.Username = employee.Username;
            updateEmployee.Email = employee.Email;
            updateEmployee.Password = employee.Password;
            updateEmployee.ConfirmPassword = employee.ConfirmPassword;

            return View(updateEmployee);
        }
        [HttpPost]
        public ActionResult Edit(Employee userprofile)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (ModelState.IsValid)
            {
                string username = User.Identity.Name;
                // Get the userprofile
                Employee employee = db.Employee.FirstOrDefault(u => u.Username.Equals(username));

                // Update fields
                //employee.FirstName = userprofile.FirstName;
                //employee.LastName = userprofile.LastName;
                //employee.ZipCode = userprofile.ZipCode;
                employee.Username = userprofile.Username;
                employee.Email = userprofile.Email;
                employee.Password = userprofile.Password;
                employee.ConfirmPassword = userprofile.ConfirmPassword;

                db.Entry(employee).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index", "Home"); // or whatever
            }

            return View(userprofile);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phase3App01.Models;
namespace Phase3App01.Controllers
{
    public class AdminController : Controller
    {
        EcommerceContext context = new EcommerceContext();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        private bool ValidateUser(string uname, string pwd)
        {
            List<AdminModel> users = new List<AdminModel>()
            {
                new AdminModel{UserName="kiran",Password="1234"}
            };
            return users.Exists(x => x.UserName == uname && x.Password == pwd);
        }
        [HttpPost]
        public IActionResult Login(AdminModel user)
        {

            if (ValidateUser(user.UserName, user.Password))
            {
                return RedirectToAction("Dashboard", "Admin", new { uname = user.UserName });
            }
            else
                ViewBag.msg = "Invalid input credentials...";
            /*  if (user.UserName == "kiran" && user.Password == "kumar")
                  ViewBag.msg = "Credentials found correct...";
              else
                  ViewBag.msg = "Invalid input credentials...";
              */
            return View();
        }
        public IActionResult Dashboard(string uname)
        {
            ViewBag.msg = $"Hello {uname} ,welcome to dashboard";
            return View();
        }
        public IActionResult ViewProduct()
        {
            return View(context.Productlists);
        }
        public IActionResult ViewCustomer()
        {
            return View(context.CustomerDbs);
        }
        public IActionResult ViewSeller()
        {
            return View(context.SellerDb1s);
        }
        public ActionResult EditProduct(int id)
        {
            //ViewBag.user = id;
            return View(context.Productlists.Single(x => x.Pid == id));
        }

        // POST: SellerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(int id, Productlist d)
        {
            try
            {
                context.Update(d);
                context.SaveChanges();
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditCustomer(int id)
        {
            //ViewBag.user = id;
            return View(context.CustomerDbs.Single(x => x.Cid== id));
        }

        // POST: SellerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer(int id, CustomerDb d)
        {
            try
            {
                context.CustomerDbs.Update(d);
                context.SaveChanges();
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult CreateProduct()
        {
            return View();
        }
        
        // POST: SellerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(Productlist d)
        {
            try
            {
                context.Productlists.Add(d);
                context.SaveChanges();
                ViewBag.msg = "Product Created successfully";
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteProduct(int id)
        {
            return View(context.Productlists.Single(x => x.Pid == id));
        }

        // POST: SellerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int id, Productlist d)
        {
            try
            {
                context.Productlists.Remove(d);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DetailsProduct(int id)
        {
            return View(context.Productlists.Single(x => x.Pid == id));
        }
        public ActionResult DetailsCustomer(int id)
        {
            return View(context.CustomerDbs.Single(x => x.Cid == id));
        }

        public ActionResult DeleteCustomer(int id)
        {
            return View(context.CustomerDbs.Single(x => x.Cid == id));
        }

        // POST: SellerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomer(int id, CustomerDb d)
        {
            try
            {
                context.CustomerDbs.Remove(d);
                context.SaveChanges();
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}

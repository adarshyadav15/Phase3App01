using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phase3App01.Models;
using Microsoft.Data.SqlClient;

namespace Phase3App01.Controllers
{
    public class SellerController : Controller
    {
        EcommerceContext context = new EcommerceContext();
        // GET: SellerController
        SqlConnection cn = null;
        string cs = string.Empty;
        string uname = "";
        public SqlConnection ConnectToSql()
        {
            this.cs = "server=H5CG1220K24\\MSSQLSERVER01;integrated Security=true;database=Ecommerce";
            this.cn = new SqlConnection(cs);
            cn.Open();
            return cn;
        }
        public ActionResult Index()
        {
            return View(context.SellerDb1s);
        }
        [HttpGet]
        public IActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Login(SellerLogin d) //For seller login -done
        {
            cn = ConnectToSql();
            List<SellerDb1> seller = new List<SellerDb1>();
            string query = "select * from SellerDB1";
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SellerDb1 s = new SellerDb1();
                s.Susername = dr[0].ToString();
                s.Sname = dr[1].ToString();
                s.Saddress = dr[2].ToString();
                s.Spassword = dr[3].ToString();
                seller.Add(s);
            }
            ViewBag.seller = seller;
            foreach (SellerDb1 s in seller)
            {
                if(s.Susername==d.Susername)
                {
                    if(s.Spassword==d.Spassword)
                    {
                        this.uname = s.Sname;
                        return RedirectToAction("Dashboard", "Seller", new { username = s.Susername });
                        //ViewBag.msg = "Successfull";
                    }
                    else
                    {
                        ViewBag.msg = "Wrong Password";
                    }
                    break;
                }
                else
                {
                    ViewBag.msg = "User Not found";
                }
            }
            
            return View();
        }
        // GET: SellerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        //Dashboard
        public ActionResult Dashboard(string username)
        {
            ViewBag.msg = $"Welcome {username} ";
            ViewBag.user = username;
            //this.uname = username;
            return View();
        }

        // GET: SellerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SellerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SellerDb1 d)
        {
            try
            {
                context.SellerDb1s.Add(d);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SellerController/Edit/5
        public ActionResult Edit(string id)
        {
            //ViewBag.user = id;
            return View(context.SellerDb1s.Single(x=>x.Susername==id));
        }

        // POST: SellerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SellerDb1 d)
        {
            try
            {
                context.Update(d);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
        // GET: SellerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SellerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
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
        public ActionResult ViewProduct(string seller)
        {
            //return View(context.Productlists.Any(x=>x.Sname==seller));
            return View(context.Productlists.Where(x=>x.Sname=="adarsh"));
            //return View(context.Productlists);
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
            return View(context.Productlists.Single(x=>x.Pid==id));
        }

    }
}

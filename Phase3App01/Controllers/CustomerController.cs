using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phase3App01.Models;
using Microsoft.Data.SqlClient;
using System.Dynamic;

namespace Phase3App01.Controllers
{
    public class CustomerController : Controller
    {
        EcommerceContext context = new EcommerceContext();
        // List<> cartdetail = new List<CartDetail1>();
        ShippingDetail sd = new ShippingDetail();
        SqlConnection cn = null;
        string cs = string.Empty;
        string uname = "";
        public IActionResult Index()
        {
            return View();
        }
        public SqlConnection ConnectToSql()
        {
            this.cs = "server=H5CG1220K24\\MSSQLSERVER01;integrated Security=true;database=Ecommerce";
            this.cn = new SqlConnection(cs);
            cn.Open();
            return cn;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerDb d)
        {
            try
            {
                context.CustomerDbs.Add(d);
                context.SaveChanges();
                ViewBag.msg = "Account Created Successfully....";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.msg = "Not Created...Try Again";
                return View();
            }
        }
        //Customer Login
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(CustomerDb d) //For customer login -done
        {
            cn = ConnectToSql();
            List<CustomerDb> cust = new List<CustomerDb>();
            string query = "select * from CustomerDB";
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            
            while (dr.Read())
            {
                CustomerDb s = new CustomerDb();
                s.Cusername = dr[0].ToString();
                s.Cpassword = dr[1].ToString();
                s.Caddress = dr[2].ToString();
                s.Cid = Convert.ToInt32(dr[3]);
                cust.Add(s);
            }
            //ViewBag.seller = seller;
            foreach (CustomerDb s in cust)
            {
                if (s.Cusername == d.Cusername)
                {
                    if (s.Cpassword == d.Cpassword)
                    {
                        this.uname = s.Cusername;
                        cn = ConnectToSql();
                        string query1 = "delete from CartDetail11";
                        SqlCommand cmd1 = new SqlCommand(query1, cn);
                         dr = cmd1.ExecuteReader();
                        return RedirectToAction("Dashboard", "Customer", new { username = s.Cusername });
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
        public ActionResult Dashboard(string username)
        {
            ViewBag.msg = $"Welcome {username} ";
            ViewBag.user = username;
            this.uname = username;
            return View();
        }
        public ActionResult ViewProducts()
        {
            
            return View(context.Productlists);
        }
        
        public ActionResult ViewCart()
        {
            return View(context.CartDetail11s);
        }
        public ActionResult DeleteProduct(int id)
        {
            return View(context.CartDetail11s.Single(x => x.Itemid == id));
        }

        // POST: SellerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int id, CartDetail11 d)
        {
            try
            {
                context.CartDetail11s.Remove(d);
                context.SaveChanges();
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AddCart(int id)
        {
            /*
            ViewBag.msg = id;
            cn = ConnectToSql();
            CartDetail11 ct = new CartDetail11();
            List<Productlist> cust = new List<Productlist>();
            string query = "select * from Productlist where pid="+id;
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Productlist s = new Productlist();
                s.Pid = Convert.ToInt32(dr[0]);
                s.Pname = dr[1].ToString();
                s.Pprice = Convert.ToInt32(dr[2]);
                s.Pdetails = dr[3].ToString();
                s.Sname = dr[4].ToString();
                cust.Add(s);
            }
            foreach (Productlist s in cust)
            {
                ct.Itemname = s.Pname;
                ct.Itemprice = s.Pprice;
                context.CartDetail11s.Add(ct);
                context.SaveChanges();
            }
            */
            CartDetail11 ct = new CartDetail11();
            var x = context.Productlists.Single(y => y.Pid == id);
            ct.Itemname = x.Pname;
            ct.Itemprice = (int)x.Pprice;
            context.CartDetail11s.Add(ct);
            context.SaveChanges();

            ViewBag.msg = "Added Successfully";
            
            
            
            return View(context.CartDetail11s);
        }
        
        public ActionResult Checkout()
        {
            return View();
        }

        // POST: SellerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(ShippingDetail d)
        {
            this.sd = d;
            context.ShippingDetails.Add(d);
            return RedirectToAction("Confirmation", "Customer", new { shipping = sd });
        }

        public ActionResult Confirmation(ShippingDetail d)
        {
            
            dynamic model = new ExpandoObject();
            cn = ConnectToSql();
            CartDetail11 ct = new CartDetail11();
            List<CartDetail11> cust = new List<CartDetail11>();
            string query = "select * from CartDetail11";
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                CartDetail11 s = new CartDetail11();
                s.Itemid = Convert.ToInt32(dr[0]);
                s.Itemname = dr[1].ToString();
                s.Itemprice = Convert.ToInt32(dr[2]);
                
                cust.Add(s);
            }
            List<ShippingDetail> dust = new List<ShippingDetail>();
            string query1 = "select * from ShippingDetail";
            SqlCommand cmd1 = new SqlCommand(query1, cn);
            //SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr.Read())
            {
                ShippingDetail s = new ShippingDetail();
                s.Name = dr[0].ToString();
                s.Address = dr[1].ToString();
                s.State = dr[2].ToString();
                s.Phone = Convert.ToInt32(dr[3]);

                dust.Add(s);
            }
            FinalModel fm = new FinalModel();
           
            
            fm.ShippingDetails = dust;
            fm.CartDetail11 = cust;
            
            return View(fm);
        }
        


    }
}

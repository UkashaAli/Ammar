using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;


namespace ProBattle.Controllers
{
    public class CustomerController : Controller
    {
        [HttpGet]
        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [ActionName("register")]
        public ActionResult register_Post()
        {

            CustomerDataAccessLayer customerLayer = new CustomerDataAccessLayer();
            if (ModelState.IsValid)
            {
                Customer customer = new Customer();
                UpdateModel(customer);

                customerLayer.addCustomer(customer);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        [ActionName("login")]
        public ActionResult login_Post()
        {
            CustomerDataAccessLayer customerLayer = new CustomerDataAccessLayer();
            if (ModelState.IsValid)
            {
                CustomerLoginClass customer = new CustomerLoginClass();
                UpdateModel(customer);

                int flag = customerLayer.authenticateCustomer(customer);

                if(flag > 0)
                {
                    Session["username"] = customer.customerEmail;
                    Session["userId"] = flag;

                    return RedirectToAction("AdminPanel");
                }
                
            }
            return View();
        }
        public ActionResult AdminPanel()
        {
            CustomerDataAccessLayer customerLayer = new CustomerDataAccessLayer();
            if(Session["username"] != null && Session["userId"] != null)
            {
                Customer customer = customerLayer.CustomerDetails(Convert.ToInt32(Session["userId"]));
                return View(customer);
            }
            return RedirectToAction("login");
        }

        public ActionResult logout()
        {
            Session.Remove("username");
            Session.Remove("userId");

            return RedirectToAction("Index", "Home");
        }
}
}
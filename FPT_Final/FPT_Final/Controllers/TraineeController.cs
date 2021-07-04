using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FPT_Final.Models.Entities;

namespace FPT_Final.Controllers
{
    public class TraineeController : Controller
    {
        FptdatataEntities1 context = new FptdatataEntities1();
        // GET: Trainee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(Trainee model)
        {
            var result = context.Admins.Where(x => x.Email == model.Email && x.Password == model.Password).Count();
            if (result > 0)
            {
                return View("Index");
            }
            else
            {
                ViewBag.error = "mày ngu quá nhập sai rồi!";
                return View("login");
            }
        }
    }
}
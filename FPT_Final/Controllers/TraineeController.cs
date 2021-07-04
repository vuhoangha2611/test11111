using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FPT_Final.Models.Entities;


namespace FPT_Final.Controllers
{
    public class TraineeController : Controller
    {
        FPTUniversityEntities1 context = new FPTUniversityEntities1();
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
            var result = context.Trainees.Where(x => x.Email == model.Email && x.Password == model.Password).Count();
            if (result > 0)
            {
                Session["id"] = model.ID;
                Session["email"] = model.Email;
                Session["password"] = model.Password;
               
                return View("Index");
            }
            else
            {
                ViewBag.error = "Something wrong!";
                return View("login");
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("login");
        }

        public ActionResult ViewProfile( )
        {
            string email = Session["email"].ToString();            
            var list = context.Trainees.Where(x => x.Email.Contains(email)).FirstOrDefault();
            return View(list);
        }
       

        public ActionResult ViewCourse(Course model)
        {
            List<Course> list = context.Courses.ToList();
            return View(list);
        }

        public ActionResult SettingAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SettingAccount(Trainee model)
        {
            string email = Session["email"].ToString();
            string password = Session["password"].ToString();
            var result = context.Trainees.Where(x => x.Password == password).FirstOrDefault();
            if (result != null)
            {
                var detail = context.Trainees.FirstOrDefault(x => x.Email == email);
                if (detail != null)
                {
                    detail.Password = model.NewPassword;
                    context.SaveChanges();
                    ViewBag.Message = "change successfully!";
                }
            }
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FPT_Final.Models.Entities;

namespace FPT_Final.Controllers
{
    public class TrainerController : Controller
    {
        FPTUniversityEntities1 context = new FPTUniversityEntities1();
        // GET: Trainer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(Trainer model)
        {
            var result = context.Trainers.Where(x => x.Email == model.Email && x.Password == model.Password).Count();
            if (result > 0)
            {
                Session["email"] = model.Email;
                Session["password"] = model.Password;
                return View("Index");
            }
            else
            {
                ViewBag.error = "Some thing wrong!";
                return View("login");
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("login");
        }

        public ActionResult ViewProfile()
        {
            string email = Session["email"].ToString();
            var list = context.Trainers.Where(x => x.Email.Contains(email)).FirstOrDefault();
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
        public ActionResult SettingAccount(Trainer model)
        {
            string email = Session["email"].ToString();
            string password = Session["password"].ToString();
            var result = context.Trainers.Where(x => x.Password == password).FirstOrDefault();
            if (result != null)
            {
                var detail = context.Trainers.FirstOrDefault(x => x.Email == email);
                if (detail != null)
                {
                    detail.Password = model.NewPassword;
                    context.SaveChanges();
                    ViewBag.Message = "change successfully!";
                }
            }
            return View();
        }

        public ActionResult Myclass()
        {
            return View(context.Classes.ToList());
        }
    }
}
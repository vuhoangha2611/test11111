using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FPT_Final.Models.Entities;

namespace FPT_Final.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        FPTUniversityEntities1 context = new FPTUniversityEntities1();
        public ActionResult login()
        {
            return View();
        }     
        
        [HttpPost]
        public ActionResult login (Admin model)
        {
            //khai báo biến đếm result 
            var result = context.Admins.Where(x => x.Email == model.Email && x.Password == model.Password).Count();
            if(result > 0) // nếu có tài khoản admin trong database
            {
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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexStaff()
        {
            
            return View(context.Staffs.ToList());
        }

        public ActionResult CreateStaff()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStaff( Staff model)
        {
            try
            {
                // TODO: Add insert logic here
                context.Staffs.Add(model);
                context.SaveChanges();
                return RedirectToAction("IndexTrainer");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditStaff(long? id)
        {
            var edit = context.Staffs.Where(x => x.ID == id).FirstOrDefault();
            return View(edit);
        }

        [HttpPost]
        public ActionResult EditStaff(int id, Staff model)
        {
            try
            {
                // TODO: Add update logic here
                //khai báo biến oldItem để tìm id của bảng Trainee
                var oldItem = context.Staffs.Find(model.ID);
                //với mỗi dữ liệu của bảng cũ(oldItem) được thay thế bằng dữ liệu mới
                oldItem.Email = model.Email;
                oldItem.Password = model.Password;
                oldItem.Name = model.Name;
                oldItem.PhoneNumber = model.PhoneNumber;
                context.SaveChanges(); // lưu thay đổi
                return RedirectToAction("IndexStaff");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DetailStaff(int id)
        {
            var list = context.Trainers.Where(x => x.ID == id).FirstOrDefault();
            return View(list);
        }

        public ActionResult DeleteStaff(int id)
        {
            var delete = context.Staffs.Find(id);
            return View(delete);
        }

        [HttpPost]
        public ActionResult DeleteStaff(int id, Staff model)
        {
            try
            {
                // TODO: Add delete logic here
                var delete = context.Staffs.Find(id);
                context.Staffs.Remove(delete);
                context.SaveChanges();
                return RedirectToAction("IndexStaff");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult IndexTrainer()
        {
            List<Trainer> list = context.Trainers.ToList();
            return View(list);
        }

        public ActionResult CreateTrainer()
        {
            Trainer a = new Trainer();
            return View(a);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTrainer(Trainer model)
        {
            try
            {
                // TODO: Add insert logic here
                if (model.ImageUpload != null)
                {
                    string file = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                    string extension = Path.GetExtension(model.ImageUpload.FileName);
                    file = file + extension;
                    model.Images = "~/Content/Images/" + file;
                    model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/"), file));
                }
                context.Trainers.Add(model);
                context.SaveChanges();
                return RedirectToAction("IndexTrainer");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditTrainer(int id)
        {
            // kahi báo biến edit để tìm id
            var edit = context.Trainers.Where(x => x.ID == id).FirstOrDefault();
            return View(edit);
        }

        [HttpPost]
        public ActionResult EditTrainer(int id, Trainer model)
        {
            try
            {
                if (model.ImageUpload != null)
                {
                    string file = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                    string extension = Path.GetExtension(model.ImageUpload.FileName);
                    file = file + extension;
                    model.Images = "~/Content/Images/" + file;
                    model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/"), file));
                }
                // TODO: Add update logic here
                //khai báo biến oldItem để tìm id của bảng Trainee
                var oldItem = context.Trainers.Find(model.ID);
                //với mỗi dữ liệu của bảng cũ(oldItem) được thay thế bằng dữ liệu mới
                oldItem.Images = model.Images;
                oldItem.Email = model.Email;
                oldItem.Password = model.Password;
                oldItem.Name = model.Name;
                oldItem.Age = model.Age;
                oldItem.Gender = model.Gender;
                oldItem.PhoneNumber = model.PhoneNumber;
                oldItem.Education = model.Education;
                context.SaveChanges(); // lưu thay đổi
                return RedirectToAction("IndexTrainer");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DetailTrainer(int id)
        {
            var list = context.Trainers.Where(x => x.ID == id).FirstOrDefault();
            return View(list);
        }

        public ActionResult DeleteTrainer(int id)
        {
            var delete = context.Trainers.Find(id);
            return View(delete);
        }

        [HttpPost]
        public ActionResult DeleteTrainer(int id, Trainer model)
        {
            try
            {
                // TODO: Add delete logic here
                var delete = context.Trainers.Find(id);
                context.Trainers.Remove(delete);
                context.SaveChanges();
                return RedirectToAction("IndexTrainer");
            }
            catch
            {
                return View();
            }
        }
    }
}
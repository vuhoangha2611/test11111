using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FPT_Final.Models.Entities;
namespace FPT_Final.Controllers
{
    public class StaffController : Controller
    {
        FPTUniversityEntities1 context = new FPTUniversityEntities1();
        // GET: Staff
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(Staff model)
        {
            //khai báo biến đếm reddult
            var result = context.Staffs.Where(x => x.Email == model.Email && x.Password == model.Password).Count();
            if (result > 0)
            {
                //lưu emai và password trong session
                Session["name"] = model.Name;
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
        
        public ActionResult Create()
        {
            //khai báo biến position lấy ra toàn bộ position trong database
            Trainee pro = new Trainee();
            return View(pro);
        }

        // POST: Staff/Create
        [HttpPost]
        public ActionResult Create(Trainee model)
        {
            try
            {
                // TODO: Add insert logic here
                if(model.ImageUpload != null)
                {
                    string file = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                    string extension = Path.GetExtension(model.ImageUpload.FileName);
                    file = file + extension;
                    model.Images = "~/Content/Images/" + file;
                    model.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/"), file));
                }
                context.Trainees.Add(model);
                context.SaveChanges();
                return RedirectToAction("ViewAll");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Detail(int id)
        {
            var list = context.Trainees.Where(x=>x.ID == id).FirstOrDefault();
            return View(list);
        }
        // GET: Staff/Edit/5
        public ActionResult Edit(int id)
        {
            // kahi báo biến edit để tìm id
            var edit = context.Trainees.Where(x => x.ID == id).FirstOrDefault();
            return View(edit);
        }

        // POST: Staff/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Trainee model)
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
                var oldItem = context.Trainees.Find(model.ID);
                //với mỗi dữ liệu của bảng cũ(oldItem) được thay thế bằng dữ liệu mới
                oldItem.Images = model.Images;
                oldItem.Email = model.Email;
                oldItem.Password = model.Password;
                oldItem.Name = model.Name;
                oldItem.Age = model.Age;
                oldItem.Gender = model.Gender;
                oldItem.PhoneNumber = model.PhoneNumber;
                oldItem.ProgrammingLanguage = model.ProgrammingLanguage;
                context.SaveChanges(); // lưu thay đổi
                return RedirectToAction("ViewAll");
            }
            catch
            {
                return View();
            }
        }

        // GET: Staff/Delete/5
        public ActionResult Delete(int id)
        {
            var delete = context.Trainees.Find(id);
            return View(delete);
        }

        // POST: Staff/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Trainee model)
        {
            try
            {
                // TODO: Add delete logic here
                var delete = context.Trainees.Find(id);
                context.Trainees.Remove(delete);
                context.SaveChanges();
                return RedirectToAction("ViewAll");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ViewAll(string input)
        {
            //tạo list toàn bộ dữ liệu trong bảng trainee
            List<Trainee> list = context.Trainees.ToList();

            if (!string.IsNullOrEmpty(input)) //kiểm tra giá trị nhập vào có null ko 
            {
                //gọi ra danh sách tất cả trainee có traineecode tương ứng với giá trị nhập
                list = list.Where(x => x.Name.Contains(input)).ToList(); 
            }
            return View(list);
        }

        public ActionResult SettingAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SettingAccount(Staff model,string input)
        {
            //khai báo biến email và password gọi ra session đã lưu tại login
            string email = Session["email"].ToString(); 
            string password = Session["password"].ToString();
            var result = context.Staffs.Where(x => x.Password == password).FirstOrDefault();//trả về phần tử đầu tiên của bảng 
            
            if(result != null)
            {
                //khai báo biến detail so sánh email của phần tử tìm được với email được lưu trong session
                var detail = context.Staffs.FirstOrDefault(x => x.Email == email);
                if (detail != null)
                {
                    detail.Password = model.NewPassword; //thay đổi password 
                    context.SaveChanges();// lưu thay đổi
                    ViewBag.Message = "change successfully!";
                }
            }
            return View();
        }

        public ActionResult ViewCourse()
        {
            List<Course> list = context.Courses.ToList();
            return View(list);
        }

        public ActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCourse(Course model)
        {
            try
            {
                context.Courses.Add(model);
                context.SaveChanges();
                return RedirectToAction("ViewCourse");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditCourse(int id)
        {
            var edit = context.Courses.Find(id);
            return View(edit);
        }

        [HttpPost]
        public ActionResult EditCourse(int id,Course model)
        {
            
            try
            {
                // TODO: Add update logic here
                var oldItem = context.Courses.Find(model.ID);
                oldItem.CourseName = model.CourseName;
                oldItem.Description = model.Description;
                oldItem.Time = model.Time;
                context.SaveChanges();
                return RedirectToAction("ViewCourse");
            }
            catch
            {
                return View();
            }           
        }

        public ActionResult DetailCourse(int id)
        {
            var detail = context.Courses.Where(x => x.ID == id).FirstOrDefault();
            return View(detail);
        }

        public ActionResult DeleteCourse(int id)
        {
            var delete = context.Courses.Find(id);
            return View(delete);
        }


        [HttpPost]
        public ActionResult DeleteCourse(int id, Course model)
        {
            try
            {
                // TODO: Add delete logic here
                var delete = context.Courses.Find(id);
                context.Courses.Remove(delete);
                context.SaveChanges();
                return RedirectToAction("ViewCourse");
            }
            catch
            {
                return View();
            }
        }
      
    }
}
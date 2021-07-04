using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FPT_Final.Models.Entities;
namespace FPT_Final.Controllers
{
    public class StaffController : Controller
    {
        FptdatataEntities1 context = new FptdatataEntities1();
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
        public ActionResult login(staff model)
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

        // GET: Staff/Create
        public ActionResult Create()
        {
            var position = new SelectList(context.positions, "id", "position1");
            ViewBag.positionId = position;
            return View();
        }

        // POST: Staff/Create
        [HttpPost]
        public ActionResult Create(Trainee model)
        {
            try
            {
                // TODO: Add insert logic here
                context.Trainees.Add(model);
                context.SaveChanges();
                return RedirectToAction("ViewAll");
            }
            catch
            {
                return View();
            }
        }

        // GET: Staff/Edit/5
        public ActionResult Edit(int id)
        {
            var edit = context.Trainees.Find(id);
            var position = new SelectList(context.positions, "id", "position1");
            ViewBag.positionId = position;
            return View(edit);
        }

        // POST: Staff/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Trainee model)
        {
            try
            {
                // TODO: Add update logic here
                var oldItem = context.Trainees.Find(model.TraineeID);
                oldItem.Email = model.Email;
                oldItem.Password = model.Password;
                oldItem.TraineeCode = model.TraineeCode;
                oldItem.Name = model.Name;
                oldItem.Age = model.Age;
                oldItem.Phone_number = model.Phone_number;
                oldItem.Programming_language = model.Programming_language;
                oldItem.positionId = model.positionId;
                context.SaveChanges();
                return RedirectToAction("Index");
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
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var delete = context.Trainees.Find(id);
                context.Trainees.Remove(delete);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ViewAll(string input)
        {
            List<Trainee> list = context.Trainees.ToList();

            if (!string.IsNullOrEmpty(input))
            {
                list = list.Where(x => x.TraineeCode.Contains(input)).ToList();
            }
            return View(list);
        }
    }
}
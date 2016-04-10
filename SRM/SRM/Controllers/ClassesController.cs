using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SRM.Models;
using SRM.Utils;

namespace SRM.Controllers
{
    public class ClassesController : Controller
    {
        private readonly SRMEntities db = new SRMEntities();

        // GET: Classes
        public ActionResult Index()
        {
            return View();
        }

        // GET: Classes/Details/5
        public ActionResult Details(decimal id)
        {
            var obj = db.v_GetClassList.Find(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            return View("Profile", obj);
        }

        // GET: Classes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassID,Name,Capacity,Location,Status,CreatedBy,CreatedDate,ModifiedDate,code,Notes")] tbClass tbClass)
        {
            if (ModelState.IsValid)
            {
                tbClass.CreatedDate = DateTime.Now;
                tbClass.CreatedBy = (decimal?) Session[SessionIndex.userID.ToString()];
                tbClass.ModifiedDate = DateTime.Now;
                db.tbClasses.Add(tbClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbClass);
        }

        // GET: Classes/Edit/5
        public ActionResult Edit(decimal id)
        {
            var tbClass = db.tbClasses.Find(id);
            if (tbClass == null)
            {
                return HttpNotFound();
            }
            return View(tbClass);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassID,Name,Capacity,Location,Status,CreatedBy,CreatedDate,ModifiedDate,code,Notes")] tbClass tbClass)
        {
            if (ModelState.IsValid)
            {
                tbClass.ModifiedDate = DateTime.Now;
                db.Entry(tbClass).State = EntityState.Modified;
                db.SaveChanges();
                return View("Index");
            }
            return View(tbClass);
        }
        [HttpPost]
        public JsonResult Delete(decimal? id)
        {
            var obj = db.tbClasses.Find(id);
            if (obj == null) Json(false, JsonRequestBehavior.AllowGet);
            obj.isDeleted = true;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetClassList([DataSourceRequest]DataSourceRequest request)
        {
            return Json(db.v_GetClassList.Where(x=> x.isDeleted != true).ToDataSourceResult(request));
        }
        [HttpPost]
        public JsonResult IsValidData(bool state,int id,string action)
        {
            var obj = db.tbPermissionByForms.FirstOrDefault(x => x.FormID == id);
            if (obj == null)
            {
                var item = new tbPermissionByForm
                {
                     CreatedBy = (decimal?) Session[SessionIndex.userID.ToString()],
                     CreatedBy_name = (string) Session[SessionIndex.UserName.ToString()],
                     FormID = id,
                     CreatedDate = DateTime.Now,
                     ModifiedBy = (decimal?)Session[SessionIndex.userID.ToString()],
                     ModifiedByName = (string)Session[SessionIndex.UserName.ToString()],
                     ModifiedDate = DateTime.Now,
                     
                };
            }
            switch (action)
            {
                case "Add":

                    break;
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        
    }

    public enum ActionParameter
    {
        Add,
        Update,
        Delete,
        View
    }
}

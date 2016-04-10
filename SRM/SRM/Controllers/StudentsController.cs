using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
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
    public class StudentsController : Controller
    {
        private readonly SRMEntities _db = new SRMEntities();
        // GET: Students
        public ActionResult Index()
        {
            if (Session[SessionIndex.userID.ToString()] == null) return RedirectToAction("Index", "AdminLte");
            return View();
        }

        // GET: Students/Details/5
        public ActionResult Details(decimal id)
        {
            var objStudent = _db.v_GetStudentList.Find(id);
            if (objStudent == null)
            {
                return HttpNotFound();
            }
            return View("Profile", objStudent);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            if (Session[SessionIndex.userID.ToString()] == null)
                return RedirectToRoute("Home");//RedirectToAction("Index", "AdminLte");

            ViewBag.ClassID = new SelectList(_db.tbClasses, "ClassID", "Name");
            ViewBag.catID = new SelectList(_db.tbStudentCategories, "catID", "Descriptions");

            // populate previous address
            ViewBag.PreCountry = new SelectList(_db.tbCountries, "CountryID", "Name");
            ViewBag.PreProvince = new SelectList(_db.tbProvinces,"ProID","Name");
            ViewBag.PreDistrict = new SelectList(_db.tbDistricts, "DisID", "Name");
            ViewBag.PreCommune = new SelectList(_db.tbCommunes, "ComID", "Name");
            ViewBag.PreVillage = new SelectList(_db.tbVillages, "ViID", "Name");
            
            // populate current address
            ViewBag.CurCountry = new SelectList(_db.tbCountries, "CountryID", "Name");
            ViewBag.CurProvince = new SelectList(_db.tbProvinces, "ProID", "Name");
            ViewBag.CurDistrict = new SelectList(_db.tbDistricts, "DisID", "Name");
            ViewBag.CurCommune = new SelectList(_db.tbCommunes, "ComID", "Name");
            ViewBag.CurVillage = new SelectList(_db.tbVillages, "ViID", "Name");
            ViewBag.DefaultID = GetAutoStudentId();
            return View();
        }

        private string GetAutoStudentId()
        {
            var result = DateTime.Now.ToString("yyyyMMdd");
            decimal sq = 0;
            var tb = _db.tbStudents.OrderByDescending(x => x.stdID).FirstOrDefault();
            if (tb == null)
            {
                sq = 1;
            }
            else
            {
                var cuted = tb.stdID.ToString(CultureInfo.InvariantCulture).Substring(8, 4);
                sq = Convert.ToDecimal(cuted) + 1;
            }
            return $@"{result}{sq.ToString(CultureInfo.InvariantCulture).PadLeft(4,'0')}";
        }
        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stdID,Name,Gender,catID,ClassID,DOB,Father_name,Mother_name,PreAddress,CurrenctAddress,HouseNo,ContactPhone")] tbStudent tbStudent)
        {
            if (ModelState.IsValid)
            {
                tbStudent.stdID = Convert.ToDecimal(GetAutoStudentId());
                tbStudent.CreatedDate = DateTime.Now;
                tbStudent.ModifiedDate = DateTime.Now;
                tbStudent.CreatedBy = (decimal?) Session[SessionIndex.userID.ToString()];
                tbStudent.ModifiedBy = tbStudent.CreatedBy;
                tbStudent.CreatedBy_name = (string) Session[SessionIndex.UserName.ToString()];
                _db.tbStudents.Add(tbStudent);
                _db.SaveChanges();
                //return RedirectToAction("Index");
                return View("Index");
            }
            ViewBag.ClassID = new SelectList(_db.tbClasses, "ClassID", "Name",tbStudent.ClassID);
            ViewBag.catID = new SelectList(_db.tbStudentCategories, "catID", "Descriptions",tbStudent.catID);

            // populate previous address
            ViewBag.PreCountry = new SelectList(_db.tbCountries, "CountryID", "Name");
            ViewBag.PreProvince = new SelectList(_db.tbProvinces, "ProID", "Name");
            ViewBag.PreDistrict = new SelectList(_db.tbDistricts, "DisID", "Name");
            ViewBag.PreCommune = new SelectList(_db.tbCommunes, "ComID", "Name");
            ViewBag.PreVillage = new SelectList(_db.tbVillages, "ViID", "Name",tbStudent.PreAddress);

            // populate current address
            ViewBag.CurCountry = new SelectList(_db.tbCountries, "CountryID", "Name");
            ViewBag.CurProvince = new SelectList(_db.tbProvinces, "ProID", "Name");
            ViewBag.CurDistrict = new SelectList(_db.tbDistricts, "DisID", "Name");
            ViewBag.CurCommune = new SelectList(_db.tbCommunes, "ComID", "Name");
            ViewBag.CurVillage = new SelectList(_db.tbVillages, "ViID", "Name");
            ViewBag.DefaultID = GetAutoStudentId();
            return View(tbStudent);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(decimal id)
        {
            var tbStudent = _db.tbStudents.Find(id);
            if (tbStudent == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(_db.tbClasses, "ClassID", "Name", tbStudent.ClassID);
            ViewBag.catID = new SelectList(_db.tbStudentCategories, "catID", "Descriptions", tbStudent.catID);

            // populate previous address
            ViewBag.PreCountry = new SelectList(_db.tbCountries, "CountryID", "Name");
            ViewBag.PreProvince = new SelectList(_db.tbProvinces, "ProID", "Name");
            ViewBag.PreDistrict = new SelectList(_db.tbDistricts, "DisID", "Name");
            ViewBag.PreCommune = new SelectList(_db.tbCommunes, "ComID", "Name");
            ViewBag.PreVillage = new SelectList(_db.tbVillages, "ViID", "Name", tbStudent.PreAddress);

            // populate current address
            ViewBag.CurCountry = new SelectList(_db.tbCountries, "CountryID", "Name");
            ViewBag.CurProvince = new SelectList(_db.tbProvinces, "ProID", "Name");
            ViewBag.CurDistrict = new SelectList(_db.tbDistricts, "DisID", "Name");
            ViewBag.CurCommune = new SelectList(_db.tbCommunes, "ComID", "Name");
            ViewBag.CurVillage = new SelectList(_db.tbVillages, "ViID", "Name",tbStudent.CurrenctAddress); 
            return View("Edit",tbStudent);
        }
        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stdID,Name,Gender,catID,ClassID,DOB,Father_name,Mother_name,PreAddress,CurrenctAddress,HouseNo,ContactPhone,CreatedBy,CreatedDate,CreatedBy_name,isDeleted")] tbStudent tbStudent)
        {
            if (ModelState.IsValid)
            {
                tbStudent.ModifiedDate = DateTime.Now;
                tbStudent.ModifiedBy = Session[SessionIndex.userID.ToString()].ToDecimal();
                _db.Entry(tbStudent).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(_db.tbClasses, "ClassID", "Name", tbStudent.ClassID);
            ViewBag.catID = new SelectList(_db.tbStudentCategories, "catID", "Descriptions", tbStudent.catID);

            // populate previous address
            ViewBag.PreCountry = new SelectList(_db.tbCountries, "CountryID", "Name");
            ViewBag.PreProvince = new SelectList(_db.tbProvinces, "ProID", "Name");
            ViewBag.PreDistrict = new SelectList(_db.tbDistricts, "DisID", "Name");
            ViewBag.PreCommune = new SelectList(_db.tbCommunes, "ComID", "Name");
            ViewBag.PreVillage = new SelectList(_db.tbVillages, "ViID", "Name", tbStudent.PreAddress);

            // populate current address
            ViewBag.CurCountry = new SelectList(_db.tbCountries, "CountryID", "Name");
            ViewBag.CurProvince = new SelectList(_db.tbProvinces, "ProID", "Name");
            ViewBag.CurDistrict = new SelectList(_db.tbDistricts, "DisID", "Name");
            ViewBag.CurCommune = new SelectList(_db.tbCommunes, "ComID", "Name");
            ViewBag.CurVillage = new SelectList(_db.tbVillages, "ViID", "Name",tbStudent.CurrenctAddress);

            return View(tbStudent);
        }

        // GET: Students/Delete/5
        //public JsonResult Delete(decimal id)
        //{
        //    tbStudent tbStudent = _db.tbStudents.Find(id);
        //    if (tbStudent == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tbStudent);
        //}

        [HttpPost]
        public JsonResult Delete(decimal? id)
        {
            var obj = _db.tbStudents.Find(id);
            if (obj == null) Json(false, JsonRequestBehavior.AllowGet);
            obj.isDeleted = true;
            _db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region my OwnFunctions

        [HttpPost]
        public JsonResult GetProvinceListById(string id)
        {
            
            return Json(new SelectList(SingletonDataBindingList.GetProvinces(id),"ProID","Name"),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDistrictById(string id)
        {
            return Json(new SelectList(SingletonDataBindingList.GetDistricts(id),"DisID","Name"), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetCommuneById(string id)
        {
            return Json(new SelectList(SingletonDataBindingList.GetCommunes(id),"ComID","Name"), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetVillageById(string id)
        {
            return Json(new SelectList(SingletonDataBindingList.GeTbVillages(id),"ViID","Name"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsValidData()
        {
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StudentRead([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_db.v_GetStudentList.Where(x=>x.isDeleted != true).ToDataSourceResult(request));
        }
        #endregion
    }
}

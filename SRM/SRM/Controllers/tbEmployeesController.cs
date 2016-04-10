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
    public class tbEmployeesController : Controller
    {
        private SRMEntities db = new SRMEntities();

        // GET: tbEmployees
        public ActionResult Index()
        {
            return View();
        }

        // GET: tbEmployees/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmployee tbEmployee = db.tbEmployees.Find(id);
            if (tbEmployee == null)
            {
                return HttpNotFound();
            }

            // populate previous address
            ViewBag.PreCountry = new SelectList(db.tbCountries, "CountryID", "Name");
            ViewBag.PreProvince = new SelectList(db.tbProvinces, "ProID", "Name");
            ViewBag.PreDistrict = new SelectList(db.tbDistricts, "DisID", "Name");
            ViewBag.PreCommune = new SelectList(db.tbCommunes, "ComID", "Name");
            ViewBag.PreVillage = new SelectList(db.tbVillages, "ViID", "Name", tbEmployee.PerAddress);

            // populate current address
            ViewBag.CurCountry = new SelectList(db.tbCountries, "CountryID", "Name");
            ViewBag.CurProvince = new SelectList(db.tbProvinces, "ProID", "Name");
            ViewBag.CurDistrict = new SelectList(db.tbDistricts, "DisID", "Name");
            ViewBag.CurCommune = new SelectList(db.tbCommunes, "ComID", "Name");
            ViewBag.CurVillage = new SelectList(db.tbVillages, "ViID", "Name", tbEmployee.CurrentAddress);

            ViewBag.ID = tbEmployee.empID;
            return View(tbEmployee);
        }

        // GET: tbEmployees/Create
        public ActionResult Create()
        {
            // populate previous address
            ViewBag.PreCountry = new SelectList(db.tbCountries, "CountryID", "Name");
            ViewBag.PreProvince = new SelectList(db.tbProvinces, "ProID", "Name");
            ViewBag.PreDistrict = new SelectList(db.tbDistricts, "DisID", "Name");
            ViewBag.PreCommune = new SelectList(db.tbCommunes, "ComID", "Name");
            ViewBag.PreVillage = new SelectList(db.tbVillages, "ViID", "Name");

            // populate current address
            ViewBag.CurCountry = new SelectList(db.tbCountries, "CountryID", "Name");
            ViewBag.CurProvince = new SelectList(db.tbProvinces, "ProID", "Name");
            ViewBag.CurDistrict = new SelectList(db.tbDistricts, "DisID", "Name");
            ViewBag.CurCommune = new SelectList(db.tbCommunes, "ComID", "Name");
            ViewBag.CurVillage = new SelectList(db.tbVillages, "ViID", "Name");
            ViewBag.ID = GetID();
            return View();
        }

        private decimal GetID()
        {
            var obj = db.tbEmployees.OrderByDescending(x => x.empID).First();
            var tmpSq = obj == null ? 0 : Convert.ToInt32(obj.empID.ToString(CultureInfo.InvariantCulture).Substring(8));
            return Convert.ToDecimal($@"{DateTime.Now.ToString("yyyyMMdd")}{(tmpSq + 1).ToString().PadLeft(4, '0')}");
        }
        // POST: tbEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "empID,Name,IDCard,DOB,ContactPhone,Gender,CurrentAddress,PerAddress,Position")] tbEmployee tbEmployee)
        {
            tbEmployee.empID = GetID();
            if (ModelState.IsValid)
            {
                tbEmployee.CreatedDate = DateTime.Now;
                tbEmployee.CreatedBy = (decimal?) Session[SessionIndex.userID.ToString()];
                tbEmployee.ModifiedDate  = DateTime.Now;
                tbEmployee.isDeleted = false;
                tbEmployee.isActivated = true;
                db.tbEmployees.Add(tbEmployee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // populate previous address
            ViewBag.PreCountry = new SelectList(db.tbCountries, "CountryID", "Name");
            ViewBag.PreProvince = new SelectList(db.tbProvinces, "ProID", "Name");
            ViewBag.PreDistrict = new SelectList(db.tbDistricts, "DisID", "Name");
            ViewBag.PreCommune = new SelectList(db.tbCommunes, "ComID", "Name");
            ViewBag.PreVillage = new SelectList(db.tbVillages, "ViID", "Name",tbEmployee.PerAddress);

            // populate current address
            ViewBag.CurCountry = new SelectList(db.tbCountries, "CountryID", "Name");
            ViewBag.CurProvince = new SelectList(db.tbProvinces, "ProID", "Name");
            ViewBag.CurDistrict = new SelectList(db.tbDistricts, "DisID", "Name");
            ViewBag.CurCommune = new SelectList(db.tbCommunes, "ComID", "Name");
            ViewBag.CurVillage = new SelectList(db.tbVillages, "ViID", "Name",tbEmployee.CurrentAddress);
            return View(tbEmployee);
        }

        // GET: tbEmployees/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbEmployee tbEmployee = db.tbEmployees.Find(id);
            if (tbEmployee == null)
            {
                return HttpNotFound();
            }
            // populate previous address
            ViewBag.PreCountry = new SelectList(db.tbCountries, "CountryID", "Name");
            ViewBag.PreProvince = new SelectList(db.tbProvinces, "ProID", "Name");
            ViewBag.PreDistrict = new SelectList(db.tbDistricts, "DisID", "Name");
            ViewBag.PreCommune = new SelectList(db.tbCommunes, "ComID", "Name");
            ViewBag.PreVillage = new SelectList(db.tbVillages, "ViID", "Name", tbEmployee.PerAddress);

            // populate current address
            ViewBag.CurCountry = new SelectList(db.tbCountries, "CountryID", "Name");
            ViewBag.CurProvince = new SelectList(db.tbProvinces, "ProID", "Name");
            ViewBag.CurDistrict = new SelectList(db.tbDistricts, "DisID", "Name");
            ViewBag.CurCommune = new SelectList(db.tbCommunes, "ComID", "Name");
            ViewBag.CurVillage = new SelectList(db.tbVillages, "ViID", "Name", tbEmployee.CurrentAddress);
            ViewBag.ID = tbEmployee.empID;
            return View(tbEmployee);
        }

        // POST: tbEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "empID,Name,IDCard,DOB,ContactPhone,Gender,CurrentAddress,PerAddress,CreatedBy,CreatedDate,ModifiedDate,Position,isActivated,isDeleted")] tbEmployee tbEmployee)
        {
            if (ModelState.IsValid)
            {
                tbEmployee.ModifiedDate = DateTime.Now;
                db.Entry(tbEmployee).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbEmployee);
        }

        [HttpPost]
        public JsonResult Delete(decimal id)
        {
            var obj = db.tbEmployees.Find(id);
            if (obj == null) return Json(false, JsonRequestBehavior.AllowGet);
            obj.isActivated = false;
            obj.isDeleted = true;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult GetEmployeeList([DataSourceRequest]DataSourceRequest request)
        {
            var exp = (decimal?)Session[SessionIndex.userID.ToString()];
            var id = db.tbUsers.Find(exp).empId;
            return Json(db.v_EmplyeeList.Where(x => x.isDeleted == false && x.empID != id).ToDataSourceResult(request));
        }

        #region Own Function
        [HttpPost]
        public JsonResult GetProvinceListById(string id)
        {

            return Json(new SelectList(SingletonDataBindingList.GetProvinces(id), "ProID", "Name"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDistrictById(string id)
        {
            return Json(new SelectList(SingletonDataBindingList.GetDistricts(id), "DisID", "Name"), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetCommuneById(string id)
        {
            return Json(new SelectList(SingletonDataBindingList.GetCommunes(id), "ComID", "Name"), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetVillageById(string id)
        {
            return Json(new SelectList(SingletonDataBindingList.GeTbVillages(id), "ViID", "Name"), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}

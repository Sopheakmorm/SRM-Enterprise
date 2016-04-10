using System;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SRM.DataAccess.UserAuth;
using SRM.Models;
using SRM.Utils;

namespace SRM.Controllers
{
    public class UserAuthController : Controller
    {
        private static readonly FactoryCollection _factory = new FactoryCollection();
        private readonly SRMEntities db = new SRMEntities();
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (Session[SessionIndex.userID.ToString()] != null)
            {
                return RedirectToAction("Index", "AdminLte");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(tbUser model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var resultID = string.Empty;
            var result = await UserAuthDataAccess.LoginInResult(model, out resultID);
            if (result == SignInStatus.Success)
            {
                InitUserSession(Convert.ToDecimal(resultID));
                RedirectToLocal(returnUrl);
                Session[SessionIndex.LoginFailed.ToString()] = null; // Reset Login Failure
            }
            else if (result == SignInStatus.Failure)
            {
                Session[SessionIndex.LoginFailed.ToString()] = "User name or password incorrect";
            }
            return RedirectToLocal(returnUrl);
        }

        private void InitiUserCookies(decimal id)
        {
            var r = db.tbUsers.FirstOrDefault(x => x.Userid == id);
            var isConnected = Functions.CheckForInternetConnection();
            var cook = new Cookie
            {
                Name = "ID"
            };

        }
        private void InitUserSession(decimal id)
        {
            var r = db.tbUsers.FirstOrDefault(x => x.Userid == id);
            var isConnected = Functions.CheckForInternetConnection();
            Session[SessionIndex.userID.ToString()] = r.Userid;
            Session[SessionIndex.Position.ToString()] = r.tbEmployee.Position;
            Session[SessionIndex.UserName.ToString()] = r.UserName;
            Session[SessionIndex.RegisteredDate.ToString()] = r.CreatedDate != null
                ? Convert.ToDateTime(r.CreatedDate).ToString("D")
                : "Unknown";

            Session[SessionIndex.isOnlined.ToString()] = isConnected ? "Online" : "Offline";


            if (r.tbEmployee.photo != null)
            {
                Session[SessionIndex.userImagePath.ToString()] = _factory.CreatePhotoById((int)r.tbEmployee.photo,
                    r.UserName);
            }
            else
            {
                Session[SessionIndex.userImagePath.ToString()] = _factory.CreateDefaultPhoto();
            }
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            //if (Url.IsLocalUrl(returnUrl))
            //{
            //    return Redirect(returnUrl);
            //}
            return RedirectToAction("Index", "AdminLte");
        }

        // POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            resetSessionUser();
            return RedirectToAction("Login", "UserAuth");
        }

        private void resetSessionUser()
        {
            Session.Clear();
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {

            ViewBag.usertype = new SelectList(db.tbUserTypes, "id", "UserType");
            var emps = db.tbEmployees.Where(x => !db.tbUsers.Any(x1 => x1.empId == x.empID));
            ViewBag.empList = new SelectList(emps, "empID", "Name");
            ViewBag.id = GetID();
            return View();
        }

        private decimal GetID()
        {
            var obj = db.tbUsers.OrderByDescending(x => x.Userid).First();
            var tmpSq = obj == null ? 0 : Convert.ToInt32(obj.Userid.ToString(CultureInfo.InvariantCulture).Substring(8));
            return Convert.ToDecimal($@"{DateTime.Now.ToString("yyyyMMdd")}{(tmpSq + 1).ToString().PadLeft(4, '0')}");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Userid,UserName,UserType,empId,pwd")] tbUser user)
        {
            if (ModelState.IsValid)
            {
                user.Userid = GetID();
                user.isActibe = true;
                user.isDeleted = false;
                user.CreatedDate = DateTime.Now;
                user.Createdby = (decimal?)Session[SessionIndex.userID.ToString()];
                user.createdByName = (string)Session[SessionIndex.UserName.ToString()];
                user.modifiedDate = DateTime.Now;
                user.modifiedby = (decimal?)Session[SessionIndex.userID.ToString()];
                db.tbUsers.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.usertype = new SelectList(db.tbUserTypes, "id", "UserType", user.UserType);
            var emps = db.tbEmployees.Where(x => !db.tbUsers.Any(x1 => x1.empId == x.empID));
            ViewBag.empList = new SelectList(emps, "empID", "Name", user.empId);
            return View(user);
        }

        public ActionResult GetUserList([DataSourceRequest]DataSourceRequest request)
        {
            var exp = (decimal?)Session[SessionIndex.userID.ToString()];
            return Json(db.v_GetUserList.Where(x => x.isDeleted == false && x.Userid != exp).ToDataSourceResult(request));
        }
        [Route("UserInformation")]
        public ActionResult Detail(decimal id)
        {
            var user = db.tbUsers.Find(id);
            ViewBag.usertype = new SelectList(db.tbUserTypes, "id", "UserType");
            var emps = db.tbEmployees;//.Where(x => !db.tbUsers.Any(x1 => x1.empId == x.empID));
            ViewBag.empList = new SelectList(emps, "empID", "Name", user.empId);
            ViewBag.id = user.Userid;
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Userid,UserName,UserType,empId,pwd,CreatedBy,CreatedDate,CreatedByName,isActibe,isDeleted")] tbUser user)
        {
            if (ModelState.IsValid)
            {
                user.modifiedDate = DateTime.Now;
                user.modifiedby = (decimal?)Session[SessionIndex.userID.ToString()];
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.usertype = new SelectList(db.tbUserTypes, "id", "UserType", user.UserType);
            var emps = db.tbEmployees.Where(x => !db.tbUsers.Any(x1 => x1.empId == x.empID));
            ViewBag.empList = new SelectList(emps, "empID", "Name", user.empId);
            return View(user);
        }

        public ActionResult Edit(decimal id)
        {
            var user = db.tbUsers.Find(id);
            if (user == null) return HttpNotFound();
            ViewBag.usertype = new SelectList(db.tbUserTypes, "id", "UserType", user.UserType);
            var emps = db.tbEmployees.Where(x => x.empID == user.empId || !db.tbUsers.Any(x1 => x1.empId == x.empID));
            ViewBag.empList = new SelectList(emps, "empID", "Name", user.empId);
            return View(user);
        }
        public static string EncryptPassword(string password)
        {
            //var provider = new SHA1CryptoServiceProvider();
            //var encoding = new UnicodeEncoding();
            //return provider.ComputeHash(encoding.GetBytes(password));
            return password;
        }

        [HttpPost]
        public JsonResult Delete(decimal id)
        {
            var obj = db.tbUsers.Find(id);
            if (obj == null) Json(false, JsonRequestBehavior.AllowGet);
            obj.isActibe = false;
            obj.isDeleted = true;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPermissionList([DataSourceRequest]DataSourceRequest request,decimal? id)
        {
            var list = db.fn_GetPermissionListByUserId(id).ToList();
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public JsonResult UpdatePermission(string id, string user_id, bool? state, EAction action)
        {
            var formId = id.ToInt();
            var userid = user_id.ToInt();
            var permission = db.tbPermissionByForms.FirstOrDefault(x => x.FormID == formId && x.UserId == userid);
            if (permission == null)
            {
                permission= new tbPermissionByForm
                {
                    CreatedBy = Session[SessionIndex.userID.ToString()].ToDecimal(),
                    CreatedDate = DateTime.Now,
                    CreatedBy_name = Session[SessionIndex.UserName.ToString()].ToString(),
                    FormID = formId,
                    UserId = userid,
                    UserName = Session[SessionIndex.UserName.ToString()].ToString()
                };
                db.tbPermissionByForms.Add(permission);
            }
            switch (action)
            {
                case EAction.CanAdd:
                    permission.canAdd = state;
                    break;
                case EAction.CanDelete:
                    permission.canDelete = state;
                    break;
                case EAction.CanUpdate:
                    permission.canUpdate = state;
                    break;
                case EAction.CanView:
                    permission.canView = state;
                    break;
            }
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
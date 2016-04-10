using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using SRM.Models;

namespace SRM.Utils
{

    public static class SingletonObject
    {
        private static string GeneralItemIcon = "fa-cog";

        public static AjaxOptions _ajaxOption(string panel = "pMain")
        {
            return  new AjaxOptions
            {
                UpdateTargetId = panel,
                InsertionMode = InsertionMode.Replace,
                HttpMethod = "GET"
            };
        }
        public static AjaxOptions _ajaxOption(string panel,bool isPost = false)
        {
            return new AjaxOptions
            {
                UpdateTargetId = panel ?? mainPanelID(),
                InsertionMode = InsertionMode.Replace,
                HttpMethod = isPost ? "POST" : "GET"
            };
        }

        public static Crumb crumbAdminLte_Index(UrlHelper url)
        {
            return new Crumb("Dashboard",url.Action("Index","AdminLte"), "fa-dashboard");
        }
        public static Crumb crumbStudent_Index(UrlHelper url)
        {
            return new Crumb("Student", url.Action("Index", "Students"), "fa-user-md");
        }
        public static Crumb crumbStudent_Create(UrlHelper url)
        {
            return new Crumb("Register", url.Action("Create", "Students"), GeneralItemIcon);
        }
        public static Crumb crumbStudent_Edit(UrlHelper url)
        {
            return new Crumb("Update", url.Action("Edit", "Students"), GeneralItemIcon);
        }
        public static Crumb crumbClass_Index(UrlHelper url)
        {
            return new Crumb("Class Information", url.Action("Index", "Classes"), "fa-building-o");
        }
        public static Crumb crumbClass_Create(UrlHelper url)
        {
            return new Crumb("Create Class", url.Action("Create", "Classes"), GeneralItemIcon);
        }
        public static Crumb crumbClass_Update(UrlHelper url)
        {
            return new Crumb("Update Class", url.Action("Edit", "Classes"), GeneralItemIcon);
        }
        public static string mainPanelClass()
        {
            return ".content-wrapper";
        }
        public static string mainPanelID()
        {
            return "pMain";
        }
    }
}
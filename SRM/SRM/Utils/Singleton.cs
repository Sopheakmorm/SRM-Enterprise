using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SRM.Models;

namespace SRM.Utils
{
    public class Singleton
    {
        private static SRMEntities db;

        private Singleton()
        {
            
        }

        public static SRMEntities GetDatabase()
        {
            return db ?? (db = new SRMEntities());
        }

        public static tbUser userLogin { get; set; }

    }

    public class SingletonBinding
    {
        public static IEnumerable<SelectListItem> countryList = Singleton.GetDatabase().tbCountries.Select(c=> new SelectListItem
        {
            Text = c.Name,
            Value = c.CountryID
        });
    }
    public enum SessionIndex
    {
        userID,
        UserName,
        pwd,
        Position,
        RegisteredDate,
        isOnlined,
        LoginFailed,
        userImagePath
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SRM.Models;

namespace SRM.Utils
{
    public class SingletonDataBindingList
    {
        private static readonly SRMEntities db = Singleton.GetDatabase();
        public static List<tbCountry> GetCountries()
        {
            return db.tbCountries.Select(x => new tbCountry
            {
                CountryID = x.CountryID,
                Name = x.Name
            }).ToList();
        }
        public static List<tbProvince> GetProvinces(string countryId = null)
        {
            var list = string.IsNullOrEmpty(countryId)
                ? db.tbProvinces
                : db.tbProvinces.Where(x => x.couID == countryId);
            var l = list.ToList().Select(x => new tbProvince
            {
                ProID = x.ProID,
                Name = x.Name
            }).ToList();

            return l;
        }
        public static List<tbDistrict> GetDistricts(string provinceID = null)
        {
            var list = string.IsNullOrEmpty(provinceID)
                ? db.tbDistricts
                : db.tbDistricts.Where(x => x.ProID == provinceID);

            return list.ToList().Select(x => new tbDistrict
            {
                DisID = x.DisID,
                Name = x.Name
            }).ToList();
        }

        public static List<tbCommune> GetCommunes(string districtId = null)
        {
            var list = string.IsNullOrEmpty(districtId)
                ? db.tbCommunes
                : db.tbCommunes.Where(x => x.DisID == districtId);

            return list.ToList().Select(x => new tbCommune
            {
                ComID = x.ComID,
                Name = x.Name
            }).ToList();
        }

        public static List<tbVillage> GeTbVillages(string commmuneId = null)
        {
            var list = string.IsNullOrEmpty(commmuneId)
                ? db.tbVillages
                : db.tbVillages.Where(x => x.CommuneID == commmuneId);

            return list.ToList().Select(x => new tbVillage
            {
                ViID = x.ViID,
                Name = x.Name
            }).ToList();
        }
    }
}
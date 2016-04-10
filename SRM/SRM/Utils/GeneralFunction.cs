using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRM.Utils
{
    public static class GeneralFunction
    {
        public static int ToInt(this object obj)
        {
            try
            {
                return int.Parse(obj.ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static decimal ToDecimal(this object obj)
        {
            try
            {
                return decimal.Parse(obj.ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SRM.Utils
{
    public class Functions
    {
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
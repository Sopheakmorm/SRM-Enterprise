using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SRM.Models;
using SRM.Utils;

namespace SRM.DataAccess.UserAuth
{
    public class UserAuthDataAccess
    {

        private static SRMEntities db = Singleton.GetDatabase();
        [DebuggerStepThrough]
        public static Task<SignInStatus> LoginInResult(tbUser user, out string resultId)
        {
            var r = db.tbUsers.FirstOrDefault(x => x.UserName == user.UserName && x.pwd == user.pwd);
            resultId = string.Empty;
            if (r != null) resultId = r.Userid.ToString(CultureInfo.InvariantCulture);
            var resultRequest = r == null ? SignInStatus.Failure : SignInStatus.Success;
            var t = new Task<SignInStatus>(() => resultRequest);
            t.Start();
            return t;
        }

        public static v_UserProfile GetUserProfile(object id)
        {
            var id1 = id == null ? 0 : Convert.ToDecimal(id);
            var result = db.v_UserProfile.FirstOrDefault(x => x.Userid == id1);
            return result;
        }
    }
}
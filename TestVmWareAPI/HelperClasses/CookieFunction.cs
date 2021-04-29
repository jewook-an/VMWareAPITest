using NCCFramework2.Util;
using NCCVDIAdminCCMS.Models;
using NCCVDIAdminCCMS.Models.Login;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Security;

namespace NCCVDIAdminCCMS.HelperClasses {
	public class CookieFunction {
		public static void Clear(HttpCookieCollection cookies) {
			FormsAuthentication.SignOut();

			HttpCookie cookie = cookies["AdminDataJson"];
			if (cookie == null)
				return;

			cookie.Expires = DateTime.Now.AddDays(-1);
		}

		public static UserData GetUser(HttpCookieCollection cookies) {
			try {
				HttpCookie cookie = cookies["AdminDataJson"];
				if (cookie == null)
					return null;

				return JsonConvert.DeserializeObject<UserData>(Cryptography.AESDecryptString(cookie.Value));
			}
			catch (Exception) {
				return null;
			}
		}

		public static void SetUser(HttpCookieCollection cookies, UserData user) {
			FormsAuthentication.SetAuthCookie(user.ID, false);
			HttpCookie dataCookie = new HttpCookie("AdminDataJson", Cryptography.AESEncryptString(JsonConvert.SerializeObject(user)));

			dataCookie.Expires = DateTime.Now.AddYears(1);
			cookies.Add(dataCookie);
		}
	}
}

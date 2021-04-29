using NCCVDIAdminCCMS.CS;
using NCCVDIAdminCCMS.Models;
using NCCVDIAdminCCMS.Models.Login;
using System.Web.Mvc;
using Unity;

namespace NCCVDIAdminCCMS.HelperClasses {
	public class UserDataController : Controller {
		protected UserData UserData => base.User as UserData;

		[Dependency]
		public GLOBAL Global { get; set; }

		public string Language { get; set; } = "KO";
	}
}
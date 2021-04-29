using Microsoft.Ajax.Utilities;
using NCCVDIAdminCCMS.Models;
using NCCVDIAdminCCMS.Models.Login;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;

namespace NCCVDIAdminCCMS.HelperClasses {
	public class UserDataApiController : ApiController {
		protected UserData UserData => this.User as UserData;

		protected T ParseParam<T>(string json) where T : RequestBase, new() {
			T result = JsonConvert.DeserializeObject<T>(json);
			result.AdminID = this.UserData.ID;
			result.OrganizationAssign = this.UserData.OrganizationAssign;
			return result;
		}

		protected List<T> ParseParamList<T>(string json) where T : RequestBase {
			List<T> result = JsonConvert.DeserializeObject<List<T>>(json);

			foreach (var item in result) {
				item.AdminID = this.UserData.ID;
				item.OrganizationAssign = this.UserData.OrganizationAssign;
			}

			return result;
		}
	}
}
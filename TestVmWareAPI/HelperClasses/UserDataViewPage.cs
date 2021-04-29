using NCCVDIAdminCCMS.CS;
using NCCVDIAdminCCMS.Models;
using NCCVDIAdminCCMS.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace NCCVDIAdminCCMS.HelperClasses {
	public abstract class UserDataViewPage : WebViewPage {
		public UserData UserData => base.User as UserData;

		[Dependency]
		public GradeUrlManager UrlManager { get; set; }

		[Dependency]
		public GLOBAL Global { get; set; }

		[Dependency]
		public PropertyData Property { get; set; }

		public GradeUrlItem GradeUrlItem { get; set; }

		public bool IsCanMod(string mod) => this.GradeUrlItem.IsCanMod(mod);

		public string ParentName {
			get {
				if (this.GradeUrlItem == null)
					this.GradeUrlItem = this.UrlManager.GetUrlItem(this.Request, this.UserData);
				return this.GradeUrlItem?.Parent_Name;
			}
		}
		public string Name {
			get {
				if (this.GradeUrlItem == null)
					this.GradeUrlItem = this.UrlManager.GetUrlItem(this.Request, this.UserData);

				return this.GradeUrlItem?.Name;
			}
		}

		public string AppPath {
			get {
				return this.Request.ApplicationPath == "/" ? string.Empty : this.Request.ApplicationPath;
			}
		}

		public string Language { get; set; } = "KO";
	}

	public abstract class UserDataViewPage<TModel> : WebViewPage<TModel> {
		public UserData UserData => base.User as UserData;

		[Dependency]
		public GradeUrlManager UrlManager { get; set; }

		[Dependency]
		public GLOBAL Global { get; set; }

		[Dependency]
		public PropertyData Property { get; set; }

		public GradeUrlItem GradeUrlItem { get; set; }

		public bool IsCanMod(string mod) => this.GradeUrlItem.IsCanMod(mod);

		public string ParentName {
			get {
				if (this.GradeUrlItem == null)
					this.GradeUrlItem = this.UrlManager.GetUrlItem(this.Request, this.UserData);

				return this.GradeUrlItem?.Parent_Name;
			}
		}
		public string Name {
			get {
				if (this.GradeUrlItem == null)
					this.GradeUrlItem = this.UrlManager.GetUrlItem(this.Request, this.UserData);

				return this.GradeUrlItem?.Name;
			}
		}

		public string AppPath {
			get {
				return this.Request.ApplicationPath == "/" ? string.Empty : this.Request.ApplicationPath;
			}
		}

		public string Language { get; set; } = "KO";
	}
}
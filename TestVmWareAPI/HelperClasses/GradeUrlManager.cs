using NCCVDIAdminCCMS.Models;
using NCCVDIAdminCCMS.Models.Login;
using NCCVDIAdminCCMS.Repository.Account;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Unity;

namespace NCCVDIAdminCCMS.HelperClasses {
	public class GradeUrlManager {
		private Dictionary<string, List<GradeUrlItem>> gradeUrl = new Dictionary<string, List<GradeUrlItem>>();

		public GradeUrlManager() {
			this.LoadData();
		}

		public void LoadData() {
			AdminRoleRepository repo = UnityConfig.Container.Resolve<AdminRoleRepository>();
			List<GradeUrlItem> data = repo.GetMenu();
			this.gradeUrl = data.GroupBy(g => g.Grade_Code)
								.ToDictionary(s => s.Key, s => s.ToList());
		}

		public bool IsAuther(UserData user, string controller, string action) {
			return this.IsAuther(user, controller, action, "R");
		}

		public bool IsAuther(UserData user, string controller, string action, string mod) {
			try {
				GradeUrlItem url = this.gradeUrl[user.Grade.ToLower()]
									   .FirstOrDefault(p => p.Controller == controller.ToLower() && p.Action == action.ToLower());
				if (url == null)
					return false;

				return url.ModCheck[mod];
			}
			catch (Exception) {
				return false;
			}
		}

		public List<GradeUrlItem> GetUrlsFromGrade(string grade) {
			if (!this.gradeUrl.ContainsKey(grade.ToLower()))
				return new List<GradeUrlItem>();

			return this.gradeUrl[grade.ToLower()];
		}

		public GradeUrlItem GetUrlItem(HttpRequestBase request, UserData user) {
			var routeValue = request.RequestContext.RouteData.Values;

			return this.GetUrlsFromGrade(user.Grade)
				.FirstOrDefault(p => string.Compare(p.Controller, routeValue["controller"].ToString(), true) == 0
				&& string.Compare(p.Action, routeValue["action"].ToString(), true) == 0);
		}
	}

	public class GradeUrlItem {
		private string _gradeCode = string.Empty;
		public string Grade_Code {
			get => this._gradeCode;
			set => this._gradeCode = value.ToLower();
		}

		public int SEQ { get; set; }

		private string _controller = string.Empty;
		public string Controller {
			get => this._controller;
			set => this._controller = value.ToLower();
		}

		private string _action = string.Empty;
		public string Action {
			get => this._action;
			set => this._action = value.ToLower();
		}

		public string Name { get; set; }

		public int Parent { get; set; }
		public int Order { get; set; }
		public int Parent_Seq { get; set; }
		public int Parent_Order { get; set; }

		public string Parent_Name { get; set; }

		private string _mod = string.Empty;
		public string Mod {
			get => this._mod;
			set {
				this._mod = value;

				this._modCheck = value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
									  .ToDictionary(s => s.ToUpper(), s => true);
			}
		}

		private Dictionary<string, bool> _modCheck = new Dictionary<string, bool>();
		public Dictionary<string, bool> ModCheck => this._modCheck;

		public bool IsCanMod(string mod) {
			if (!this.ModCheck.ContainsKey(mod))
				return false;

			return this.ModCheck[mod];
		}

		public override string ToString() {
			return $"{this.Parent_Name}, {this.Name}, {this.Controller}, {this.Action}";
		}
	}
}
using log4net;
using NCCFramework2.DB;
using NCCVDIAdminCCMS.CS;
using Unity;

namespace NCCVDIAdminCCMS.HelperClasses {
	public class DBHelper : SqlHelper {
		private ILog logger = LogManager.GetLogger("DB");

		public DBHelper() {
			this.Log = msg => this.logger.Info(msg);
		}

		public DBHelper(string procedureName) : base(procedureName) {
			this.Log = msg => this.logger.Info(msg);
		}

		public DBHelper(string connectionString, string procedureName) : base(connectionString, procedureName) {
			this.Log = msg => this.logger.Info(msg);
		}

#if DEBUG
		protected override string GetConnectionString() => Properties.Settings.Default.SqlDB;
#else
		protected override string GetConnectionString() => UnityConfig.Container.Resolve<GLOBAL>().siteConfig._NCCDBInfo.ConnectionString;
#endif
	}
}
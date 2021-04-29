using log4net;
using NCCVDIAdminCCMS.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace NCCVDIAdminCCMS.HelperClasses {
	public class TraceEntities : DBEntities {
		private ILog logger = LogManager.GetLogger("DB");

		public TraceEntities() : base() {
			this.Database.Log = p => this.logger.Debug(p);

#if !DEBUG
			this.Database.Connection.ConnectionString = UnityConfig.Container.Resolve<GLOBAL>().siteConfig._NCCDBInfo.ConnectionString;
#endif
		}
	}
}
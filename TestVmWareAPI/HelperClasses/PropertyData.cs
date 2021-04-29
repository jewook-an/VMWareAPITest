using Grpc.Core;
using log4net;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace NCCVDIAdminCCMS.HelperClasses {
	public class PropertyData {
		private ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public string DDCServerPort { get; private set; }

		public string HostAddress_HA { get; private set; }

		public string Netbios { get; private set; }

		public string IsExcelConfirm { get; private set; }
		
		public string VMList { get; set; }

		public int[] Dashboard { get; set; }

		public int DDCServerCount => this.DDCServerIPQueue.Count;

		private Queue<string> DDCServerIPQueue { get; set; } = new Queue<string>();

		public string DDCServerIP => DDCServerIPQueue != null && DDCServerIPQueue.Count > 0 ? this.DDCServerIPQueue.Peek() : string.Empty;

		public ServerIPConfigManager AdminGradeService { get; }
		public ServerIPConfigManager UserAccountService { get; }
		public ServerIPConfigManager HrService { get; }
		public ServerIPConfigManager BoardService { get; }
		public ServerIPConfigManager PolicyService { get; }
		public ServerIPConfigManager OtpService { get; }
		public ServerIPConfigManager LoggingService { get; }
		public ServerIPConfigManager VdiService { get; }
		public ServerIPConfigManager VdiskService { get; }
		public ServerIPConfigManager HistoryService { get; }
		public ServerIPConfigManager AccountMessageService { get; }
		public ServerIPConfigManager NasService { get; }

		public ServerIPConfigManager ApprovalService { get; }

		public PropertyData() {
			this.DDCServerPort = ConfigurationManager.AppSettings["DDC_PORT"].ToString();
			this.HostAddress_HA = ConfigurationManager.AppSettings["HOST_ADDRESS_HA"].ToString();
			this.Netbios = ConfigurationManager.AppSettings["NETBIOS"].ToString();
			this.IsExcelConfirm = ConfigurationManager.AppSettings["EXCEL_CONFIRM"].ToString();
			this.VMList = ConfigurationManager.AppSettings["VMList"].ToString();
			this.Dashboard = ConfigurationManager.AppSettings["Dashboard"].ToString()
								.Split(',')
								.Select(s => int.Parse(s))
								.ToArray();

			string ip1 = Convert.ToString(ConfigurationManager.AppSettings["DDC_ADDRESS"]);
			string ip2 = Convert.ToString(ConfigurationManager.AppSettings["DDC_ADDRESS_HA"]);

			if (!string.IsNullOrEmpty(ip1))
				this.DDCServerIPQueue.Enqueue(ip1);

			if (!string.IsNullOrEmpty(ip2))
				this.DDCServerIPQueue.Enqueue(ip2);

			this.AdminGradeService = new ServerIPConfigManager(ConfigurationManager.AppSettings["ADMIN_GRADE_SERVICE"].ToString());
			this.UserAccountService = new ServerIPConfigManager(ConfigurationManager.AppSettings["USER_ACCOUNT_SERVICE"].ToString());
			this.HrService = new ServerIPConfigManager(ConfigurationManager.AppSettings["HR_SERVICE"].ToString());
			this.BoardService = new ServerIPConfigManager(ConfigurationManager.AppSettings["BOARD_SERVICE"].ToString());
			this.PolicyService = new ServerIPConfigManager(ConfigurationManager.AppSettings["POLICY_SERVICE"].ToString());
			this.OtpService = new ServerIPConfigManager(ConfigurationManager.AppSettings["OTP_SERVICE"].ToString());
			this.LoggingService = new ServerIPConfigManager(ConfigurationManager.AppSettings["LOGGING_SERVICE"].ToString());
			this.VdiService = new ServerIPConfigManager(ConfigurationManager.AppSettings["VDI_SERVICE"].ToString());
			this.VdiskService = new ServerIPConfigManager(ConfigurationManager.AppSettings["VDISK_SERVICE"].ToString());
			this.HistoryService = new ServerIPConfigManager(ConfigurationManager.AppSettings["HISTORY_SERVICE"].ToString());
			this.AccountMessageService = new ServerIPConfigManager(ConfigurationManager.AppSettings["ACCOUNT_MESSAGE_SERVICE"].ToString());
			this.NasService = new ServerIPConfigManager(ConfigurationManager.AppSettings["NAS_SERVICE"].ToString());
			this.ApprovalService = new ServerIPConfigManager(ConfigurationManager.AppSettings["APPROVAL_SERVICE"].ToString());
		}

		public void DDCServerIPSwap() {
			if (this.DDCServerIPQueue.Count > 0)
			{
				string tmp = this.DDCServerIPQueue.Dequeue();
				this.DDCServerIPQueue.Enqueue(tmp);

				this.logger.Warn($"DDC IP Change : {tmp} -> {this.DDCServerIP}");
			}
		}
	}
}
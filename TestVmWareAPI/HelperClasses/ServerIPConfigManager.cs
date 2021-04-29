using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TestVmWareAPI.HelperClasses {
	public class ServerIPConfigManager {
		private ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public ServerIPConfig IP => this.IPs.Peek();
		public Queue<ServerIPConfig> IPs { get; set; } = new Queue<ServerIPConfig>();
		public int Count => this.IPs.Count;

		public ServerIPConfigManager(string data) {
			foreach (string item in data.Split(','))
				this.IPs.Enqueue(new ServerIPConfig(item));
		}

		public void Swap() {
			ServerIPConfig tmp = this.IPs.Dequeue();
			this.IPs.Enqueue(tmp);

			this.logger.Warn($"IP Change : {tmp} -> {this.IP}");
		}
	}
}
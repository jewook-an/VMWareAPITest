using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestVmWareAPI.HelperClasses {
	public class ServerIPConfig {
		public ServerIPConfig(string connectionString) {
			string[] strings = connectionString.Split(':');
			this.Address = strings[0];
			this.Port = int.Parse(strings[1]);
		}

		public string Address { get; set; }
		public int Port { get; set; }

		public override string ToString() => $"{this.Address}:{this.Port}";
	}
}
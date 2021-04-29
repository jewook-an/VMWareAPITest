using NCCRequireService.Util.PowerShell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace vmwareapi.vmware.vcenter
{
	public class Connect
	{
		public string IPAddress { get; set; }
		public string LoginID { get; set; }
		public string LoginPassword { get; set; }
		public string Protocol { get; set; }

		public Connect(string IPAddress, string LoginID, string LoginPassword, string Protocol = "https", bool AutoLogin = true)
		{
			this.IPAddress = IPAddress;
			this.LoginID = LoginID;
			this.LoginPassword = LoginPassword;
			this.Protocol = Protocol;
		}

		public List<string> LoginCommands()
		{
			List<string> Commands = new List<string>();

			try
			{
				if (Protocol.Equals("https", StringComparison.OrdinalIgnoreCase))
				{
					Commands.Add($@"Set-PowerCLIConfiguration -InvalidCertificateAction ignore -confirm:$false");
				}

				Commands.Add($@"Connect-VIServer -Server {IPAddress} -Protocol {Protocol} -User '{LoginID}' -Password '{LoginPassword}'");
			}
			catch
			{
				throw;
			}

			return Commands;
		}
	}
}

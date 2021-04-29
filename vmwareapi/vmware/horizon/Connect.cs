using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace vmwareapi.vmware.horizon
{
	public class Connect
    {
        private readonly PowerShell pPs = null;

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

            pPs = PowerShell.Create();
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

                Commands.Add($@"$hvserver = Connect-HVServer -Server {IPAddress} -User '{LoginID}' -Password '{LoginPassword}'");
                Commands.Add($@"Connect-HVServer -Server {IPAddress} -User '{LoginID}' -Password '{LoginPassword}'");
            }
			catch
			{
				throw;
			}

			return Commands;
		}

		public List<string> CredCommands()
		{
			List<string> Commands = new List<string>();

			try
			{
				//Commands.Add($@"$hvserver = Connect-HVServer -Server {IPAddress} -User '{LoginID}' -Password '{LoginPassword}'");
				Commands.Add($@"[string][ValidateNotNullOrEmpty()] $passwd = '{LoginPassword}'");
				Commands.Add($@"$secpasswd = ConvertTo-SecureString -String $passwd -AsPlainText -Force");
				Commands.Add($@"$creds = New-Object Management.Automation.PSCredential ('{LoginID}', $secpasswd)");
				Commands.Add($@"$connectviewserver = $null");
				Commands.Add($@"$connectviewserver = Connect-HVServer -server '{IPAddress}' -Credential $creds");
				//Commands.Add($@"");
			}
			catch
			{
				throw;
			}
			return Commands;
		}

		public List<string> CenterCred()
		{
			List<string> Commands = new List<string>();

			try
			{
                Commands.Add($@"[string][ValidateNotNullOrEmpty()] $passwd = '{LoginPassword}'");
                Commands.Add($@"$secpasswd = ConvertTo-SecureString -String $passwd -AsPlainText -Force");
                Commands.Add($@"$creds = New-Object Management.Automation.PSCredential ('{LoginID}', $secpasswd)");
                Commands.Add($@"$connectcenterserver = $null");
                Commands.Add($@"$connectcenterserver = Connect-VIServer -server '{IPAddress}' -Credential $creds");


                //if (Protocol.Equals("https", StringComparison.OrdinalIgnoreCase))
                //{
                //	Commands.Add($@"Set-PowerCLIConfiguration -InvalidCertificateAction ignore -confirm:$false");
                //}

                //Commands.Add($@"Connect-VIServer -Server {IPAddress} -Protocol {Protocol} -User '{LoginID}' -Password '{LoginPassword}'");

            }
			catch
			{
				throw;
			}
			return Commands;
		}

		public string LogoutCommands()
		{
			string commend = "Disconnect-HVServer * -Force -Confirm:$false -ErrorAction SilentlyContinue";
			return commend;
		}

		public string CenterLogout()
		{
			string commend = "Disconnect-VIServer * -Force -Confirm:$false -ErrorAction SilentlyContinue";
			return commend;
		}
	}

}

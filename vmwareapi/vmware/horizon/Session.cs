using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace vmwareapi.vmware.horizon
{
	public class Session
	{
		private PowerShell pPs = null;
		Runspace runSpace = null;

		public string HorizonIP { get; set; }
		public string HorizonID { get; set; }
		public string HorizonPassword { get; set; }
		public string Protocol { get; set; }

		public Session(string HorizonIP, string HorizonID, string HorizonPassword, string Protocol = "https")
		{
			this.HorizonIP = HorizonIP;
			this.HorizonID = HorizonID;
			this.HorizonPassword = HorizonPassword;
			this.Protocol = Protocol;

			pPs = PowerShell.Create();

		}

		/// <summary>
		/// 대화형 연결설정
		/// </summary>
		/// <returns></returns>
		public List<string> SessionCommands()
		{
			List<string> Commands = new List<string>();

			try
			{
                #region runSpace > 일단주석
                //using (runSpace = System.Management.Automation.Runspaces.RunspaceFactory.CreateRunspace())
                //{
                //	runSpace.Open();
                //	using (System.Management.Automation.PowerShell pwsh = System.Management.Automation.PowerShell.Create())
                //	{
                //		pwsh.Runspace = runSpace;
                //	}
                //}
                #endregion

                //# Administraotor >> success
                //Commands.Add($@"[string][ValidateNotNullOrEmpty()] $passwd = '{HorizonPassword}'");
                //Commands.Add($@"$secpasswd = ConvertTo-SecureString -String $passwd -AsPlainText -Force");
                //Commands.Add($@"$creds = New-Object Management.Automation.PSCredential ('{HorizonID}', $secpasswd)");
                //Commands.Add($@"Enter-PSSession -ComputerName {HorizonIP} -Credential $creds");
                //Commands.Add($@"Get-Module");

                Commands.Add($@"New-PSSession -ComputerName {HorizonIP} -Credential (New-Object System.Management.Automation.PSCredential '{HorizonID}', (Convertto-SecureString -String '{HorizonPassword}' -AsPlainText -force))");
            }
			catch
			{
				throw;
			}
			return Commands;
		}

		/// <summary>
		/// 대화형 연결 Exit
		/// </summary>
		/// <returns></returns>
		public List<string> ExitSessionCommands()
		{
			List<string> Commands = new List<string>();
			try
			{
				Commands.Add($@"Exit-PSSession");
			}
			catch
			{
				throw;
			}
			return Commands;
		}

		/// <summary>
		/// 영구 연결 설정
		/// </summary>
		/// <returns></returns>
		public List<string> NewSessionCommands()
		{
			List<string> Commands = new List<string>();

			try
			{
				//# Administraotor >> success
				Commands.Add($@"[string][ValidateNotNullOrEmpty()] $passwd = '{HorizonPassword}'");
				Commands.Add($@"$secpasswd = ConvertTo-SecureString -String $passwd -AsPlainText -Force");
				Commands.Add($@"$creds = New-Object Management.Automation.PSCredential ('{HorizonID}', $secpasswd)");
				Commands.Add($@"New-PSSession -ComputerName {HorizonIP} -Credential $creds");
				//Commands.Add($@"Get-Module");
			}
			catch
			{
				throw;
			}
			return Commands;
		}
	}

}

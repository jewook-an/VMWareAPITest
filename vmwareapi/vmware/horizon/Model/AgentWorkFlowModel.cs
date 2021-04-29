using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace vmwareapi.vmware.horizon.Model
{
	public class AgentWorkFlowModel
	{
		public class run1 
		{
			public Sleep sleep;
			public CommandRun commandRun;
			public IPChange1 ipChange1;
			public HostNameChange hostNameChange;
			public StepChange stepChange;
			public Reboot reboot;
		}

		public class run2
		{
			public Sleep sleep;
			public IPChange1 ipChange1;
			public DomainJoin domainJoin;
			public AutoLoginAdd autoLoginAdd;
			public Sleep1 sleep1;
			public StepChange stepChange;
			public Reboot reboot;
		}

		public class finish
        {
			public CommandRun commandRun;
			public LocalAdminGroup localAdminGroup;
			public StepDelete stepDelete;
			public Sleep sleep;
			public StateCallBack stateCallBack;
			public Shutdown shutdown;
        }

        public class Sleep
        {
			public int Wait { get; set; }
		}
		public class Sleep1
		{
			public int Wait { get; set; }
		}

		public class CommandRun
		{
			public enum AddYN { No = 0, Yes = 1 }
			public string WorkingDir { get; set; }
			public string FullDirFileName { get; set; }
			public string Arguments { get; set; }
			public enum VerbYN { No = 0, Yes = 1 }
		}

		public class IPChange1
		{
			public int SettingYN { get; set; }
			public string IPAddress { get; set; }
			public string SubnetMask { get; set; }
			public string Gateway { get; set; }
			public string DNS1 { get; set; }
			public string DNS2 { get; set; }
		}

		public class HostNameChange
		{
			public int ChangeYN { get; set; }
            public string HostName { get; set; }
			public string DomainNetBios { get; set; }
			public string DomainAdminID { get; set; }
			public string DomainAdminPW { get; set; }
		}

		public class StepChange
        {
			public string From { get; set; }
			public string To { get; set; }
		}

		public class StepDelete
		{
			public enum RemoveYN { No = 0, Yes = 1}
		}

		public class Reboot
        {
			public int Rebooting { get; set; }
        }

		public class DomainJoin
		{
			public int AddYN { get; set; }
			public string DomainNetBios { get; set; }
			public string DomainAdminID { get; set; }
			public string DomainAdminPW { get; set; }
			public string DomainOUDC { get; set; }
		}

		public class AutoLoginAdd
		{
			public enum WinYN { No = 0, Yes = 1 }
			public enum WinVersion { Unknown = 0, WinXP = 6, Win7 = 7, Win8 = 8, Win10 = 10 }
			public string UserID { get; set; }
			public string UserPW { get; set; }
		}

		public class LocalAdminGroup
		{
			public int AddYN { get; set; }
			public string DomainUserID { get; set; }
			public string DomainNetbios { get; set; }
			public string DomainAdminID { get; set; }
			public string DomainAdminPW { get; set; }
		}


		public class StateCallBack
		{
            public int AddYN { get; set; }
			public string IPAddressPortnIPPORT { get; set; }
			public int CurrentState { get; set; }
			public int CREATE_ID { get; set; }
			public int MachineType { get; set; }
		}

        public class Shutdown
		{
            public int PowerOff { get; set; }
		}
	}
}
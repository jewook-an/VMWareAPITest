//using NCCRequireService.Util.PowerShell;
using System;
using System.Collections.Generic;
using System.Configuration;
using vmwareapi.Util.PowerShell;
using vmwareapi.vmware.horizon;
using vmwareapi.vmware.horizon.Model;
using vmwareapi.vmware.horizon.Pool;
using vmwareapi.vmware.horizon.Provisioning;
using vmwareapi.vmware.vcenter.VM.DataDef;

namespace VMProvisioningTest
{
    static class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        static void Main(string[] args)
        {
            //TEST (지정VM, IP부여, 도메인조인, 사용자할당)
            /*
            1. Connection Server 연결
            2. Enter-Session
            3. Import-Module (PowerCLI, Hv.Helper, ViewBroker)
            4. 지정 VM, IP 부여 (DNS, Sub DNS)
            5. 도메인 조인
            6. 사용자 할당 (Session확인 > 강제LogOff & Restart > 사용자할당)
            */

            //private static readonly string knoxUrl = ConfigurationManager.AppSettings["KNOX_URL"];
            string IPAddress = ConfigurationManager.AppSettings["horizonIP"];
            string LoginID = ConfigurationManager.AppSettings["horizonID"];
            string LoginPassword = ConfigurationManager.AppSettings["horizonPW"];
            string Protocol = ConfigurationManager.AppSettings["horizonProtocol"];
            string hUser = ConfigurationManager.AppSettings["horizonUser"];
            string SessionID = ConfigurationManager.AppSettings["SessionID"];
            string Domain = ConfigurationManager.AppSettings["horizonDomain"];
            string vCenterIP = ConfigurationManager.AppSettings["vCenterIP"];
            string vCenterID = ConfigurationManager.AppSettings["vCenterID"];
            string vCenterPassword = ConfigurationManager.AppSettings["vCenterPW"];

            List<string> Commands = new List<string>();

            try
            {
                #region GetPool & SetPool
                /*
                ViewPool vPool = new ViewPool(IPAddress, LoginID, LoginPassword);
                UserEntitlement userEnt = new UserEntitlement(IPAddress, LoginID, LoginPassword);
                // SET Pool 확인
                List<PoolModel> poollist = new List<PoolModel>();
                ViewResult result = null;

                //string poolName = "AutoFloat01";
                string poolName = "Win10Pro-StaticIP";
                string VM = "Win10Pro-SIP002";
                // Horizon Server All Pool Information
                //poollist = vPool.GetPool<List<PoolModel>>(poolName);
                //poollist = new ViewPool(IPAddress, LoginID, LoginPassword).GetPool<List<PoolModel>>();
                //poollist = vPool.GetPool<List<PoolModel>>(IPAddress, LoginID, LoginPassword, Protocol);
                string userName = "namurnd\\viewuser2";
                string userType = "User";
                //poollist = userEnt.PoolUserAssignment<List<PoolModel>>(userName, poolName, userType);

                // Pool User Assign
                //result = userEnt.PoolUserAssignment<ViewResult>(userName, poolName, userType);

                // VM User Assign
                //result = userEnt.SystemUserAssignment<ViewResult>(userName, VM);

                // VM User UnAssign
                result = userEnt.SystemUserUnAssignment<ViewResult>(VM);

                //ViewPool vPool2 = new ViewPool(IPAddress, LoginID, LoginPassword);
                //foreach (var item in poollist)
                //{
                //    vPool2.SetPool<PoolModel>(item.Name, true, true);
                //}
                 */
                #endregion

                #region System(VM삭제)
                /*
                ViewPool vPool = new ViewPool(IPAddress, LoginID, LoginPassword);
                ViewResult result = null;
                CreatePoolModel createPool = new CreatePoolModel();
                createPool.PoolName = "FullCloneTEST";
                createPool.PoolDisplayName = "TestPool1";
                createPool.Description = "VMtoPoolTEST";

                string removeVM = "FCTest0003";

                // Horizon 프로비저닝된 VM(리소스>시스템)은 삭제시 재생성 되므로 프로비저닝 중지
                createPool.EnableProvisioning = true;
                // Horizon Pool 사용안함 처리
                createPool.enabled = true;

                //result = vPool.RemovePoolSystem<ViewResult>(createPool, removeVM);
                result = vPool.ResetPoolSystem<ViewResult>(removeVM);
                */

                #endregion

                #region Menual Dedicate 풀

                /*
                // Menual Dedicate 풀 생성 (vCenter내 VM 을 가지고 풀 만들어 사용자 지정)
                ViewPool vPool = new ViewPool(IPAddress, LoginID, LoginPassword);

                // 선처리 (요청VM Name Count) (ex : PoolTest1, PoolTest2)
                string[] vmlist = { "PoolTest1", "PoolTest2" };

                // 1. 풀생성을 위한 요청VM정보를 Count 
                int PoolCnt = vmlist.Count();

                // Create Manual Dedicate Clone Pool
                CreatePoolModel createPool = new CreatePoolModel();
                createPool.PoolName = "ProvTestPool1";
                createPool.PoolDisplayName = "TestPool1";
                createPool.Description = "VMtoPoolTEST";

                string[] poolCreateVM = new string[PoolCnt];
                for (int i = 0; i < PoolCnt; i++)
                {
                    poolCreateVM[i] = vmlist[i];
                }

                createPool.VM = poolCreateVM;
                createPool.enableHTMLAccess = true;

                createPool.PoolType = "Manual";
                createPool.UserAssignment = "DEDICATED";        // Dedi or Floating

                vPool.NewPool<CreatePoolModel>(createPool);
                */

                #endregion

                #region LinkedClone
                /*
                ViewPool vPool = new ViewPool(IPAddress, LoginID, LoginPassword);
                CreatePoolModel createPool = new CreatePoolModel();
                */
                #region LinkedClone Paremeter
                //New-HVPool -LinkedClone 
                //-PoolName                       'vmwarepool' 
                //-UserAssignment                 FLOATING 
                //-ParentVM                       'Agent_vmware' 
                //-SnapshotVM                     'kb-hotfix' 
                //-VmFolder                       'vmware' 
                //-HostOrCluster                  'CS-1' 
                //-ResourcePool                   'CS-1' 
                //-Datastores                     'datastore1' 
                //-NamingMethod                   PATTERN 
                //-PoolDisplayName                'vmware linkedclone pool' 
                //-Description                    'created linkedclone pool from ps' 
                //-EnableProvisioning             $true 
                //-StopProvisioningOnError        $false 
                //-NamingPattern                  "vmware2" 
                //-MinReady                       0 
                //-MaximumCount                   1 
                //-SpareCount                     1 
                //-ProvisioningTime               UP_FRONT 
                //-SysPrepName                    vmwarecust 
                //-CustType                       SYS_PREP 
                //-NetBiosName                    adviewdev 
                //-DomainAdmin                    root
                #endregion

                #region DataStore 경고
                //데스크톱 풀이 단일 ESXi 호스트 또는 단일 ESXi 호스트가 포함된 클러스터에 구성될 경우 이 사항은 무시하십시오.
                //그러한 경우에는 연결된 클론을 제약 없이 로컬 데이터스토어에 저장할 수 있습니다.
                //또 다른 경우, 연결된 클론을 로컬 데이터스토어에 저장하면 다음과 같은 제약이 발생합니다.
                //1) VMotion, VMware High Availability 및 vSphere DRS(Distributed Resource Scheduler)가 지원되지 않습니다.
                //2) 복제가 로컬 데이터스토어에 있는 경우, View Composer 복제와 연결된 클론을 서로 다른 데이터스토어에 저장할 수 없습니다.
                //3) 호스트가 여러 개인 ESXi 클러스터의 경우, 한 호스트에 연결된 로컬 데이터스토어는 기본적으로 해당 클러스터의 다른 호스트에서 액세스할 수 없습니다. 
                //데이터 동기화를 사용하기 위한 다른 메커니즘이 없는 상태에서, 다중 호스트 클러스터에 있는 로컬 데이터스토어에 복제본, 연결된 클론 또는 영구 디스크를 저장하면 
                //View Composer 작업(영구 디스크 프로비저닝, 재구성, 재조정 또는 관리)이 실패할 수 있습니다.
                #endregion
                /*
                createPool.PoolType = "LinkedClone";

                #region BASE Param
                createPool.PoolName = "LinkedCloneTEST";
                createPool.UserAssignment = "FLOATING";        // "DEDICATED" or "FLOATING"
                createPool.PoolDisplayName = "PoolDisplayName";
                createPool.Description = "Description";
                createPool.enableHTMLAccess = true;
                #endregion

                createPool.ParentVM = "Windows10Pro-x64";
                createPool.SnapshotVM = "Win10Pro_Snap1";
                createPool.VmFolder = "/Datacenter/vm/TestVM";
                createPool.HostOrCluster = "/Datacenter/host/NM_VMWC";
                createPool.ResourcePool = "/Datacenter/host/NM_VMWC/Resources";

                string[] dslist = { "DataStore15", "DataStore16" };
                string[] datastores = new string[2];
                for (int i = 0; i < datastores.Length; i++)
                {
                    datastores[i] = dslist[i];
                }
                createPool.Datastores = datastores;

                createPool.NamingMethod = "PATTERN";
                createPool.EnableProvisioning = true;
                createPool.StopProvisioningOnError = true;
                createPool.NamingPattern = "LCTest{n:fixed=4}";
                createPool.MinReady = 0;                        // default : 0
                createPool.MaximumCount = 1;                    // default : 1
                createPool.SpareCount = 1;                      // default : 1
                createPool.ProvisioningTime = "UP_FRONT";       // 프로비저닝 시기 결정(ON_DEMAND / UP_FRONT)
                createPool.SysPrepName = "";
                createPool.CustType = "QUICK_PREP";             // 사용자정의유형 ('CLONE_PREP','QUICK_PREP','SYS_PREP','NONE')
                createPool.NetBiosName = "NAMURND";
                createPool.DomainAdmin = "administrator";

                // 1차 Error Message : Failed to create Pool with error: No parent VM found with Name: [/Datacenter/vm/Windows10Pro-x64]
                // >> 수정1. /Datacenter/vm/Windows10Pro-x64 >> Windows10Pro-x64 / 수정2. /Win10Pro_Snap1 > Win10Pro_Snap1
                // 2차 Error Message : Redirect Windows profile must be false for linked clone floating desktops
                // LinkedClone FLOATING >> FRedirectWindowsProfile >> False 이어야 한다.
                createPool.RedirectWindowsProfile = false;

                vPool.NewPool<CreatePoolModel>(createPool);
                */
                #endregion

                #region FullClone
                //ViewPool vPool = new ViewPool(IPAddress, LoginID, LoginPassword);
                //CreatePoolModel createPool = new CreatePoolModel();
                /*
                // New-HVPool -FullClone
                -PoolName           "FullClone"
                -PoolDisplayName    "FullClonePra"
                -Description        "create full clone"
                -UserAssignment     DEDICATED
                -Template           'powerCLI-VM-TEMPLATE'
                -VmFolder           'vmware'
                -HostOrCluster      'CS-1'
                -ResourcePool       'CS-1'
                -Datastores         'datastore1'
                -NamingMethod       PATTERN
                -NamingPattern      'FullCln1'
                -SysPrepName        vmwarecust
                -CustType           SYS_PREP
                -NetBiosName        adviewdev
                -DomainAdmin        root
                */
                /*
                createPool.PoolType = "FullClone";

                #region BASE Param
                createPool.PoolName = "FullCloneTEST";
                createPool.PoolDisplayName = "PoolDisplayName";
                createPool.Description = "Description";
                createPool.UserAssignment = "DEDICATED";             // "DEDICATED" or "FLOATING"
                createPool.enableHTMLAccess = true;
                #endregion

                createPool.Template = "Windows 10 Home";
                createPool.VmFolder = "/Datacenter/vm/TestVM";
                createPool.HostOrCluster = "/Datacenter/host/NM_VMWC";
                createPool.ResourcePool = "/Datacenter/host/NM_VMWC/Resources";
                //createPool.SnapshotVM = "Win10Pro_Snap1";

                string[] dslist = { "DataStore15", "DataStore16" };
                string[] datastores = new string[2];
                for (int i = 0; i < datastores.Length; i++)
                {
                    datastores[i] = dslist[i];
                }
                createPool.Datastores = datastores;

                createPool.NamingMethod = "PATTERN";
                createPool.NamingPattern = "FCTest{n:fixed=4}";
                createPool.SysPrepName = "";
                createPool.CustType = "NONE";             // 사용자정의유형 ('CLONE_PREP','QUICK_PREP'(X),'SYS_PREP','NONE')
                createPool.NetBiosName = "NAMURND";
                createPool.DomainAdmin = "administrator";

                //createPool.EnableProvisioning = true;
                //createPool.StopProvisioningOnError = true;
                //createPool.MinReady = 0;                        // default : 0
                //createPool.MaximumCount = 1;                    // default : 1
                //createPool.SpareCount = 1;                      // default : 1
                //createPool.ProvisioningTime = "UP_FRONT";       // 프로비저닝 시기 결정(ON_DEMAND / UP_FRONT)

                vPool.NewPool<CreatePoolModel>(createPool);
                */
                #endregion

                #region AgentSetting
                SystemSetting vmset = new SystemSetting(IPAddress, LoginID, LoginPassword);
                SystemSetting vmset2 = new SystemSetting(vCenterIP, vCenterID, vCenterPassword);
                //string ProvisioningType = "";
                string DomainNetbios = "namurnd";
                string DomainFQDN = "namurnd.io";
                string DomainAdminID = LoginID;
                string DomainAdminPW = LoginPassword;
                string Domain_User_ID = hUser;
                string DomainOUDC = "OU=Computer,DC=namurnd,DC=io";
                
                string SetIPAddress = "192.168.101.222";
                string SubnetMask = "255.255.255.0";
                string GateWay = "192.168.101.1";
                string DNS1 = "192.168.50.2";
                string DNS2 = "192.168.50.3";
                string StateCallBack_IPAddressPort_IPPORT = "192.168.1.195,9081/192.168.1.96,9081";
                int StateCallBack_CREATE_ID = 160;

                string VMName = "LCTest002"; 
                //bool createOnly = false;

                string ReturnValue = string.Empty;
                string JsonText = string.Empty;

                //int AD_SYNC_WAIT_RUN2_SECONDS = 0;
                //int AD_SYNC_WAIT_FINISH_SECONDS = 0;
                int AD_SYNC_WAIT_FINISH_SECONDS = 5;

                bool rtnValue = vmset.SetAgentDataNew_VMWare(DomainNetbios, DomainFQDN, DomainAdminID, DomainAdminPW, Domain_User_ID, DomainOUDC, SetIPAddress, SubnetMask, GateWay, DNS1, DNS2, StateCallBack_IPAddressPort_IPPORT, StateCallBack_CREATE_ID, VMName, AD_SYNC_WAIT_FINISH_SECONDS);

                ViewResult result = null;

                if (rtnValue)
                {
                    //SetCopyVMGuestFile copyFile = new SetCopyVMGuestFile();
                    VMGuestFileModel copyFile = new VMGuestFileModel();
                    copyFile.Source = string.Format("D:\\Temp\\{0}.json", VMName);
                    copyFile.Destination = "${Env:ProgramFiles(x86)}\\Namutech\\ProvisioningAgent\\AgentWorkFlow.json";
                    copyFile.GuestUser = "Admin";
                    copyFile.GuestPassword = "P@ssw0rd";
                    copyFile.VMName = VMName;

                    result = vmset2.FileCopyToVM<ViewResult>(copyFile);
                    //FileCopyToVM

                    //new vmwareapi.vmware.vcenter.VM.VMManager(vCenterIP, vCenterID, vCenterPassword).SetCopyGuestFile(copyFile);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.Connection, "VMWare.HorizonView.Connection Exception", ex);
            }

            #region 참고 주석 : PSSession, ViewBroker

            /*
            // Enter-PSSession
            Session sess = new Session(IPAddress, SessionID, LoginPassword, Protocol);
            Commands.Clear();
            Commands = sess.NewSessionCommands();
            //Commands = sess.SessionCommands();
            //Commands = sess.ExitSessionCommands();

            Collection<PSObject> result2 = null;
            Collection<PSObject> result3 = null;

            try
            {
                using (runSpace2 = RunspaceFactory.CreateRunspace())
                {
                    runSpace2.Open();
                    using (PowerShell pPs2 = PowerShell.Create())
                    {
                        pPs2.Runspace = runSpace2;
                        foreach (var item in Commands)
                        {
                            pPs2.AddScript(item);
                            //result2 = pPs2.Invoke();
                        }

                        result2 = pPs2.Invoke();
                        if (result2.Count == 0 || (result2.Count > 0 && result2[0] == null))
                        {
                            Trace.WriteLine("[Session] HorizonView.EnterSession Failed");
                            throw new CliException(CliException.Reason.Session);
                        }

                        ViewBroker broker = new ViewBroker(IPAddress, hUser, LoginPassword, Domain);
                        Commands.Clear();
                        Commands = broker.BrokerCommands();

                        foreach (var item in Commands)
                        {
                            pPs2.AddScript(item);
                            result3 = pPs2.Invoke();
                            if (result3.Count == 0 || (result3.Count > 0 && result3[0] == null))
                            {
                                Trace.WriteLine("[Broker] HorizonView.ViewBroker Failed");
                                throw new CliException(CliException.Reason.ViewBroker);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.Session, "Horizon EnterSession Exception", ex);
            }

            ViewBroker broker = new ViewBroker(IPAddress, hUser, LoginPassword, Domain);
            Commands.Clear();
            Commands = broker.BrokerCommands();

            Collection<PSObject> result3 = null;

            try
            {
                using (runSpace3 = RunspaceFactory.CreateRunspace())
                {
                    runSpace3.Open();
                    using (PowerShell pPs3 = PowerShell.Create())
                    {
                        foreach (var item in Commands)
                        {
                            pPs3.Runspace = runSpace3;
                            pPs3.AddScript(item);
                            result3 = pPs3.Invoke();
                            if (result3.Count == 0 || (result3.Count > 0 && result3[0] == null))
                            {
                                Trace.WriteLine("[Broker] HorizonView.ViewBroker Failed");
                                throw new CliException(CliException.Reason.ViewBroker);
                            }
                        }
                        broker.BrokerSnapIn();
                    }
                }

                //Commands.Clear();
                // ViewBroker Snapin
            }
            catch (Exception)
            {

                throw;
            }
             */
            #endregion
        }
    }
}

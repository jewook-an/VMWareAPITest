using NCCRequireService.Util.PowerShell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using vmwareapi.Util.PowerShell;
using vmwareapi.vmware.horizon.Model;

namespace vmwareapi.vmware.horizon.Pool
{
    public class VeiwPoolGuideAdd : Connect
    {
        //private PowerShell pPs = null;
        Runspace runSpace = null;
        List<string> Commands = new List<string>();

        public VeiwPoolGuideAdd(string IPAddress, string LoginID, string LoginPassword, string Protocol = "https", bool AutoLogin = true) : base(IPAddress, LoginID, LoginPassword, Protocol = "https", AutoLogin = true)
        {
            if (AutoLogin)
            {
                Commands.Clear();
                Commands = LoginCommands();
            }
        }

        /// <summary>
        /// DeskTop Pool Summary 정보확인
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetPool<T>(string PoolName)
        {
            Collection<PSObject> result = null;

            T retValue = default(T);

            // Horizon Connect
            //Connect conn = new Connect(IPAddress, LoginID, LoginPassword, Protocol);
            //Commands.Clear();
            //Commands = conn.LoginCommands();
            //Commands.Add($@"Set-PowerCLIConfiguration -InvalidCertificateAction ignore -confirm:$false");
            //Commands.Add($@"Connect-HVServer -Server {IPAddress} -User '{LoginID}' -Password '{LoginPassword}'");

            try
            {
                using (runSpace = RunspaceFactory.CreateRunspace())
                {
                    runSpace.Open();
                    using (PowerShell pPs = PowerShell.Create())
                    {
                        pPs.Runspace = runSpace;
                        foreach (var item in Commands)
                        {
                            pPs.AddScript(item);
                        }
                        result = pPs.Invoke();
                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[Connect] HorizonView.Connection Failed");
                            throw new CliException(CliException.Reason.Connection);
                        }

                        //-PoolName(풀이름) -PoolType(MANUAL/AUTOMATED/RDS) -UserAssignment(FLOATING/DEDICATED)
                        string script = string.Format("(Get-HVPoolSummary).DesktopSummaryData | where {{$_.Name -eq '{0}'}}", PoolName);
                        Commands.Clear();
                        Commands.Add(script);

                        foreach (var item in Commands)
                        {
                            pPs.AddScript(item);
                        }
                        result = pPs.Invoke();

                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[Connect] HorizonView.GetPool Failed");
                            throw new CliException(CliException.Reason.GetPool);
                        }

                        retValue = GetResult<T>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.GetPool, "VMWare.HorizonView.PoolInfo Exception", ex);
            }

            return retValue;
        }

        /// <summary>
        /// DeskTop Pool 사용,미사용 프로비저닝사용, 비사용 Update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="poolname"></param>
        /// <param name="pooluse"></param>
        /// <param name="provisioning"></param>
        /// <returns></returns>
        public T SetPool<T>(string poolname, bool pooluse, bool provisioning)
        {
            Collection<PSObject> result = null;

            T retValue = default(T);

            try
            {
                using (runSpace = RunspaceFactory.CreateRunspace())
                {
                    runSpace.Open();
                    using (PowerShell pPs = PowerShell.Create())
                    {
                        pPs.Runspace = runSpace;
                        foreach (var item in Commands)
                        {
                            pPs.AddScript(item);
                        }

                        //ViewResult result = new ViewResult();
                        string SetPoolOption = string.Empty;
                        SetPoolOption += pooluse == true ? " -Enable" : " -Disable";
                        SetPoolOption += provisioning == true ? " -Start" : " -Stop";

                        string[] scripts = new string[3];
                        scripts[0] = "$retSetPool = \"\" | select ErrorDescription";
                        scripts[1] = string.Format("try{{Set-HVPool -PoolName \"{0}\" {1} -ErrorAction Stop}}catch{{$retSetPool=$_.Exception.Message}}", poolname, SetPoolOption);
                        scripts[2] = "$retSetPool";

                        for (int i = 0; i < scripts.Length; i++)
                        {
                            pPs.AddScript(scripts[i]);
                        }
                        // 정상시 Return {@ErrorDescription=}
                        result = pPs.Invoke();

                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[Connect] HorizonView.GetPool Failed");
                            throw new CliException(CliException.Reason.SetPool);
                        }
                        retValue = GetResult<T>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.GetPool, "VMWare.HorizonView.PoolInfo Exception", ex);
            }
            return retValue;
        }

        /// <summary>
        /// DeskTop Pool Create
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="createPool"></param>
        /// <returns></returns>
        public T NewPool<T>(CreatePoolModel createPool)
        {
            Collection<PSObject> result = null;
            T retValue = default(T);

            // 1. 유형 (자동, 수동, RDS)
            // 2. 자동(전용[자동할당], 부동) / 수동(전용[자동할당], 부동) / RDS
            // 3-1. 자동 > 인스턴트클론, ViewComposer, 전체가상시스템
            // 3-2. 수동 > vCenter가상시스템, 기타소스
            // 3-3. RDS > Skip
            switch (createPool.PoolType)
            {
                case "InstantClone":
                    retValue = CreateInstantClonePool<T>(createPool);
                    break;
                case "LinkedClone":
                    retValue = CreateLinkedClonePool<T>(createPool);
                    break;
                case "FullClone":
                    retValue = CreateFullClonePool<T>(createPool);
                    break;
                case "Manual":
                    retValue = CreateManualPool<T>(createPool);
                    break;
                case "RDS":
                    CreateRDSPool(createPool);
                    break;
                default:
                    break;
            }

            return retValue;
        }

        /// <summary>
        /// InstantClone Create
        /// </summary>
        /// <param name="createPool"></param>
        private T CreateInstantClonePool<T>(CreatePoolModel createPool)
        {
            Collection<PSObject> result = null;
            T retValue = default(T);

            /*
            // New-HVPool -InstantClone -PoolName "InsPoolvmware" -PoolDisplayName "insPool" -Description "create instant pool" -UserAssignment FLOATING -ParentVM 'Agent_vmware' -SnapshotVM 'kb-hotfix' -VmFolder 'vmware' -HostOrCluster  'CS-1' -ResourcePool 'CS-1' -NamingMethod PATTERN -Datastores 'datastore1' -NamingPattern "inspool2" -NetBiosName 'adviewdev' -DomainAdmin root
            */
            try
            {
                using (runSpace = RunspaceFactory.CreateRunspace())
                {
                    runSpace.Open();
                    using (PowerShell pPs = PowerShell.Create())
                    {
                        pPs.Runspace = runSpace;
                        foreach (var item in Commands)
                        {
                            pPs.AddScript(item);
                        }
                        result = pPs.Invoke();
                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[Connect] HorizonView.Connection Failed");
                            throw new CliException(CliException.Reason.Connection);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.NewPool, "VMWare.HorizonView.InstantClone Exception", ex);
            }

            #region 권한부여
            // ###############################################################################
            // 사용자 권한 부여 추가 (Dedicate 일시)
            // ###############################################################################
            if (createPool.UserAssignment == "DEDICATED")
            {
                // New-HVEntitlement -User domain\username -ResourceName $pool -Type User -HvServer $vcs
                // New-HVEntitlement -User arun@arundev.me -ResourceName mypool
                // New-HVEntitlement -User domain\group -ResourceName 'poolname' -Type Group
            }
            #endregion
            return retValue;
        }

        /// <summary>
        /// Linked Clone Create (createPool.)
        /// </summary>
        /// <param name="createPool"></param>
        private T CreateLinkedClonePool<T>(CreatePoolModel createPool)
        {
            Collection<PSObject> result = null;
            T retValue = default(T);

            /*
            New-HVPool -LinkedClone -PoolName 'vmwarepool' -UserAssignment FLOATING -ParentVM 'Agent_vmware' -SnapshotVM 'kb-hotfix' -VmFolder 'vmware' -HostOrCluster 'CS-1' -ResourcePool 'CS-1' -Datastores 'datastore1' -NamingMethod PATTERN -PoolDisplayName 'vmware linkedclone pool' -Description  'created linkedclone pool from ps' -EnableProvisioning $true -StopProvisioningOnError $false -NamingPattern  "vmware2" -MinReady 0 -MaximumCount 1 -SpareCount 1 -ProvisioningTime UP_FRONT -SysPrepName vmwarecust -CustType SYS_PREP -NetBiosName adviewdev -DomainAdmin root
            */
            try
            {
                using (runSpace = RunspaceFactory.CreateRunspace())
                {
                    runSpace.Open();
                    using (PowerShell pPs = PowerShell.Create())
                    {
                        pPs.Runspace = runSpace;
                        foreach (var item in Commands)
                        {
                            pPs.AddScript(item);
                        }
                        result = pPs.Invoke();
                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[Connect] HorizonView.Connection Failed");
                            throw new CliException(CliException.Reason.Connection);
                        }

                        string datastores = string.Empty;
                        for (int i = 0; i < createPool.Datastores.Length; i++)
                        {
                            datastores += i == 0 ? "'" + createPool.Datastores[i] + "'" : " ,'" + createPool.Datastores[i] + "'";
                        }


                        StringBuilder sb = new StringBuilder();
                        // Try&Catch로 진행 >> Exception확인 // Debugging시 Invoke Result확인
                        sb.AppendFormat(@"try {{New-HVPool -LinkedClone -PoolName ""{0}"" -PoolDisplayName ""{1}"" -Description ""{2}"" -UserAssignment {3} -ParentVM ""{4}"" -SnapshotVM ""{5}"" -VmFolder ""{6}"" -HostOrCluster ""{7}"" -ResourcePool ""{8}"" -Datastores {9} -NamingMethod ""{10}"" -NamingPattern ""{11}"" ", createPool.PoolName, createPool.PoolDisplayName, createPool.Description, createPool.UserAssignment, createPool.ParentVM, createPool.SnapshotVM, createPool.VmFolder, createPool.HostOrCluster, createPool.ResourcePool, datastores, createPool.NamingMethod, createPool.NamingPattern);

                        //-EnableProvisioning $true             // 프로비저닝 사용
                        //-StopProvisioningOnError $false       // 오류시 프로비저닝 중지
                        //-RedirectWindowsProfile               // LinkedClone Floating >> false (기본값:true)
                        sb.AppendFormat(@" -EnableProvisioning ${0} -StopProvisioningOnError ${1} -RedirectWindowsProfile ${2}", createPool.EnableProvisioning, createPool.StopProvisioningOnError, createPool.RedirectWindowsProfile);

                        //-MinReady 0                           // View Composer 유지보수작업에 준비된(프로비저닝된) 시스템 최소수. (default 0) // LinkedClone
                        //-MaximumCount 1                       // 풀에있는 최대 시스템 수(default 1) - Full,Linked,Instant Clone
                        //-SpareCount 1                         // 풀에서 예비 전원이 켜진 시스템 수.(default 1)
                        sb.AppendFormat(@" -MinReady {0} -MaximumCount {1} -SpareCount {2}", createPool.MinReady, createPool.MaximumCount, createPool.SpareCount);

                        //-ProvisioningTime UP_FRONT            // 프로비저닝 시기 결정 (ON_DEMAND/UP_FRONT)
                        //-SysPrepName vmwarecust               // 사용할 사용자정의 스펙 - Full,Linked Clone
                        //-CustType SYS_PREP                    // 사용할 사용자정의 유형('CLONE_PREP', 'QUICK_PREP', 'SYS_PREP', 'NONE') - Full,Linked Clone
                        //-NetBiosName namurnd                  // 도메인 NetBios Name - Full,Linked,Instant Clone
                        //-DomainAdmin root                     // 도메인 가입시 도메인 관리자 이름 - Full,Linked,Instant Clone
                        sb.AppendFormat(@" -ProvisioningTime {0} -SysPrepName ""{1}"" -CustType ""{2}"" -NetBiosName ""{3}"" -DomainAdmin ""{4}""", createPool.ProvisioningTime, createPool.SysPrepName, createPool.CustType, createPool.NetBiosName, createPool.DomainAdmin);
                        sb.AppendFormat(@" -ErrorAction Stop}}catch{{$retLinkedClonePool.ErrorDescription=$_.Exception.Message}}");

                        string[] scripts = new string[3];
                        scripts[0] = "$retLinkedClonePool=\"\" | select ErrorDescription";
                        scripts[1] = sb.ToString();
                        Trace.WriteLine(scripts[1]);
                        scripts[2] = "$retLinkedClonePool";

                        for (int i = 0; i < scripts.Length; i++)
                        {
                            pPs.AddScript(scripts[i]);
                        }
                        // Success Return Value : {@ErrorDescription=}
                        result = pPs.Invoke();

                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[CreatePool] HorizonView.CreateLinkedClonePool Failed");
                            throw new CliException(CliException.Reason.NewPool);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.NewPool, "VMWare.HorizonView.LinkedClone Exception", ex);
            }

            #region 권한부여
            // ###############################################################################
            // 사용자 권한 부여 추가 (Dedicate 일시)
            // ###############################################################################
            if (createPool.UserAssignment == "DEDICATED")
            {
                // New-HVEntitlement -User domain\username -ResourceName $pool -Type User -HvServer $vcs
                // New-HVEntitlement -User arun@arundev.me -ResourceName mypool
                // New-HVEntitlement -User domain\group -ResourceName 'poolname' -Type Group
            }
            #endregion
            return retValue;
        }

        /// <summary>
        /// Full Clone Create
        /// </summary>
        /// <param name="createPool"></param>
        private T CreateFullClonePool<T>(CreatePoolModel createPool)
        {
            Collection<PSObject> result = null;
            T retValue = default(T);

            #region FullClone 기본 Process
            // # (AD 서버에 DHCP 서버구성후) DHCP > 일반 권장 구성 사항 >> 실 환경에서 고정 IP를 사용해야 하면 배포시 1. 사용자 지정규격 항목 직접 IP 설정, 2. 부팅 스크립트 설정을 통해 고정 IP를 사용
            // # OS 설치, VM Tools 설치, OS Optimizing, 기본 Profile 설정, Util 설치, Hostname 변경, 도메인 가입, View Agent 설치 작업이 완료된 Master Image를 템플릿 변환 준비
            // 01. 작업시작 Log
            // 02. 작업 정보 조회
            // 03. 풀 정보 조회
            // 04. 사용자 확인
            // 05. vCenter 연결 확인
            // 06. 네트워크 대역대 확인
            // 07. 미 할당된 VM
            // 08. VM 이름 생성 및 예약
            // 09. VM 폴더 생성
            // 10. VM 복제
            // 11. VM 기본정보
            // 12. VM 전원 On

            // Naming Method Pattern 사용 FullClone 풀 생성 (DEDICATED)
            // New-HVPool -FullClone -PoolName "FullClone" -PoolDisplayName "FullClonePra" -Description "create full clone" -UserAssignment DEDICATED -Template 'powerCLI-VM-TEMPLATE' -VmFolder 'vmware' -HostOrCluster 'CS-1' -ResourcePool 'CS-1' -Datastores 'datastore1' -NamingMethod PATTERN -NamingPattern 'FullCln1' -SysPrepName vmwarecust -CustType SYS_PREP -NetBiosName adviewdev -DomainAdmin root
            #endregion

            try
            {
                using (runSpace = RunspaceFactory.CreateRunspace())
                {
                    runSpace.Open();
                    using (PowerShell pPs = PowerShell.Create())
                    {
                        pPs.Runspace = runSpace;
                        foreach (var item in Commands)
                        {
                            pPs.AddScript(item);
                        }
                        result = pPs.Invoke();
                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[Connect] HorizonView.Connection Failed");
                            throw new CliException(CliException.Reason.Connection);
                        }

                        string datastores = string.Empty;
                        for (int i = 0; i < createPool.Datastores.Length; i++)
                        {
                            datastores += i == 0 ? "'" + createPool.Datastores[i] + "'" : " ,'" + createPool.Datastores[i] + "'";
                        }

                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat(@"try {{New-HVPool -FullClone -PoolName ""{0}"" -PoolDisplayName ""{1}"" -Description ""{2}"" -UserAssignment {3} -Template ""{4}"" -VmFolder ""{5}"" -HostOrCluster ""{6}"" -ResourcePool ""{7}"" -Datastores {8} -NamingMethod ""{9}"" -NamingPattern ""{10}"" ", createPool.PoolName, createPool.PoolDisplayName, createPool.Description, createPool.UserAssignment, createPool.Template, createPool.VmFolder, createPool.HostOrCluster, createPool.ResourcePool, datastores, createPool.NamingMethod, createPool.NamingPattern);

                        //-SysPrepName vmwarecust               // 사용할 사용자정의 스펙 - Full,Linked Clone
                        //-CustType SYS_PREP                    // 사용할 사용자정의 유형('CLONE_PREP', 'QUICK_PREP', 'SYS_PREP', 'NONE') - Full,Linked Clone
                        //-NetBiosName namurnd                  // 도메인 NetBios Name - Full,Linked,Instant Clone
                        // ***** 도메인 관리자 이름을 넣었을때 Horizon 오류남...ㅜㅜ *****
                        // New-HVPool : 지정한 명명된 매개 변수를 사용하여 매개 변수 집합을 확인할 수 없습니다.
                        // ***********************************************************
                        sb.AppendFormat(@" -SysPrepName ""{0}"" -CustType ""{1}"" -NetBiosName ""{2}"" ", createPool.SysPrepName, createPool.CustType, createPool.NetBiosName);
                        sb.AppendFormat(@" -ErrorAction Stop}}catch{{$retFullClonePool.ErrorDescription=$_.Exception.Message}}");

                        string[] scripts = new string[3];
                        scripts[0] = "$retFullClonePool=\"\" | select ErrorDescription";
                        scripts[1] = sb.ToString();
                        Trace.WriteLine(scripts[1]);
                        scripts[2] = "$retFullClonePool";

                        for (int i = 0; i < scripts.Length; i++)
                        {
                            pPs.AddScript(scripts[i]);
                        }
                        // Success Return Value : {@ErrorDescription=}
                        result = pPs.Invoke();

                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[CreatePool] HorizonView.CreateFullClonePool Failed");
                            throw new CliException(CliException.Reason.NewPool);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.NewPool, "VMWare.HorizonView.FullClone Exception", ex);
            }

            #region 권한부여
            // ###############################################################################
            // 사용자 권한 부여 추가 (Dedicate 일시)
            // ###############################################################################
            if (createPool.UserAssignment == "DEDICATED")
            {
                // New-HVEntitlement -User domain\username -ResourceName $pool -Type User -HvServer $vcs
                // New-HVEntitlement -User arun@arundev.me -ResourceName mypool
                // New-HVEntitlement -User domain\group -ResourceName 'poolname' -Type Group
            }
            #endregion
            return retValue;
        }

        /// <summary>
        /// 수동풀 전용 Create (vCenter VM선택 후 사용자지정)
        /// </summary>
        /// <param name="createPool"></param>
        private T CreateManualPool<T>(CreatePoolModel createPool)
        {
            Collection<PSObject> result = null;
            T retValue = default(T);
            try
            {
                using (runSpace = RunspaceFactory.CreateRunspace())
                {
                    runSpace.Open();
                    using (PowerShell pPs = PowerShell.Create())
                    {
                        pPs.Runspace = runSpace;
                        foreach (var item in Commands)
                        {
                            pPs.AddScript(item);
                        }
                        result = pPs.Invoke();
                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[Connect] HorizonView.Connection Failed");
                            throw new CliException(CliException.Reason.Connection);
                        }

                        string poolvm = string.Empty;
                        for (int i = 0; i < createPool.VM.Length; i++)
                        {
                            poolvm += i == 0 ? "'" + createPool.VM[i] + "'" : " ,'" + createPool.VM[i] + "'";
                        }

                        StringBuilder sb = new StringBuilder();
                        // vCenter내 VM > Managed 수동풀 Floating (사용O) >> 풀생성 완료후 권한 부여
                        // Try&Catch로 진행 >> Exception확인 // Debugging시 Invoke Result확인
                        sb.AppendFormat(@"try {{New-HVPool -MANUAL -PoolName ""{0}"" -PoolDisplayName ""{1}"" -Description ""{2}"" -UserAssignment {3} -Source VIRTUAL_CENTER -VM {4} -enableHTMLAccess ${5}}}catch{{$retNewManualPool.ErrorDescription=$_.Exception.Message}}", createPool.PoolName, createPool.PoolDisplayName, createPool.Description, createPool.UserAssignment, poolvm, createPool.enableHTMLAccess);

                        #region Horizon v7.1
                        // New-HVPool -Manual -PoolName <String> [-PoolDisplayName <String>] [-Description <String>] [-AccessGroup <String>] [-GlobalEntitlement <String>] -UserAssignment <String> [-AutomaticAssignment <Boolean>] [-Enable <Boolean>] [-ConnectionServerRestrictions <String[]>] [-allowUsersToResetMachines <Boolean>] [-supportedDisplayProtocols <String[]>] [-defaultDisplayProtocol <String>] [-allowUsersToChooseProtocol <Int32>] [-enableHTMLAccess <Boolean>] [-Quality <String>] [-Throttling <String>] [-Vcenter <String>] [-TransparentPageSharingScope <String>] -Source <String> -VM <String[]> [-HvServer <Object>] [-WhatIf] [-Confirm] [<CommonParameters>]
                        // -Source(수동풀VM 소스) : 지원되는값 ('VIRTUAL_CENTER' : vCenter관리 VM) or ('UNMANAGED' : 물리적 시스템,  VM >> vCenter관리 VM XX)
                        #endregion
                        #region Horizon v7.7
                        //sb.AppendFormat(@"try {{Add-ManualPool -Pool_id ""{0}"" -Description ""{1}"" -DisplayName ""{2}"" -PowerPolicy ""{3}"" -Vc_name ""{4}"" -Id (Get-DesktopVM -name {5}).id -IsUserResetAllowed ${6} -AutoLogoffTime ""{7}"" -Persistence Persistent -FolderId / -DefaultProtocol ""{8}"" -AllowProtocolOverride ${9} -FlashQuality ""{10}"" -FlashThrottling ""{11}"" -ErrorAction Stop}}catch{{$retAddManualPool.ErrorDescription=$_.Exception.Message}}",poolIdentity, description, displayName, powerPolicy, vcenterserver, vmName, isUserResetAllowed, autoLogOffTime, defaultProtocol, allowedPorocolOverride, flashQuality, flashThrottling);
                        #endregion

                        string[] scripts = new string[3];
                        scripts[0] = "$retNewManualPool=\"\" | select ErrorDescription";
                        scripts[1] = sb.ToString();
                        Trace.WriteLine(scripts[1]);
                        scripts[2] = "$retNewManualPool";

                        for (int i = 0; i < scripts.Length; i++)
                        {
                            pPs.AddScript(scripts[i]);
                        }
                        // Success Return Value : {@ErrorDescription=}
                        result = pPs.Invoke();

                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[CreatePool] HorizonView.CreateManualPool Failed");
                            throw new CliException(CliException.Reason.NewPool);
                        }
                        #region 권한부여
                        // ###############################################################################
                        // 사용자 권한 부여 추가 (Dedicate 일시)
                        // ###############################################################################
                        if (createPool.UserAssignment == "DEDICATED")
                        {
                            // New-HVEntitlement -User domain\username -ResourceName $pool -Type User -HvServer $vcs
                            // New-HVEntitlement -User arun@arundev.me -ResourceName mypool
                            // New-HVEntitlement -User domain\group -ResourceName 'poolname' -Type Group
                        }
                        #endregion
                        retValue = GetResult<T>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.NewPool, "VMWare.HorizonView.ManualPool Exception", ex);
            }

            return retValue;
        }

        /// <summary>
        /// RDS Create
        /// </summary>
        /// <param name="createPool"></param>
        private void CreateRDSPool(CreatePoolModel createPool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// PowerShell Return Data 처리
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="psData"></param>
        /// <returns></returns>
        public T GetResult<T>(Collection<PSObject> psData)
        {
            T retValue = Activator.CreateInstance<T>();
            string fieldName = string.Empty;

            if (retValue is IList)
            {
                Type t = retValue.GetType().GetGenericArguments()[0];
                foreach (PSObject r in psData)
                {
                    object listitem = Activator.CreateInstance(t);
                    foreach (PSMemberInfo m in r.Members)
                    {
                        fieldName = m.Name;
                        if (t.GetProperty(fieldName) != null)
                        {
                            t.GetProperty(fieldName).SetValue(listitem, m.Value, null);
                        }
                    }
                    (retValue as IList).Add(listitem);
                }
            }
            else
            {
                if (psData.Count > 0)
                {
                    PSObject r = psData[0];
                    foreach (var property in retValue.GetType().GetProperties())
                    {
                        foreach (PSMemberInfo m in r.Members)
                        {
                            if (0 == string.Compare(property.Name, m.Name))
                            {
                                property.SetValue(retValue, m.Value, null);
                                break;
                            }
                        }
                    }
                }
            }

            return retValue;
        }
    }
}

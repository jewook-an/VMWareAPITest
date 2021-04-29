using NCCRequireService.Util.PowerShell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class ViewPool : Connect
    {
        Runspace runSpace = null;
        List<string> Commands = new List<string>();
        List<string> CredCommands = new List<string>();

        public ViewPool(string IPAddress, string LoginID, string LoginPassword, string Protocol = "https", bool AutoLogin = true) : base(IPAddress, LoginID, LoginPassword, Protocol = "https", AutoLogin = true)
        {
            if (AutoLogin)
            {
                Commands.Clear();
                Commands = LoginCommands();
            }
        }

        public DataTable chkLogin()
        {
            DataTable ret = new DataTable();
            try
            {
                using (PowerCli powerCli = new PowerCli())
                {
                    Commands.Clear();
                    Commands = CredCommands();
                    Commands.Add($@"$connectviewserver | Select Name, User, Domain");
                    ret = powerCli.Run_Table(Commands);
                }
            }
            catch
            {
                throw;
            }
            return ret;
        }

        public DataTable GetAllPoolList()
        {
            DataTable ret = new DataTable();

            try
            {
                using (PowerCli powerCli = new PowerCli())
                {
                    Commands.Add($@"(Get-HVPoolSummary).DesktopSummaryData");
                    ret = powerCli.Run_Table(Commands);
                }
            }
            catch
            {
                throw;
            }
            return ret;
        }

        public DataTable GetAllVMList()
        {
            DataTable ret = new DataTable();

            try
            {
                using (PowerCli powerCli = new PowerCli())
                {
                    Commands.Add($@"(Get-HVMachine).base | Sort -Property Name");
                    ret = powerCli.Run_Table(Commands);
                }
            }
            catch
            {
                throw;
            }
            return ret;
        }

        public DataTable PoolVMList(string Poolname)
        {
            DataTable ret = new DataTable();

            try
            {
                using (PowerCli powerCli = new PowerCli())
                {
                    Commands.Add($@"(Get-HVMachine).base | Where {{$_.DesktopName -eq '{Poolname}'}}| Sort -Property Name");
                    ret = powerCli.Run_Table(Commands);
                }
            }
            catch
            {
                throw;
            }
            return ret;
        }

        public List<DataTable> GetPool(List<string> PoolLists)
        {
            List<DataTable> ret = new List<DataTable>();
            string poolType = "AUTOMATED";
            try
            {
                // Pool Defalut Information
                using (PowerCli powerCli = new PowerCli())
                {
                    DataTable dt = new DataTable();
                    string Pools = string.Join(", ", PoolLists?.Where(x => !string.IsNullOrEmpty(x)).ToList());

                    Commands.Add($@"(Get-HVPoolSummary).DesktopSummaryData | where {{$_.Name -eq '{Pools}'}}");
                    dt = powerCli.Run_Table(Commands);
                    
                    if (dt.Rows.Count > 0)
                    {
                        poolType = dt.Rows[0]["Type"].ToString();
                    }
                    dt.TableName = "GetHVPool";
                    ret.Add(dt);
                }

                // Pool 관련 Template, Snapshot 등 Path
                if (poolType == "AUTOMATED")
                {
                    using (PowerCli powerCli = new PowerCli())
                    {
                        DataTable dt = new DataTable();
                        string Pools = string.Join(", ", PoolLists?.Where(x => !string.IsNullOrEmpty(x)).ToList());
                        
                        Commands.Add($@"(Get-HVPool -PoolName '{Pools}').AutomatedDesktopData.VirtualCenterNamesData");
                        dt = powerCli.Run_Table(Commands);
                        dt.TableName = "GetHVPoolPath";
                        ret.Add(dt);
                    }

                    // Pool Naming Set
                    using (PowerCli powerCli = new PowerCli())
                    {
                        DataTable dt = new DataTable();

                        string Pools = string.Join(", ", PoolLists?.Where(x => !string.IsNullOrEmpty(x)).ToList());
                        Commands.Add($@"(Get-HVPool -PoolName '{Pools}').AutomatedDesktopData.VmNamingSettings.PatternNamingSettings");
                        dt = powerCli.Run_Table(Commands);
                        dt.TableName = "GetHVPoolNameSet";
                        ret.Add(dt);
                    }

                    // Guest User Setting
                    //(Get-HVPool -PoolName FullCloneTest).AutomatedDesktopData.CustomizationSettings
                    using (PowerCli powerCli = new PowerCli())
                    {
                        DataTable dt = new DataTable();

                        string Pools = string.Join(", ", PoolLists?.Where(x => !string.IsNullOrEmpty(x)).ToList());
                        Commands.Add($@"(Get-HVPool -PoolName '{Pools}').AutomatedDesktopData.CustomizationSettings");
                        dt = powerCli.Run_Table(Commands);
                        dt.TableName = "GetHVPoolGuestSet";
                        ret.Add(dt);
                    }
                }

                // Pool 사용자 Assignment
                using (PowerCli powerCli = new PowerCli())
                {
                    DataTable dt = new DataTable();

                    string Pools = string.Join(", ", PoolLists?.Where(x => !string.IsNullOrEmpty(x)).ToList());
                    if (poolType == "MANUAL")
                    {
                        Commands.Add($@"(Get-HVPool -PoolName '{Pools}').ManualDesktopData.UserAssignment");
                    }
                    else
                    {
                        Commands.Add($@"(Get-HVPool -PoolName '{Pools}').AutomatedDesktopData.UserAssignment");
                    }
                    dt = powerCli.Run_Table(Commands);
                    dt.TableName = "GetHVPoolUserAssign";
                    ret.Add(dt);
                }
            }
            catch
            {
                throw;
            }
            return ret;
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

                        string usePool = pooluse == true ? " -Enable" : " -Disable";
                        string useProvisioning = provisioning == true ? " -Start" : " -Stop";

                        #region 프로비저닝 설정 Tab - Command
                        //프로비저닝 설정 Tab
                        //automatedDesktopData.vmNamingSettings.patternNamingSettings.namingPattern             // 이름지정패턴      
                        //automatedDesktopData.vmNamingSettings.patternNamingSettings.maxNumberOfMachines       // 최대시스템수
                        //automatedDesktopData.vmNamingSettings.patternNamingSettings.numberOfSpareMachines     // 예비시스템수
                        //automatedDesktopData.vmNamingSettings.patternNamingSettings.provisioningTime          // 시스템프로비저닝 옵션(요구시-ON_DEMAND, 미리-UP_FRONT)
                        //automatedDesktopData.vmNamingSettings.patternNamingSettings.minNumberOfMachines       // 요구시 > 최소시스템수
                        #endregion

                        string[] scripts = new string[4];
                        scripts[0] = "$retSetPool = \"\" | select ErrorDescription";
                        scripts[1] = string.Format("try{{Set-HVPool -PoolName \"{0}\" {1} -ErrorAction Stop}}catch{{$retSetPool=$_.Exception.Message}}", poolname, usePool);
                        scripts[2] = string.Format("try{{Set-HVPool -PoolName \"{0}\" {1} -ErrorAction Stop}}catch{{$retSetPool=$_.Exception.Message}}", poolname, useProvisioning);
                        scripts[3] = "$retSetPool";

                        for (int i = 0; i < scripts.Length; i++)
                        {
                            pPs.AddScript(scripts[i]);
                        }
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
        public T NewPool<T>(PoolModel createPool)
        {
            T retValue = default(T);
            string poolType = createPool.PoolType.ToUpper();
            if (poolType == "AUTOMATED")
            {
                poolType = createPool.Source == "VIEW_COMPOSER" ? "LINKEDCLONE" : "FULLCLONE";
            }

            switch (poolType)
            {
                // 미사용
                case "INSTANTCLONE":
                    retValue = CreateInstantClonePool<T>(createPool);
                    break;
                case "LINKEDCLONE":
                    retValue = CreateLinkedClonePool<T>(createPool);
                    break;
                case "FULLCLONE":
                    retValue = CreateFullClonePool<T>(createPool);
                    break;
                case "MANUAL":
                    retValue = CreateManualPool<T>(createPool);
                    break;
                // 미사용
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
        private T CreateInstantClonePool<T>(PoolModel createPool)
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
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.NewPool, "VMWare.HorizonView.InstantClone Exception", ex);
            }
            finally
            {
                using (PowerShell pPs = PowerShell.Create())
                {
                    pPs.AddScript(LogoutCommands());
                    result = pPs.Invoke();
                }
            }

            #region 권한부여 (Dedicate)
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
        private T CreateLinkedClonePool<T>(PoolModel createPool)
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

                        string datastores = string.Empty;
                        for (int i = 0; i < createPool.Datastores.Length; i++)
                        {
                            datastores += i == 0 ? "'" + createPool.Datastores[i] + "'" : " ,'" + createPool.Datastores[i] + "'";
                        }

                        string PoolName = createPool.PoolName;
                        string PoolDisplayName = createPool.PoolName;
                        string PoolDescription = createPool.PoolName;
                        //string PoolDisplayName = createPool.PoolDisplayName;
                        //string PoolDescription = createPool.Description;

                        // createPool.NamingPattern
                        // Names that contain fixed-length tokens have a 15-character limit, including your naming pattern and the number of digits in the token
                        // https://docs.vmware.com/en/VMware-Horizon-7/7.12/horizon-virtual-desktops/GUID-26AD6C7D-553A-46CB-B8B3-DA3F6958CD9C.html

                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat(@"try {{New-HVPool -LinkedClone -PoolName ""{0}"" -PoolDisplayName ""{1}"" -Description ""{2}"" -UserAssignment {3} -ParentVM ""{4}"" -SnapshotVM ""{5}"" -VmFolder ""{6}"" -HostOrCluster ""{7}"" -ResourcePool ""{8}"" -Datastores {9} -NamingMethod {10} -NamingPattern ""{11}"" ", PoolName, PoolDisplayName, PoolDescription, createPool.UserAssignment, createPool.ParentVM, createPool.SnapshotVM, createPool.VmFolder, createPool.HostOrCluster, createPool.ResourcePool, datastores, createPool.NamingMethod, createPool.NamingPattern);

                        sb.AppendFormat(@" -EnableProvisioning ${0} -StopProvisioningOnError ${1} -RedirectWindowsProfile ${2}", createPool.EnableProvisioning, createPool.StopProvisioningOnError, createPool.RedirectWindowsProfile);

                        sb.AppendFormat(@" -MinReady {0} -MaximumCount {1} -SpareCount {2}", createPool.MinReady, createPool.MaximumCount, createPool.SpareCount);
                        
                        sb.AppendFormat(@" -ProvisioningTime {0} {1} -CustType {2} -NetBiosName ""{3}"" -DomainAdmin ""{4}""", createPool.ProvisioningTime, createPool.SysPrepName, createPool.CustType, createPool.NetBiosName, createPool.DomainAdmin);
                        
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
                        result = pPs.Invoke();
                        retValue = GetResult<T>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.NewPool, "VMWare.HorizonView.LinkedClone Exception", ex);
            }
            finally
            {
                using (PowerShell pPs = PowerShell.Create())
                {
                    pPs.AddScript(LogoutCommands());
                    result = pPs.Invoke();
                }
            }
            return retValue;
        }

        /// <summary>
        /// Full Clone Create
        /// </summary>
        /// <param name="createPool"></param>
        private T CreateFullClonePool<T>(PoolModel createPool)
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

                        string datastores = string.Empty;
                        for (int i = 0; i < createPool.Datastores.Length; i++)
                        {
                            datastores += i == 0 ? "'" + createPool.Datastores[i] + "'" : " ,'" + createPool.Datastores[i] + "'";
                        }

                        string PoolName = createPool.PoolName;
                        string PoolDisplayName = createPool.PoolName;
                        string PoolDescription = createPool.PoolName;
                        //string PoolDisplayName = createPool.PoolDisplayName;
                        //string PoolDescription = createPool.Description;

                        StringBuilder sb = new StringBuilder();

                        sb.AppendFormat(@"try {{New-HVPool -FullClone -PoolName ""{0}"" -PoolDisplayName ""{1}"" -Description ""{2}"" -UserAssignment {3} -Template ""{4}"" -VmFolder ""{5}"" -HostOrCluster ""{6}"" -ResourcePool ""{7}"" -Datastores {8} -NamingMethod {9} -NamingPattern ""{10}"" ", PoolName, PoolDisplayName, PoolDescription, createPool.UserAssignment, createPool.Template, createPool.VmFolder, createPool.HostOrCluster, createPool.ResourcePool, datastores, createPool.NamingMethod, createPool.NamingPattern);
                        
                        sb.AppendFormat(@" -EnableProvisioning ${0} -StopProvisioningOnError ${1} -MaximumCount {2} -SpareCount {3}", createPool.EnableProvisioning, createPool.StopProvisioningOnError, createPool.MaximumCount, createPool.SpareCount);
                        
                        sb.AppendFormat(@" -ProvisioningTime {0} {1} -CustType {2} -ErrorAction Stop}}catch{{$retFullClonePool.ErrorDescription=$_.Exception.Message}}", createPool.ProvisioningTime, createPool.SysPrepName, createPool.CustType);
                        
                        string[] scripts = new string[3];
                        scripts[0] = "$retFullClonePool=\"\" | select ErrorDescription";
                        scripts[1] = sb.ToString();
                        Trace.WriteLine(scripts[1]);
                        scripts[2] = "$retFullClonePool";

                        for (int i = 0; i < scripts.Length; i++)
                        {
                            pPs.AddScript(scripts[i]);
                        }
                        result = pPs.Invoke();
                        retValue = GetResult<T>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.NewPool, "VMWare.HorizonView.FullClone Exception", ex);
            }
            finally
            {
                using (PowerShell pPs = PowerShell.Create())
                {
                    pPs.AddScript(LogoutCommands());
                    result = pPs.Invoke();
                }
            }
            return retValue;
        }

        /// <summary>
        /// 수동풀 전용 Create (vCenter VM선택 후 사용자지정) - VM 필수지정
        /// </summary>
        /// <param name="createPool"></param>
        private T CreateManualPool<T>(PoolModel createPool)
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

                        string PoolName = createPool.PoolName;
                        string PoolDisplayName = createPool.PoolName;
                        string PoolDescription = createPool.PoolName;
                        //string PoolDisplayName = createPool.PoolDisplayName;
                        //string PoolDescription = createPool.Description;

                        StringBuilder sb = new StringBuilder();
                        // vCenter내 VM > Managed 수동풀 Floating (사용O) >> 풀생성 완료후 권한 부여
                        // -Source(수동풀VM 소스) : 지원되는값 ('VIRTUAL_CENTER' : vCenter관리 VM) or ('UNMANAGED' : 물리적 시스템,  VM >> vCenter관리 VM XX)
                        sb.AppendFormat(@"try {{New-HVPool -MANUAL -PoolName ""{0}"" -PoolDisplayName ""{1}"" -Description ""{2}"" -UserAssignment {3} -Source VIRTUAL_CENTER -VM {4} -enableHTMLAccess ${5}}}catch{{$retNewManualPool.ErrorDescription=$_.Exception.Message}}", PoolName, PoolDisplayName, PoolDescription, createPool.UserAssignment, poolvm, createPool.enableHTMLAccess);

                        string[] scripts = new string[3];
                        scripts[0] = "$retNewManualPool=\"\" | select ErrorDescription";
                        scripts[1] = sb.ToString();
                        Trace.WriteLine(scripts[1]);
                        scripts[2] = "$retNewManualPool";

                        for (int i = 0; i < scripts.Length; i++)
                        {
                            pPs.AddScript(scripts[i]);
                        }
                        result = pPs.Invoke();
                        retValue = GetResult<T>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.NewPool, "VMWare.HorizonView.ManualPool Exception", ex);
            }
            finally
            {
                using (PowerShell pPs = PowerShell.Create())
                {
                    pPs.AddScript(LogoutCommands());
                    result = pPs.Invoke();
                }
            }
            return retValue;
        }

        /// <summary>
        /// RDS Create
        /// </summary>
        /// <param name="createPool"></param>
        private void CreateRDSPool(PoolModel createPool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove System (VM 삭제 > 프로비저닝중일시 삭제후 재 프로비저닝 됨 > 프로비저닝 중지후 삭제)
        /// </summary>
        /// <param name="removePool"></param>
        public T RemovePoolSystem<T>(PoolModel removePool, string removeSystem)
        {
            Collection<PSObject> result = null;
            T retValue = default(T);
            string PoolEnable = string.Empty;
            string ProvisionEnable = string.Empty;
            try
            {
                using (runSpace = RunspaceFactory.CreateRunspace())
                {
                    runSpace.Open();
                    using (PowerShell pPs = PowerShell.Create())
                    {
                        pPs.Runspace = runSpace;

                        CredCommands.Clear();
                        CredCommands = CredCommands();

                        foreach (var item in CredCommands)
                        {
                            pPs.AddScript(item);
                        }

                        PoolEnable = removePool.enabled ? "-Enable" : "-Disable";                 // 풀 사용여부
                        ProvisionEnable = removePool.EnableProvisioning ? "-Start" : "-Stop";     // 프로비저닝 사용여부

                        string[] scripts = new string[11];
                        scripts[0] = "$retRemoveHVMachine=\"\" | select ErrorDescription";
                        scripts[1] = string.Format("Set-HVPool -PoolName '{0}' {1}", removePool.PoolName, PoolEnable);
                        scripts[2] = string.Format("Set-HVPool -PoolName '{0}' {1}", removePool.PoolName, ProvisionEnable);
                        scripts[3] = "$Services = $connectviewserver.extensiondata";
                        scripts[4] = string.Format("$Machinename='{0}'", removeSystem);
                        scripts[5] = "$Machineid = (Get-HVMachine -Machinename $Machinename).id";
                        scripts[6] = "$Machineservice = New-Object VMware.Hv.MachineService";
                        scripts[7] = "$spec = New-Object VMware.Hv.machinedeletespec";
                        scripts[8] = "$spec.deleteFromDisk = $TRUE";
                        scripts[9] = "$retRemoveHVMachine = $Machineservice.Machine_Delete($Services, $Machineid, $spec)";
                        scripts[10] = "$retRemoveHVMachine";

                        for (int i = 0; i < scripts.Length; i++)
                        {
                            pPs.AddScript(scripts[i]);
                        }
                        result = pPs.Invoke();
                        retValue = GetResult<T>(result);
                    }
                    runSpace.Close();
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.DeleteMachine, "VMWare.HorizonView.RemoveHVMachine Exception", ex);
            }
            finally
            {
                using (PowerShell pPs = PowerShell.Create())
                {
                    pPs.AddScript(LogoutCommands());
                    result = pPs.Invoke();
                }
            }
            return retValue;
        }

        public ViewResult DeletePool(PoolModel deletepool)
        {
            ViewResult result = new ViewResult();

            string poolname = deletepool.PoolName;

            SetPool<PoolModel>(poolname, false, false);

            string[] scripts = new string[3];
            scripts[0] = "$retDeletePool = \"\" | select ErrorDescription";
            scripts[1] = string.Format("try{{Remove-HVPool -PoolName \"{0}\" -DeleteFromDisk -Confirm:$false -ErrorAction Stop}}catch{{$retEditPool=$_.Exception.Message}}", poolname);
            scripts[2] = "$retDeletePool";

            try
            {
                result = Execute<ViewResult>(scripts);
            }
            catch (Exception ex)
            {
                result.ErrorDescription = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Reset System (VM 리셋)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resetSystem"></param>
        /// <returns></returns>
        public T ResetPoolSystem<T>(string resetSystem)
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

                        CredCommands.Clear();
                        CredCommands = CredCommands();

                        foreach (var item in CredCommands)
                        {
                            pPs.AddScript(item);
                        }

                        string[] scripts = new string[7];
                        scripts[0] = "$retResetHVMachine=\"\" | select ErrorDescription";
                        scripts[1] = "$Services = $connectviewserver.extensiondata";
                        scripts[2] = string.Format("$Machinename='{0}'", resetSystem);
                        scripts[3] = "$Machineid = (Get-HVMachine -Machinename $Machinename).id";
                        scripts[4] = "$Machineservice = New-Object VMware.Hv.MachineService";
                        scripts[5] = "$retResetHVMachine = $Machineservice.Machine_Reset($Services, $Machineid)";
                        scripts[6] = "$retResetHVMachine";

                        for (int i = 0; i < scripts.Length; i++)
                        {
                            pPs.AddScript(scripts[i]);
                        }

                        result = pPs.Invoke();

                        retValue = GetResult<T>(result);
                    }
                    runSpace.Close();
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.ResetMachine, "VMWare.HorizonView.ResetHVMachine Exception", ex);
            }
            finally
            {
                using (PowerShell pPs = PowerShell.Create())
                {
                    pPs.AddScript(LogoutCommands());
                    result = pPs.Invoke();
                }
            }
            return retValue;
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

        public ViewResult ClonePool(PoolModel poolclone, string originpoolname)
        {
            ViewResult result = new ViewResult();

            string PoolName = poolclone.PoolName;
            string PoolDisplayName = poolclone.PoolName;
            string PoolDescription = poolclone.PoolName;
            // Names that contain fixed-length tokens have a 15-character limit, including your naming pattern and the number of digits in the token
            // https://docs.vmware.com/en/VMware-Horizon-7/7.12/horizon-virtual-desktops/GUID-26AD6C7D-553A-46CB-B8B3-DA3F6958CD9C.html
            string NamePattern = poolclone.NamingPattern;
            int MaxSystem = poolclone.MaximumCount;

            string[] scripts = new string[4];
            scripts[0] = "$retClonePool = \"\" | select ErrorDescription";
            scripts[1] = string.Format("try{{$vmwarepool = Get-HVPool -PoolName '{0}' -ErrorAction Stop}}catch{{$retClonePool=$_.Exception.Message}}", originpoolname);
            scripts[2] = string.Format("try{{New-HVPool -ClonePool $vmwarepool -PoolName '{0}' -NamingPattern '{1}' -ErrorAction Stop}}catch{{$retClonePool=$_.Exception.Message}}", PoolName, NamePattern);
            scripts[3] = "$retClonePool";

            try
            {
                result = Execute<ViewResult>(scripts);
            }
            catch (Exception ex)
            {
                result.ErrorDescription = ex.Message;
            }
            return result;
        }

        public ViewResult EditPool(PoolModel editpool)
        {
            ViewResult result = new ViewResult();

            string poolname = editpool.PoolName;
            bool enable = editpool.Enable;
            bool provision = editpool.EnableProvisioning;

            string NamePattern = editpool.NamingPattern;
            int MaxSystem = editpool.MaximumCount;

            string patternScript = "automatedDesktopData.vmNamingSettings.patternNamingSettings.namingPattern";
            string systemScript = "automatedDesktopData.vmNamingSettings.patternNamingSettings.maxNumberOfMachines";
            //string provisionScript = "automatedDesktopData.vmNamingSettings.patternNamingSettings.provisioningTime";    //프로비저닝 시기 결정 (ON_DEMAND/UP_FRONT)

            SetPool<PoolModel>(poolname, enable, provision);

            string[] scripts = new string[4];
            scripts[0] = "$retEditPool = \"\" | select ErrorDescription";
            scripts[1] = string.Format("try{{Set-HVPool -PoolName \"{0}\" -key '{1}' -value '{2}' -ErrorAction Stop}}catch{{$retEditPool=$_.Exception.Message}}", poolname, patternScript, NamePattern);
            scripts[2] = string.Format("try{{Set-HVPool -PoolName \"{0}\" -key '{1}' -value {2} -ErrorAction Stop}}catch{{$retEditPool=$_.Exception.Message}}", poolname, systemScript, MaxSystem);
            //scripts[2] = string.Format("try{{Set-HVPool -PoolName \"{0}\" -key '{1}' -value '{2}' -ErrorAction Stop}}catch{{$retEditPool=$_.Exception.Message}}", poolname, provisionScript, "ON_DEMAND/UP_FRONT");
            scripts[3] = "$retEditPool";

            try
            {
                result = Execute<ViewResult>(scripts);
            }
            catch (Exception ex)
            {
                result.ErrorDescription = ex.Message;
            }
            return result;
        }

        public T Execute<T>(string[] scripts)
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

                        foreach (string script in scripts)
                        {
                            pPs.AddScript(script);
                        }
                        result = pPs.Invoke();

                        if (!string.IsNullOrEmpty(result[0].BaseObject.ToString()))
                        {
                            if (typeof(T) == typeof(ViewResult))
                            {
                                var vResult = new ViewResult();
                                vResult.ErrorDescription = result[0].BaseObject.ToString();
                                return (T)Convert.ChangeType(vResult, typeof(T));
                                //return converter.ConvertFromString(vResult.ErrorDescription);
                            }
                        }
                        retValue = GetResult<T>(result);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                using (PowerShell pPs = PowerShell.Create())
                {
                    pPs.AddScript(LogoutCommands());
                    result = pPs.Invoke();
                }
            }
            return retValue;
        }

        public T EditPool<T>(PoolModel editpool)
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

                        #region 참고 Option
                        /*
						poolmodel.Template = txtTemplatePath.Text;
						poolmodel.ParentVM = txtParentVMPath.Text;
						poolmodel.SnapshotVM = txtSnapshotPath.Text;
						poolmodel.VmFolder = txtVmFolderPath.Text;
						poolmodel.HostOrCluster = txtHostOrClusterPath.Text;
						poolmodel.ResourcePool = txtResourcePoolPath.Text;

						var datastores = txtDatastorePaths.Text;
						char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
						string[] datastoreList = datastores.Split(delimiterChars);

						poolmodel.Datastores = datastoreList;
						 */

                        /*
                        >> automatedDesktopData.vmNamingSettings.patternNamingSettings
                        NamingPattern         : FullCloneTest
                        MaxNumberOfMachines   : 1
                        NumberOfSpareMachines : 1
                        ProvisioningTime      : UP_FRONT
                        MinNumberOfMachines   : 

                        Set-HVPool -PoolName FullCloneTest -key 'automatedDesktopData.vmNamingSettings.patternNamingSettings.maxNumberOfMachines','automatedDesktopData.vmNamingSettings.patternNamingSettings.namingPattern' -value 1,FullCloneTest1
                         */
                        #endregion

                        string poolname = editpool.PoolName;
                        bool enable = editpool.Enable;
                        bool provision = editpool.EnableProvisioning;

                        string NamePattern = editpool.NamingPattern;
                        int MaxSystem = editpool.MaximumCount;

                        string patternScript = "automatedDesktopData.vmNamingSettings.patternNamingSettings.namingPattern";
                        string systemScript = "automatedDesktopData.vmNamingSettings.patternNamingSettings.maxNumberOfMachines";
                        string provisionScript = "automatedDesktopData.vmNamingSettings.patternNamingSettings.provisioningTime";    //프로비저닝 시기 결정 (ON_DEMAND/UP_FRONT)

                        SetPool<T>(poolname, enable, provision);

                        string[] scripts = new string[4];
                        scripts[0] = "$retEditPool = \"\" | select ErrorDescription";
                        scripts[1] = string.Format("try{{Set-HVPool -PoolName \"{0}\" -key '{1}' -value '{2}' -ErrorAction Stop}}catch{{$retEditPool=$_.Exception.Message}}", poolname, patternScript, NamePattern);
                        scripts[2] = string.Format("try{{Set-HVPool -PoolName \"{0}\" -key '{1}' -value {2} -ErrorAction Stop}}catch{{$retEditPool=$_.Exception.Message}}", poolname, systemScript, MaxSystem);
                        //scripts[2] = string.Format("try{{Set-HVPool -PoolName \"{0}\" -key '{1}' -value '{2}' -ErrorAction Stop}}catch{{$retEditPool=$_.Exception.Message}}", poolname, provisionScript, "ON_DEMAND/UP_FRONT");
                        scripts[3] = "$retEditPool";
                        
                        for (int i = 0; i < scripts.Length; i++)
                        {
                            pPs.AddScript(scripts[i]);
                        }
                        result = pPs.Invoke();

                        //EnvConnectionStringInPowerShell = result[0].BaseObject.ToString();

                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[Connect] HorizonView.EditPool Failed");
                            throw new CliException(CliException.Reason.EditPool);
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
    }
}

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
    public class UserEntitlement : Connect
    {
        // New-HVEntitlement -User domain\username -ResourceName $pool -Type User -HvServer $vcs
        // New-HVEntitlement -User arun@arundev.me -ResourceName mypool
        // New-HVEntitlement -User domain\group -ResourceName 'poolname' -Type Group
        // Set-HVMachine -MachineName Win10Pro-SIP001 -User "namurnd\viewuser1"
        /*
         
        StringBuilder sbassign = new StringBuilder();
        sbassign.AppendFormat(@"try {{New-HVEntitlement -User ""{0}"" -ResourceName ""{1}"" -Type ""{2}"" ", domainUser, );
        sbassign.AppendFormat(@" -ErrorAction Stop}}catch{{$retEntitlement.ErrorDescription=$_.Exception.Message}}");

        string[] scripts2 = new string[3];
        scripts2[0] = "$retEntitlement=\"\" | select ErrorDescription";
        scripts2[1] = sbassign.ToString();
        Trace.WriteLine(scripts2[1]);
        scripts2[2] = "$retEntitlement";
         
         */
        Runspace runSpace = null;
        List<string> Commands = new List<string>();

        public UserEntitlement(string IPAddress, string LoginID, string LoginPassword, string Protocol = "https", bool AutoLogin = true) : base(IPAddress, LoginID, LoginPassword, Protocol = "https", AutoLogin = true)
        {
            if (AutoLogin)
            {
                Commands.Clear();
                Commands = LoginCommands();
            }
        }

        /// <summary>
        /// Horizon DesktopPool 사용자 권한부여
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="user"></param>
        /// <param name="poolname"></param>
        /// <param name="usertype"></param>
        /// <returns></returns>
        public T PoolUserAssignment<T>(string user, string poolname, string usertype)
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

                        #region Connect HVServer Connect
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
                        #endregion

                        #region Horizon ViewServer Pool Search
                        //-PoolName(풀이름) -PoolType(MANUAL/AUTOMATED/RDS) -UserAssignment(FLOATING/DEDICATED)
                        string poolinfo = string.Format("(Get-HVPoolSummary).DesktopSummaryData | where {{$_.Name -eq '{0}'}}", poolname);
                        Commands.Clear();
                        Commands.Add(poolinfo);

                        foreach (var item in Commands)
                        {
                            pPs.AddScript(item);
                        }
                        result = pPs.Invoke();

                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[Search] HorizonView.PoolSearch Failed");
                            throw new CliException(CliException.Reason.GetPool);
                        }
                        #endregion

                        #region Pool 권한부여
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat(@"try {{New-HVEntitlement -User {0} -ResourceName '{1}' -Type {2}}}catch{{$retHVEntitlement.ErrorDescription=$_.Exception.Message}}", user, poolname, usertype);

                        string[] scripts = new string[3];
                        scripts[0] = "$retHVEntitlement=\"\" | select ErrorDescription";
                        scripts[1] = sb.ToString();
                        Trace.WriteLine(scripts[1]);
                        scripts[2] = "$retHVEntitlement";

                        for (int i = 0; i < scripts.Length; i++)
                        {
                            pPs.AddScript(scripts[i]);
                        }
                        result = pPs.Invoke();

                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[PoolUserAssignment] HorizonView.UserHVEntitlement Failed");
                            throw new CliException(CliException.Reason.SetPool);
                        }
                        #endregion
                        retValue = GetResult<T>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.SetPool, "VMWare.HorizonView.PoolUserAssignment Exception", ex);
            }

            return retValue;
        }

        /// <summary>
        /// Horizon System(VM) 사용자 할당
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="user"></param>
        /// <param name="system"></param>
        /// <returns></returns>
        public T SystemUserAssignment<T>(string user, string system)
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

                        #region Connect HVServer Connect
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
                        #endregion

                        #region Horizon ViewServer System(VM) Search
                        string vminfo = string.Format("Get-HVMachineSummary -MachineName '{0}'", system);
                        Commands.Clear();
                        Commands.Add(vminfo);

                        foreach (var item in Commands)
                        {
                            pPs.AddScript(item);
                        }
                        result = pPs.Invoke();

                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[Search] HorizonView.VMSearch Failed");
                            throw new CliException(CliException.Reason.GetData);
                        }
                        #endregion

                        #region VM 사용자 할당
                        StringBuilder sb = new StringBuilder();
                        // Set-HVMachine -MachineName Win10Pro-SIP001 -User "namurnd\viewuser1"
                        sb.AppendFormat(@"try {{Set-HVMachine -MachineName {0} -User '{1}'}}catch{{$retSetHVMachine.ErrorDescription=$_.Exception.Message}}", system, user);

                        string[] scripts = new string[3];
                        scripts[0] = "$retSetHVMachine=\"\" | select ErrorDescription";
                        scripts[1] = sb.ToString();
                        Trace.WriteLine(scripts[1]);
                        scripts[2] = "$retSetHVMachine";

                        for (int i = 0; i < scripts.Length; i++)
                        {
                            pPs.AddScript(scripts[i]);
                        }
                        result = pPs.Invoke();

                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[SystemUserAssignment] HorizonView.UserHVEntitlement Failed");
                            throw new CliException(CliException.Reason.UserAssign);
                        }
                        #endregion
                        retValue = GetResult<T>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.UserAssign, "VMWare.HorizonView.SystemUserAssignment Exception", ex);
            }

            return retValue;
        }

        /// <summary>
        /// Horizon DesktopPool User 권한제거
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="user"></param>
        /// <param name="poolname"></param>
        /// <param name="usertype"></param>
        /// <returns></returns>
        public T PoolUserUnAssignment<T>(string user, string poolname, string usertype)
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

                        #region Connect HVServer Connect
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
                        #endregion

                        #region Horizon ViewServer Pool Search
                        string poolinfo = string.Format("(Get-HVPoolSummary).DesktopSummaryData | where {{$_.Name -eq '{0}'}}", poolname);
                        Commands.Clear();
                        Commands.Add(poolinfo);

                        foreach (var item in Commands)
                        {
                            pPs.AddScript(item);
                        }
                        result = pPs.Invoke();

                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[Search] HorizonView.PoolSearch Failed");
                            throw new CliException(CliException.Reason.GetPool);
                        }
                        #endregion

                        #region Pool 권한 제거
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat(@"try {{Remove-HVEntitlement -User {0} -ResourceName '{1}' -Type {2}}}catch{{$retHVEntitlement.ErrorDescription=$_.Exception.Message}}", user, poolname, usertype);

                        string[] scripts = new string[3];
                        scripts[0] = "$retHVEntitlement=\"\" | select ErrorDescription";
                        scripts[1] = sb.ToString();
                        Trace.WriteLine(scripts[1]);
                        scripts[2] = "$retHVEntitlement";

                        for (int i = 0; i < scripts.Length; i++)
                        {
                            pPs.AddScript(scripts[i]);
                        }
                        result = pPs.Invoke();

                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[PoolUserAssignment] HorizonView.UserHVEntitlement Failed");
                            throw new CliException(CliException.Reason.SetPool);
                        }
                        #endregion
                        retValue = GetResult<T>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.SetPool, "VMWare.HorizonView.PoolUserUnAssignment Exception", ex);
            }

            return retValue;
        }

        /// <summary>
        /// Horizon System(VM) 사용자 할당 제거
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="user"></param>
        /// <param name="system"></param>
        /// <returns></returns>
        public T SystemUserUnAssignment<T>(string system)
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

                        #region Connect HVServer Connect
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
                        #endregion

                        #region Horizon ViewServer System(VM) Search
                        string vminfo = string.Format("Get-HVMachineSummary -MachineName '{0}'", system);
                        Commands.Clear();
                        Commands.Add(vminfo);

                        foreach (var item in Commands)
                        {
                            pPs.AddScript(item);
                        }
                        result = pPs.Invoke();

                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[Search] HorizonView.VMSearch Failed");
                            throw new CliException(CliException.Reason.GetData);
                        }
                        #endregion

                        #region VM 할당 해제
                        string[] scripts = new string[7];
                        scripts[0] = "$AssignServices=$hvserver.extensiondata";
                        scripts[1] = string.Format("$Machinename='{0}'", system);
                        scripts[2] = "$Machineid=(get-hvmachine -machinename $Machinename).id";
                        scripts[3] = "$Machineservice=new-object vmware.hv.machineservice";
                        scripts[4] = "$Machineinfohelper=$Machineservice.read($AssignServices, $Machineid)";
                        scripts[5] = "$Machineinfohelper.getbasehelper().setuser($null)";
                        scripts[6] = "$Machineservice.update($AssignServices, $Machineinfohelper)";

                        for (int i = 0; i < scripts.Length; i++)
                        {
                            pPs.AddScript(scripts[i]);
                        }
                        result = pPs.Invoke();
                            #region Error Check Delete
                            /*
                            if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                            {
                                Trace.WriteLine("[SystemUserAssignment] HorizonView.UserHVEntitlement Failed");
                                throw new CliException(CliException.Reason.UserAssign);
                            }
                            */
                            #endregion
                        #endregion
                        retValue = GetResult<T>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.UserAssign, "VMWare.HorizonView.SystemUserAssignment Exception", ex);
            }

            return retValue;
        }

        /*
         * -HVServer <object> : 삭제할 머신이있는 Horizon 서버. (필수아님) 서버 미지정 > 먼저 connect-hvserver 사용
         * -MachineNames <Array> : 단일VM, VM 배열
         * -DeleteFromDisk true or false : linkedclone, instantclone >> 기본값은 true // 다른 모든 유형 >> false
        
        1. Deletes VM 'LAX-WIN10-002' from HV Server 'horizonserver123'
        PS C:\>Remove-HVMachine -HVServer 'horizonserver123' -MachineNames 'LAX-WIN10-002'
        2. Deletes VM's contained within an array of machine names from HV Server 'horizonserver123'
        PS C:\>Remove-HVMachine -HVServer 'horizonserver123' -MachineNames $machines
        3. Deletes VM 'ManualVM01' from Horizon inventory, but not from vSphere. Note this only works for Full Clone VMs.
        PS C:\>Remove-HVMachine -HVServer 'horizonserver123' -MachineNames 'ManualVM01' -DeleteFromDisk:$false
         */

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
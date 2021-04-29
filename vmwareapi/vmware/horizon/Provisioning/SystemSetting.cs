using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation.Runspaces;
using vmwareapi.vmware.horizon.Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Diagnostics;
using vmwareapi.Util.PowerShell;

namespace vmwareapi.vmware.horizon.Provisioning
{
    public class SystemSetting : Connect
    {

        Dictionary<string, object> ActionStep = new Dictionary<string, object>();
        Runspace runSpace = null;
        List<string> Commands = new List<string>();
        List<string> CredCommands = new List<string>();

        public SystemSetting(string IPAddress, string LoginID, string LoginPassword, string Protocol = "https", bool AutoLogin = true) : base(IPAddress, LoginID, LoginPassword, Protocol = "https", AutoLogin = true)
        {
            if (AutoLogin)
            {
                Commands.Clear();
                Commands = LoginCommands();
            }
        }

        #region "VMWare AGENT WORKFLOW STEP"
        public bool SetAgentDataNew_VMWare(
                                        string DomainNetbios
                                        , string DomainFQDN
                                        , string DomainAdminID
                                        , string DomainAdminPW
                                        , string DomainUserID
                                        , string DomainOUDC
                                        , string IPAddress_F
                                        , string SubnetMask_F
                                        , string Gateway_F
                                        , string DNS1_F
                                        , string DNS2_F
                                        , string StateCallBack_IPAddressPort_IPPORT
                                        , int StateCallBack_CREATE_ID
                                        , string VMName
                                        , int AD_SYNC_WAIT_FINISH_SECONDS)
        {
            bool Return_Value = false;

            try
            {
                // ------------------------------ agentWorkflow ------------------------------//
                using (NCCWorkFlow.NCCServer.NCCAgent nccagent = new NCCWorkFlow.NCCServer.NCCAgent())
                {
                    #region 1단계
                    Dictionary<string, object> tempOrder1 = new Dictionary<string, object>();

                    using (NCCWorkFlow.NCCProvAgent.Sleep step1_sleep = new NCCWorkFlow.NCCProvAgent.Sleep())
                    {
                        if (!step1_sleep.SetSleep(1))
                        {
                            return Return_Value;
                        }
                        tempOrder1.Add(Convert.ToString(step1_sleep), step1_sleep);
                    }

                    //DISK D 생성 (에이전트 설치되어야 함)
                    using (NCCWorkFlow.NCCProvAgent.CommandRun commandRun = new NCCWorkFlow.NCCProvAgent.CommandRun())
                    {
                        commandRun.CommandRun_WorkingDir = string.Empty;
                        if (!commandRun.Run(NCCWorkFlow.NCCProvAgent.AgentKeywordSet.A_CommandRun_Add_YN_Kind.Yes, @"C:\Windows\System32\diskpart.exe", @"/s """ + Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\Namutech\ProvisioningAgent\Script\Diskpart_D_Assign.txt""", NCCWorkFlow.NCCProvAgent.AgentKeywordSet.A_CommandRun_Verb_YN_Kind.Yes))
                        {
                            return Return_Value;
                        }

                        string temp = Convert.ToString(commandRun);
                        int cnt = 0;

                        while (tempOrder1.ContainsKey(temp))
                        {
                            cnt++;
                            temp = Convert.ToString(commandRun) + "_" + Convert.ToString(cnt); //1 ~ 9까지만 가능
                            //temp = Convert.ToString(commandRun) + "/" + Convert.ToString(cnt); //이렇게 넣으면 갯수 상관없음
                        }

                        tempOrder1.Add(temp, commandRun);
                    }

                    using (NCCWorkFlow.NCCProvAgent.IPChange1 step1_ipchange1 = new NCCWorkFlow.NCCProvAgent.IPChange1())
                    {
                        if (!step1_ipchange1.SetFirstNetwork(IPAddress_F, SubnetMask_F, Gateway_F, DNS1_F, DNS2_F, true))
                        {
                            return Return_Value;
                        }
                        tempOrder1.Add(Convert.ToString(step1_ipchange1), step1_ipchange1);
                    }

                    #region step1_domainjoin 주석
                    //using (NCCWorkFlow.NCCProvAgent.DomainJoin step1_domainjoin = new NCCWorkFlow.NCCProvAgent.DomainJoin())
                    //{
                    //    if (!step1_domainjoin.SetDomainJoin(DomainFQDN, DomainAdminID, NCCFramework2.Util.Cryptography.AESEncryptString(DomainAdminPW), true))
                    //    {
                    //        return Return_Value;
                    //    }
                    //    tempOrder1.Add(Convert.ToString(step1_domainjoin), step1_domainjoin);
                    //}
                    #endregion

                    using (NCCWorkFlow.NCCProvAgent.HostNameChange step1_hostnamechange = new NCCWorkFlow.NCCProvAgent.HostNameChange())
                    {
                        if (!step1_hostnamechange.SetHostNameChange(VMName, DomainNetbios, DomainAdminID, NCCFramework2.Util.Cryptography.AESEncryptString(DomainAdminPW), true))
                        {
                            return Return_Value;
                        }
                        tempOrder1.Add(Convert.ToString(step1_hostnamechange), step1_hostnamechange);
                    }

                    #region step1_autologin 주석
                    //using (NCCWorkFlow.NCCProvAgent.AutoLoginAdd step1_autologin = new NCCWorkFlow.NCCProvAgent.AutoLoginAdd(NCCWorkFlow.NCCProvAgent.AgentKeywordSet.A_AutoLogin_Add_Win_Version_Key_Kind.Win7))
                    //{
                    //    if (!step1_autologin.SetAutoLoginID(DomainNetbios, DomainAdminID, NCCFramework2.Util.Cryptography.AESEncryptString(DomainAdminPW), true))
                    //    {
                    //        return Return_Value;
                    //    }
                    //    tempOrder1.Add(Convert.ToString(step1_autologin), step1_autologin);
                    //}
                    #endregion

                    using (NCCWorkFlow.NCCProvAgent.StepChange step1_stepchange = new NCCWorkFlow.NCCProvAgent.StepChange())
                    {
                        if (!step1_stepchange.SetNextStep(NCCWorkFlow.NCCProvAgent.AgentKeywordSet.S_Run1_Key, NCCWorkFlow.NCCProvAgent.AgentKeywordSet.S_Run2_Key))
                        {
                            return Return_Value;
                        }
                        tempOrder1.Add(Convert.ToString(step1_stepchange), step1_stepchange);
                    }

                    using (NCCWorkFlow.NCCProvAgent.Reboot step1_reboot = new NCCWorkFlow.NCCProvAgent.Reboot())
                    {
                        if (!step1_reboot.SetReboot(NCCWorkFlow.NCCProvAgent.AgentKeywordSet.A_Reboot_Key_Kind.Force))
                        {
                            return Return_Value;
                        }
                        tempOrder1.Add(Convert.ToString(step1_reboot), step1_reboot);
                    }

                    nccagent.StepOrder.Add(NCCWorkFlow.NCCProvAgent.AgentKeywordSet.S_Run1_Key, tempOrder1);
                    #endregion

                    #region 2단계
                    Dictionary<string, object> tempOrder2 = new Dictionary<string, object>();

                    using (NCCWorkFlow.NCCProvAgent.Sleep step2_sleep = new NCCWorkFlow.NCCProvAgent.Sleep())
                    {
                        if (!step2_sleep.SetSleep(5))
                        {
                            return Return_Value;
                        }

                        string temp = Convert.ToString(step2_sleep);
                        int cnt = 0;

                        while (tempOrder2.ContainsKey(temp))
                        {
                            cnt++;
                            temp = Convert.ToString(step2_sleep) + "_" + Convert.ToString(cnt); //1 ~ 9까지만 가능
                            //temp = Convert.ToString(step2_sleep) + "/" + Convert.ToString(cnt); //이렇게 넣으면 갯수 상관없음
                        }

                        tempOrder2.Add(temp, step2_sleep);
                    }

                    using (NCCWorkFlow.NCCProvAgent.IPChange1 step2_ipchange1 = new NCCWorkFlow.NCCProvAgent.IPChange1())
                    {
                        if (!step2_ipchange1.SetFirstNetwork(IPAddress_F, SubnetMask_F, Gateway_F, DNS1_F, DNS2_F, true))
                        {
                            return Return_Value;
                        }
                        tempOrder2.Add(Convert.ToString(step2_ipchange1), step2_ipchange1);
                    }

                    #region 명령어 실행 샘플 - 주석
                    //using (CommandRun Windows_License_Activation_1 = new CommandRun())
                    //{
                    //    Windows_License_Activation_1.CommandRun_WorkingDir = @"C:\Windows\System32\";
                    //    if (!Windows_License_Activation_1.Run(AgentKeywordSet.A_CommandRun_Add_YN_Kind.Yes, @"C:\Windows\System32\Win_Act_Lic.bat", @"", AgentKeywordSet.A_CommandRun_Verb_YN_Kind.Yes))
                    //    {
                    //        return Return_Value;
                    //    }

                    //    string temp = Convert.ToString(Windows_License_Activation_1);
                    //    int cnt = 0;

                    //    while (tempOrder2.ContainsKey(temp))
                    //    {
                    //        cnt++;
                    //        temp = Convert.ToString(Windows_License_Activation_1) + "_" + Convert.ToString(cnt); //1 ~ 9까지만 가능
                    //        temp = Convert.ToString(Windows_License_Activation_1) + "/" + Convert.ToString(cnt); //이렇게 넣으면 갯수 상관없음
                    //    }

                    //    tempOrder2.Add(temp, Windows_License_Activation_1);
                    //}
                    #endregion

                    using (NCCWorkFlow.NCCProvAgent.DomainJoin step2_domainjoin = new NCCWorkFlow.NCCProvAgent.DomainJoin())
                    {
                        if (!step2_domainjoin.SetDomainJoin(DomainFQDN, DomainAdminID, NCCFramework2.Util.Cryptography.AESEncryptString(DomainAdminPW), DomainOUDC, true))
                        {
                            return Return_Value;
                        }
                        tempOrder2.Add(Convert.ToString(step2_domainjoin), step2_domainjoin);
                    }

                    using (NCCWorkFlow.NCCProvAgent.AutoLoginAdd step2_autologin = new NCCWorkFlow.NCCProvAgent.AutoLoginAdd(NCCWorkFlow.NCCProvAgent.AgentKeywordSet.A_AutoLogin_Add_Win_Version_Key_Kind.Win7))
                    {
                        if (!step2_autologin.SetAutoLoginID(DomainNetbios, DomainAdminID, NCCFramework2.Util.Cryptography.AESEncryptString(DomainAdminPW), true))
                        {
                            return Return_Value;
                        }
                        tempOrder2.Add(Convert.ToString(step2_autologin), step2_autologin);
                    }

                    using (NCCWorkFlow.NCCProvAgent.Sleep step2_sleep = new NCCWorkFlow.NCCProvAgent.Sleep())
                    {
                        if (!step2_sleep.SetSleep(10))
                        {
                            return Return_Value;
                        }

                        string temp = Convert.ToString(step2_sleep);
                        int cnt = 0;

                        while (tempOrder2.ContainsKey(temp))
                        {
                            cnt++;
                            temp = Convert.ToString(step2_sleep) + "_" + Convert.ToString(cnt); //1 ~ 9까지만 가능
                            //temp = Convert.ToString(step2_sleep) + "/" + Convert.ToString(cnt); //이렇게 넣으면 갯수 상관없음
                        }

                        tempOrder2.Add(temp, step2_sleep);
                    }

                    using (NCCWorkFlow.NCCProvAgent.StepChange step2_stepchange = new NCCWorkFlow.NCCProvAgent.StepChange())
                    {
                        if (!step2_stepchange.SetNextStep(NCCWorkFlow.NCCProvAgent.AgentKeywordSet.S_Run2_Key, NCCWorkFlow.NCCProvAgent.AgentKeywordSet.S_Finish_Key))
                        {
                            return Return_Value;
                        }
                        tempOrder2.Add(Convert.ToString(step2_stepchange), step2_stepchange);
                    }

                    using (NCCWorkFlow.NCCProvAgent.Reboot step2_reboot = new NCCWorkFlow.NCCProvAgent.Reboot())
                    {
                        if (!step2_reboot.SetReboot(NCCWorkFlow.NCCProvAgent.AgentKeywordSet.A_Reboot_Key_Kind.Force))
                        {
                            return Return_Value;
                        }
                        tempOrder2.Add(Convert.ToString(step2_reboot), step2_reboot);
                    }

                    nccagent.StepOrder.Add(NCCWorkFlow.NCCProvAgent.AgentKeywordSet.S_Run2_Key, tempOrder2);
                    #endregion

                    #region 3단계
                    Dictionary<string, object> tempOrder3 = new Dictionary<string, object>();

                    //JOB CODE가 생성만이면 결과업데이트
                    //할당 작업 GET -> Y : 작업 요청, N : 중지

                    #region Step3 CommandRun 주석 >> 확인필요
                    /*
                     */
                    using (NCCWorkFlow.NCCProvAgent.CommandRun step3_commandrun = new NCCWorkFlow.NCCProvAgent.CommandRun())
                    {
                        step3_commandrun.CommandRun_WorkingDir = string.Empty;
                        if (!step3_commandrun.Run(NCCWorkFlow.NCCProvAgent.AgentKeywordSet.A_CommandRun_Add_YN_Kind.Yes, @"C:\Windows\System32", @"cmd.exe", @" /c REG ADD ""HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\WinLogon"" /v AutoAdminLogon /t REG_SZ /d 0 /f", NCCWorkFlow.NCCProvAgent.AgentKeywordSet.A_CommandRun_Verb_YN_Kind.Yes))
                        {
                            return Return_Value;
                        }

                        tempOrder3.Add(Convert.ToString(step3_commandrun), step3_commandrun);
                    }
                    #endregion

                    using (NCCWorkFlow.NCCProvAgent.LocalAdminGroup step3_localadmingroup = new NCCWorkFlow.NCCProvAgent.LocalAdminGroup())
                    {
                        if (!step3_localadmingroup.SetLocalAdminGroup(DomainNetbios, DomainAdminID, NCCFramework2.Util.Cryptography.AESEncryptString(DomainAdminPW), DomainUserID, true))
                        {
                            return Return_Value;
                        }
                        tempOrder3.Add(Convert.ToString(step3_localadmingroup), step3_localadmingroup);
                    }

                    using (NCCWorkFlow.NCCProvAgent.StepDelete step3_delete = new NCCWorkFlow.NCCProvAgent.StepDelete())
                    {
                        if (!step3_delete.SetStepRemove(NCCWorkFlow.NCCProvAgent.AgentKeywordSet.A_StepRemove_YN_Key_Kind.Yes))
                        {
                            return Return_Value;
                        }
                        tempOrder3.Add(Convert.ToString(step3_delete), step3_delete);
                    }

                    #region 데이터디스크 주석부분
                    /*
                    // 데이터디스크 자동추가됨
                    foreach (KeyValuePair<string, object> key in ActionStep)
                    {
                        if (Convert.ToString(key.Value).Equals(Convert.ToString(new NCCWorkFlow.NCCServer.CreateVMDataDisk())))
                        {
                            NCCWorkFlow.NCCServer.CreateVMDataDisk temp_createvmdatadisk = key.Value as NCCWorkFlow.NCCServer.CreateVMDataDisk;

                            if (
                                        temp_createvmdatadisk.CreateVM_SR_Name_DATA_Add_Type == NCCWorkFlow.NCCServer.ServerKeywordSet.CreateVM_SR_Name_DATA_Add_Type_Key_Kind.ISCSI
                                    ||  temp_createvmdatadisk.CreateVM_SR_Name_DATA_Add_Type == NCCWorkFlow.NCCServer.ServerKeywordSet.CreateVM_SR_Name_DATA_Add_Type_Key_Kind.NFS
                                )
                            {
                                using (NCCWorkFlow.NCCProvAgent.BlockDiskExtend stepfinish_blockdiskextend = new NCCWorkFlow.NCCProvAgent.BlockDiskExtend(NCCWorkFlow.NCCProvAgent.AgentKeywordSet.A_Block_DiskExtend_Win_Version_Key_Kind.Win7))
                                {
                                    //--> DriverLetter 값 설정시 해당드라이브로 디스크 추가 또는 확장
                                    //--> DriverLetter 값 미설정시 해당드라이브로 임의의 Letter로 디스크 추가
                                    if (temp_createvmdatadisk.CreateVM_SR_Name_DATA_Add_Letter.Equals(string.Empty) && !stepfinish_blockdiskextend.SetBlockDiskExtend(true))
                                    {
                                        return Return_Value;
                                    }
                                    else if (!temp_createvmdatadisk.CreateVM_SR_Name_DATA_Add_Letter.Equals(string.Empty) && !stepfinish_blockdiskextend.SetBlockDiskExtend(temp_createvmdatadisk.CreateVM_SR_Name_DATA_Add_Letter.Trim(), true))     //--> DriverLetter 값 미설정시 해당드라이브로 임의의 Letter로 디스크 추가
                                    {
                                        return Return_Value;
                                    }
                                    //if (!stepfinish_blockdiskextend.SetBlockDiskExtend("E", true))  //--> DriverLetter 값 설정시 해당드라이브로 디스크 추가 또는 확장
                                    //if (!stepfinish_blockdiskextend.SetBlockDiskExtend(true))     

                                    string temp = Convert.ToString(stepfinish_blockdiskextend);
                                    int cnt = 0;

                                    while (tempOrder2.ContainsKey(temp))
                                    {
                                        cnt++;
                                        temp = Convert.ToString(stepfinish_blockdiskextend) + "_" + Convert.ToString(cnt); //1 ~ 9까지만 가능
                                        //temp = Convert.ToString(Windows_License_Activation_1) + "/" + Convert.ToString(cnt); //이렇게 넣으면 갯수 상관없음
                                    }

                                    tempOrder2.Add(temp, stepfinish_blockdiskextend);
                                }
                            }
                        }
                    }
                     */
                    #endregion

                    using (NCCWorkFlow.NCCProvAgent.Sleep step3_sleep = new NCCWorkFlow.NCCProvAgent.Sleep())
                    {
                        if (!step3_sleep.SetSleep(AD_SYNC_WAIT_FINISH_SECONDS))
                        {
                            return Return_Value;
                        }
                        tempOrder3.Add(Convert.ToString(step3_sleep), step3_sleep);
                    }

                    using (NCCWorkFlow.NCCProvAgent.StateCallBack step3_statecallback = new NCCWorkFlow.NCCProvAgent.StateCallBack())
                    {
                        if (!step3_statecallback.SetCallback(NCCWorkFlow.NCCProvAgent.AgentKeywordSet.A_StateCallBack_Add_YN_Kind.No, StateCallBack_IPAddressPort_IPPORT, NCCWorkFlow.NCCProvAgent.AgentKeywordSet.A_StateCallBack_CurrentState_Key_Kind.Unknown, StateCallBack_CREATE_ID, NCCWorkFlow.NCCProvAgent.AgentKeywordSet.A_StateCallBack_MachineType_Key_Kind.Unknown))
                        {
                            return Return_Value;
                        }
                        tempOrder3.Add(Convert.ToString(step3_statecallback), step3_statecallback);
                    }

                    #region AutoLoginDelete > 주석 - 현재 Sample Json 내 없음
                    /*
                    using (NCCWorkFlow.NCCProvAgent.AutoLoginDelete step3_AutoLoginDelete = new NCCWorkFlow.NCCProvAgent.AutoLoginDelete(NCCWorkFlow.NCCProvAgent.AgentKeywordSet.A_AutoLogin_Remove_Win_Version_Key_Kind.Win10))
                    {
                        if (!step3_AutoLoginDelete.SetStepRemove(true))
                        {
                            return Return_Value;
                        }
                        tempOrder3.Add(Convert.ToString(step3_AutoLoginDelete), step3_AutoLoginDelete);
                    }
                     */
                    #endregion

                    using (NCCWorkFlow.NCCProvAgent.Shutdown step3_shutdown = new NCCWorkFlow.NCCProvAgent.Shutdown())
                    {
                        if (!step3_shutdown.SetReboot(NCCWorkFlow.NCCProvAgent.AgentKeywordSet.A_Shutdown_Key_Kind.Force))
                        {
                            return Return_Value;
                        }
                        tempOrder3.Add(Convert.ToString(step3_shutdown), step3_shutdown);
                    }

                    nccagent.StepOrder.Add(NCCWorkFlow.NCCProvAgent.AgentKeywordSet.S_Finish_Key, tempOrder3);
                    #endregion
                    //-----------------------------Finish 단계 끝---------------------------------------//

                    ActionStep.Add(Convert.ToString(nccagent), nccagent);

                    string json = JsonConvert.SerializeObject(nccagent.StepOrder);
                    var filePath = string.Format(@"D:\Temp\{0}.json", VMName);
                    var file = new FileInfo(filePath);
                    if (!(file.Exists))
                    {
                        file.Directory.Create();
                    }

                    System.IO.File.WriteAllText(filePath, json);
                    Return_Value = true;
                }
            }
            catch (Exception ex)
            {
                Return_Value = false;
            }

            return Return_Value;
        }
        #endregion "VMWare AGENT WORKFLOW STEP"

        /// <summary>
        /// Json File Copy for AgentWorkflow Execute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resetSystem"></param>
        /// <returns></returns>
        /*
        public T FileCopyToVM<T>(string filepath)
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
                        //Copy-VMGuestFile -Source D:\Temp\MyApp.war -Destination D:\Tomcat\webapps\MyApp.war -LocalToGuest -VM MYAPPWeb01 -GuestUser Administrator -GuestPassword xxxxxxxxxx -Force
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
        */

        public T FileCopyToVM<T>(VMGuestFileModel copyFile)
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
                        CredCommands = CenterCred();

                        foreach (var item in CredCommands)
                        {
                            pPs.AddScript(item);
                        }

                        StringBuilder sb = new StringBuilder();
                        
                        // Try&Catch로 진행 >> Exception확인 // Debugging시 Result확인가능
                        sb.AppendFormat(@"try {{Copy-VMGuestFile -Source ""{0}"" -Destination ""{1}"" -LocalToGuest -VM ""{2}"" -GuestUser {3} -GuestPassword {4} -Force}}catch{{$retFileCopy.ErrorDescription=$_.Exception.Message}}", copyFile.Source, copyFile.Destination, copyFile.VMName, copyFile.GuestUser, copyFile.GuestPassword);

                        string[] scripts = new string[3];
                        scripts[0] = "$retFileCopy=\"\" | select ErrorDescription";
                        scripts[1] = sb.ToString();
                        Trace.WriteLine(scripts[1]);
                        scripts[2] = "$retFileCopy";

                        for (int i = 0; i < scripts.Length; i++)
                        {
                            pPs.AddScript(scripts[i]);
                        }

                        result = pPs.Invoke();

                        if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                        {
                            Trace.WriteLine("[CreatePool] VMWare.vCenter.FileCopy Failed");
                            throw new CliException(CliException.Reason.GetData);
                        }

                        retValue = GetResult<T>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.GetData, "VMWare.vCenter.FileCopy Exception", ex);
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

            //if (retValue is IList)
            //{
            //    Type t = retValue.GetType().GetGenericArguments()[0];
            //    foreach (PSObject r in psData)
            //    {
            //        object listitem = Activator.CreateInstance(t);
            //        foreach (PSMemberInfo m in r.Members)
            //        {
            //            fieldName = m.Name;
            //            if (t.GetProperty(fieldName) != null)
            //            {
            //                t.GetProperty(fieldName).SetValue(listitem, m.Value, null);
            //            }
            //        }
            //        (retValue as IList).Add(listitem);
            //    }
            //}
            //else
            //{
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
            //}

            return retValue;
        }

    }
}

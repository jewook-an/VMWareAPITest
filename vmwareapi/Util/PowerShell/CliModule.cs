using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
using System.Collections;

namespace vmwareapi.Util.PowerShell
{
    public class CliModule
    {
        private System.Management.Automation.PowerShell pPs = null;

        /// <summary>
        /// 생성자
        /// </summary>
        public CliModule()
        {
            pPs = System.Management.Automation.PowerShell.Create();
            //Initialize();
        }

        /// <summary>
        /// 서명되지 않은 스크립트를 실행 (기본 PowerShell Admin Execute)
        /// </summary>
        public void PolicySetting()
        {
            try
            {
                pPs.Commands.AddScript("Set-ExecutionPolicy Unrestricted -force");
                pPs.Invoke();
            }
            catch (Exception ex)
            {
                throw new CliException(CliException.Reason.PolicySetting, "Set-ExecutionPolicy Unrestricted Exception", ex);
            }
        }

        public void Initialize()
        {
            try
            {
                PSSnapInException ex = null;
                pPs.Runspace.RunspaceConfiguration.AddPSSnapIn("VMware.VimAutomation.Core", out ex);

                if (ex != null)
                {
                    throw new CliException(CliException.Reason.Initialze, "VMWare.VimAutomation.Core SnapIn Exception", ex);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ImportPowerCLI()
        {
            Collection<PSObject> result = null;
            try
            {
                // Import Module
                pPs.Commands.Clear();
                pPs.AddScript(@"import-module Vmware.PowerCLI");
                pPs.Invoke();

                // MessageBroker Server Connect
                // pPs.Commands.Clear();
                // pPs.Commands.AddScript(string.Format("Connect-ViewConnServer -user '{0}' -password '{1}' -domain '{2}' -viewConnServer '{3}'", mUsername, mPassword, mDomain, mServer));
                //result = pPs.Invoke();

                //if (result.Count == 0 || (result.Count > 0 && result[0] == null))
                //{
                //    throw new CliException(CliException.Reason.ImportModule);
                //}
                //mConnected = true;

                //if (pPs == null) pPs = PowerShell.Create();

                pPs.Commands.Clear();
                pPs.AddScript("Add-PSSnapin VMware.View.Broker");
                pPs.Invoke();

                pPs.Commands.Clear();
                pPs.AddScript("Get-PSSnapin VMware.View.Broker");
                result = pPs.Invoke();
                if (result.Count == 0)
                {
                    throw new CliException(CliException.Reason.AddSnapIn, "VMWare.View.Broker SnapIn Exception");
                }

                pPs.Commands.Clear();
                pPs.AddScript("Add-PSSnapin VMware.VimAutomation.Core");
                pPs.Invoke();

                pPs.Commands.Clear();
                pPs.AddScript("Get-PSSnapin VMware.VimAutomation.Core");
                result = pPs.Invoke();
                if (result.Count == 0)
                {
                    //throw new ConnectException(ConnectException.Reason.AddSnapIn, "VMware.VimAutomation.Core SnapIn Exception");
                }

                //pPs.Commands.Clear();
                //pPs.AddScript(POWERSHELL_FUNCTION);
                //result = pPs.Invoke();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T Execute<T>(string script)
        {
            T retValue = default(T);

            pPs.Commands.Clear();
            pPs.Commands.AddScript(script);
            retValue = Convert<T>(pPs.Invoke());

            return retValue;
        }

        public T Execute<T>(string[] scripts)
        {
            T retValue = default(T);

            pPs.Commands.Clear();

            foreach (string script in scripts)
            {
                pPs.Commands.AddScript(script);
            }
            retValue = Convert<T>(pPs.Invoke());

            return retValue;
        }

        private T Convert<T>(Collection<PSObject> psData)
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
                            if (m.Value != null)
                            {
                                if (t.GetProperty(fieldName).PropertyType.Equals(m.Value.GetType()) == true)
                                {
                                    t.GetProperty(fieldName).SetValue(listitem, m.Value, null);
                                }
                                else
                                {
                                    t.GetProperty(fieldName).SetValue(listitem, m.Value.ToString(), null);
                                }
                            }
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
                    if (r != null)
                    {
                        foreach (var property in retValue.GetType().GetProperties())
                        {
                            foreach (PSMemberInfo m in r.Members)
                            {
                                if (0 == string.Compare(property.Name, m.Name))
                                {
                                    if (property.PropertyType.ToString() == "System.String" && m.GetType().ToString() == "System.Management.Automation.PSNoteProperty")
                                    {
                                        if (m.Value != null) property.SetValue(retValue, m.Value.ToString(), null);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            if (m.Value != null) property.SetValue(retValue, m.Value, null);
                                        }
                                        catch (Exception)
                                        {
                                            if (property.PropertyType.ToString() == "System.String")
                                            {
                                                if (m.Value != null) property.SetValue(retValue, m.Value.ToString(), null);
                                            }
                                        }
                                    }

                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return retValue;
        }
    }
}

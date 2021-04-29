using NCCRequireService.Util.PowerShell;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using vmwareapi.vmware.vcenter.VM.Model;

namespace vmwareapi.vmware.vcenter.VM
{
	public class VMManager : Connect
	{
		public enum PowerActionKind { PowerOn = 1, ForceShutdown = 2, ForceReboot = 3, ForceSuspend = 4, GuestReboot = 11, GuestShutdown = 12, GuestStandby = 13, }

		List<string> Commands = new List<string>();

		public VMManager(string IPAddress, string LoginID, string LoginPassword, string Protocol = "https", bool AutoLogin = true) : base(IPAddress, LoginID, LoginPassword, Protocol = "https", AutoLogin = true)
		{
			if (AutoLogin)
			{
				Commands.Clear();
				Commands = LoginCommands();
			}
		}

		public DataTable GetAllVMList()
		{
			DataTable ret = new DataTable();

			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					Commands.Add($@"Get-VM");
					ret = powerCli.Run_Table(Commands);
				}
			}
			catch
			{
				throw;
			}

			return ret;
		}

		public DataTable GetAllTemplateList()
		{
			DataTable ret = new DataTable();

			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					// Commands.Add($@"Get-Template -Location DatacenterName");		// -Location "데이터센터명"
					Commands.Add($@"Get-Template");
					ret = powerCli.Run_Table(Commands);
				}
			}
			catch
			{
				throw;
			}

			return ret;
		}

		public List<DataTable> GetVM(List<string> VMLists)
		{
			List<DataTable> ret = new List<DataTable>();

			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					DataTable dt = new DataTable();

					string VMs = string.Join(", ", VMLists?.Where(x => !string.IsNullOrEmpty(x)).ToList());

					Commands.Add($@"Get-VM -Name {VMs}");
					dt = powerCli.Run_Table(Commands);
					dt.TableName = "GetVM";
					ret.Add(dt);
				}

				using (PowerCli powerCli = new PowerCli())
				{
					DataTable dt = new DataTable();

					string VMs = string.Join(", ", VMLists?.Where(x => !string.IsNullOrEmpty(x)).ToList());

					Commands.Add($@"Get-VM -Name {VMs} | Get-HardDisk");
					dt = powerCli.Run_Table(Commands);
					dt.TableName = "GetHardDisk";
					ret.Add(dt);
				}

			}
			catch
			{
				throw;
			}

			return ret;
		}

		public void SetPower(List<string> VMLists, PowerActionKind powerActionKind)
		{
			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					string VMs = string.Join(", ", VMLists?.Where(x => !string.IsNullOrEmpty(x)).ToList());

					switch (true)
					{
						case bool _ when powerActionKind == PowerActionKind.PowerOn:
							{
								Commands.Add($@"Start-VM -VM {VMs}");
								break;
							}
						case bool _ when powerActionKind == PowerActionKind.ForceShutdown:
							{
								Commands.Add($@"Stop-VM -VM {VMs} -Confirm:$False");
								break;
							}
						case bool _ when powerActionKind == PowerActionKind.ForceReboot:
							{
								Commands.Add($@"Restart-VM -VM {VMs} -Confirm:$False");
								break;
							}
						case bool _ when powerActionKind == PowerActionKind.ForceSuspend:
							{
								Commands.Add($@"Suspend-VM -VM {VMs} -Confirm:$False");
								break;
							}
						case bool _ when powerActionKind == PowerActionKind.GuestReboot:
							{
								Commands.Add($@"Restart-VMGuest -VM {VMs} -Confirm:$False");
								break;
							}
						case bool _ when powerActionKind == PowerActionKind.GuestShutdown:
							{
								Commands.Add($@"Shutdown-VMGuest -VM {VMs} -Confirm:$False");
								break;
							}
						case bool _ when powerActionKind == PowerActionKind.GuestStandby:
							{
								Commands.Add($@"Suspend-VMGuest -VM {VMs} -Confirm:$False");
								break;
							}
					}

					powerCli.Run(Commands);
				}
			}
			catch
			{
				throw;
			}
		}

		public void SetResource(List<DataDef.SetResourceChange> resourceChangesList)
		{
			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					for (int i = 0; resourceChangesList != null && i < resourceChangesList?.Count; i++)
					{
						if (string.IsNullOrEmpty(resourceChangesList?[i].VMName))
						{
							continue;
						}

						Commands.Add($@"$HotAddVMs = Get-Datacenter | Get-VM -Name {resourceChangesList?[i].VMName} | Select Name, @{{N=""CpuHotAddEnabled"";E={{$_.ExtensionData.Config.CpuHotAddEnabled}}}}, @{{N=""MemoryHotAddEnabled"";E={{$_.ExtensionData.Config.MemoryHotAddEnabled}}}}");
						string Command_VMHotAddEnabled = string.Empty;
						Command_VMHotAddEnabled += $@"
foreach($HotAddVM in $HotAddVMs) {{
	#if ($HotAddVM.CpuHotAddEnabled -Eq $False -Or $HotAddVM.MemoryHotAddEnabled -Eq $False)
	{{
		#VM 전원 강제 종료함 (HotAddEnabled가 적용안되어 있는것. 본사에서는 HotAddEbaled 속성이 되어 있어도 즉시 반영이 되지 않아 일단 종료함)
		Stop-VM -VM $HotAddVM.Name -Confirm:$False
	}}
}}

#본사에서는 강제 종료함
Stop-VM -VM {string.Join(", ", resourceChangesList?.Where(x => !string.IsNullOrEmpty(x.VMName)).ToList())}";

						Commands.Add($@"{Command_VMHotAddEnabled}");
					}

					{
						//본사에서는 강제 종료하여 10초 기다림
						//Wait 10
						Commands.Add($@"Start-Sleep -s 10");
					}

					for (int i = 0; resourceChangesList != null && i < resourceChangesList?.Count; i++)
					{
						string AppendCommand = string.Empty;
						AppendCommand += resourceChangesList?[i].vCPU % 2 == 0
											? $@" -CoresPerSocket 2"
											: string.Empty;
						AppendCommand += resourceChangesList?[i].vCPU >= 1 ? $@" -NumCpu {resourceChangesList?[i].vCPU}" : string.Empty;
						AppendCommand += resourceChangesList?[i].MemoryMB >= 1024 ? $@" -MemoryMB {resourceChangesList?[i].MemoryMB}" : string.Empty;

						Commands.Add($@"Get-VM -Name {resourceChangesList?[i].VMName} | Set-VM {AppendCommand} -Confirm:$False");

						for (int j = 0; j < resourceChangesList?[i].Disks.Count; j++)
						{
							if (!string.IsNullOrEmpty(resourceChangesList?[i].Disks[j].DiskName) && resourceChangesList?[i].Disks[j]?.DiskAction == DataDef.SetResourceChange.Disk.DiskActionKind.Change)
							{
								Commands.Add($@"$hdd_{i}_{j} = Get-VM -Name {resourceChangesList?[i].VMName} | Get-HardDisk -Name '{resourceChangesList?[i]?.Disks[j].DiskName}'");
								Commands.Add($@"Set-HardDisk -HardDisk $hdd_{i}_{j} -CapacityKB {resourceChangesList?[i]?.Disks[j]?.DiskCapacityKB} -Confirm:$False");
							}
							else if (resourceChangesList?[i].Disks[j]?.DiskAction == DataDef.SetResourceChange.Disk.DiskActionKind.Add)
							{
								Commands.Add($@"Get-VM -Name {resourceChangesList?[i].VMName} | New-HardDisk -CapacityKB {resourceChangesList?[i]?.Disks[j]?.DiskCapacityKB} -Persistence persistent");
							}
							else if (resourceChangesList?[i].Disks[j]?.DiskAction == DataDef.SetResourceChange.Disk.DiskActionKind.Delete)
							{
								Commands.Add($@"Get-VM -Name {resourceChangesList?[i].VMName} | Get-HardDisk -Name '{resourceChangesList?[i]?.Disks[j].DiskName}' | Remove-HardDisk -Confirm:$False");
							}
						}
					}

					powerCli.Run(Commands);
				}
			}
			catch
			{
				throw;
			}
		}

		public void SetCopyGuestFile(DataDef.SetCopyVMGuestFile copyFileInfo)
		{
			try
			{
				//var PSScript = string.Format("Copy-VMGuestFile -Source {} -Destination ""{copyFileInfo.Destination}"" -LocalToGuest -VM ""{copyFileInfo.VMName}"" -GuestUser {copyFileInfo.GuestUser} -GuestPassword '{copyFileInfo.GuestPassword}' -Force");
				using (PowerCli powerCli = new PowerCli())
				{
					Commands.Add($@"Copy-VMGuestFile -Source {copyFileInfo.Source} -Destination {copyFileInfo.Destination} -LocalToGuest -VM {copyFileInfo.VMName} -GuestUser {copyFileInfo.GuestUser} -GuestPassword {copyFileInfo.GuestPassword} -Force");
					powerCli.Run(Commands);
				}

			}
			catch (Exception)
			{
				throw;
			}
		}

		public DataTable AvailableVMList(string lang, string cluster)
		{
			DataTable ret = new DataTable();

			try
			{
				// 조건 : configOS > Win10, Snapshot(X) > Clone된, 
				using (PowerCli powerCli = new PowerCli())
				{
					// -eq : == / -ne : != / -lt : < / -le : <= / -gt : > / -ge : >=
					string configOS = lang == "KO" ? "MicrosoftWindows10(64비트)" : "MicrosoftWindows10(64-bit)";
					Commands.Add($@"Get-VM -Location {cluster} | Sort | Get-View -Property @('Name', 'Config.GuestFullName', 'Guest.GuestFullName', 'Snapshot.RootSnapshotList') | Select -Property Name, @{{N='ConfiguredOS';E={{$_.Config.GuestFullName -replace ' ', ''}}}},  @{{N='RunningOS';E={{$_.Guest.GuestFullName}}}}, @{{N='SnapShot';E={{$_.Snapshot.RootSnapshotList.Name}}}} | Where-Object {{($_.Snapshot.Length -eq 0) -and ($_.ConfiguredOS -eq '{configOS}')}}");

					ret = powerCli.Run_Table(Commands);
				}
			}
			catch
			{
				throw;
			}

			return ret;
		}

		public DataTable AvailableTemplateList(string lang)
		{
			DataTable ret = new DataTable();
			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					string configOS = lang == "KO" ? "MicrosoftWindows10(64비트)" : "MicrosoftWindows10(64-bit)";
					Commands.Add($@"Get-Template | Sort | Get-View -Property @('Name', 'Config.GuestFullName') | Select -Property Name, @{{N='ConfiguredOS';E={{$_.Config.GuestFullName -replace ' ', ''}}}} | Where-Object {{$_.ConfiguredOS -eq '{configOS}'}}");
					ret = powerCli.Run_Table(Commands);
				}
			}
			catch
			{
				throw;
			}
			return ret;
		}

		public DataTable ParentVMList()
		{
			DataTable ret = new DataTable();
			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					string replicafolder = "VMwareViewComposerReplicaFolder";
					Commands.Add($@"Get-VM | Select-Object -Property Name, VMHost, @{{N = 'Cluster';E={{$_.VMHost.Parent}}}}, @{{N = 'Folder';E={{$_.Folder.Name}}}}, @{{N = 'SnapShot';E={{$_.ExtensionData.Snapshot}}}} | Where-Object {{($_.Folder -ne '{replicafolder}') -and ($_.Snapshot.Length -ne 0)}}");
					ret = powerCli.Run_Table(Commands);
				}
			}
			catch
			{
				throw;
			}
			return ret;
		}

		public DataTable GetSnapShotList(string ParentVMName)
		{
			DataTable ret = new DataTable();
			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					Commands.Add($@"Get-Snapshot -VM {ParentVMName}");
					ret = powerCli.Run_Table(Commands);
				}
			}
			catch
			{
				throw;
			}
			return ret;
		}

		public DataTable GetFolderList()
		{
			DataTable schdt = new DataTable();
			string DatacenterName = string.Empty;
			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					Commands.Add($@"Get-Folder -Location vm | Select-Object -Property ParentId, Parent, Name, Id");
					schdt = powerCli.Run_Table(Commands);
					for (int i = 0; i < schdt.Rows.Count; i++)
					{
						string folderid = schdt.Rows[i]["Id"].ToString();
						string foldername = schdt.Rows[i]["Name"].ToString();
						string Parentfolderid = schdt.Rows[i]["ParentId"].ToString();
						string Parentfoldername = schdt.Rows[i]["Parent"].ToString();

						// 1Depth
						if (Parentfoldername == "vm")
						{
							// Parentfolder로 Datacenter Name 확인
							DatacenterName = GetFolderDatacenter(Parentfolderid);
							schdt.Rows[i]["Name"] = "/" + DatacenterName + "/vm/" + schdt.Rows[i]["Name"];
						}
						else
						{
							schdt.Rows[i]["Name"] = "/" + DatacenterName + "/vm" + GetFolderPath(folderid, schdt.Rows[i]["Name"].ToString());
						}
					}
				}
			}
			catch
			{
				throw;
			}
			return schdt;
		}

		public string GetFolderPath(string folderid, string foldername)
		{
			DataTable schdt = new DataTable();
			DataTable chkdt = new DataTable();
			string rtnFolder = "/" + foldername;
			string chkFolderid = folderid;
			int chkFolderCnt = 1;
			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					while (chkFolderCnt > 0)
					{
						Commands.Clear();
						Commands = LoginCommands();
						Commands.Add($@"Get-Folder -Location vm | Select-Object -Property ParentId, Parent, Name, Id | Where-Object {{$_.Id -eq '{chkFolderid}'}}");
						chkdt = powerCli.Run_Table(Commands);

						//PowerCLI Call 1Depth Reduce - vm folder call X
						if (chkdt == null || chkdt.Rows.Count == 0 || chkdt.Rows[0]["Parent"].ToString() == "vm")
						//if (chkdt == null || chkdt.Rows.Count == 0)
						{
							break;
						}
						else
						{
							rtnFolder = "/" + chkdt.Rows[0]["Parent"].ToString() + rtnFolder;
							chkFolderid = chkdt.Rows[0]["ParentId"].ToString();
							chkFolderCnt = chkdt.Rows.Count;
						}
						continue;
					}
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			return rtnFolder;
		}

		public string GetFolderDatacenter(string folderid)
		{
			DataTable ret = new DataTable();
			string DatacenterName = string.Empty;

			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					Commands.Clear();
					Commands = LoginCommands();
					Commands.Add($@"(Get-Datacenter).ExtensionData | Where {{$_.VmFolder -eq '{folderid}'}} | Select Name");
					ret = powerCli.Run_Table(Commands);

					if (ret == null || ret.Rows.Count == 0)
					{
						DatacenterName = "NoDataceter";
					}
					else
					{
						DatacenterName = ret.Rows[0]["Name"].ToString();
					}
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			return DatacenterName;
		}

		public DataTable GetClusterList()
		{
			DataTable ret = new DataTable();
			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					Commands.Add($@"Get-Cluster | Select Parentfolder, Name, @{{N='ClusterPath';E={{'/' + (Get-Datacenter -Cluster $_.Name).Name + '/' + $_.Parentfolder + '/' + $_.Name}}}}");
					ret = powerCli.Run_Table(Commands);
				}
			}
			catch
			{
				throw;
			}
			return ret;
		}

		public string GetClusterPath(string ClusterName)
		{
			DataTable ret = new DataTable();
			string ClusterPath = string.Empty;
			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					Commands.Add($@"Get-Cluster -Name {ClusterName} | Select @{{N='ClusterPath';E={{'/' + (Get-Datacenter -Cluster $_.Name).Name + '/' + $_.Parentfolder + '/' + $_.Name}}}}");
					ret = powerCli.Run_Table(Commands);

					ClusterPath = "Cluster Path : " + ret.Rows[0]["ClusterPath"].ToString();
				}
			}
			catch
			{
				throw;
			}
			return ClusterPath;
		}

		public DataTable GetClusterHost(string ClusterName)
		{
			DataTable ret = new DataTable();
			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					Commands.Add($@"Get-Cluster -Name {ClusterName} | Get-VMHost | Select Id, Name, State, Version, ParentId, Parent, NumCpu, CpuTotalMhz, CpuUsageMhz, MemoryTotalGB, MemoryUsageGB, MemoryTotalMB, MemoryUsageMB, StorageInfo, DatastoreIdList, @{{N='IPAddress';E={{($_.ExtensionData.Config.Network.Vnic | ? {{$_.Device -eq 'vmk0'}}).Spec.Ip.IpAddress}}}}");
					ret = powerCli.Run_Table(Commands);
				}
			}
			catch
			{
				throw;
			}
			return ret;
		}

		public DataTable GetResourcePoolList(string clusterPath)
		{
			DataTable ret = new DataTable();
			try
			{
				string[] cluster = clusterPath.Split('/');
				string clustername = cluster[cluster.Length-1].ToString();
				
				using (PowerCli powerCli = new PowerCli())
				{
					Commands.Add($@"Get-ResourcePool -Location {clustername} | Select ParentId, Parent, Id, Name, @{{N='ResourcePoolPath';E={{'{clusterPath}' + '/' + $_.Name}}}}");
					ret = powerCli.Run_Table(Commands);

					for (int i = 0; i < ret.Rows.Count; i++)
					{
						string ResourcePoolId = ret.Rows[i]["Id"].ToString();
						string ResourcePoolName = ret.Rows[i]["Name"].ToString();
						string ParentResourcePoolId = ret.Rows[i]["ParentId"].ToString();
						string ParentResourcePoolName = ret.Rows[i]["Parent"].ToString();

						// 1Depth
						if (ParentResourcePoolName == "Resources" || ParentResourcePoolName == clustername)
						{
							// @{{N='ResourcePoolPath';E={{'{clusterPath}' + '/' + $_.Name}}}}		// 기존처리
							ret.Rows[i]["Name"] = clusterPath + '/' + ret.Rows[i]["Name"];
						}
						else
						{
							ret.Rows[i]["Name"] = clusterPath + GetResourcePoolPath(clustername, ResourcePoolId, ret.Rows[i]["Name"].ToString());
						}
					}
				}
			}
			catch
			{
				throw;
			}
			return ret;
		}

		public string GetResourcePoolPath(string clustername, string ResourcePoolId, string ResourcePoolName)
		{
			DataTable schdt = new DataTable();
			DataTable chkdt = new DataTable();
			string rtnResourcepool = "/" + ResourcePoolName;		// Disuse
			string chkResourcePoolId = ResourcePoolId;				// 14391
			int chkresourcepoolCnt = 1;
			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					while (chkresourcepoolCnt > 0)
					{
						Commands.Clear();
						Commands = LoginCommands();

						//Get-ResourcePool -Location {clustername} | Select ParentId, Parent, Id, Name
						Commands.Add($@"Get-ResourcePool -Location {clustername} | Select ParentId, Parent, Id, Name | Where-Object {{$_.Id -eq '{chkResourcePoolId}'}}");
						chkdt = powerCli.Run_Table(Commands);

						//PowerCLI Call 1Depth Reduce - resourcepool call X
						if (chkdt == null || chkdt.Rows.Count == 0 || chkdt.Rows[0]["Parent"].ToString() == "Resources")
						//if (chkdt == null || chkdt.Rows.Count == 0)
						{
							break;
						}
						else
						{
							rtnResourcepool = "/" + chkdt.Rows[0]["Parent"].ToString() + rtnResourcepool;
							chkResourcePoolId = chkdt.Rows[0]["ParentId"].ToString();
							chkresourcepoolCnt = chkdt.Rows.Count;
						}
						continue;
					}
				}
			}
			catch (Exception ex)
			{
				throw;
			}
			return rtnResourcepool;
		}

		public DataTable GetClusterResourcePool(string clusterPath)
		{
			DataTable ret = new DataTable();
			try
			{
				string[] cluster = clusterPath.Split('/');
				string clustername = cluster[cluster.Length - 1].ToString();

				using (PowerCli powerCli = new PowerCli())
				{
					// 일단 보류
					clusterPath = GetClusterPath(clustername);

					Commands.Add($@"Get-Cluster -Name {clustername} | Get-ResourcePool | Select Id, Name, ParentId, Parent, CpuReservationMHz, CpuLimitMHz, MemSharesLevel, NumMemShares, MemReservationGB, MemLimitGB, ResourcePoolPath, ClusterPath");
					ret = powerCli.Run_Table(Commands);

					for (int i = 0; i < ret.Rows.Count; i++)
					{
						string ResourcePoolId = ret.Rows[i]["Id"].ToString();
						string ResourcePoolName = ret.Rows[i]["Name"].ToString();
						string ParentResourcePoolId = ret.Rows[i]["ParentId"].ToString();
						string ParentResourcePoolName = ret.Rows[i]["Parent"].ToString();

						ret.Rows[i]["ClusterPath"] = clusterPath;
						// 1Depth
						if (ParentResourcePoolName == "Resources" || ParentResourcePoolName == clustername)
						{
							ret.Rows[i]["ResourcePoolPath"] = clusterPath + '/' + ret.Rows[i]["Name"];
						}
						else
						{
							ret.Rows[i]["ResourcePoolPath"] = clusterPath + GetResourcePoolPath(clustername, ResourcePoolId, ret.Rows[i]["Name"].ToString());
						}
					}
				}
			}
			catch
			{
				throw;
			}
			return ret;
		}

		public DataTable GetDatastoreList(string hostandcluster)
		{
			DataTable ret = new DataTable();
			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					Commands.Add($@"Get-Datastore | Select Name, Accessible, @{{N='DatastorePath';E={{'{hostandcluster}' + '/' + $_.Name}}}} | Where-Object {{$_.Accessible -eq 'True'}}");
					ret = powerCli.Run_Table(Commands);
				}
			}
			catch
			{
				throw;
			}
			return ret;
		}

		public DataTable GetClusterDataStore(string clustername, string clusterpath)
		{
			DataTable ret = new DataTable();
			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					Commands.Add($@"Get-Cluster -Name {clustername} | Get-Datastore | Select Name, Accessible, @{{N='DatastorePath';E={{'{clusterpath}' + '/' + $_.Name}}}} | Where-Object {{$_.Accessible -eq 'True'}}");
					ret = powerCli.Run_Table(Commands);
				}
			}
			catch
			{
				throw;
			}
			return ret;
		}

		public DataTable GetClusterNetworkID(string ClusterName)
		{
			DataTable ret = new DataTable();
			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					Commands.Add($@"(Get-Cluster -Name {ClusterName}).ExtensionData.Network | Select @{{N='NetworkID';E={{$_.Type + '-' + $_.Value}}}}");
					ret = powerCli.Run_Table(Commands);
				}
			}
			catch
			{
				throw;
			}
			return ret;
		}

		public DataTable GetVLANList()
		{
			DataTable ret = new DataTable();
			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					Commands.Add($@"Get-VirtualNetwork | Sort -Property Name | Select Id, Name, NetworkType");
					ret = powerCli.Run_Table(Commands);
				}
			}
			catch
			{
				throw;
			}
			return ret;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="infra">Location : Folder, Datacenter</param>
		/// <returns></returns>
		public DataTable GetVLAN(InfraModel infra)
		{
			DataTable ret = new DataTable();
			string sParam = "-Name " + infra.vLanName;
			try
			{
				if (infra.Search == "id")
				{
					sParam = "-Id " + infra.vLanId;
				}
				else if (infra.Search == "location")
				{
					sParam = "-Location " + infra.iLocation;
				}

				using (PowerCli powerCli = new PowerCli())
				{
					Commands.Add($@"Get-VirtualNetwork {sParam} | Select Id, Name, NetworkType");
					ret = powerCli.Run_Table(Commands);
				}
			}
			catch
			{
				throw;
			}
			return ret;
		}

		public DataTable GetVLANDetail(string vLanName)
		{
			DataTable ret = new DataTable();
			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					Commands.Add($@"(Get-VirtualNetwork -Name '{vLanName}').ExtensionData | Select Name, Host, Vm, Parent");
					ret = powerCli.Run_Table(Commands);
				}
			}
			catch
			{
				throw;
			}
			return ret;
		}

		public DataTable GetVLANSummary(string vLanName)
		{
			DataTable ret = new DataTable();
			try
			{
				using (PowerCli powerCli = new PowerCli())
				{
					Commands.Add($@"(Get-VirtualNetwork -Name '{vLanName}').ExtensionData.Summary | Select Network, Name, Accessible");
					ret = powerCli.Run_Table(Commands);
				}
			}
			catch
			{
				throw;
			}
			return ret;
		}

	}
}

using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestVmWareAPI
{
	public class gRPCRepository
	{
		Channel channel = null;
		NCCProto.Service.Boards.BoardService.BoardServiceClient client = null;
		NCCProto.Service.VMWareService.VMWareService.VMWareServiceClient clientvmw = null;
		public bool IsConnect = false;

		public gRPCRepository(List<string> IPAddressPort)
		{
			try
			{
				for (int i = 0; IPAddressPort != null && i < IPAddressPort.Count; i++)
				{
					try
					{
						channel = new Channel(IPAddressPort[i], ChannelCredentials.Insecure);
						client = new NCCProto.Service.Boards.BoardService.BoardServiceClient(channel);
						clientvmw = new NCCProto.Service.VMWareService.VMWareService.VMWareServiceClient(channel);
						IsConnect = true;

						break;
					}
					catch
					{
						continue;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
		public void Close()
		{
			if (channel != null)
			{
				channel.ShutdownAsync().Wait();
			}

			IsConnect = false;
		}

		public string SendCommand(string command, string data)
		{
			string returnValue = string.Empty;

			try
			{
				switch (command)
				{
					case "GetPoolVMStatus":
						{
							NCCProto.Service.VMWareService.VMStatusRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.VMStatusRequest>(data);

							NCCProto.Service.VMWareService.VMStatusResponse response = clientvmw.GetPoolVMStatus(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "GetPoolVM":
						{
							NCCProto.Service.VMWareService.PoolVMRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.PoolVMRequest>(data);

							NCCProto.Service.VMWareService.PoolVMResponse response = clientvmw.GetPoolVM(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "SetVMPower":
						{
							NCCProto.Service.VMWareService.VMPowerRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.VMPowerRequest>(data);

							NCCProto.Service.VMWareService.VMPowerResponse response = clientvmw.SetVMPower(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "Maintenance":
						{
							NCCProto.Service.VMWareService.VMMaintenanceRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.VMMaintenanceRequest>(data);

							NCCProto.Service.VMWareService.VMMaintenanceResponse response = clientvmw.SetMaintenanceModeVM(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "LogOff":
						{
							NCCProto.Service.VMWareService.VMSessionRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.VMSessionRequest>(data);

							NCCProto.Service.VMWareService.VMSessionResponse response = clientvmw.SetPSSessionMode(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "Message":
						{
							NCCProto.Service.VMWareService.VMMessageRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.VMMessageRequest>(data);

							NCCProto.Service.VMWareService.VMMessageResponse response = clientvmw.SendMessage(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "Discovery":
						{
							NCCProto.Service.VMWareService.VMWareInfraRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.VMWareInfraRequest>(data);

							NCCProto.Service.VMWareService.VMWareInfraResponse response = clientvmw.InfraInformation(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "NewDesktopPool":
						{
							NCCProto.Service.VMWareService.CreateDesktopPoolRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.CreateDesktopPoolRequest>(data);

							NCCProto.Service.VMWareService.CreateDesktopPoolResponse response = clientvmw.NewDesktopPool(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "ClusterInformation":
						{
							NCCProto.Service.VMWareService.GetClusterRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.GetClusterRequest>(data);

							NCCProto.Service.VMWareService.GetClusterResponse response = clientvmw.ClusterInformation(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "ClusterSummaryDetail":
						{
							NCCProto.Service.VMWareService.GetClusterRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.GetClusterRequest>(data);

							NCCProto.Service.VMWareService.GetClusterResponseDetail response = clientvmw.ClusterSummaryDetail(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "SetvCenter":
						{
							NCCProto.Service.VMWareService.CenterRegistRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.CenterRegistRequest>(data);

							NCCProto.Service.VMWareService.CenterRegistResponse response = clientvmw.SetVCenter(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "SetHorizon":
						{
							NCCProto.Service.VMWareService.HorizonRegistRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.HorizonRegistRequest>(data);

							NCCProto.Service.VMWareService.HorizonRegistResponse response = clientvmw.SetHorizon(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "GetClusterHost":
						{
							NCCProto.Service.VMWareService.GetClusterRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.GetClusterRequest>(data);

							NCCProto.Service.VMWareService.GetHostResponse response = clientvmw.GetClusterHost(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "GetClusterResourcePool":
						{
							NCCProto.Service.VMWareService.GetClusterRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.GetClusterRequest>(data);

							NCCProto.Service.VMWareService.GetResourcePoolResponse response = clientvmw.GetClusterResourcePool(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "GetClusterDataStore":
						{
							NCCProto.Service.VMWareService.GetClusterRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.GetClusterRequest>(data);

							NCCProto.Service.VMWareService.GetDataStoreResponse response = clientvmw.GetClusterDataStore(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "FolderDiscovery":
						{
							NCCProto.Service.VMWareService.FLDiscoveryRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.FLDiscoveryRequest>(data);

							NCCProto.Service.VMWareService.FLDiscoveryResponse response = clientvmw.FolderDiscovery(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "GetFolderInfo":
						{
							NCCProto.Service.VMWareService.GetFolderRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.GetFolderRequest>(data);

							NCCProto.Service.VMWareService.GetFolderResponse response = clientvmw.GetFolderList(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "GetClusterNetwork":
						{
							NCCProto.Service.VMWareService.GetClusterRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.GetClusterRequest>(data);

							NCCProto.Service.VMWareService.GetNetworkResponse response = clientvmw.GetClusterVLAN(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "CloneVM":
						{
							NCCProto.Service.VMWareService.CloneRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.CloneRequest>(data);

							NCCProto.Service.VMWareService.CloneResponse response = clientvmw.CloneAndAssign(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "SetVMName":
						{
							NCCProto.Service.VMWareService.SetVMRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.SetVMRequest>(data);

							NCCProto.Service.VMWareService.SetVMResponse response = clientvmw.SetVMName(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}
					case "ResetVM":
						{
							NCCProto.Service.VMWareService.ResetRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<NCCProto.Service.VMWareService.ResetRequest>(data);

							NCCProto.Service.VMWareService.ResetResponse response = clientvmw.CloneAndReSet(request);
							return Newtonsoft.Json.JsonConvert.SerializeObject(response);
						}

					default:
						break;
				}
			}
			catch(Exception)
			{
				throw;
			}

			return returnValue;
		}
	}
}

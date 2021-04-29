using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vmwareapi.vmware.horizon.Model;
using vmwareapi.vmware.horizon.Pool;
using vmwareapi.vmware.horizon.Provisioning;
using vmwareapi.vmware.vcenter.VM;
using vmwareapi.vmware.vcenter.VM.DataDef;


using NCCProto.Service;
using NCCProto.Service.VMWareService;
using TestVmWareAPI.HelperClasses;
using static NCCProto.Service.VMWareService.VMWareService;
using Grpc.Core;
using log4net;
using System.Threading;
using TestVmWareAPI.EFDB;
using NCCFramework2.Util;
using vmwareapi.vmware.vcenter.VM.Model;

namespace TestVmWareAPI
{
	public partial class MainFrm : Form
	{
		public MainFrm()
		{
			InitializeComponent();
		}

		private async void btnvCenterVMList_Click(object sender, EventArgs e)
		{
			lstvCenterResult.Items.Clear();
			try
			{
				await Task.Run(() =>
				{
					DataTable dt = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).GetAllVMList();

					if (dt == null || dt.Rows.Count == 0)
					{
						MessageBox.Show("받은 데이터가 없습니다.");
						return;
					}

					for (int i = 0; i < dt.Rows.Count; i++)
					{
						lstvCenterResult.Invoke(new MethodInvoker(delegate ()
						{
							lstvCenterResult.Items.Add(dt.Rows[i]["Name"]);
						}));

					}
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private async void btnHorizonVMList_Click(object sender, EventArgs e)
		{
			lstHorizonResult.Items.Clear();
			try
			{
				await Task.Run(() =>
				{
					DataTable dt = new ViewPool(txtHorizonURL.Text, txtHorizonID.Text, txtHorizonPW.Text).GetAllVMList();

					if (dt == null || dt.Rows.Count == 0)
					{
						MessageBox.Show("받은 데이터가 없습니다.");
						return;
					}

					for (int i = 0; i < dt.Rows.Count; i++)
					{
						lstHorizonResult.Invoke(new MethodInvoker(delegate ()
						{
							// Name, DnsName, DesktopName, BasicState, Type, OperatingSystem, AgentVersion
							lstHorizonResult.Items.Add(dt.Rows[i]["Name"]);
						}));
					}
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private async void btnHorizonPoolList_Click(object sender, EventArgs e)
		{
			lstHorizonResult.Items.Clear();
			try
			{
				await Task.Run(() =>
				{
					//DataTable dt1 = new ViewPool(txtHorizonURL.Text, txtHorizonID.Text, txtHorizonPW.Text).chkLogin();
					DataTable dt = new ViewPool(txtHorizonURL.Text, txtHorizonID.Text, txtHorizonPW.Text).GetAllPoolList();

					if (dt == null || dt.Rows.Count == 0)
					{
						MessageBox.Show("받은 데이터가 없습니다.");
						return;
					}

					for (int i = 0; i < dt.Rows.Count; i++)
					{
						lstHorizonResult.Invoke(new MethodInvoker(delegate ()
						{
							lstHorizonResult.Items.Add(dt.Rows[i]["Name"]);
						}));

					}
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private async void btnVMDetails_Click(object sender, EventArgs e)
		{
			try
			{
				for (int i = 1; i <= 5; i++)
				{
					Control[] temp_nud_ctl = this.Controls.Find($@"nudvCenterHdd{i}", true);

					if (temp_nud_ctl != null && temp_nud_ctl.Length == 1)
					{
						NumericUpDown temp_nud = temp_nud_ctl[0] as NumericUpDown;
						temp_nud.Value = 0;
					}

					Control[] temp_lbl_ctl = this.Controls.Find($@"lblvCenterHdd{i}", true);

					if (temp_lbl_ctl != null && temp_lbl_ctl.Length == 1)
					{
						Label temp_lbl = temp_lbl_ctl[0] as Label;
						temp_lbl.Text = "-";
						temp_lbl.Tag = null;
					}
				}

				await Task.Run(() =>
				{
					List<DataTable> dt_List = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).GetVM(new List<string> { txtvCenterVMName.Text });

					if (dt_List == null || dt_List.Count == 0)
					{
						MessageBox.Show("받은 데이터가 없습니다.");
						return;
					}

					foreach (DataTable dt in dt_List ?? Enumerable.Empty<DataTable>())
					{
						if (dt.TableName.Equals("GetVM", StringComparison.OrdinalIgnoreCase))
						{
							if (dt != null && dt.Rows.Count > 0)
							{
								this.Invoke(new MethodInvoker(delegate ()
								{
									lblPowerState.Text = Convert.ToString(dt.Rows[0]["PowerState"]);
									nudvCenterVCPUCore.Value = Convert.ToDecimal(dt.Rows[0]["NumCPU"] ?? -1);
									nudvCenterRAM.Value = Convert.ToDecimal(dt.Rows[0]["MemoryGB"] ?? -1);
								}));
							}
						}
						else if (dt.TableName.Equals("GetHardDisk", StringComparison.OrdinalIgnoreCase))
						{
							if (dt != null && dt.Rows.Count > 0)
							{
								this.Invoke(new MethodInvoker(delegate ()
								{
									for (int i = 0; i < dt.Rows.Count; i++)
									{
										Control[] temp_nud_ctl = this.Controls.Find($@"nudvCenterHdd{i + 1}", true);

										if (temp_nud_ctl != null && temp_nud_ctl.Length == 1)
										{
											NumericUpDown temp_nud = temp_nud_ctl[0] as NumericUpDown;
											temp_nud.Value = 1;
										}

										Control[] temp_lbl_ctl = this.Controls.Find($@"lblvCenterHdd{i + 1}", true);

										if (temp_lbl_ctl != null && temp_lbl_ctl.Length == 1)
										{
											Label temp_lbl = temp_lbl_ctl[0] as Label;
											temp_lbl.Text = Convert.ToString(dt.Rows[i]["CapacityGB"]) + " GB";
											temp_lbl.Tag = Convert.ToString(dt.Rows[i]["Name"]);
										}
									}
								}));
							}
						}
					}
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private void MainFrm_Load(object sender, EventArgs e)
		{
			foreach (var key in Enum.GetValues(typeof(VMManager.PowerActionKind)))
			{
				cmbvCenterPowerKind.Items.Add(Convert.ToString(key));
			}

			cmbvCenterPowerKind.SelectedIndex = cmbvCenterPowerKind.Items.Count > 0 ? 0 : -1;
			cmbLanguage.SelectedIndex = 0;
		}

		private async void btnvCenterPowerRun_Click(object sender, EventArgs e)
		{
			await Task.Run(() =>
			{
				this.Invoke(new MethodInvoker(delegate ()
				{
					new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).SetPower(
							new List<string> { txtvCenterVMName.Text },
							(VMManager.PowerActionKind)Enum.Parse(typeof(VMManager.PowerActionKind), cmbvCenterPowerKind.Text)
					);
				}));
			});
		}

		private async void lblvCenterCPU_DoubleClick(object sender, EventArgs e)
		{
			await Task.Run(() =>
			{
				this.Invoke(new MethodInvoker(delegate ()
				{
				}));
			});
		}

		private async void lblvCenterRam_DoubleClick(object sender, EventArgs e)
		{
			await Task.Run(() =>
			{
				this.Invoke(new MethodInvoker(delegate ()
				{
				}));
			});
		}

		private async void btnvCenterResourceChange_Click(object sender, EventArgs e)
		{
			try
			{
				List<SetResourceChange.Disk> Disk_List = new List<SetResourceChange.Disk>();
				
				for (int i = 1; i < 6; i++)
				{
					Control[] temp_lbl_ctl = this.Controls.Find($@"lblvCenterHdd{i}", true);

					if (temp_lbl_ctl != null && temp_lbl_ctl.Length == 1)
					{
						Label temp_lbl = temp_lbl_ctl[0] as Label;

						//if (temp_lbl.Tag != null)
						{
							Control[] temp_nud_ctl = this.Controls.Find($@"nudvCenterHdd{i}", true);

							if (temp_nud_ctl != null && temp_nud_ctl.Length == 1)
							{
								NumericUpDown temp_nud = temp_nud_ctl[0] as NumericUpDown;

								//if (temp_nud.Value > 0)
								{
									int temp_int = 0;

									Disk_List.Add(
										new SetResourceChange.Disk()
										{
											DiskAction = temp_lbl.Text.Equals("New", StringComparison.OrdinalIgnoreCase)								//New 키워드가 있으면 Disk 추가
															? SetResourceChange.Disk.DiskActionKind.Add
															: temp_lbl.Text.Equals("Delete", StringComparison.OrdinalIgnoreCase)						//Delete 키워드가 있으면 Disk 삭제
																? SetResourceChange.Disk.DiskActionKind.Delete
																: temp_lbl.Text.IndexOf("GB") >= 0 && int.TryParse(temp_lbl.Text.Substring(0, temp_lbl.Text.IndexOf("GB")), out temp_int)   //XX GB에 XX가 숫자이면 Disk 용량 변경
																	? SetResourceChange.Disk.DiskActionKind.Change
																	: SetResourceChange.Disk.DiskActionKind.None,
											DiskCapacityKB = (
																	((int)(temp_nud.Value))
																+	(
																		temp_lbl.Text.IndexOf("GB") >= 0 && int.TryParse(temp_lbl.Text.Substring(0, temp_lbl.Text.IndexOf("GB")), out temp_int)
																			? int.Parse(temp_lbl.Text.Substring(0, temp_lbl.Text.IndexOf("GB")))
																			: 0
																	)
															  ) * 1024L * 1024L,
											DiskName = Convert.ToString(temp_lbl.Tag),
										}
									);
								}
							}
						}
					}
				}

				SetResourceChange setResourceChange = new SetResourceChange()
				{
					VMHost = txtvCenterURL.Text,
					VMName = txtvCenterVMName.Text,
					vCPU = (int)nudvCenterVCPUCore.Value,
					MemoryMB = (int)nudvCenterRAM.Value * 1024,
					Disks = Disk_List,
				};

				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).SetResource(
								new List<SetResourceChange>()
								{
									setResourceChange,
								}
						); ;
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private async void btnvCenterHost_Click(object sender, EventArgs e)
		{
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string cencluster = txtCenterCluster.Text;

						if (string.IsNullOrEmpty(cencluster))
						{
							MessageBox.Show("Cluster 정보를 입력하세요.");
							return;
						}
						lstvCenterResult.Items.Clear();

						string[] cluster = cencluster.Split('/');
						string clustername = cluster[cluster.Length - 1].ToString();

						DataTable dt = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).GetClusterHost(clustername);

						if (dt == null || dt.Rows.Count == 0)
						{
							MessageBox.Show("받은 데이터가 없습니다.");
							return;
						}

						for (int i = 0; i < dt.Rows.Count; i++)
						{
							lstvCenterResult.Items.Add(dt.Rows[i]["Name"]);
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}

		}

		private async void btnvCenterHostJson_Click(object sender, EventArgs e)
		{
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string cencluster = txtCenterCluster.Text;

						if (string.IsNullOrEmpty(cencluster))
						{
							MessageBox.Show("Cluster 정보를 입력하세요.");
							return;
						}

						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(txtServiceIP.Text + ":" + txtServicePort.Text);

						gRPCRepository tr = new gRPCRepository(IPAddressPort);
						try
						{
							string[] cluster = cencluster.Split('/');
							string clustername = cluster[cluster.Length - 1].ToString();

							GetClusterRequest req = new GetClusterRequest()
							{
								ClusterUUID = "",
								ClusterName = clustername,
								VCenterUUID = "efddc656-7523-49c0-939d-e5211f482365",
								VCenterName = "vCenter Namu",
							};

							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(req);
							dynamic result = tr.SendCommand("GetClusterHost", input);
							rtbResultVI.Text = Newtonsoft.Json.Linq.JToken.Parse(result).ToString(Newtonsoft.Json.Formatting.Indented);
							MessageBox.Show("Host Information Read Success.");
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						finally
						{
							tr.Close();
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private async void btnvCenterStorage_Click(object sender, EventArgs e)
		{
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string cencluster = txtCenterCluster.Text;

						if (string.IsNullOrEmpty(cencluster))
						{
							MessageBox.Show("Cluster 정보를 입력하세요.");
							return;
						}
						lstvCenterResult.Items.Clear();

						string[] cluster = cencluster.Split('/');
						string clustername = cluster[cluster.Length - 1].ToString();

						DataTable dt = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).GetClusterDataStore(clustername, cencluster);

						if (dt == null || dt.Rows.Count == 0)
						{
							MessageBox.Show("받은 데이터가 없습니다.");
							return;
						}

						for (int i = 0; i < dt.Rows.Count; i++)
						{
							lstvCenterResult.Items.Add(dt.Rows[i]["DatastorePath"]);
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private async void btnvCenterStorageJson_Click(object sender, EventArgs e)
		{
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string cencluster = txtCenterCluster.Text;

						if (string.IsNullOrEmpty(cencluster))
						{
							MessageBox.Show("Cluster 정보를 입력하세요.");
							return;
						}

						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(txtServiceIP.Text + ":" + txtServicePort.Text);

						gRPCRepository tr = new gRPCRepository(IPAddressPort);
						try
						{
							string[] cluster = cencluster.Split('/');
							string clustername = cluster[cluster.Length - 1].ToString();

							GetClusterRequest req = new GetClusterRequest()
							{
								ClusterUUID = "",
								ClusterName = clustername,
								VCenterUUID = "efddc656-7523-49c0-939d-e5211f482365",
								VCenterName = "vCenter Namu",
							};

							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(req);
							dynamic result = tr.SendCommand("GetClusterDataStore", input);
							rtbResultVI.Text = Newtonsoft.Json.Linq.JToken.Parse(result).ToString(Newtonsoft.Json.Formatting.Indented);
							MessageBox.Show("Storage Information Read Success.");
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						finally
						{
							tr.Close();
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private void lblvCenterHDD2_DoubleClick(object sender, EventArgs e)
		{
			Label temp_lbl = null;

			if(sender is Label)
			{
				temp_lbl = sender as Label;
			}

			switch(true)
			{
				case bool _ when temp_lbl.Text == "-":
					{
						temp_lbl.Text = "NEW";

						Control[] temp_nud_ctl = this.Controls.Find($@"nudvCenterHdd{temp_lbl.Name.Substring(temp_lbl.Name.Length - 1, 1)}", true);
						if (temp_nud_ctl != null && temp_nud_ctl.Length == 1)
						{
							NumericUpDown temp_nud = temp_nud_ctl[0] as NumericUpDown;
							temp_nud.Value = 1;
						}

						break;
					}

				case bool _ when Convert.ToString(temp_lbl.Text).Equals("Delete", StringComparison.OrdinalIgnoreCase) && temp_lbl.Text.Equals("Delete", StringComparison.OrdinalIgnoreCase):
					{
						temp_lbl.Text = "New";

						Control[] temp_nud_ctl = this.Controls.Find($@"nudvCenterHdd{temp_lbl.Name.Substring(temp_lbl.Name.Length - 1, 1)}", true);
						if (temp_nud_ctl != null && temp_nud_ctl.Length == 1)
						{
							NumericUpDown temp_nud = temp_nud_ctl[0] as NumericUpDown;
							temp_nud.Value = 1;
						}

						break;
					}

				case bool _ when Convert.ToString(temp_lbl.Text).Equals("New", StringComparison.OrdinalIgnoreCase) && temp_lbl.Text.Equals("New", StringComparison.OrdinalIgnoreCase):
					{
						temp_lbl.Text = "DELETE";

						Control[] temp_nud_ctl = this.Controls.Find($@"nudvCenterHdd{temp_lbl.Name.Substring(temp_lbl.Name.Length - 1, 1)}", true);
						if (temp_nud_ctl != null && temp_nud_ctl.Length == 1)
						{
							NumericUpDown temp_nud = temp_nud_ctl[0] as NumericUpDown;
							temp_nud.Value = 0;
						}

						break;
					}

				default:
					{
						temp_lbl.Text = "-";

						Control[] temp_nud_ctl = this.Controls.Find($@"nudvCenterHdd{temp_lbl.Name.Substring(temp_lbl.Name.Length - 1, 1)}", true);
						if (temp_nud_ctl != null && temp_nud_ctl.Length == 1)
						{
							NumericUpDown temp_nud = temp_nud_ctl[0] as NumericUpDown;
							temp_nud.Value = 0;
						}

						break;
					}
			}
		}

		public class poolmodel
		{
			string Name { get; set; }
			string Type { get; set; }
			string Source { get; set; }
			string NumMachines { get; set; }
			string NumSessions { get; set; }
		}

        private async void btnPoolDetails_Click(object sender, EventArgs e)
		{
			try
			{
				var test = nudNumMachines.Value;

				await Task.Run(() =>
				{

					//var getP = new ViewPool(txtHorizonURL.Text, txtHorizonID.Text, txtHorizonPW.Text).GetPool<poolmodel>(txtHorizonPoolName.Text);
					List<DataTable> dt_List = new ViewPool(txtHorizonURL.Text, txtHorizonID.Text, txtHorizonPW.Text).GetPool(new List<string> { txtHorizonPoolName.Text });

					if (dt_List == null || dt_List.Count == 0)
					{
						MessageBox.Show("받은 데이터가 없습니다.");
						return;
					}

					foreach (DataTable dt in dt_List ?? Enumerable.Empty<DataTable>())
					{
						if (dt.TableName.Equals("GetHVPool", StringComparison.OrdinalIgnoreCase))
						{
							if (dt != null && dt.Rows.Count > 0)
							{
								this.Invoke(new MethodInvoker(delegate ()
								{
									Utils.SelectItemByValue(cmbPoolType, Convert.ToString(dt.Rows[0]["Type"]));
									//Utils.SelectItemByValue(cmbUserAssign, Convert.ToString(dt.Rows[0]["UserAssignment"]));
									Utils.SelectItemByValue(cmbPoolSource, Convert.ToString(dt.Rows[0]["Source"]));
									Utils.SelectItemByValue(cmbPoolEnabled, Convert.ToString(dt.Rows[0]["Enabled"]));
									Utils.SelectItemByValue(cmbProvisionEnabled, Convert.ToString(dt.Rows[0]["ProvisioningEnabled"]));
									nudNumMachines.Value = Convert.ToDecimal(dt.Rows[0]["NumMachines"] ?? -1);
									nudNumSessions.Value = Convert.ToDecimal(dt.Rows[0]["NumSessions"] ?? -1);
								}));
							}
                            else
                            {
								MessageBox.Show("해당 Pool 정보가 존재하지 않습니다.");
								return;
							}
						}
						else if (dt.TableName.Equals("GetHVPoolPath", StringComparison.OrdinalIgnoreCase))
						{
							if (dt != null && dt.Rows.Count > 0)
							{
								this.Invoke(new MethodInvoker(delegate ()
								{
									txtTemplatePath.Text = Convert.ToString(dt.Rows[0]["TemplatePath"]);
									txtParentVMPath.Text = Convert.ToString(dt.Rows[0]["ParentVmPath"]);
									txtSnapshotPath.Text = Convert.ToString(dt.Rows[0]["SnapshotPath"]);
									txtDatacenterPath.Text = Convert.ToString(dt.Rows[0]["DatacenterPath"]);
									txtVmFolderPath.Text = Convert.ToString(dt.Rows[0]["VmFolderPath"]);
									txtHostOrClusterPath.Text = Convert.ToString(dt.Rows[0]["HostOrClusterPath"]);
									txtResourcePoolPath.Text = Convert.ToString(dt.Rows[0]["ResourcePoolPath"]);
									txtDatastorePaths.Text = Convert.ToString(dt.Rows[0]["DatastorePaths"]);
								}));
							}
                            else
							{
								txtTemplatePath.Text = string.Empty;
								txtParentVMPath.Text = string.Empty;
								txtSnapshotPath.Text = string.Empty;
								txtDatacenterPath.Text = string.Empty;
								txtVmFolderPath.Text = string.Empty;
								txtHostOrClusterPath.Text = string.Empty;
								txtResourcePoolPath.Text = string.Empty;
								txtDatastorePaths.Text = string.Empty;
							}
                        }
                        else if (dt.TableName.Equals("GetHVPoolUserAssign", StringComparison.OrdinalIgnoreCase))
						{
							if (dt != null && dt.Rows.Count > 0)
							{
								string strUserAssign = Convert.ToString(dt.Rows[0]["UserAssignment"]);
								this.Invoke(new MethodInvoker(delegate ()
								{
									chkUserAutoAssign.Enabled = true;
									Utils.SelectItemByValue(cmbUserAssign, strUserAssign);
									//cmbUserAssign.SelectedText = Convert.ToString(dt.Rows[0]["UserAssignment"]);
									chkUserAutoAssign.Checked = Convert.ToBoolean(dt.Rows[0]["AutomaticAssignment"]);
                                    if (strUserAssign == "FLOATING")
                                    {
										chkUserAutoAssign.Enabled = false;
                                    }
								}));
							}
							else
							{
								return;
							}
						}
						else if (dt.TableName.Equals("GetHVPoolNameSet", StringComparison.OrdinalIgnoreCase))
						{
							if (dt != null && dt.Rows.Count > 0)
							{
								this.Invoke(new MethodInvoker(delegate ()
								{
									txtNamingRule.Text = Convert.ToString(dt.Rows[0]["NamingPattern"]);
									//txtNamingRule.Text = Convert.ToString(dt.Rows[0]["MaxNumberOfMachines"]);
									//txtNamingRule.Text = Convert.ToString(dt.Rows[0]["NumberOfSpareMachines"]);
									//txtNamingRule.Text = Convert.ToString(dt.Rows[0]["ProvisioningTime"]);
								}));
							}
							else
							{
								return;
							}
						}
						else if (dt.TableName.Equals("GetHVPoolGuestSet", StringComparison.OrdinalIgnoreCase))
						{
							if (dt != null && dt.Rows.Count > 0)
							{
								this.Invoke(new MethodInvoker(delegate ()
								{
									Utils.SelectItemByValue(cmbCustType, Convert.ToString(dt.Rows[0]["CustomizationType"]));
								}));
							}
							else
							{
								return;
							}
						}
					}
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private void txtHorizonPoolName_DragDrop(object sender, DragEventArgs e)
		{
			string temp = Convert.ToString(e.Data.GetData(typeof(string)));

			(sender as TextBox).Text = temp;
		}

		private void txtHorizonPoolName_DragEnter(object sender, DragEventArgs e)
		{
			if ((e.AllowedEffect & DragDropEffects.Copy) != 0 && e.Data.GetDataPresent(typeof(string)))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

        private void cmbPoolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPoolType.SelectedIndex == -1)
            {
				cmbPoolType.Enabled = true;
			}
            else
            {
				string pooltype = cmbPoolType.SelectedItem.ToString();
                //cmbPoolType.Enabled = false;
                if (pooltype == "MANUAL")
                {
					cmbPoolSource.SelectedItem = "VIRTUAL_CENTER";
					cmbPoolSource.Enabled = false;

					cmbCustType.SelectedIndex = -1;
					cmbCustType.Enabled = false;

					txtHorizonSystemName.BackColor = Color.AntiqueWhite;

					txtTemplatePath.BackColor = Color.White;
					txtDatacenterPath.BackColor = Color.White;
					txtVmFolderPath.BackColor = Color.White;
					txtHostOrClusterPath.BackColor = Color.White;
					txtResourcePoolPath.BackColor = Color.White;
					txtDatastorePaths.BackColor = Color.White;

					txtTemplatePath.Text = string.Empty;
					txtDatacenterPath.Text = string.Empty;
					txtVmFolderPath.Text = string.Empty;
					txtHostOrClusterPath.Text = string.Empty;
					txtResourcePoolPath.Text = string.Empty;
					txtDatastorePaths.Text = string.Empty;

					txtTemplatePath.Enabled = false;
					txtDatacenterPath.Enabled = false;
					txtVmFolderPath.Enabled = false;
					txtHostOrClusterPath.Enabled = false;
					txtResourcePoolPath.Enabled = false;
					txtDatastorePaths.Enabled = false;
				}
                else
				{
					cmbPoolSource.Enabled = true;
					cmbCustType.Enabled = true;

					txtHorizonSystemName.BackColor = Color.White;

					txtTemplatePath.BackColor = Color.AntiqueWhite;
					txtDatacenterPath.BackColor = Color.AntiqueWhite;
					txtVmFolderPath.BackColor = Color.AntiqueWhite;
					txtHostOrClusterPath.BackColor = Color.AntiqueWhite;
					txtResourcePoolPath.BackColor = Color.AntiqueWhite;
					txtDatastorePaths.BackColor = Color.AntiqueWhite;

					txtTemplatePath.Enabled = true;
					txtDatacenterPath.Enabled = true;
					txtVmFolderPath.Enabled = true;
					txtHostOrClusterPath.Enabled = true;
					txtResourcePoolPath.Enabled = true;
					txtDatastorePaths.Enabled = true;
				}
			}
        }

        private void cmbPoolSource_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbPoolSource.SelectedIndex == -1)
			{
				cmbPoolSource.Enabled = true;
			}
			else
			{
				string pooltype = cmbPoolType.SelectedItem.ToString();
				string poolsource = cmbPoolSource.SelectedItem.ToString();
				//cmbPoolSource.Enabled = false;
				if (poolsource == "VIEW_COMPOSER")
				{
					txtTemplatePath.BackColor = Color.White;
					txtParentVMPath.BackColor = Color.AntiqueWhite;
					txtSnapshotPath.BackColor = Color.AntiqueWhite;
					txtTemplatePath.Enabled = false;
					txtParentVMPath.Enabled = true;
					txtSnapshotPath.Enabled = true;

					cmbCustType.Enabled = true;
				}
                else
                {
					cmbCustType.Enabled = true;

					txtTemplatePath.BackColor = Color.AntiqueWhite;
					txtParentVMPath.BackColor = Color.White;
					txtSnapshotPath.BackColor = Color.White;
					txtTemplatePath.Enabled = true;

					txtParentVMPath.Enabled = false;
					txtSnapshotPath.Enabled = false;
					//Template, VMfolder, host&cluster, Resource, datastore
					txtParentVMPath.Text = string.Empty;
					txtSnapshotPath.Text = string.Empty;
                }
			}
		}

        private void cmbUserAssign_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (cmbUserAssign.SelectedIndex == -1)
			{
				cmbUserAssign.Enabled = true;
			}
			else
			{
				string userassign = cmbUserAssign.SelectedItem.ToString();
				//cmbUserAssign.Enabled = false;
			}
		}

		/*
        private async void btnNewPool_Click(object sender, EventArgs e)
        {
            try
			{
				PoolModel poolmodel = new PoolModel();
				ResultModel<bool> retValue = new ResultModel<bool>();

				bool tempResult = true;
				string tempDescription = string.Empty;
				char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

				await Task.Run(() =>
				{
					#region Process
					//01. PoolName, PoolDisplayName, Description
					//02. PoolType : -InstantClone, -LinkedClone, -FullClone, -Manual, -Rds
					//03. UserAssignment ( + AutomaticAssignment ) : FLOATING or DEDICATED (true or false)
					//04. Enable : True or False
					//05. EnableProvisioning : True or False
					//06. NamingPattern : test-{n:fixed=3}
					//07. MaximumCount
					//08. Template
					//09. ParentVM
					//10. SnapshotVM
					//11. Datacenter
					//12. VmFolder
					//13. HostOrCluster
					//14. ResourcePool
					//15. Datastores
					#endregion
					this.Invoke(new MethodInvoker(delegate ()
					{
						poolmodel.PoolName = txtHorizonPoolName.Text;
						poolmodel.PoolType = cmbPoolType.SelectedItem.ToString();
						poolmodel.UserAssignment = cmbUserAssign.SelectedItem.ToString();
						poolmodel.AutomaticAssignment = chkUserAutoAssign.Checked;
						poolmodel.Source = cmbPoolSource.SelectedItem.ToString();
						poolmodel.Enable = Convert.ToBoolean(cmbPoolEnabled.SelectedItem);
						poolmodel.EnableProvisioning = Convert.ToBoolean(cmbProvisionEnabled.SelectedItem);
						poolmodel.NamingPattern = txtNamingRule.Text;
						poolmodel.MaximumCount = Convert.ToInt32(nudNumMachines.Value);

						poolmodel.Template = txtTemplatePath.Text;
						poolmodel.ParentVM = txtParentVMPath.Text;
						poolmodel.SnapshotVM = txtSnapshotPath.Text;
						poolmodel.datacenter = txtDatacenterPath.Text;
						poolmodel.VmFolder = txtVmFolderPath.Text;
						poolmodel.HostOrCluster = txtHostOrClusterPath.Text;
						poolmodel.ResourcePool = txtResourcePoolPath.Text;
						// 사용할 사용자 정의 유형 (Full Clone, Linked Clone 풀 적용) >> 'CLONE_PREP', 'QUICK_PREP', 'SYS_PREP', 'NONE'
						poolmodel.CustType = cmbCustType.SelectedItem == null? "" : cmbCustType.SelectedItem.ToString();

						// 기본 처리 (example : -ProvisioningTime UP_FRONT -SysPrepName vmwarecust -CustType SYS_PREP)
						poolmodel.NamingMethod = "PATTERN";				// 기본값 : PATTERN (Instant Clone > PATTERN)
						poolmodel.ProvisioningTime = "UP_FRONT";        // 프로비저닝 시기 결정 (ON_DEMAND, UP_FRONT)  (기본값은 UP_FRONT / Full, Linked, Instant Clone Pools.)
						poolmodel.NetBiosName = txtDomainNetbios.Text;
						poolmodel.DomainAdmin = txtDomainAdminID.Text;
						poolmodel.SpareCount = 1;
						poolmodel.SysPrepName = string.Empty;

						string datastores = txtDatastorePaths.Text;
						if (string.IsNullOrEmpty(datastores))
                        {
							poolmodel.Datastores = null;
                        }
                        else
                        {
							string[] datastoreList = datastores.Split(delimiterChars);
							poolmodel.Datastores = datastoreList;
						}
						string vcentervm = txtHorizonSystemName.Text;
						if (string.IsNullOrEmpty(vcentervm))
                        {
							poolmodel.VM = null;
						}
                        else
                        {
							string[] vmList = vcentervm.Split(delimiterChars);
							poolmodel.VM = vmList;
                        }

						#region 추가 필요항목
						//1. Automatic
						//2. Manual
						//	- VM 지정 (Search 후) > Manual Pool에 적용가능한 VM 확인
						//	- Display Name, Description
						//	- 사용권한 부여 방식 >> New-HVEntitlement -User domain\group -ResourceName 'poolname' -Type Group
						#endregion

						ViewResult tempRet = new ViewPool(txtHorizonURL.Text, txtHorizonID.Text, txtHorizonPW.Text).NewPool<ViewResult>(poolmodel);

						retValue.Description = tempRet.ErrorDescription;

						if (!tempRet.Success)
						{
							tempResult = false;
							tempDescription = tempRet.ErrorDescription;
							MessageBox.Show(tempRet.ErrorDescription);
						}
						else
						{
							retValue.Result = tempResult;
							retValue.Success = tempResult;
							retValue.Description = tempDescription;
							MessageBox.Show("Success");
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}
		 */

		private async void btnNewPool_Click(object sender, EventArgs e)
		{
			Utils.Login clogin = Utils.GetCenterInfo(txtvCenterURL.Text);
			Utils.Login hlogin = Utils.GetHorizonInfo(txtHorizonURL.Text);
			try
			{
				PoolModel poolmodel = new PoolModel();
				ResultModel<bool> retValue = new ResultModel<bool>();

				bool tempResult = true;
				string tempDescription = string.Empty;
				char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						poolmodel.PoolName = txtHorizonPoolName.Text;
						poolmodel.PoolType = cmbPoolType.SelectedItem.ToString();
						poolmodel.UserAssignment = cmbUserAssign.SelectedItem.ToString();
						poolmodel.AutomaticAssignment = chkUserAutoAssign.Checked;
						poolmodel.Source = cmbPoolSource.SelectedItem.ToString();
						poolmodel.Enable = Convert.ToBoolean(cmbPoolEnabled.SelectedItem);
						poolmodel.EnableProvisioning = Convert.ToBoolean(cmbProvisionEnabled.SelectedItem);
						poolmodel.NamingPattern = txtNamingRule.Text;
						poolmodel.MaximumCount = Convert.ToInt32(nudNumMachines.Value);

						poolmodel.Template = txtTemplatePath.Text;
						poolmodel.ParentVM = txtParentVMPath.Text;
						poolmodel.SnapshotVM = txtSnapshotPath.Text;
						poolmodel.datacenter = txtDatacenterPath.Text;
						poolmodel.VmFolder = txtVmFolderPath.Text;
						poolmodel.HostOrCluster = txtHostOrClusterPath.Text;
						poolmodel.ResourcePool = txtResourcePoolPath.Text;
						// 사용할 사용자 정의 유형 (Full Clone, Linked Clone 풀 적용) >> 'CLONE_PREP', 'QUICK_PREP', 'SYS_PREP', 'NONE'
						poolmodel.CustType = cmbCustType.SelectedItem == null ? "" : cmbCustType.SelectedItem.ToString();

						// 기본 처리 (example : -ProvisioningTime UP_FRONT -SysPrepName vmwarecust -CustType SYS_PREP)
						poolmodel.NamingMethod = "PATTERN";             // 기본값 : PATTERN (Instant Clone > PATTERN)
						poolmodel.ProvisioningTime = "UP_FRONT";        // 프로비저닝 시기 결정 (ON_DEMAND, UP_FRONT)  (기본값은 UP_FRONT / Full, Linked, Instant Clone Pools.)
						poolmodel.NetBiosName = txtDomainNetbios.Text;
						poolmodel.DomainAdmin = txtDomainAdminID.Text;
						poolmodel.SpareCount = 1;
						poolmodel.SysPrepName = string.Empty;

						string datastores = txtDatastorePaths.Text;
						if (string.IsNullOrEmpty(datastores))
						{
							poolmodel.Datastores = null;
						}
						else
						{
							string[] datastoreList = datastores.Split(delimiterChars);
							poolmodel.Datastores = datastoreList;
						}
						string vcentervm = txtHorizonSystemName.Text;
						if (string.IsNullOrEmpty(vcentervm))
						{
							poolmodel.VM = null;
						}
						else
						{
							string[] vmList = vcentervm.Split(delimiterChars);
							poolmodel.VM = vmList;
						}

						//ViewResult tempRet = new ViewPool(hlogin.URL, hlogin.ID, hlogin.PW).NewPool<ViewResult>(poolmodel);

						//retValue.Description = tempRet.ErrorDescription;

						//if (!tempRet.Success)
						//{
						//	tempResult = false;
						//	tempDescription = tempRet.ErrorDescription;
						//	MessageBox.Show(tempRet.ErrorDescription);
						//}
						//else
						//{
						//	retValue.Result = tempResult;
						//	retValue.Success = tempResult;
						//	retValue.Description = tempDescription;
						//	MessageBox.Show("Success");
						//}

						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(txtServiceIP.Text + ":" + txtServicePort.Text);
						gRPCRepository tr = new gRPCRepository(IPAddressPort);
						try
						{
							//VMStatusRequest request = new VMStatusRequest()
							//{
							//	PoolName = txtHorizonPoolName.Text
							//};

							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(poolmodel);
							dynamic result = tr.SendCommand("NewDesktopPool", input);
							rtbResult.Text = (result != null) ? result.ToString() : string.Empty;
							//lstHorizonResult.Items.Add((result != null) ? result.ToString() : string.Empty);
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						finally
						{
							tr.Close();
						}

					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private async void btnEditPool_Click(object sender, EventArgs e)
        {
			try
			{
				PoolModel poolmodel = new PoolModel();
				ResultModel<bool> retValue = new ResultModel<bool>();

				bool tempResult = true;
				string tempDescription = string.Empty;

				await Task.Run(() =>
				{
                    #region Process
                    /*
					01. PoolDisplayName, Description
					02. Enable : True or False
					03. EnableProvisioning : True or False
					04. NamingPattern : test-{n:fixed=3}	>> 길이제한 15
					05. MaximumCount
					06. Template (Auto > Full Clone Only)
					07. ParentVM
					08. SnapshotVM
					09. VmFolder
					10. HostOrCluster
					11. ResourcePool
					12. Datastores
					*/
                    //poolmodel.PoolDisplayName = "";
                    //poolmodel.Description = "";
                    #endregion

                    this.Invoke(new MethodInvoker(delegate ()
					{
						poolmodel.PoolName = txtHorizonPoolName.Text;
						poolmodel.Enable = Convert.ToBoolean(cmbPoolEnabled.SelectedItem);
						poolmodel.EnableProvisioning = Convert.ToBoolean(cmbProvisionEnabled.SelectedItem);
						poolmodel.NamingPattern = txtNamingRule.Text;
						poolmodel.MaximumCount = Convert.ToInt32(nudNumMachines.Value);

						if (string.IsNullOrEmpty(poolmodel.PoolName))
						{
							MessageBox.Show("Pool 관련 정보가 필요합니다.");
							txtHorizonPoolName.Focus();
							return;
						}

						#region 추가내역 - 필요시 주석제거
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
						#endregion

						//PoolModel rtnPool = new ViewPool(txtHorizonURL.Text, txtHorizonID.Text, txtHorizonPW.Text).EditPool<PoolModel>(poolmodel);
						ViewResult tempRet = new ViewPool(txtHorizonURL.Text, txtHorizonID.Text, txtHorizonPW.Text).EditPool(poolmodel);

						retValue.Description = tempRet.ErrorDescription;
						
						if (!tempRet.Success)
						{
							tempResult = false;
							tempDescription = tempRet.ErrorDescription;
							MessageBox.Show(tempRet.ErrorDescription);
						}
                        else
                        {
							retValue.Result = tempResult;
							retValue.Success = tempResult;
							retValue.Description = tempDescription;
							MessageBox.Show("Success");
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private async void btnAvailableVMList_Click(object sender, EventArgs e)
        {
			lstHorizonResult.Items.Clear();
			bool chkAllVM = chkAllVMList.Checked;
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string clusterpath = txtHostOrClusterPath.Text;
						string Language = cmbLanguage.SelectedItem.ToString();

						if (string.IsNullOrEmpty(clusterpath))
						{
							MessageBox.Show("Cluster 정보를 입력하세요.");
							return;
						}

						string[] cluster = clusterpath.Split('/');
						string clustername = cluster[cluster.Length - 1].ToString();

						// vCenter내 전체 VM > 사용 불가능 OS, 마스터Image 제외 >> 너무 많아 Cluster 필터링
						DataTable dt = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).AvailableVMList(Language, clustername);
						// 현재 Pool 내 사용중인 VM List
						DataTable dtPoolVM = new ViewPool(txtHorizonURL.Text, txtHorizonID.Text, txtHorizonPW.Text).GetAllVMList();

						if (dt == null || dt.Rows.Count == 0)
						{
							MessageBox.Show("받은 데이터가 없습니다.");
							return;
						}

						for (int i = 0; i < dt.Rows.Count; i++)
						{
							if (chkAllVM)
							{
								lstHorizonResult.Items.Add(dt.Rows[i]["Name"]);
							}
							else
							{
								var results = dtPoolVM.Rows.Cast<DataRow>().FirstOrDefault(x => x.Field<string>("Name") == dt.Rows[i]["Name"].ToString());

								if (results == null)
								{
									lstHorizonResult.Items.Add(dt.Rows[i]["Name"]);
								}
							}
						}

                        if (lstHorizonResult.Items.Count == 0)
						{
							MessageBox.Show("Manual Pool 사용가능 VM이 없습니다.");
							return;
						}

					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private void btnAvailableVMList_MouseHover(object sender, EventArgs e)
        {
			string tooltipText = "\r\n";
			tooltipText += "1. VM이 지원되지 않는 게스트 OS를 사용함" + "\r\n";
			tooltipText += "2. VM이 다른 데스크톱 풀에서 이미 사용되고 있음" + "\r\n";
			tooltipText += "3. VM이 마스터 이미지로 사용됨" + "\r\n";
			this.toolTip1.ToolTipTitle = "호환되지 않는 항목 필터링 됨.";
			this.toolTip1.IsBalloon = true;
			this.toolTip1.SetToolTip(this.btnAvailableVMList, tooltipText);
        }

        private void txtHorizonSystemName_DragDrop(object sender, DragEventArgs e)
        {
			string temp = Convert.ToString(e.Data.GetData(typeof(string)));

			(sender as TextBox).Text = temp;
		}

        private void txtHorizonSystemName_DragEnter(object sender, DragEventArgs e)
        {
			if ((e.AllowedEffect & DragDropEffects.Copy) != 0 && e.Data.GetDataPresent(typeof(string)))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

        private void cmbPoolSource_MouseHover(object sender, EventArgs e)
		{
			//MessageBox.Show("cmbPoolSource MouseHover");
			string tooltipPoolsource = "\r\n";
			tooltipPoolsource += "1. VIEW_COMPOSER >> Linked Clone" + "\r\n";
			tooltipPoolsource += "2. VIRTUAL_CENTER >> Full Clone" + "\r\n";
			this.toolTip2.ToolTipTitle = "Pool Type Description";
			//this.toolTip2.IsBalloon = true;
			this.toolTip2.SetToolTip(this.cmbPoolSource, tooltipPoolsource);
		}

        private async void btnDeletePool_Click(object sender, EventArgs e)
        {
			try
			{
				PoolModel poolmodel = new PoolModel();
				ResultModel<bool> retValue = new ResultModel<bool>();

				bool tempResult = true;
				string tempDescription = string.Empty;

				await Task.Run(() =>
				{
					#region Process
					/*
					1. Pool 존재여부확인 >> Message
					2. Pool 삭제 >> Success 확인
					3. Pool VM 강제 Shutdown
					*/
					#endregion
					this.Invoke(new MethodInvoker(delegate ()
					{
						poolmodel.PoolName = txtHorizonPoolName.Text;

						List<DataTable> dt_List = new ViewPool(txtHorizonURL.Text, txtHorizonID.Text, txtHorizonPW.Text).GetPool(new List<string> { txtHorizonPoolName.Text });

						if (dt_List == null || dt_List.Count == 0)
						{
							MessageBox.Show("해당 Pool을 조회 할 수 없습니다.");
							return;
						}

						ViewResult tempRet = new ViewPool(txtHorizonURL.Text, txtHorizonID.Text, txtHorizonPW.Text).DeletePool(poolmodel);

						retValue.Description = tempRet.ErrorDescription;

						if (!tempRet.Success)
						{
							tempResult = false;
							tempDescription = tempRet.ErrorDescription;
							MessageBox.Show(tempRet.ErrorDescription);
						}
						else
						{
                            // Pool 삭제시 VM Power ON >> 삭제 불가 - Power Off 후처리
							DataTable poolVMList = new ViewPool(txtHorizonURL.Text, txtHorizonID.Text, txtHorizonPW.Text).PoolVMList(poolmodel.PoolName);

							if (poolVMList == null || poolVMList.Rows.Count == 0)
							{
							}
                            else
                            {
								for (int i = 0; i < poolVMList.Rows.Count; i++)
								{
									//dt.Rows[i]["Name"]);
									new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).SetPower(
											new List<string> { poolVMList.Rows[i]["Name"].ToString() },
											(VMManager.PowerActionKind)Enum.Parse(typeof(VMManager.PowerActionKind), VMManager.PowerActionKind.ForceShutdown.ToString())
									);
								}
                            }

							retValue.Result = tempResult;
							retValue.Success = tempResult;
							retValue.Description = tempDescription;
							MessageBox.Show("Success");
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private async void btnClonePool_Click(object sender, EventArgs e)
        {
			try
			{
				PoolModel poolmodel = new PoolModel();
				ResultModel<bool> retValue = new ResultModel<bool>();

				bool tempResult = true;
				string tempDescription = string.Empty;
				string selectedPool = lstHorizonResult.SelectedItem == null ? null : lstHorizonResult.SelectedItem.ToString();

				await Task.Run(() =>
				{
					#region Process
					/*
					01. PoolDisplayName, Description
					02. Enable : True or False
					03. EnableProvisioning : True or False
					04. NamingPattern : test-{n:fixed=3}	>> 길이제한 15
					05. MaximumCount
					*/
					#endregion

					this.Invoke(new MethodInvoker(delegate ()
					{
						if (string.IsNullOrEmpty(selectedPool))
                        {
							MessageBox.Show("Pool 검색후 복제할 Pool을 선택해 주세요!");
							return;
                        }
						poolmodel.PoolName = txtHorizonPoolName.Text;
						//poolmodel.PoolDisplayName = "";
						//poolmodel.Description = "";
						poolmodel.Enable = Convert.ToBoolean(cmbPoolEnabled.SelectedItem);
						poolmodel.EnableProvisioning = Convert.ToBoolean(cmbProvisionEnabled.SelectedItem);
						poolmodel.NamingPattern = txtNamingRule.Text;
						poolmodel.MaximumCount = Convert.ToInt32(nudNumMachines.Value);

						ViewResult tempRet = new ViewPool(txtHorizonURL.Text, txtHorizonID.Text, txtHorizonPW.Text).ClonePool(poolmodel, selectedPool);

						retValue.Description = tempRet.ErrorDescription;

						if (!tempRet.Success)
						{
							tempResult = false;
							tempDescription = tempRet.ErrorDescription;
							MessageBox.Show(tempRet.ErrorDescription);
						}
						else
						{
							retValue.Result = tempResult;
							retValue.Success = tempResult;
							retValue.Description = tempDescription;
							MessageBox.Show("Success");
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private async void btnTemplateList_Click(object sender, EventArgs e)
        {
			lstHorizonResult.Items.Clear();
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{

						string Language = cmbLanguage.SelectedItem.ToString();

						//string Language = cmbLanguage.SelectedText;
						// vCenter내 전체 Template
						DataTable dt = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).AvailableTemplateList(Language);

						if (dt == null || dt.Rows.Count == 0)
						{
							MessageBox.Show("받은 데이터가 없습니다.");
							return;
						}

						for (int i = 0; i < dt.Rows.Count; i++)
						{
							lstHorizonResult.Items.Add(dt.Rows[i]["Name"]);
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private async void btnParentVMList_Click(object sender, EventArgs e)
        {
			lstHorizonResult.Items.Clear();
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						DataTable dt = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).ParentVMList();

						if (dt == null || dt.Rows.Count == 0)
						{
							MessageBox.Show("받은 데이터가 없습니다.");
							return;
						}

						for (int i = 0; i < dt.Rows.Count; i++)
						{
							lstHorizonResult.Items.Add(dt.Rows[i]["Name"]);
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private async void btnSnapshotList_Click(object sender, EventArgs e)
        {
			lstHorizonResult.Items.Clear();
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						// ParentVM 선택후 Snapshot List Call
						string parentVM = txtParentVMPath.Text;
                        if (string.IsNullOrEmpty(parentVM))
                        {
							MessageBox.Show("Parent VM을 선택 해주세요.");
							return;
                        }

						DataTable dt = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).GetSnapShotList(parentVM);

						if (dt == null || dt.Rows.Count == 0)
						{
							MessageBox.Show("받은 데이터가 없습니다.");
							return;
						}

						for (int i = 0; i < dt.Rows.Count; i++)
						{
							lstHorizonResult.Items.Add(dt.Rows[i]["Name"]);
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private async void btnVMFolderList_Click(object sender, EventArgs e)
        {
			lstHorizonResult.Items.Clear();
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						DataTable dt = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).GetFolderList();

						if (dt == null || dt.Rows.Count == 0)
						{
							MessageBox.Show("받은 데이터가 없습니다.");
							return;
						}

						for (int i = 0; i < dt.Rows.Count; i++)
						{
							lstHorizonResult.Items.Add(dt.Rows[i]["Name"]);
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private async void btnHostClusterList_Click(object sender, EventArgs e)
        {
			lstHorizonResult.Items.Clear();
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						DataTable dt = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).GetClusterList();

						if (dt == null || dt.Rows.Count == 0)
						{
							MessageBox.Show("받은 데이터가 없습니다.");
							return;
						}

						for (int i = 0; i < dt.Rows.Count; i++)
						{
							lstHorizonResult.Items.Add(dt.Rows[i]["ClusterPath"]);
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private async void btnResourcePoolList_Click(object sender, EventArgs e)
        {
			lstHorizonResult.Items.Clear();
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string hostandcluster = txtHostOrClusterPath.Text;

                        if (string.IsNullOrEmpty(hostandcluster))
                        {
							MessageBox.Show("Host & Cluster 정보를 입력하세요.");
							return;
                        }

						DataTable dt = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).GetResourcePoolList(hostandcluster);

						if (dt == null || dt.Rows.Count == 0)
						{
							MessageBox.Show("받은 데이터가 없습니다.");
							return;
						}

						for (int i = 0; i < dt.Rows.Count; i++)
						{
							lstHorizonResult.Items.Add(dt.Rows[i]["Name"]);
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private async void btnDatastoreList_Click(object sender, EventArgs e)
        {
			lstHorizonResult.Items.Clear();
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string hostandcluster = txtHostOrClusterPath.Text;

						if (string.IsNullOrEmpty(hostandcluster))
						{
							MessageBox.Show("Host & Cluster 정보를 입력하세요.");
							return;
						}

						DataTable dt = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).GetDatastoreList(hostandcluster);

						if (dt == null || dt.Rows.Count == 0)
						{
							MessageBox.Show("받은 데이터가 없습니다.");
							return;
						}

						for (int i = 0; i < dt.Rows.Count; i++)
						{
							lstHorizonResult.Items.Add(dt.Rows[i]["DatastorePath"]);
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		#region DragDrop & DragEnter
		private void lstvCenterResult_MouseDown(object sender, MouseEventArgs e)
		{
			DoDragDrop(((ListBox)sender).Text, DragDropEffects.Copy);
		}

		private void lstHorizonResult_MouseDown(object sender, MouseEventArgs e)
		{
			DoDragDrop(((ListBox)sender).Text, DragDropEffects.Copy);
		}

		private void txtvCenterVMName_DragDrop(object sender, DragEventArgs e)
		{
			string temp = Convert.ToString(e.Data.GetData(typeof(string)));

			(sender as TextBox).Text = temp;
		}

		private void txtvCenterVMName_DragEnter(object sender, DragEventArgs e)
		{
			if ((e.AllowedEffect & DragDropEffects.Copy) != 0 && e.Data.GetDataPresent(typeof(string)))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void txtTemplatePath_DragDrop(object sender, DragEventArgs e)
		{
			string temp = Convert.ToString(e.Data.GetData(typeof(string)));
			(sender as TextBox).Text = temp;
		}

		private void txtTemplatePath_DragEnter(object sender, DragEventArgs e)
		{
			if ((e.AllowedEffect & DragDropEffects.Copy) != 0 && e.Data.GetDataPresent(typeof(string)))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

        private void txtParentVMPath_DragDrop(object sender, DragEventArgs e)
        {
			string temp = Convert.ToString(e.Data.GetData(typeof(string)));
			(sender as TextBox).Text = temp;
		}

        private void txtParentVMPath_DragEnter(object sender, DragEventArgs e)
        {
			if ((e.AllowedEffect & DragDropEffects.Copy) != 0 && e.Data.GetDataPresent(typeof(string)))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

        private void txtSnapshotPath_DragDrop(object sender, DragEventArgs e)
        {
			string temp = Convert.ToString(e.Data.GetData(typeof(string)));
			(sender as TextBox).Text = temp;
		}

        private void txtSnapshotPath_DragEnter(object sender, DragEventArgs e)
        {
			if ((e.AllowedEffect & DragDropEffects.Copy) != 0 && e.Data.GetDataPresent(typeof(string)))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

        private void txtVmFolderPath_DragDrop(object sender, DragEventArgs e)
        {
			string temp = Convert.ToString(e.Data.GetData(typeof(string)));
			(sender as TextBox).Text = temp;
		}

        private void txtVmFolderPath_DragEnter(object sender, DragEventArgs e)
        {
			if ((e.AllowedEffect & DragDropEffects.Copy) != 0 && e.Data.GetDataPresent(typeof(string)))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

        private void txtHostOrClusterPath_DragDrop(object sender, DragEventArgs e)
        {
			string temp = Convert.ToString(e.Data.GetData(typeof(string)));
			(sender as TextBox).Text = temp;
		}

        private void txtHostOrClusterPath_DragEnter(object sender, DragEventArgs e)
        {
			if ((e.AllowedEffect & DragDropEffects.Copy) != 0 && e.Data.GetDataPresent(typeof(string)))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

        private void txtResourcePoolPath_DragDrop(object sender, DragEventArgs e)
        {
			string temp = Convert.ToString(e.Data.GetData(typeof(string)));
			(sender as TextBox).Text = temp;
		}

        private void txtResourcePoolPath_DragEnter(object sender, DragEventArgs e)
        {
			if ((e.AllowedEffect & DragDropEffects.Copy) != 0 && e.Data.GetDataPresent(typeof(string)))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

        private void txtDatastorePaths_DragDrop(object sender, DragEventArgs e)
        {
			string temp = Convert.ToString(e.Data.GetData(typeof(string)));
			(sender as TextBox).Text = temp;
		}

        private void txtDatastorePaths_DragEnter(object sender, DragEventArgs e)
        {
			if ((e.AllowedEffect & DragDropEffects.Copy) != 0 && e.Data.GetDataPresent(typeof(string)))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}
        #endregion

        private void cmbCustType_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (cmbCustType.SelectedIndex == -1)
			{
				cmbCustType.Enabled = true;
			}
			else
			{
				string CustomizationType = cmbCustType.SelectedItem.ToString();
				//cmbCustType.Enabled = false;
			}
		}

        private async void btnAgentSetting_Click(object sender, EventArgs e)
        {
			try
			{
				AgentSettingModel agentmodel = new AgentSettingModel();
				SystemSetting horizonSet = new SystemSetting(txtHorizonURL.Text, txtHorizonID.Text, txtHorizonPW.Text);
				SystemSetting vCenterSet = new SystemSetting(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text);

				//bool tempResult = true;
				string tempDescription = string.Empty;

				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string DomainNetbios = txtDomainNetbios.Text;
						string DomainFQDN = txtDomainFQDN.Text;
						string DomainAdminID = txtDomainAdminID.Text;
						string DomainAdminPW = txtDomainAdminPW.Text;
						string DomainSetUserID = txtDomainSetUserID.Text;
						string DomainSetUserPW = txtDomainSetUserPW.Text;
						string DomainOUDC = txtDomainOUDC.Text;
						string AddressIP = txtAddressIP.Text;
						string AddressSubnet = txtAddressSubnet.Text;
						string AddressGateway = txtAddressGateway.Text;
						string AddressDNS1 = txtAddressDNS1.Text;
						string AddressDNS2 = txtAddressDNS2.Text;
						string AddressStateCallBack = txtAddressStateCallBack.Text;

						string VMName = txtHorizonSystemName.Text;
                        if (string.IsNullOrEmpty(VMName))
                        {
							MessageBox.Show("VM Name을 입력해 주세요");
							txtHorizonSystemName.Focus();
							return;
						}

						if (string.IsNullOrEmpty(DomainSetUserID) || string.IsNullOrEmpty(DomainSetUserPW))
						{
							MessageBox.Show("UserID & UserPW를 입력해 주세요");
							txtDomainSetUserID.Focus();
							return;
						}

						if (string.IsNullOrEmpty(AddressIP))
						{
							MessageBox.Show("IP Address를 입력해 주세요");
							txtAddressIP.Focus();
							return;
						}

						string ReturnValue = string.Empty;
						string JsonText = string.Empty;

						int StateCallBack_CREATE_ID = 160;
						int AD_SYNC_WAIT_FINISH_SECONDS = 5;

						bool rtnValue = horizonSet.SetAgentDataNew_VMWare(DomainNetbios, DomainFQDN, DomainAdminID, DomainAdminPW, DomainSetUserID, DomainOUDC, AddressIP, AddressSubnet, AddressGateway, AddressDNS1, AddressDNS2, AddressStateCallBack, StateCallBack_CREATE_ID, VMName, AD_SYNC_WAIT_FINISH_SECONDS);

						ViewResult result = null;

						if (rtnValue)
						{
							VMGuestFileModel copyFile = new VMGuestFileModel();
							copyFile.Source = string.Format("D:\\Temp\\{0}.json", VMName);
							copyFile.Destination = "${Env:ProgramFiles(x86)}\\Namutech\\ProvisioningAgent\\AgentWorkFlow.json";
							copyFile.GuestUser = DomainSetUserID;
							copyFile.GuestPassword = DomainSetUserPW;
							copyFile.VMName = VMName;

							result = vCenterSet.FileCopyToVM<ViewResult>(copyFile);
						}

						if (!result.Success)
						{
							MessageBox.Show(result.ErrorDescription);
						}
						else
						{
							MessageBox.Show("Success");
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private async void btnVMStatus_Click(object sender, EventArgs e)
		{
			try
            {
				await Task.Run(() =>
				{
					lstHorizonResult.Invoke(new MethodInvoker(delegate ()
					{
						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(txtServiceIP.Text + ":" + txtServicePort.Text);
						gRPCRepository tr = new gRPCRepository(IPAddressPort);
						try
						{
							//DBEntities db = new DBEntities();
							//var chk = db.TB_HORIZON_VIEW.Where(h  => h.HORIZON_URL == txtHorizonURL.Text).FirstOrDefault();

							PoolVMRequest request = new PoolVMRequest()
							{
								VmName = txtHorizonSystemName.Text,
								//HorizonName = chk.HORIZON_IP,
								//HorizonUUID = chk.HORIZON_UUID,
								HorizonName = txtHorizonName.Text,
								HorizonUUID = txtHorizonUUID.Text,
							};

							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(request);
							dynamic result = tr.SendCommand("GetPoolVM", input);
							rtbResult.Text = (result != null) ? result.ToString() : string.Empty;
							//lstHorizonResult.Items.Add((result != null) ? result.ToString() : string.Empty);
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						finally
						{
							tr.Close();
						}
					}));
				});

            }
            catch (Exception)
            {
                throw;
            }
		}

		private async void btnPoolVMStatus_Click(object sender, EventArgs e)
		{
			try
			{
				await Task.Run(() =>
				{
					lstHorizonResult.Invoke(new MethodInvoker(delegate ()
					{
						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(txtServiceIP.Text + ":" + txtServicePort.Text);
						gRPCRepository tr = new gRPCRepository(IPAddressPort);
						try
						{
							// Db Setting Error
							//DBEntities db = new DBEntities();
							//var chk = db.TB_HORIZON_VIEW.Where(h => h.HORIZON_URL == txtHorizonURL.Text).FirstOrDefault();
							//chk.HORIZON_IP

							VMStatusRequest request = new VMStatusRequest()
							{
								PoolName = txtHorizonPoolName.Text,
								HorizonName = txtHorizonName.Text,
								HorizonUUID = txtHorizonUUID.Text,
							};

							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(request);
							dynamic result = tr.SendCommand("GetPoolVMStatus", input);
							rtbResult.Text = (result != null) ? result.ToString() : string.Empty;
							//lstHorizonResult.Items.Add((result != null) ? result.ToString() : string.Empty);
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						finally
						{
							tr.Close();
						}
					}));
				});

			}
			catch (Exception)
			{
				throw;
			}
		}

		//public enum PowerActionKind { Start = 1, Restart = 2, ForceRestart = 3, Shutdown = 4, ForceShutdown = 5, Suspend = 6, Resume = 7, LogOff = 8, Disconnect = 9, MaintenanceOn = 10, MaintenanceOff = 11, }

		private async void btnVMAction_Click(object sender, EventArgs e)
        {
            try
            {
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(txtServiceIP.Text + ":" + txtServicePort.Text);
						gRPCRepository tr = new gRPCRepository(IPAddressPort);

						string vmname = txtHorizonSystemName.Text;
						string act_type = cmbVMAction.SelectedItem.ToString();

						if (string.IsNullOrEmpty(vmname))
						{
							MessageBox.Show("VM Name을 입력하세요.");
							return;
						}

						try
                        {
                            if (act_type == "MaintenanceOn" || act_type == "MaintenanceOff")
                            {
								VMMaintenanceRequest request = new VMMaintenanceRequest()
								{
									VmName = vmname,
									ModeYN = act_type == "MaintenanceOn" ? true : false,
								};
								dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(request);
								dynamic result = tr.SendCommand("Maintenance", input);
								rtbResult.Text = (result != null) ? result.ToString() : string.Empty;
							}
							else if (act_type == "LogOff")
							{
								VMSessionRequest request = new VMSessionRequest()
								{
									VmName = vmname,
								};
								dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(request);
								dynamic result = tr.SendCommand("LogOff", input);
								rtbResult.Text = (result != null) ? result.ToString() : string.Empty;
							}
							else
                            {
								VMPowerRequest request = new VMPowerRequest()
								{
									VMAction = Utils.fnPowerAction(act_type),
									VmName = vmname
								};
								dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(request);
								dynamic result = tr.SendCommand("SetVMPower", input);
								rtbResult.Text = (result != null) ? result.ToString() : string.Empty;
							}

                            //lstHorizonResult.Items.Add((result != null) ? result.ToString() : string.Empty);
                        }
						catch (Exception)
                        {
                            throw;
                        }
					}));
				});
			}
            catch (Exception)
            {
                throw;
            }

			/*
            if (cmbVMAction.SelectedIndex == -1)
            {
                MessageBox.Show("VM Action Select 필요");
            }
            else
            {
                //MessageBox.Show(act_type);
                switch (act_type)
                {
                    case "Start":
                    case "Restart":
                    case "ForceRestart":
                    case "Shutdown":
                    case "ForceShutdown":
                    case "Suspend":
                    case "Resume":
                    case "LogOff":
                    case "Disconnect":
                    case "MaintenanceOn":
                    case "MaintenanceOff":
                    default:
                        break;
                }
            }
			*/
        }

        private async void btnSendMessage_Click(object sender, EventArgs e)
        {
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(txtServiceIP.Text + ":" + txtServicePort.Text);
						gRPCRepository tr = new gRPCRepository(IPAddressPort);

						string vmname = txtHorizonSystemName.Text;
						string messagetype = cmbMessageType.SelectedItem.ToString();
						string sendmessage = txtSendMessage.Text;

						if (string.IsNullOrEmpty(vmname))
						{
							MessageBox.Show("VM Name을 입력하세요.");
							return;
						}

						if (string.IsNullOrEmpty(sendmessage))
						{
							MessageBox.Show("메시지를 입력하세요.");
							return;
						}

						try
						{
							VMMessageRequest request = new VMMessageRequest()
							{
								VmName = vmname,
								Message = sendmessage,
								MessageType = messagetype,
							};
							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(request);
							dynamic result = tr.SendCommand("Message", input);
							rtbResult.Text = (result != null) ? result.ToString() : string.Empty;
						}
						catch (Exception)
						{
							throw;
						}
					}));
				});
			}
			catch (Exception)
			{
				throw;
			}
		}

        private void btnNamuCenter_Click(object sender, EventArgs e)
        {
			txtvCenterURL.Text = "192.168.50.16";
			txtvCenterID.Text = "administrator@vsphere.local";
			txtvCenterPW.Text = "P@$$w0rd";
			txtServiceIP.Text = "192.168.50.70";
			txtServicePort.Text = "9721";
			txtDatacenterName.Text = "Datacenter";
			txtCenterUUID.Text = "efddc656-7523-49c0-939d-e5211f482365";
			txtCenterName.Text = "vCenter Namu";
		}

        private void btnSDSCenter_Click(object sender, EventArgs e)
        {
			txtvCenterURL.Text = "sbc-vcsa.cloud.corp.samsungelectronics.net";
			txtvCenterID.Text = "Cloud\\vdi_master";
			txtvCenterPW.Text = "wpfl21)@Cp";
			txtServiceIP.Text = "182.192.0.184";
			txtServicePort.Text = "9721";
			txtDatacenterName.Text = "Datacenter-SBC";
			txtCenterUUID.Text = "7bd72515-5ae2-4f75-95f1-d2b80105e671";
			txtCenterName.Text = "vCenter SDS";
		}

        private void btnNamuHorizon_Click(object sender, EventArgs e)
        {
			txtHorizonURL.Text = "nm-vwcs.namurnd.io";
			txtHorizonID.Text = "namurnd\\administrator";
			txtHorizonPW.Text = "namudev!23$";
			txtServiceIP.Text = "192.168.50.70";
			txtServicePort.Text = "9721";
			txtHorizonUUID.Text = "5f0cb840-4391-47a0-ae2d-b84e1a30cd86";
			txtHorizonName.Text = "nm-vwcs.namurnd.io";
		}

        private void btnSDSHorizon_Click(object sender, EventArgs e)
        {
			txtHorizonURL.Text = "sec.sbc-cloud.com";
			txtHorizonID.Text = "Cloud\\vdi_master";
			txtHorizonPW.Text = "wpfl21)@Cp";
			txtServiceIP.Text = "182.192.0.184";
			txtServicePort.Text = "9721";
			txtHorizonUUID.Text = "dda2c190-2ff8-4f11-838f-259acc89d7e";
			txtHorizonName.Text = "Horizon View SDS";
		}

        private async void btnvCenterResourcePool_Click(object sender, EventArgs e)
		{
			lstvCenterResult.Items.Clear();
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string cencluster = txtCenterCluster.Text;

						if (string.IsNullOrEmpty(cencluster))
						{
							MessageBox.Show("Cluster 정보를 입력하세요.");
							return;
						}

						DataTable dt = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).GetResourcePoolList(cencluster);

						if (dt == null || dt.Rows.Count == 0)
						{
							MessageBox.Show("받은 데이터가 없습니다.");
							return;
						}

						for (int i = 0; i < dt.Rows.Count; i++)
						{
							lstvCenterResult.Items.Add(dt.Rows[i]["Name"]);
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private async void btnvCenterResourcePoolJson_Click(object sender, EventArgs e)
		{
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string cencluster = txtCenterCluster.Text;

						if (string.IsNullOrEmpty(cencluster))
						{
							MessageBox.Show("Cluster 정보를 입력하세요.");
							return;
						}

						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(txtServiceIP.Text + ":" + txtServicePort.Text);

						gRPCRepository tr = new gRPCRepository(IPAddressPort);
						try
						{
							string[] cluster = cencluster.Split('/');
							string clustername = cluster[cluster.Length - 1].ToString();

							GetClusterRequest req = new GetClusterRequest()
							{
								ClusterUUID = "",
								ClusterName = clustername,
								VCenterUUID = "efddc656-7523-49c0-939d-e5211f482365",
								VCenterName = "vCenter Namu",
							};

							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(req);
							dynamic result = tr.SendCommand("GetClusterResourcePool", input);
							rtbResultVI.Text = Newtonsoft.Json.Linq.JToken.Parse(result).ToString(Newtonsoft.Json.Formatting.Indented);
							MessageBox.Show("ResourcePool Information Read Success.");
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						finally
						{
							tr.Close();
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private async void btnClusterDiscovery_Click(object sender, EventArgs e)
		{
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string hostandcluster = txtHostOrClusterPath.Text;

						if (string.IsNullOrEmpty(hostandcluster))
						{
							MessageBox.Show("Host & Cluster 정보를 입력하세요.");
							return;
						}

						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(txtServiceIP.Text + ":" + txtServicePort.Text);

						gRPCRepository tr = new gRPCRepository(IPAddressPort);
						try
						{
							string[] cluster = hostandcluster.Split('/');
							string clustername = cluster[cluster.Length - 1].ToString();

							VMWareInfraRequest request = new VMWareInfraRequest()
							{
								ClusterUUID = "",
								ClusterName = clustername,
								VCenterUUID = txtCenterUUID.Text,			//"efddc656-7523-49c0-939d-e5211f482365",
								VCenterName = txtCenterName.Text,
							};

							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(request);
							dynamic result = tr.SendCommand("Discovery", input);

							//rtbResult.Text = (result != null) ? result.ToString() : string.Empty;
							rtbResult.Text = Newtonsoft.Json.Linq.JToken.Parse(result).ToString(Newtonsoft.Json.Formatting.Indented);
							MessageBox.Show("Cluster Discovery Success.");
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						finally
						{
							tr.Close();
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private async void btnvCenterCluster_Click(object sender, EventArgs e)
		{
			lstvCenterResult.Items.Clear();
			string clusterName = string.Empty;
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						DataTable dt = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).GetClusterList();

						if (dt == null || dt.Rows.Count == 0)
						{
							MessageBox.Show("받은 데이터가 없습니다.");
							return;
						}

						for (int i = 0; i < dt.Rows.Count; i++)
						{
							//clusterName = dt.Rows[i]["Name"].ToString() + "(" + dt.Rows[i]["ClusterPath"].ToString() + ")";
							//lstvCenterResult.Items.Add(clusterName);

							//lstvCenterResult.Items.Add(dt.Rows[i]["Name"]);
							lstvCenterResult.Items.Add(dt.Rows[i]["ClusterPath"]);
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private async void btnvCenterClusterJson_Click(object sender, EventArgs e)
		{
			string clusterName = string.Empty;
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string clusterpath = txtCenterCluster.Text;

						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(txtServiceIP.Text + ":" + txtServicePort.Text);

						gRPCRepository tr = new gRPCRepository(IPAddressPort);
						try
						{
							string[] cluster = clusterpath.Split('/');
							string clustername = cluster[cluster.Length - 1].ToString();

							GetClusterRequest req = new GetClusterRequest()
                            {
                                ClusterUUID = "",
                                ClusterName = clustername,
                                VCenterUUID = "efddc656-7523-49c0-939d-e5211f482365",
                                VCenterName = "vCenter Namu",
                            };

							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(req);
							dynamic result = tr.SendCommand("ClusterInformation", input);
							rtbResultVI.Text = Newtonsoft.Json.Linq.JToken.Parse(result).ToString(Newtonsoft.Json.Formatting.Indented);
							MessageBox.Show("Cluster Information Read Success.");
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						finally
						{
							tr.Close();
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private async void btnvCenterFolder_Click(object sender, EventArgs e)
		{
			lstvCenterResult.Items.Clear();
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						DataTable dt = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).GetFolderList();

						if (dt == null || dt.Rows.Count == 0)
						{
							MessageBox.Show("받은 데이터가 없습니다.");
							return;
						}

						for (int i = 0; i < dt.Rows.Count; i++)
						{
							lstvCenterResult.Items.Add(dt.Rows[i]["Name"]);
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private async void btnvCenterFolderJson_Click(object sender, EventArgs e)
		{
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string datacentername = txtDatacenterName.Text;

						if (string.IsNullOrEmpty(datacentername))
						{
							MessageBox.Show("DataCenter Name을 입력하세요.");
							return;
						}

						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(txtServiceIP.Text + ":" + txtServicePort.Text);

						gRPCRepository tr = new gRPCRepository(IPAddressPort);
						try
						{
							GetFolderRequest req = new GetFolderRequest()
							{
								FolderID = "",
								FolderName = "",
								DatacenterName = datacentername,
								VCenterUUID = "efddc656-7523-49c0-939d-e5211f482365",
								VCenterName = "vCenter Namu",
							};

							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(req);
							dynamic result = tr.SendCommand("GetFolderInfo", input);
							rtbResultVI.Text = Newtonsoft.Json.Linq.JToken.Parse(result).ToString(Newtonsoft.Json.Formatting.Indented);
							MessageBox.Show("Folder Information Read Success.");
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						finally
						{
							tr.Close();
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

		private async void btnvCenterVLAN_Click(object sender, EventArgs e)
		{
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string cencluster = txtCenterCluster.Text;

						if (string.IsNullOrEmpty(cencluster))
						{
							MessageBox.Show("Cluster 정보를 입력하세요.");
							return;
						}
						lstvCenterResult.Items.Clear();

						string[] cluster = cencluster.Split('/');
						string clustername = cluster[cluster.Length - 1].ToString();

						// Cluster 내 포함된 NetworkAdapter ID List
						DataTable dt = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).GetClusterNetworkID(clustername);
						if (dt == null || dt.Rows.Count == 0)
						{
							MessageBox.Show("받은 데이터가 없습니다.");
							return;
						}

						for (int i = 0; i < dt.Rows.Count; i++)
						{
							InfraModel inf = new InfraModel();
							inf.vLanId = dt.Rows[i]["NetworkID"].ToString();
							inf.Search = "id";
							DataTable landt = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).GetVLAN(inf);
							if (landt == null || landt.Rows.Count == 0)
							{
								lstvCenterResult.Items.Add("ID Search 오류 - " + inf.vLanId);
                            }
                            else
                            {
								lstvCenterResult.Items.Add(landt.Rows[0]["Name"]);
							}
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private async void btnvCenterVLANJson_Click(object sender, EventArgs e)
		{
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string cencluster = txtCenterCluster.Text;

						if (string.IsNullOrEmpty(cencluster))
						{
							MessageBox.Show("Cluster 정보를 입력하세요.");
							return;
						}
						lstvCenterResult.Items.Clear();

						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(txtServiceIP.Text + ":" + txtServicePort.Text);

						gRPCRepository tr = new gRPCRepository(IPAddressPort);
						try
						{
							string[] cluster = cencluster.Split('/');
							string clustername = cluster[cluster.Length - 1].ToString();

							GetClusterRequest req = new GetClusterRequest()
							{
								ClusterUUID = "",
								ClusterName = clustername,
								VCenterUUID = "efddc656-7523-49c0-939d-e5211f482365",
								VCenterName = "vCenter Namu",
							};

							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(req);
							dynamic result = tr.SendCommand("GetClusterNetwork", input);
							rtbResultVI.Text = Newtonsoft.Json.Linq.JToken.Parse(result).ToString(Newtonsoft.Json.Formatting.Indented);
							MessageBox.Show("Network Information Read Success.");
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						finally
						{
							tr.Close();
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private async void btnvCenterTemplateList_Click(object sender, EventArgs e)
        {
			lstvCenterResult.Items.Clear();
			try
			{
				await Task.Run(() =>
				{
					DataTable dt = new VMManager(txtvCenterURL.Text, txtvCenterID.Text, txtvCenterPW.Text).GetAllTemplateList();

					if (dt == null || dt.Rows.Count == 0)
					{
						MessageBox.Show("받은 데이터가 없습니다.");
						return;
					}

					for (int i = 0; i < dt.Rows.Count; i++)
					{
						lstvCenterResult.Invoke(new MethodInvoker(delegate ()
						{
							lstvCenterResult.Items.Add(dt.Rows[i]["Name"]);
						}));

					}
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private void txtCenterCluster_DragDrop(object sender, DragEventArgs e)
        {
			string temp = Convert.ToString(e.Data.GetData(typeof(string)));

			(sender as TextBox).Text = temp;
		}

        private void txtCenterCluster_DragEnter(object sender, DragEventArgs e)
        {
			if ((e.AllowedEffect & DragDropEffects.Copy) != 0 && e.Data.GetDataPresent(typeof(string)))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

        private async void btnClusterDetail_Click(object sender, EventArgs e)
        {
			string clusterName = string.Empty;
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string clusterpath = txtCenterCluster.Text;

						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(txtServiceIP.Text + ":" + txtServicePort.Text);

						gRPCRepository tr = new gRPCRepository(IPAddressPort);
						try
						{
							string[] cluster = clusterpath.Split('/');
							string clustername = cluster[cluster.Length - 1].ToString();

							GetClusterRequest req = new GetClusterRequest()
							{
								ClusterUUID = "",
								ClusterName = clustername,
								VCenterUUID = "efddc656-7523-49c0-939d-e5211f482365",
								VCenterName = "vCenter Namu",
							};

							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(req);
							dynamic result = tr.SendCommand("ClusterSummaryDetail", input);
							//rtbResultVI.Text = (result != null) ? result.ToString() : string.Empty;
							//lstHorizonResult.Items.Add((result != null) ? result.ToString() : string.Empty);

							//rtbResult.Text = (result != null) ? result.ToString() : string.Empty;
							rtbResultVI.Text = Newtonsoft.Json.Linq.JToken.Parse(result).ToString(Newtonsoft.Json.Formatting.Indented);
							MessageBox.Show("Cluster Detail Information Read Success.");
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						finally
						{
							tr.Close();
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private async void btnSaveVI_Click(object sender, EventArgs e)
        {
			string cUrl = txtvCenterURL.Text;
			string cID = txtvCenterID.Text;
			string cPW = txtvCenterPW.Text;
			string sIP = txtServiceIP.Text;
			string sPort = txtServicePort.Text;

			string cName = "vCenter Namu";
			string cNameEN = "vCenter Namu";
			string Desc = "Namu Tech vCenter Information";
			string cIP = "192.168.50.16";
			string cRegAdminID = "administrator";
			string cUpdateAdminID = "administrator";

			if (cUrl != "192.168.50.16")
            {
				cName = "vCenter SDS";
				cNameEN = "vCenter SDS";
				Desc = "SDS vCenter Information";
				cIP = "10.44.1.41";
				cRegAdminID = "administrator";
				cUpdateAdminID = "administrator";
			}
            
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(sIP + ":" + sPort);

						gRPCRepository tr = new gRPCRepository(IPAddressPort);
						try
						{
							CenterRegistRequest req = new CenterRegistRequest()
							{
								VCenterName = cName,
								VCenterNameEN = cNameEN,
								VCenterURL = cUrl,
								VCenterIP = cIP,
								VCenterPORT = 443,
								Description = Desc,
								AdminID = cID,
								AdminPW = cPW,
								NccServiceIP = sIP,
								NccServicePort = Convert.ToInt32(sPort),
								RegAdminID = cRegAdminID,
								UpdateAdminID = cUpdateAdminID,
							};

							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(req);
							dynamic result = tr.SendCommand("SetvCenter", input);
							//rtbResultVI.Text = (result != null) ? result.ToString() : string.Empty;
							//lstHorizonResult.Items.Add((result != null) ? result.ToString() : string.Empty);

							//rtbResult.Text = (result != null) ? result.ToString() : string.Empty;
							rtbResultVI.Text = Newtonsoft.Json.Linq.JToken.Parse(result).ToString(Newtonsoft.Json.Formatting.Indented);
							MessageBox.Show("Set vCenter Save Success.");
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						finally
						{
							tr.Close();
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private async void btnSaveHV_Click(object sender, EventArgs e)
        {
			string hUrl = txtHorizonURL.Text;
			string hID = txtHorizonID.Text;
			string hPW = txtHorizonPW.Text;
			string sIP = txtServiceIP.Text;
			string sPort = txtServicePort.Text;

			string hName = "nm-vwcs.namurnd.io";
			string hNameEN = "NamuRND Horizon View";
			string Desc = "NamuRND Horizon View Information";
			string hIP = "192.168.101.70";
			string hRegAdminID = "administrator";
			string hUpdateAdminID = "administrator";

			if (hUrl != "nm-vwcs.namurnd.io")
			{
				hName = "Horizon View SDS";
				hNameEN = "Horizon View SDS";
				Desc = "SDS Horizon View Information";
				hIP = "10.44.1.41";
				hRegAdminID = "administrator";
				hUpdateAdminID = "administrator";
			}

			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(sIP + ":" + sPort);

						gRPCRepository tr = new gRPCRepository(IPAddressPort);
						try
						{
							HorizonRegistRequest req = new HorizonRegistRequest()
							{
								HorizonName = hName,
								HorizonNameEN = hNameEN,
								HorizonURL = hUrl,
								HorizonIP = hIP,
								HorizonPORT = 443,
								Description = Desc,
								AdminID = hID,
								AdminPW = hPW,
								NccServiceIP = sIP,
								NccServicePort = Convert.ToInt32(sPort),
								RegAdminID = hRegAdminID,
								UpdateAdminID = hUpdateAdminID,
							};

							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(req);
							dynamic result = tr.SendCommand("SetHorizon", input);
							
							rtbResult.Text = Newtonsoft.Json.Linq.JToken.Parse(result).ToString(Newtonsoft.Json.Formatting.Indented);
							MessageBox.Show("Set HorizonView Save Success.");
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						finally
						{
							tr.Close();
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private async void btnXenServerVMProvision_Click(object sender, EventArgs e)
		{
			lstvCenterResult.Items.Clear();
			try
			{
				await Task.Run(() =>
				{
					lstvCenterResult.Invoke(new MethodInvoker(delegate ()
					{
						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(txtServiceIP.Text + ":" + txtServicePort.Text);
						gRPCRepository tr = new gRPCRepository(IPAddressPort);

						try
						{

							//VMProvisioningModel prov = new VMProvisioningModel();

							#region Provisioning Parameters
							CloneRequest prov = new CloneRequest()
							{
								MasterVMName = "Win10x64-1",
								TempVMName = "TEST2-1",
								StorageName = "Datastore15",
								VMName = "TEST2-1",
								CPU = 4,							// 값이 0이거나 동일할 경우 X
								Memory = 4,                        // 값이 0이거나 동일할 경우 X
								Disk = 0,                          // Disk CNT ?? or Disk Space  (값이 0이거나 동일할 경우 X)
								NIC = "VM Network",                // Get-VirtualNetwork (NIC 값은 고정임-VMWare)
								ClusterName = "NM_VMWC",			//"NewCluster"
								HostName = "192.168.101.15",
								DatastoreName = "Datastore15",
								CloneResource = "VM",              // VM, Template
								SetVMResource = "Cpu",             // Cpu, Mem, Hdd, Name
								StorageFormat = "Thin",            // or Thick
								Language = "KO",                   // or EN
								DomainSetUserID = "Admin",
								DomainSetUserPW = "P@ssw0rd",
								VCenter = "vCenter Namu",
								VCenterUUID = "efddc656-7523-49c0-939d-e5211f482365",
								Workflow = "{\"--run1\":{\"NCCWorkFlow.NCCProvAgent.Sleep\":{\"Wait\":1000},\"NCCWorkFlow.NCCProvAgent.CommandRun\":{\"CommandRun_Add_YN\":1,\"CommandRun_WorkingDir\":\"\",\"CommandRun_FullDir_FileName\":\"C:\\\\Windows\\\\System32\\\\diskpart.exe\",\"CommandRun_Arguments\":\"/s \\\"C:\\\\Program Files (x86)\\\\Namutech\\\\ProvisioningAgent\\\\Script\\\\Diskpart_D_Assign.txt\",\"CommandRun_Verb_YN\":1},\"NCCWorkFlow.NCCProvAgent.IPChange1\":{\"First_Network_Setting_YN\":1,\"First_Network_IPAddress\":\"192.168.101.225\",\"First_Network_SubnetMask\":\"255.255.255.0\",\"First_Network_Gateway\":\"192.168.101.1\",\"First_Network_DNS1\":\"192.168.50.2\",\"First_Network_DNS2\":\"192.168.50.3\"},\"NCCWorkFlow.NCCProvAgent.HostNameChange\":{\"HostName_Change_YN\":1,\"HostName_Change_HostName\":\"TEST1-1\",\"HostName_Change_DomainNetBios\":\"namurnd\",\"HostName_Change_DomainAdminID\":\"administrator\",\"HostName_Change_DomainAdminPW\":\"u8NpQh9hGz11d+Al32Pf1A==\"},\"NCCWorkFlow.NCCProvAgent.StepChange\":{\"StepChange_From\":\"--run1\",\"StepChange_To\":\"--run2\"},\"NCCWorkFlow.NCCProvAgent.Reboot\":{\"Rebooting\":1}},\"--run2\":{\"NCCWorkFlow.NCCProvAgent.Sleep\":{\"Wait\":5000},\"NCCWorkFlow.NCCProvAgent.IPChange1\":{\"First_Network_Setting_YN\":1,\"First_Network_IPAddress\":\"192.168.101.225\",\"First_Network_SubnetMask\":\"255.255.255.0\",\"First_Network_Gateway\":\"192.168.101.1\",\"First_Network_DNS1\":\"192.168.50.2\",\"First_Network_DNS2\":\"192.168.50.3\"},\"NCCWorkFlow.NCCProvAgent.DomainJoin\":{\"DomainJoin_Add_YN\":1,\"DomainJoin_Add_DomainNetBios\":\"namurnd.io\",\"DomainJoin_Add_DomainAdminID\":\"administrator\",\"DomainJoin_Add_DomainAdminPW\":\"u8NpQh9hGz11d+Al32Pf1A==\",\"DomainJoin_Add_DomainOUDC\":\"\"},\"NCCWorkFlow.NCCProvAgent.AutoLoginAdd\":{\"AutoLogin_Add_Win_YN\":1,\"AutoLogin_Add_Win_Version\":7,\"AutoLogin_Add_UserID\":\"namurnd\\\\administrator\",\"AutoLogin_Add_UserPW\":\"u8NpQh9hGz11d+Al32Pf1A==\"},\"NCCWorkFlow.NCCProvAgent.Sleep_1\":{\"Wait\":10000},\"NCCWorkFlow.NCCProvAgent.StepChange\":{\"StepChange_From\":\"--run2\",\"StepChange_To\":\"--finish\"},\"NCCWorkFlow.NCCProvAgent.Reboot\":{\"Rebooting\":1}},\"--finish\":{\"NCCWorkFlow.NCCProvAgent.CommandRun\":{\"CommandRun_Add_YN\":1,\"CommandRun_WorkingDir\":\"C:\\\\Windows\\\\System32\",\"CommandRun_FullDir_FileName\":\"cmd.exe\",\"CommandRun_Arguments\":\" /c REG ADD \\\"HKLM\\\\SOFTWARE\\\\Microsoft\\\\Windows NT\\\\CurrentVersion\\\\WinLogon\\\" /v AutoAdminLogon /t REG_SZ /d 0 /f\",\"CommandRun_Verb_YN\":1},\"NCCWorkFlow.NCCProvAgent.LocalAdminGroup\":{\"Local_AdminGroup_Add_YN\":1,\"Local_AdminGroup_Add_DomainUserID\":\"Admin\",\"Local_AdminGroup_Add_DomainNetbios\":\"namurnd\",\"Local_AdminGroup_Add_DomainAdminID\":\"administrator\",\"Local_AdminGroup_Add_DomainAdminPW\":\"u8NpQh9hGz11d+Al32Pf1A==\"},\"NCCWorkFlow.NCCProvAgent.StepDelete\":{\"StepRemove_YN\":1},\"NCCWorkFlow.NCCProvAgent.Sleep\":{\"Wait\":5000},\"NCCWorkFlow.NCCProvAgent.StateCallBack\":{\"StateCallBack_Add_YN\":0,\"StateCallBack_IPAddressPort_IPPORT\":\"\",\"StateCallBack_CurrentState\":0,\"StateCallBack_CREATE_ID\":160,\"StateCallBack_MachineType\":0},\"NCCWorkFlow.NCCProvAgent.Shutdown\":{\"PowerOff\":1}}}",
							};
							//prov.AgentInfo = prov.Workflow;
							#endregion

							#region Agent Setting Parameter
							AgentRequest Areq = new AgentRequest();
							Areq.VmUuid = "";
							Areq.VmName = "TEST1-1";
							Areq.VCenterUUID = "efddc656-7523-49c0-939d-e5211f482365";
							Areq.VCenterName = "vCenter Namu";
							Areq.DomainNetbios = "namurnd";
							Areq.DomainFQDN = "namurnd.io";
							Areq.DomainAdminID = "administrator";
							Areq.DomainAdminPW = "namudev!23$";
							Areq.DomainOUDC = "";
							Areq.AddressIP = "192.168.101.215";
							Areq.AddressSubnet = "255.255.255.0";
							Areq.AddressGateway = "192.168.101.1";
							Areq.AddressDNS1 = "192.168.50.2";
							Areq.AddressDNS2 = "192.168.50.3";
							Areq.AddressStateCallBack = "";
							Areq.DomainSetUserID = "Admin";
							Areq.DomainSetUserPW = "P@ssw0rd";
							#endregion
							Areq.JsonData = "";

							//CloneRequest req = new CloneRequest();

							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(prov);
							dynamic result = tr.SendCommand("CloneVM", input);
							rtbResultVI.Text = (result != null) ? result.ToString() : string.Empty;
							//lstHorizonResult.Items.Add((result != null) ? result.ToString() : string.Empty);
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						finally
						{
							tr.Close();
						}
					}));
				});

			}
			catch (Exception)
			{
				throw;
			}
		}

        private async void btnChangeName_Click(object sender, EventArgs e)
        {
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string vmname = txtVMName1.Text;
						string changename = txtVMName2.Text;

						if (string.IsNullOrEmpty(vmname))
						{
							MessageBox.Show("VM Name을 입력하세요.");
							return;
						}
						if (string.IsNullOrEmpty(changename))
						{
							MessageBox.Show("변경할 VM Name을 입력하세요.");
							return;
						}

						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(txtServiceIP.Text + ":" + txtServicePort.Text);

						gRPCRepository tr = new gRPCRepository(IPAddressPort);
						try
						{
							SetVMRequest req = new SetVMRequest()
							{
								VCenterUUID = "efddc656-7523-49c0-939d-e5211f482365",
								VCenterName = "vCenter Namu",
								VMName = vmname,
								ChangeName = changename,
								Action = ACTIONKIND.Update,
							};

							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(req);
							dynamic result = tr.SendCommand("SetVMName", input);
							rtbResultVI.Text = Newtonsoft.Json.Linq.JToken.Parse(result).ToString(Newtonsoft.Json.Formatting.Indented);
							MessageBox.Show("ReName VM Success.");
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						finally
						{
							tr.Close();
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private async void btnResetVM_Click(object sender, EventArgs e)
        {
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string vmname = txtVMName1.Text;
						string changename = txtVMName2.Text;

						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(txtServiceIP.Text + ":" + txtServicePort.Text);

						gRPCRepository tr = new gRPCRepository(IPAddressPort);
						try
						{
							ResetRequest reset = new ResetRequest()
							{
								MasterVMName = "Win10x64-1",
								TempVMName = "",
								StorageName = "DataStore15",
								VMName = "SDS-VMW-007",
								CPU = 2,
								Memory = 2,
								Disk = 30,
								NIC = "VM Network",
								ClusterName = "NM_VMWC",
								HostName = "192.168.101.15",
								DatastoreName = "DataStore15",
								CloneResource = "VM",
								SetVMResource = "Cpu",
								StorageFormat = "Thin",
								Language = "KO",
								DomainSetUserID = "admin",
								DomainSetUserPW = @"P@ssw0rd",
								VCenter = "vCenter Namu",
								VCenterUUID = "efddc656-7523-49c0-939d-e5211f482365",
								Workflow = "{\r\n  \"--run1\": {\r\n    \"NCCWorkFlow.NCCProvAgent.Sleep\": {\r\n      \"Wait\": 1000\r\n    },\r\n    \"NCCWorkFlow.NCCProvAgent.IPChange1\": {\r\n      \"First_Network_Setting_YN\": 1,\r\n      \"First_Network_IPAddress\": \"192.168.101.216\",\r\n      \"First_Network_SubnetMask\": \"255.255.255.0\",\r\n      \"First_Network_Gateway\": \"192.168.101.1\",\r\n      \"First_Network_DNS1\": \"192.168.50.2\",\r\n      \"First_Network_DNS2\": \"192.168.50.3\"\r\n    },\r\n    \"NCCWorkFlow.NCCProvAgent.HostNameChange\": {\r\n      \"HostName_Change_YN\": 1,\r\n      \"HostName_Change_HostName\": \"SDS-VMW-007\",\r\n      \"HostName_Change_DomainNetBios\": \"namurnd\",\r\n      \"HostName_Change_DomainAdminID\": \"administrator\",\r\n      \"HostName_Change_DomainAdminPW\": \"u8NpQh9hGz11d+Al32Pf1A==\"\r\n    },\r\n    \"NCCWorkFlow.NCCProvAgent.StepChange\": {\r\n      \"StepChange_From\": \"--run1\",\r\n      \"StepChange_To\": \"--run2\"\r\n    },\r\n    \"NCCWorkFlow.NCCProvAgent.Reboot\": {\r\n      \"Rebooting\": 1\r\n    }\r\n  },\r\n  \"--run2\": {\r\n    \"NCCWorkFlow.NCCProvAgent.Sleep\": {\r\n      \"Wait\": 5000\r\n    },\r\n    \"NCCWorkFlow.NCCProvAgent.IPChange1\": {\r\n      \"First_Network_Setting_YN\": 1,\r\n      \"First_Network_IPAddress\": \"192.168.101.216\",\r\n      \"First_Network_SubnetMask\": \"255.255.255.0\",\r\n      \"First_Network_Gateway\": \"192.168.101.1\",\r\n      \"First_Network_DNS1\": \"192.168.50.2\",\r\n      \"First_Network_DNS2\": \"192.168.50.3\"\r\n    },\r\n    \"NCCWorkFlow.NCCProvAgent.DomainJoin\": {\r\n      \"DomainJoin_Add_YN\": 1,\r\n      \"DomainJoin_Add_DomainNetBios\": \"namurnd.io\",\r\n      \"DomainJoin_Add_DomainAdminID\": \"administrator\",\r\n      \"DomainJoin_Add_DomainAdminPW\": \"u8NpQh9hGz11d+Al32Pf1A==\",\r\n      \"DomainJoin_Add_DomainOUDC\": \"OU=computers,OU=ccms,OU=SITE,DC=NAMURND,DC=IO\"\r\n    },\r\n    \"NCCWorkFlow.NCCProvAgent.AutoLoginAdd\": {\r\n      \"AutoLogin_Add_Win_YN\": 1,\r\n      \"AutoLogin_Add_Win_Version\": 7,\r\n      \"AutoLogin_Add_UserID\": \"namurnd\\\\administrator\",\r\n      \"AutoLogin_Add_UserPW\": \"u8NpQh9hGz11d+Al32Pf1A==\"\r\n    },\r\n    \"NCCWorkFlow.NCCProvAgent.Sleep_1\": {\r\n      \"Wait\": 10000\r\n    },\r\n    \"NCCWorkFlow.NCCProvAgent.StepChange\": {\r\n      \"StepChange_From\": \"--run2\",\r\n      \"StepChange_To\": \"--finish\"\r\n    },\r\n    \"NCCWorkFlow.NCCProvAgent.Reboot\": {\r\n      \"Rebooting\": 1\r\n    }\r\n  },\r\n  \"--finish\": {\r\n    \"NCCWorkFlow.NCCProvAgent.LocalAdminGroup\": {\r\n      \"Local_AdminGroup_Add_YN\": 1,\r\n      \"Local_AdminGroup_Add_DomainUserID\": \"namurnd\\\\\",\r\n      \"Local_AdminGroup_Add_DomainNetbios\": \"namurnd\",\r\n      \"Local_AdminGroup_Add_DomainAdminID\": \"administrator\",\r\n      \"Local_AdminGroup_Add_DomainAdminPW\": \"u8NpQh9hGz11d+Al32Pf1A==\"\r\n    },\r\n    \"NCCWorkFlow.NCCProvAgent.Sleep\": {\r\n      \"Wait\": 0\r\n    },\r\n    \"NCCWorkFlow.NCCProvAgent.StateCallBack\": {\r\n      \"StateCallBack_Add_YN\": 1,\r\n      \"StateCallBack_IPAddressPort_IPPORT\": \"192.168.50.70:9717/192.168.50.71:9717\",\r\n      \"StateCallBack_CurrentState\": 0,\r\n      \"StateCallBack_CREATE_ID\": 3248,\r\n      \"StateCallBack_MachineType\": 4\r\n    },\r\n    \"NCCWorkFlow.NCCProvAgent.StepDelete\": {\r\n      \"StepRemove_YN\": 1\r\n    },\r\n    \"NCCWorkFlow.NCCProvAgent.AutoLoginDelete\": {\r\n      \"AutoLogin_Remove_Win_YN\": 1,\r\n      \"AutoLogin_Remove_Win_Version\": 7\r\n    },\r\n    \"NCCWorkFlow.NCCProvAgent.Shutdown\": {\r\n      \"PowerOff\": 1\r\n    }\r\n  }\r\n}",
								ClusterUUID = "94f1541c-0ac7-9793-1e37-387c23399862",
								HorizonName = "nm-vwcs.namurnd.io",
								HorizonUUID = "5f0cb840-4391-47a0-ae2d-b84e1a30cd86",
								PreVMName = "SDS-VMW-007",
								PreVMUUID = "50103eb8-e23b-5f88-621c-44d0d41f0733",
							};


							//SetVMRequest req = new SetVMRequest()
							//{
							//	VCenterUUID = "efddc656-7523-49c0-939d-e5211f482365",
							//	VCenterName = "vCenter Namu",
							//	VMName = vmname,
							//	ChangeName = changename,
							//	Action = ACTIONKIND.Update,
							//};

							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(reset);
							dynamic result = tr.SendCommand("ResetVM", input);
							rtbResultVI.Text = Newtonsoft.Json.Linq.JToken.Parse(result).ToString(Newtonsoft.Json.Formatting.Indented);
							MessageBox.Show("ResetVM Success.");
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						finally
						{
							tr.Close();
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private async void btnVMFolderDiscovery_Click(object sender, EventArgs e)
		{
			try
			{
				await Task.Run(() =>
				{
					this.Invoke(new MethodInvoker(delegate ()
					{
						string datacentername = txtDatacenterName.Text;

						List<string> IPAddressPort = new List<string>();
						IPAddressPort.Add(txtServiceIP.Text + ":" + txtServicePort.Text);

						gRPCRepository tr = new gRPCRepository(IPAddressPort);
						try
						{
							FLDiscoveryRequest req = new FLDiscoveryRequest()
							{
								Datacenter = datacentername,
								VCenterUUID = txtCenterUUID.Text,
								VCenter = txtCenterName.Text,
							};

							dynamic input = Newtonsoft.Json.JsonConvert.SerializeObject(req);
							dynamic result = tr.SendCommand("FolderDiscovery", input);
							rtbResult.Text = Newtonsoft.Json.Linq.JToken.Parse(result).ToString(Newtonsoft.Json.Formatting.Indented);
							MessageBox.Show("FolderDiscovery Success.");
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
						finally
						{
							tr.Close();
						}
					}));
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
		}

        private void btnClusterResourcePoolDiscovery_Click(object sender, EventArgs e)
        {

        }
    }

    public static class Utils 
	{
		public static void SelectItemByValue(this ComboBox cbo, string value)
		{
            if (value == null || value == "")
			{
				cbo.SelectedIndex = -1;
			}
            else
            {
				for (int i = 0; i < cbo.Items.Count; i++)
				{
					//var prop = cbo.Items[i].GetType().GetProperty(cbo.ValueMember);
					var prop = cbo.Items[i];
					if (prop != null && prop.ToString() == value)
					{
						cbo.SelectedIndex = i;
						break;
					}
				}
            }
		}

		public static VMAction fnPowerAction(string act)
		{
			NCCProto.Service.VMWareService.VMAction action = new NCCProto.Service.VMWareService.VMAction();
			switch (act)
			{
				case "Start":
					action = VMAction.Start;
					break;
				case "Restart":
					action = VMAction.Restart;
					break;
				case "ForceRestart":
					action = VMAction.ForceRestart;
					break;
				case "Shutdown":
					action = VMAction.Shutdown;
					break;
				case "ForceShutdown":
					action = VMAction.ForceShutdown;
					break;
				case "Suspend":
					action = VMAction.Suspend;
					break;
				case "Resume":
					action = VMAction.Resume;
					break;
				case "LogOff":
					action = VMAction.LogOff;
					break;
				case "Disconnect":
					action = VMAction.Disconnect;
					break;
				case "MaintenanceOn":
					action = VMAction.MaintenanceOn;
					break;
				case "MaintenanceOff":
					action = VMAction.MaintenanceOff;
					break;
				case "RestartVMGuest":
					action = VMAction.RestartVmguest;
					break;
				case "ShutdownVMGuest":
					action = VMAction.ShutdownVmguest;
					break;
				case "SuspendVMGuest":
					action = VMAction.SuspendVmguest;
					break;
			}
			return action;
		}

		public static Login GetCenterInfo(string vCenterURL)
		{
			Login ln = new Login();

			using (DBEntities db = new DBEntities())
			{
				var centerinfo = db.TB_VCENTER.Where(v => v.VCENTER_URL == vCenterURL && v.DEL_YN == false).FirstOrDefault();
				//var centerinfo = db.TB_VCENTER.Where(v => v.VCENTER_NAME == vCenterName && v.DEL_YN == false).FirstOrDefault();
				//var centerinfo2 = db.TB_VCENTER.Where(v => v.VCENTER_UUID == vCenterUUID && v.DEL_YN == false).FirstOrDefault();

				ln.URL = centerinfo.VCENTER_URL;
				ln.ID = centerinfo.ADMIN_ID;
				ln.PW = Cryptography.Decrypt(centerinfo.ADMIN_PW);
				//ln.URL = @"sbc-vcsa.cloud.corp.samsungelectronics.net";
				//ln.CID = @"Cloud\vdi_master";
				//ln.CPW = @"wpfl21)@Cp";
			}
			return ln;
		}
		public class Login
		{
			public string URL { get; set; }
			public string ID { get; set; }
			public string PW { get; set; }
		}

		public static Login GetHorizonInfo(string HorizonURL)
		{
			Login ln = new Login();

			using (DBEntities db = new DBEntities())
			{
				var hinfo = db.TB_HORIZON_VIEW.Where(h => h.HORIZON_URL == HorizonURL && h.DEL_YN == false).FirstOrDefault();
				//var hinfo = db.TB_HORIZON_VIEW.Where(h => h.HORIZON_NAME == HorizonName && h.DEL_YN == false).FirstOrDefault();
				//var hinfo2 = db.TB_HORIZON_VIEW.Where(h => h.HORIZON_UUID == HorizonUUID && h.DEL_YN == false).FirstOrDefault();

				ln.URL = hinfo.HORIZON_URL;
				ln.ID = hinfo.ADMIN_ID;
				ln.PW = Cryptography.Decrypt(hinfo.ADMIN_PW);
				//ln.URL = @"sec.sbc-cloud.com";
				//ln.HID = @"Cloud\vdi_master";
				//ln.HPW = @"wpfl21)@Cp";
			}
			return ln;
		}
	}
}

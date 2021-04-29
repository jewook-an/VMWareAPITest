using System.Drawing;
using System.Windows.Forms;

namespace TestVmWareAPI
{
	partial class MainFrm
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rtbResultVI = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCenterName = new System.Windows.Forms.TextBox();
            this.txtCenterUUID = new System.Windows.Forms.TextBox();
            this.label61 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.btnSaveVI = new System.Windows.Forms.Button();
            this.btnSDSCenter = new System.Windows.Forms.Button();
            this.btnNamuCenter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtvCenterURL = new System.Windows.Forms.TextBox();
            this.txtvCenterID = new System.Windows.Forms.TextBox();
            this.txtvCenterPW = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstvCenterResult = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnVMDetails = new System.Windows.Forms.Button();
            this.loopyn = new System.Windows.Forms.CheckBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.rbtnXenServerFastClone = new System.Windows.Forms.RadioButton();
            this.txtDomainUserID = new System.Windows.Forms.TextBox();
            this.rbtnXenServerFullCopy = new System.Windows.Forms.RadioButton();
            this.label22 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnXenServerRemove = new System.Windows.Forms.Button();
            this.txtLocalPW = new System.Windows.Forms.TextBox();
            this.btnXenServerVMProvision = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.txtXenServerTemplateKind = new System.Windows.Forms.TextBox();
            this.txtLocalID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtXenServerStorageKind = new System.Windows.Forms.TextBox();
            this.txtNetbios = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtDomainPW = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtDNS2_F = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtDNS1_F = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtGateway_F = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSubnetMask_F = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDomainID = new System.Windows.Forms.TextBox();
            this.txtIPAddress_F = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.btnvCenterAddNIC = new System.Windows.Forms.Button();
            this.btnvCenterRemoveNIC = new System.Windows.Forms.Button();
            this.txtvCenterNicName = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbvCenterPowerKind = new System.Windows.Forms.ComboBox();
            this.btnvCenterPowerRun = new System.Windows.Forms.Button();
            this.lblPowerState = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblvCenterHDD3 = new System.Windows.Forms.Label();
            this.nudvCenterHdd3 = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.lblvCenterHDD2 = new System.Windows.Forms.Label();
            this.nudvCenterHdd2 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.lblvCenterHDD1 = new System.Windows.Forms.Label();
            this.nudvCenterHdd1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnvCenterResourceChange = new System.Windows.Forms.Button();
            this.lblvCenterCPU = new System.Windows.Forms.Label();
            this.nudvCenterRAM = new System.Windows.Forms.NumericUpDown();
            this.lblvCenterRam = new System.Windows.Forms.Label();
            this.nudvCenterVCPUCore = new System.Windows.Forms.NumericUpDown();
            this.txtvCenterVMName = new System.Windows.Forms.TextBox();
            this.btnXenServerDebug = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnResetVM = new System.Windows.Forms.Button();
            this.btnChangeName = new System.Windows.Forms.Button();
            this.txtVMName2 = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.txtVMName1 = new System.Windows.Forms.TextBox();
            this.label57 = new System.Windows.Forms.Label();
            this.txtDatacenterName = new System.Windows.Forms.TextBox();
            this.lblDatacenterName = new System.Windows.Forms.Label();
            this.btnClusterDetail = new System.Windows.Forms.Button();
            this.txtCenterCluster = new System.Windows.Forms.TextBox();
            this.lblCluster = new System.Windows.Forms.Label();
            this.btnvCenterClusterJson = new System.Windows.Forms.Button();
            this.btnvCenterVLANJson = new System.Windows.Forms.Button();
            this.btnvCenterVLAN = new System.Windows.Forms.Button();
            this.btnvCenterFolderJson = new System.Windows.Forms.Button();
            this.btnvCenterFolder = new System.Windows.Forms.Button();
            this.btnvCenterHostJson = new System.Windows.Forms.Button();
            this.btnvCenterVMList = new System.Windows.Forms.Button();
            this.btnvCenterResourcePoolJson = new System.Windows.Forms.Button();
            this.btnvCenterTemplateList = new System.Windows.Forms.Button();
            this.btnvCenterStorageJson = new System.Windows.Forms.Button();
            this.btnvCenterHost = new System.Windows.Forms.Button();
            this.btnvCenterResourcePool = new System.Windows.Forms.Button();
            this.btnvCenterStorage = new System.Windows.Forms.Button();
            this.chkXenServerOnlyCustomTemplate = new System.Windows.Forms.CheckBox();
            this.btnvCenterCluster = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label56 = new System.Windows.Forms.Label();
            this.txtDomainSetUserPW = new System.Windows.Forms.TextBox();
            this.btnAgentSetting = new System.Windows.Forms.Button();
            this.txtAddressStateCallBack = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.txtAddressDNS1 = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.txtAddressDNS2 = new System.Windows.Forms.TextBox();
            this.txtAddressGateway = new System.Windows.Forms.TextBox();
            this.txtAddressSubnet = new System.Windows.Forms.TextBox();
            this.txtAddressIP = new System.Windows.Forms.TextBox();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.txtDomainOUDC = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.txtDomainAdminPW = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.txtDomainSetUserID = new System.Windows.Forms.TextBox();
            this.txtDomainAdminID = new System.Windows.Forms.TextBox();
            this.txtDomainFQDN = new System.Windows.Forms.TextBox();
            this.txtDomainNetbios = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.lstHorizonResult = new System.Windows.Forms.ListBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.cmbMessageType = new System.Windows.Forms.ComboBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.txtSendMessage = new System.Windows.Forms.TextBox();
            this.lblSendMessage = new System.Windows.Forms.Label();
            this.lblServicePort = new System.Windows.Forms.Label();
            this.txtServicePort = new System.Windows.Forms.TextBox();
            this.txtServiceIP = new System.Windows.Forms.TextBox();
            this.lblServiceIP = new System.Windows.Forms.Label();
            this.btnPoolVMStatus = new System.Windows.Forms.Button();
            this.cmbVMAction = new System.Windows.Forms.ComboBox();
            this.btnVMStatus = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClusterResourcePoolDiscovery = new System.Windows.Forms.Button();
            this.btnVMFolderDiscovery = new System.Windows.Forms.Button();
            this.btnClusterDiscovery = new System.Windows.Forms.Button();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.lbl_Language = new System.Windows.Forms.Label();
            this.cmbCustType = new System.Windows.Forms.ComboBox();
            this.label55 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.btnDatastoreList = new System.Windows.Forms.Button();
            this.btnResourcePoolList = new System.Windows.Forms.Button();
            this.btnHostClusterList = new System.Windows.Forms.Button();
            this.btnVMFolderList = new System.Windows.Forms.Button();
            this.btnSnapshotList = new System.Windows.Forms.Button();
            this.btnParentVMList = new System.Windows.Forms.Button();
            this.btnTemplateList = new System.Windows.Forms.Button();
            this.btnClonePool = new System.Windows.Forms.Button();
            this.btnDeletePool = new System.Windows.Forms.Button();
            this.btnEditPool = new System.Windows.Forms.Button();
            this.btnNewPool = new System.Windows.Forms.Button();
            this.txtNamingRule = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.txtDatastorePaths = new System.Windows.Forms.TextBox();
            this.txtResourcePoolPath = new System.Windows.Forms.TextBox();
            this.txtHostOrClusterPath = new System.Windows.Forms.TextBox();
            this.txtVmFolderPath = new System.Windows.Forms.TextBox();
            this.txtDatacenterPath = new System.Windows.Forms.TextBox();
            this.txtSnapshotPath = new System.Windows.Forms.TextBox();
            this.txtParentVMPath = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txtTemplatePath = new System.Windows.Forms.TextBox();
            this.nudNumSessions = new System.Windows.Forms.NumericUpDown();
            this.nudNumMachines = new System.Windows.Forms.NumericUpDown();
            this.cmbProvisionEnabled = new System.Windows.Forms.ComboBox();
            this.cmbPoolEnabled = new System.Windows.Forms.ComboBox();
            this.cmbPoolSource = new System.Windows.Forms.ComboBox();
            this.chkUserAutoAssign = new System.Windows.Forms.CheckBox();
            this.cmbUserAssign = new System.Windows.Forms.ComboBox();
            this.cmbPoolType = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.btnVMAction = new System.Windows.Forms.Button();
            this.txtHorizonSystemName = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.btnPoolDetails = new System.Windows.Forms.Button();
            this.txtHorizonPoolName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.chkAllVMList = new System.Windows.Forms.CheckBox();
            this.btnAvailableVMList = new System.Windows.Forms.Button();
            this.btnHorizonVMList = new System.Windows.Forms.Button();
            this.btnHorizonPoolList = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.txtHorizonUUID = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.txtHorizonName = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.txtHorizonID = new System.Windows.Forms.TextBox();
            this.btnSaveHV = new System.Windows.Forms.Button();
            this.txtHorizonPW = new System.Windows.Forms.TextBox();
            this.btnSDSHorizon = new System.Windows.Forms.Button();
            this.btnNamuHorizon = new System.Windows.Forms.Button();
            this.label101 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.txtHorizonURL = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.templateTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.parentVMTootip = new System.Windows.Forms.ToolTip(this.components);
            this.snapshotTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.vmfolderTootip = new System.Windows.Forms.ToolTip(this.components);
            this.hostclusterTootip = new System.Windows.Forms.ToolTip(this.components);
            this.resourcepoolTootip = new System.Windows.Forms.ToolTip(this.components);
            this.datastoreTootip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudvCenterHdd3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudvCenterHdd2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudvCenterHdd1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudvCenterRAM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudvCenterVCPUCore)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumSessions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumMachines)).BeginInit();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1094, 591);
            this.tabControl1.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rtbResultVI);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.lstvCenterResult);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1086, 565);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "vCenter";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rtbResultVI
            // 
            this.rtbResultVI.Location = new System.Drawing.Point(7, 386);
            this.rtbResultVI.Name = "rtbResultVI";
            this.rtbResultVI.Size = new System.Drawing.Size(444, 155);
            this.rtbResultVI.TabIndex = 18;
            this.rtbResultVI.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCenterName);
            this.groupBox2.Controls.Add(this.txtCenterUUID);
            this.groupBox2.Controls.Add(this.label61);
            this.groupBox2.Controls.Add(this.label62);
            this.groupBox2.Controls.Add(this.btnSaveVI);
            this.groupBox2.Controls.Add(this.btnSDSCenter);
            this.groupBox2.Controls.Add(this.btnNamuCenter);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtvCenterURL);
            this.groupBox2.Controls.Add(this.txtvCenterID);
            this.groupBox2.Controls.Add(this.txtvCenterPW);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(8, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(443, 102);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connection Information";
            // 
            // txtCenterName
            // 
            this.txtCenterName.Location = new System.Drawing.Point(74, 73);
            this.txtCenterName.Name = "txtCenterName";
            this.txtCenterName.Size = new System.Drawing.Size(111, 21);
            this.txtCenterName.TabIndex = 7;
            this.txtCenterName.Text = "vCenter SDS";
            // 
            // txtCenterUUID
            // 
            this.txtCenterUUID.Location = new System.Drawing.Point(242, 73);
            this.txtCenterUUID.Name = "txtCenterUUID";
            this.txtCenterUUID.Size = new System.Drawing.Size(198, 21);
            this.txtCenterUUID.TabIndex = 8;
            this.txtCenterUUID.Text = "7bd72515-5ae2-4f75-95f1-d2b80105e671";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(196, 79);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(40, 12);
            this.label61.TabIndex = 9;
            this.label61.Text = "UUID :";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(9, 78);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(47, 12);
            this.label62.TabIndex = 10;
            this.label62.Text = "Name :";
            // 
            // btnSaveVI
            // 
            this.btnSaveVI.Location = new System.Drawing.Point(388, 47);
            this.btnSaveVI.Name = "btnSaveVI";
            this.btnSaveVI.Size = new System.Drawing.Size(52, 23);
            this.btnSaveVI.TabIndex = 6;
            this.btnSaveVI.Text = "Save";
            this.btnSaveVI.UseVisualStyleBackColor = true;
            this.btnSaveVI.Click += new System.EventHandler(this.btnSaveVI_Click);
            // 
            // btnSDSCenter
            // 
            this.btnSDSCenter.Location = new System.Drawing.Point(395, 18);
            this.btnSDSCenter.Name = "btnSDSCenter";
            this.btnSDSCenter.Size = new System.Drawing.Size(45, 23);
            this.btnSDSCenter.TabIndex = 5;
            this.btnSDSCenter.Text = "SDS";
            this.btnSDSCenter.UseVisualStyleBackColor = true;
            this.btnSDSCenter.Click += new System.EventHandler(this.btnSDSCenter_Click);
            // 
            // btnNamuCenter
            // 
            this.btnNamuCenter.Location = new System.Drawing.Point(348, 18);
            this.btnNamuCenter.Name = "btnNamuCenter";
            this.btnNamuCenter.Size = new System.Drawing.Size(48, 23);
            this.btnNamuCenter.TabIndex = 4;
            this.btnNamuCenter.Text = "Namu";
            this.btnNamuCenter.UseVisualStyleBackColor = true;
            this.btnNamuCenter.Click += new System.EventHandler(this.btnNamuCenter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "CenterIP :";
            // 
            // txtvCenterURL
            // 
            this.txtvCenterURL.Location = new System.Drawing.Point(74, 19);
            this.txtvCenterURL.Name = "txtvCenterURL";
            this.txtvCenterURL.Size = new System.Drawing.Size(272, 21);
            this.txtvCenterURL.TabIndex = 2;
            this.txtvCenterURL.Text = "sbc-vcsa.cloud.corp.samsungelectronics.net";
            // 
            // txtvCenterID
            // 
            this.txtvCenterID.Location = new System.Drawing.Point(74, 48);
            this.txtvCenterID.Name = "txtvCenterID";
            this.txtvCenterID.Size = new System.Drawing.Size(150, 21);
            this.txtvCenterID.TabIndex = 2;
            this.txtvCenterID.Text = "Cloud\\vdi_master";
            // 
            // txtvCenterPW
            // 
            this.txtvCenterPW.Location = new System.Drawing.Point(303, 48);
            this.txtvCenterPW.Name = "txtvCenterPW";
            this.txtvCenterPW.PasswordChar = '*';
            this.txtvCenterPW.Size = new System.Drawing.Size(83, 21);
            this.txtvCenterPW.TabIndex = 2;
            this.txtvCenterPW.Text = "wpfl21)@Cp";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(231, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "CenterPW :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "CenterID :";
            // 
            // lstvCenterResult
            // 
            this.lstvCenterResult.AllowDrop = true;
            this.lstvCenterResult.FormattingEnabled = true;
            this.lstvCenterResult.ItemHeight = 12;
            this.lstvCenterResult.Location = new System.Drawing.Point(8, 114);
            this.lstvCenterResult.Name = "lstvCenterResult";
            this.lstvCenterResult.Size = new System.Drawing.Size(443, 268);
            this.lstvCenterResult.Sorted = true;
            this.lstvCenterResult.TabIndex = 6;
            this.lstvCenterResult.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstvCenterResult_MouseDown);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnVMDetails);
            this.groupBox4.Controls.Add(this.loopyn);
            this.groupBox4.Controls.Add(this.panel6);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.panel3);
            this.groupBox4.Controls.Add(this.panel2);
            this.groupBox4.Controls.Add(this.panel1);
            this.groupBox4.Controls.Add(this.txtvCenterVMName);
            this.groupBox4.Controls.Add(this.btnXenServerDebug);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Location = new System.Drawing.Point(702, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(376, 535);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Control";
            // 
            // btnVMDetails
            // 
            this.btnVMDetails.Location = new System.Drawing.Point(294, 13);
            this.btnVMDetails.Name = "btnVMDetails";
            this.btnVMDetails.Size = new System.Drawing.Size(70, 21);
            this.btnVMDetails.TabIndex = 13;
            this.btnVMDetails.Text = "Run";
            this.btnVMDetails.UseVisualStyleBackColor = true;
            this.btnVMDetails.Click += new System.EventHandler(this.btnVMDetails_Click);
            // 
            // loopyn
            // 
            this.loopyn.AutoSize = true;
            this.loopyn.Checked = true;
            this.loopyn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.loopyn.Location = new System.Drawing.Point(183, 506);
            this.loopyn.Name = "loopyn";
            this.loopyn.Size = new System.Drawing.Size(61, 16);
            this.loopyn.TabIndex = 29;
            this.loopyn.Text = "On/Off";
            this.loopyn.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.rbtnXenServerFastClone);
            this.panel6.Controls.Add(this.txtDomainUserID);
            this.panel6.Controls.Add(this.rbtnXenServerFullCopy);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.btnXenServerRemove);
            this.panel6.Controls.Add(this.txtLocalPW);
            this.panel6.Controls.Add(this.btnXenServerVMProvision);
            this.panel6.Controls.Add(this.label20);
            this.panel6.Controls.Add(this.txtXenServerTemplateKind);
            this.panel6.Controls.Add(this.txtLocalID);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.label21);
            this.panel6.Controls.Add(this.txtXenServerStorageKind);
            this.panel6.Controls.Add(this.txtNetbios);
            this.panel6.Controls.Add(this.label19);
            this.panel6.Controls.Add(this.txtDomainPW);
            this.panel6.Controls.Add(this.label18);
            this.panel6.Controls.Add(this.txtDNS2_F);
            this.panel6.Controls.Add(this.label16);
            this.panel6.Controls.Add(this.txtDNS1_F);
            this.panel6.Controls.Add(this.label15);
            this.panel6.Controls.Add(this.txtGateway_F);
            this.panel6.Controls.Add(this.label14);
            this.panel6.Controls.Add(this.txtSubnetMask_F);
            this.panel6.Controls.Add(this.label13);
            this.panel6.Controls.Add(this.txtDomainID);
            this.panel6.Controls.Add(this.txtIPAddress_F);
            this.panel6.Controls.Add(this.label17);
            this.panel6.Controls.Add(this.label12);
            this.panel6.Location = new System.Drawing.Point(6, 251);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(358, 243);
            this.panel6.TabIndex = 28;
            // 
            // rbtnXenServerFastClone
            // 
            this.rbtnXenServerFastClone.AutoSize = true;
            this.rbtnXenServerFastClone.Location = new System.Drawing.Point(19, 220);
            this.rbtnXenServerFastClone.Name = "rbtnXenServerFastClone";
            this.rbtnXenServerFastClone.Size = new System.Drawing.Size(80, 16);
            this.rbtnXenServerFastClone.TabIndex = 21;
            this.rbtnXenServerFastClone.Text = "FastClone";
            this.rbtnXenServerFastClone.UseVisualStyleBackColor = true;
            // 
            // txtDomainUserID
            // 
            this.txtDomainUserID.Location = new System.Drawing.Point(272, 76);
            this.txtDomainUserID.Name = "txtDomainUserID";
            this.txtDomainUserID.Size = new System.Drawing.Size(82, 21);
            this.txtDomainUserID.TabIndex = 17;
            this.txtDomainUserID.Text = "hjkim";
            // 
            // rbtnXenServerFullCopy
            // 
            this.rbtnXenServerFullCopy.AutoSize = true;
            this.rbtnXenServerFullCopy.Checked = true;
            this.rbtnXenServerFullCopy.Location = new System.Drawing.Point(19, 200);
            this.rbtnXenServerFullCopy.Name = "rbtnXenServerFullCopy";
            this.rbtnXenServerFullCopy.Size = new System.Drawing.Size(73, 16);
            this.rbtnXenServerFullCopy.TabIndex = 20;
            this.rbtnXenServerFullCopy.TabStop = true;
            this.rbtnXenServerFullCopy.Text = "FullCopy";
            this.rbtnXenServerFullCopy.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(174, 79);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(97, 12);
            this.label22.TabIndex = 16;
            this.label22.Text = "Domain UserID :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "Template Kind :";
            // 
            // btnXenServerRemove
            // 
            this.btnXenServerRemove.Location = new System.Drawing.Point(234, 199);
            this.btnXenServerRemove.Name = "btnXenServerRemove";
            this.btnXenServerRemove.Size = new System.Drawing.Size(120, 36);
            this.btnXenServerRemove.TabIndex = 13;
            this.btnXenServerRemove.Text = "Remove";
            this.btnXenServerRemove.UseVisualStyleBackColor = true;
            // 
            // txtLocalPW
            // 
            this.txtLocalPW.Location = new System.Drawing.Point(272, 125);
            this.txtLocalPW.Name = "txtLocalPW";
            this.txtLocalPW.Size = new System.Drawing.Size(82, 21);
            this.txtLocalPW.TabIndex = 15;
            // 
            // btnXenServerVMProvision
            // 
            this.btnXenServerVMProvision.Location = new System.Drawing.Point(108, 199);
            this.btnXenServerVMProvision.Name = "btnXenServerVMProvision";
            this.btnXenServerVMProvision.Size = new System.Drawing.Size(120, 36);
            this.btnXenServerVMProvision.TabIndex = 13;
            this.btnXenServerVMProvision.Text = "Provision";
            this.btnXenServerVMProvision.UseVisualStyleBackColor = true;
            this.btnXenServerVMProvision.Click += new System.EventHandler(this.btnXenServerVMProvision_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(205, 128);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(66, 12);
            this.label20.TabIndex = 14;
            this.label20.Text = "Local PW :";
            // 
            // txtXenServerTemplateKind
            // 
            this.txtXenServerTemplateKind.AllowDrop = true;
            this.txtXenServerTemplateKind.Location = new System.Drawing.Point(108, 149);
            this.txtXenServerTemplateKind.Name = "txtXenServerTemplateKind";
            this.txtXenServerTemplateKind.Size = new System.Drawing.Size(246, 21);
            this.txtXenServerTemplateKind.TabIndex = 15;
            this.txtXenServerTemplateKind.Text = "Existing Win7 TEST Master Image";
            // 
            // txtLocalID
            // 
            this.txtLocalID.Location = new System.Drawing.Point(272, 101);
            this.txtLocalID.Name = "txtLocalID";
            this.txtLocalID.Size = new System.Drawing.Size(82, 21);
            this.txtLocalID.TabIndex = 13;
            this.txtLocalID.Text = "administrator";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "Storage Kind :";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(212, 104);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(59, 12);
            this.label21.TabIndex = 12;
            this.label21.Text = "Local ID :";
            // 
            // txtXenServerStorageKind
            // 
            this.txtXenServerStorageKind.AllowDrop = true;
            this.txtXenServerStorageKind.Location = new System.Drawing.Point(108, 173);
            this.txtXenServerStorageKind.Name = "txtXenServerStorageKind";
            this.txtXenServerStorageKind.Size = new System.Drawing.Size(246, 21);
            this.txtXenServerStorageKind.TabIndex = 19;
            this.txtXenServerStorageKind.Text = "DS1515_NFS";
            // 
            // txtNetbios
            // 
            this.txtNetbios.Location = new System.Drawing.Point(272, 52);
            this.txtNetbios.Name = "txtNetbios";
            this.txtNetbios.Size = new System.Drawing.Size(82, 21);
            this.txtNetbios.TabIndex = 11;
            this.txtNetbios.Text = "NAMU.DEV";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(215, 55);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 12);
            this.label19.TabIndex = 10;
            this.label19.Text = "Netbios :";
            // 
            // txtDomainPW
            // 
            this.txtDomainPW.Location = new System.Drawing.Point(272, 28);
            this.txtDomainPW.Name = "txtDomainPW";
            this.txtDomainPW.PasswordChar = '*';
            this.txtDomainPW.Size = new System.Drawing.Size(82, 21);
            this.txtDomainPW.TabIndex = 9;
            this.txtDomainPW.Text = "P@ssw0rd";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(157, 32);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(114, 12);
            this.label18.TabIndex = 8;
            this.label18.Text = "Domain AdminPW :";
            // 
            // txtDNS2_F
            // 
            this.txtDNS2_F.Location = new System.Drawing.Point(43, 101);
            this.txtDNS2_F.Name = "txtDNS2_F";
            this.txtDNS2_F.Size = new System.Drawing.Size(100, 21);
            this.txtDNS2_F.TabIndex = 7;
            this.txtDNS2_F.Text = "192.168.1.11";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 104);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(27, 12);
            this.label16.TabIndex = 6;
            this.label16.Text = "D2 :";
            // 
            // txtDNS1_F
            // 
            this.txtDNS1_F.Location = new System.Drawing.Point(43, 76);
            this.txtDNS1_F.Name = "txtDNS1_F";
            this.txtDNS1_F.Size = new System.Drawing.Size(100, 21);
            this.txtDNS1_F.TabIndex = 7;
            this.txtDNS1_F.Text = "192.168.1.10";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 79);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 12);
            this.label15.TabIndex = 6;
            this.label15.Text = "D1 :";
            // 
            // txtGateway_F
            // 
            this.txtGateway_F.Location = new System.Drawing.Point(43, 52);
            this.txtGateway_F.Name = "txtGateway_F";
            this.txtGateway_F.Size = new System.Drawing.Size(100, 21);
            this.txtGateway_F.TabIndex = 5;
            this.txtGateway_F.Text = "192.168.1.1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 55);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 12);
            this.label14.TabIndex = 4;
            this.label14.Text = "GW :";
            // 
            // txtSubnetMask_F
            // 
            this.txtSubnetMask_F.Location = new System.Drawing.Point(43, 28);
            this.txtSubnetMask_F.Name = "txtSubnetMask_F";
            this.txtSubnetMask_F.Size = new System.Drawing.Size(100, 21);
            this.txtSubnetMask_F.TabIndex = 3;
            this.txtSubnetMask_F.Text = "255.255.255.0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 31);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(30, 12);
            this.label13.TabIndex = 2;
            this.label13.Text = "SN :";
            // 
            // txtDomainID
            // 
            this.txtDomainID.Location = new System.Drawing.Point(272, 4);
            this.txtDomainID.Name = "txtDomainID";
            this.txtDomainID.Size = new System.Drawing.Size(82, 21);
            this.txtDomainID.TabIndex = 1;
            this.txtDomainID.Text = "administrator";
            // 
            // txtIPAddress_F
            // 
            this.txtIPAddress_F.Location = new System.Drawing.Point(43, 4);
            this.txtIPAddress_F.Name = "txtIPAddress_F";
            this.txtIPAddress_F.Size = new System.Drawing.Size(100, 21);
            this.txtIPAddress_F.TabIndex = 1;
            this.txtIPAddress_F.Text = "192.168.1.51";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(164, 7);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(107, 12);
            this.label17.TabIndex = 0;
            this.label17.Text = "Domain AdminID :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 7);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "IP :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(254, 500);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 26);
            this.button1.TabIndex = 19;
            this.button1.Text = "ThreadTest";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.btnvCenterAddNIC);
            this.panel3.Controls.Add(this.btnvCenterRemoveNIC);
            this.panel3.Controls.Add(this.txtvCenterNicName);
            this.panel3.Location = new System.Drawing.Point(6, 156);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(175, 89);
            this.panel3.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 12);
            this.label11.TabIndex = 17;
            this.label11.Text = "NIC Name :";
            // 
            // btnvCenterAddNIC
            // 
            this.btnvCenterAddNIC.Location = new System.Drawing.Point(4, 47);
            this.btnvCenterAddNIC.Name = "btnvCenterAddNIC";
            this.btnvCenterAddNIC.Size = new System.Drawing.Size(80, 37);
            this.btnvCenterAddNIC.TabIndex = 13;
            this.btnvCenterAddNIC.Text = "Add NIC";
            this.btnvCenterAddNIC.UseVisualStyleBackColor = true;
            // 
            // btnvCenterRemoveNIC
            // 
            this.btnvCenterRemoveNIC.Location = new System.Drawing.Point(88, 47);
            this.btnvCenterRemoveNIC.Name = "btnvCenterRemoveNIC";
            this.btnvCenterRemoveNIC.Size = new System.Drawing.Size(80, 37);
            this.btnvCenterRemoveNIC.TabIndex = 20;
            this.btnvCenterRemoveNIC.Text = "Remove NIC";
            this.btnvCenterRemoveNIC.UseVisualStyleBackColor = true;
            // 
            // txtvCenterNicName
            // 
            this.txtvCenterNicName.AllowDrop = true;
            this.txtvCenterNicName.Location = new System.Drawing.Point(84, 7);
            this.txtvCenterNicName.Name = "txtvCenterNicName";
            this.txtvCenterNicName.Size = new System.Drawing.Size(84, 21);
            this.txtvCenterNicName.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cmbvCenterPowerKind);
            this.panel2.Controls.Add(this.btnvCenterPowerRun);
            this.panel2.Controls.Add(this.lblPowerState);
            this.panel2.Location = new System.Drawing.Point(6, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(177, 111);
            this.panel2.TabIndex = 24;
            // 
            // cmbvCenterPowerKind
            // 
            this.cmbvCenterPowerKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbvCenterPowerKind.FormattingEnabled = true;
            this.cmbvCenterPowerKind.Location = new System.Drawing.Point(3, 22);
            this.cmbvCenterPowerKind.Name = "cmbvCenterPowerKind";
            this.cmbvCenterPowerKind.Size = new System.Drawing.Size(169, 20);
            this.cmbvCenterPowerKind.TabIndex = 24;
            // 
            // btnvCenterPowerRun
            // 
            this.btnvCenterPowerRun.Location = new System.Drawing.Point(3, 44);
            this.btnvCenterPowerRun.Name = "btnvCenterPowerRun";
            this.btnvCenterPowerRun.Size = new System.Drawing.Size(169, 30);
            this.btnvCenterPowerRun.TabIndex = 23;
            this.btnvCenterPowerRun.Text = "Run";
            this.btnvCenterPowerRun.UseVisualStyleBackColor = true;
            this.btnvCenterPowerRun.Click += new System.EventHandler(this.btnvCenterPowerRun_Click);
            // 
            // lblPowerState
            // 
            this.lblPowerState.AutoSize = true;
            this.lblPowerState.Location = new System.Drawing.Point(7, 7);
            this.lblPowerState.Name = "lblPowerState";
            this.lblPowerState.Size = new System.Drawing.Size(11, 12);
            this.lblPowerState.TabIndex = 22;
            this.lblPowerState.Text = "-";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblvCenterHDD3);
            this.panel1.Controls.Add(this.nudvCenterHdd3);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.lblvCenterHDD2);
            this.panel1.Controls.Add(this.nudvCenterHdd2);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblvCenterHDD1);
            this.panel1.Controls.Add(this.nudvCenterHdd1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnvCenterResourceChange);
            this.panel1.Controls.Add(this.lblvCenterCPU);
            this.panel1.Controls.Add(this.nudvCenterRAM);
            this.panel1.Controls.Add(this.lblvCenterRam);
            this.panel1.Controls.Add(this.nudvCenterVCPUCore);
            this.panel1.Location = new System.Drawing.Point(189, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(175, 206);
            this.panel1.TabIndex = 23;
            // 
            // lblvCenterHDD3
            // 
            this.lblvCenterHDD3.AutoSize = true;
            this.lblvCenterHDD3.Location = new System.Drawing.Point(58, 103);
            this.lblvCenterHDD3.Name = "lblvCenterHDD3";
            this.lblvCenterHDD3.Size = new System.Drawing.Size(11, 12);
            this.lblvCenterHDD3.TabIndex = 31;
            this.lblvCenterHDD3.Text = "-";
            this.lblvCenterHDD3.DoubleClick += new System.EventHandler(this.lblvCenterHDD2_DoubleClick);
            // 
            // nudvCenterHdd3
            // 
            this.nudvCenterHdd3.Location = new System.Drawing.Point(111, 100);
            this.nudvCenterHdd3.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.nudvCenterHdd3.Name = "nudvCenterHdd3";
            this.nudvCenterHdd3.Size = new System.Drawing.Size(56, 21);
            this.nudvCenterHdd3.TabIndex = 30;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 103);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 29;
            this.label10.Text = "HDD 3 :";
            // 
            // lblvCenterHDD2
            // 
            this.lblvCenterHDD2.AutoSize = true;
            this.lblvCenterHDD2.Location = new System.Drawing.Point(58, 80);
            this.lblvCenterHDD2.Name = "lblvCenterHDD2";
            this.lblvCenterHDD2.Size = new System.Drawing.Size(11, 12);
            this.lblvCenterHDD2.TabIndex = 28;
            this.lblvCenterHDD2.Text = "-";
            this.lblvCenterHDD2.DoubleClick += new System.EventHandler(this.lblvCenterHDD2_DoubleClick);
            // 
            // nudvCenterHdd2
            // 
            this.nudvCenterHdd2.Location = new System.Drawing.Point(111, 76);
            this.nudvCenterHdd2.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.nudvCenterHdd2.Name = "nudvCenterHdd2";
            this.nudvCenterHdd2.Size = new System.Drawing.Size(56, 21);
            this.nudvCenterHdd2.TabIndex = 27;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "HDD 2 :";
            // 
            // lblvCenterHDD1
            // 
            this.lblvCenterHDD1.AutoSize = true;
            this.lblvCenterHDD1.Location = new System.Drawing.Point(58, 56);
            this.lblvCenterHDD1.Name = "lblvCenterHDD1";
            this.lblvCenterHDD1.Size = new System.Drawing.Size(11, 12);
            this.lblvCenterHDD1.TabIndex = 25;
            this.lblvCenterHDD1.Text = "-";
            // 
            // nudvCenterHdd1
            // 
            this.nudvCenterHdd1.Location = new System.Drawing.Point(111, 52);
            this.nudvCenterHdd1.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.nudvCenterHdd1.Name = "nudvCenterHdd1";
            this.nudvCenterHdd1.Size = new System.Drawing.Size(56, 21);
            this.nudvCenterHdd1.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "HDD 1 :";
            // 
            // btnvCenterResourceChange
            // 
            this.btnvCenterResourceChange.Location = new System.Drawing.Point(5, 166);
            this.btnvCenterResourceChange.Name = "btnvCenterResourceChange";
            this.btnvCenterResourceChange.Size = new System.Drawing.Size(162, 33);
            this.btnvCenterResourceChange.TabIndex = 22;
            this.btnvCenterResourceChange.Text = "Resource Change";
            this.btnvCenterResourceChange.UseVisualStyleBackColor = true;
            this.btnvCenterResourceChange.Click += new System.EventHandler(this.btnvCenterResourceChange_Click);
            // 
            // lblvCenterCPU
            // 
            this.lblvCenterCPU.AutoSize = true;
            this.lblvCenterCPU.Location = new System.Drawing.Point(8, 7);
            this.lblvCenterCPU.Name = "lblvCenterCPU";
            this.lblvCenterCPU.Size = new System.Drawing.Size(44, 12);
            this.lblvCenterCPU.TabIndex = 17;
            this.lblvCenterCPU.Text = "vCPU :";
            this.lblvCenterCPU.DoubleClick += new System.EventHandler(this.lblvCenterCPU_DoubleClick);
            // 
            // nudvCenterRAM
            // 
            this.nudvCenterRAM.Location = new System.Drawing.Point(58, 28);
            this.nudvCenterRAM.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.nudvCenterRAM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudvCenterRAM.Name = "nudvCenterRAM";
            this.nudvCenterRAM.Size = new System.Drawing.Size(109, 21);
            this.nudvCenterRAM.TabIndex = 21;
            this.nudvCenterRAM.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblvCenterRam
            // 
            this.lblvCenterRam.AutoSize = true;
            this.lblvCenterRam.Location = new System.Drawing.Point(12, 31);
            this.lblvCenterRam.Name = "lblvCenterRam";
            this.lblvCenterRam.Size = new System.Drawing.Size(40, 12);
            this.lblvCenterRam.TabIndex = 17;
            this.lblvCenterRam.Text = "RAM :";
            this.lblvCenterRam.DoubleClick += new System.EventHandler(this.lblvCenterRam_DoubleClick);
            // 
            // nudvCenterVCPUCore
            // 
            this.nudvCenterVCPUCore.Location = new System.Drawing.Point(58, 4);
            this.nudvCenterVCPUCore.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudvCenterVCPUCore.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudvCenterVCPUCore.Name = "nudvCenterVCPUCore";
            this.nudvCenterVCPUCore.Size = new System.Drawing.Size(109, 21);
            this.nudvCenterVCPUCore.TabIndex = 21;
            this.nudvCenterVCPUCore.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtvCenterVMName
            // 
            this.txtvCenterVMName.AllowDrop = true;
            this.txtvCenterVMName.Location = new System.Drawing.Point(82, 13);
            this.txtvCenterVMName.Name = "txtvCenterVMName";
            this.txtvCenterVMName.Size = new System.Drawing.Size(206, 21);
            this.txtvCenterVMName.TabIndex = 16;
            this.txtvCenterVMName.Text = "W10-ID1-001-1";
            this.txtvCenterVMName.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtvCenterVMName_DragDrop);
            this.txtvCenterVMName.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtvCenterVMName_DragEnter);
            // 
            // btnXenServerDebug
            // 
            this.btnXenServerDebug.Location = new System.Drawing.Point(6, 500);
            this.btnXenServerDebug.Name = "btnXenServerDebug";
            this.btnXenServerDebug.Size = new System.Drawing.Size(109, 26);
            this.btnXenServerDebug.TabIndex = 8;
            this.btnXenServerDebug.Text = "Debug";
            this.btnXenServerDebug.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "VM Name :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnResetVM);
            this.groupBox1.Controls.Add(this.btnChangeName);
            this.groupBox1.Controls.Add(this.txtVMName2);
            this.groupBox1.Controls.Add(this.label58);
            this.groupBox1.Controls.Add(this.txtVMName1);
            this.groupBox1.Controls.Add(this.label57);
            this.groupBox1.Controls.Add(this.txtDatacenterName);
            this.groupBox1.Controls.Add(this.lblDatacenterName);
            this.groupBox1.Controls.Add(this.btnClusterDetail);
            this.groupBox1.Controls.Add(this.txtCenterCluster);
            this.groupBox1.Controls.Add(this.lblCluster);
            this.groupBox1.Controls.Add(this.btnvCenterClusterJson);
            this.groupBox1.Controls.Add(this.btnvCenterVLANJson);
            this.groupBox1.Controls.Add(this.btnvCenterVLAN);
            this.groupBox1.Controls.Add(this.btnvCenterFolderJson);
            this.groupBox1.Controls.Add(this.btnvCenterFolder);
            this.groupBox1.Controls.Add(this.btnvCenterHostJson);
            this.groupBox1.Controls.Add(this.btnvCenterVMList);
            this.groupBox1.Controls.Add(this.btnvCenterResourcePoolJson);
            this.groupBox1.Controls.Add(this.btnvCenterTemplateList);
            this.groupBox1.Controls.Add(this.btnvCenterStorageJson);
            this.groupBox1.Controls.Add(this.btnvCenterHost);
            this.groupBox1.Controls.Add(this.btnvCenterResourcePool);
            this.groupBox1.Controls.Add(this.btnvCenterStorage);
            this.groupBox1.Controls.Add(this.chkXenServerOnlyCustomTemplate);
            this.groupBox1.Controls.Add(this.btnvCenterCluster);
            this.groupBox1.Location = new System.Drawing.Point(462, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(234, 535);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List Information";
            // 
            // btnResetVM
            // 
            this.btnResetVM.Location = new System.Drawing.Point(6, 373);
            this.btnResetVM.Name = "btnResetVM";
            this.btnResetVM.Size = new System.Drawing.Size(77, 30);
            this.btnResetVM.TabIndex = 30;
            this.btnResetVM.Text = "ResetVM";
            this.btnResetVM.UseVisualStyleBackColor = true;
            this.btnResetVM.Click += new System.EventHandler(this.btnResetVM_Click);
            // 
            // btnChangeName
            // 
            this.btnChangeName.Location = new System.Drawing.Point(85, 373);
            this.btnChangeName.Name = "btnChangeName";
            this.btnChangeName.Size = new System.Drawing.Size(143, 30);
            this.btnChangeName.TabIndex = 29;
            this.btnChangeName.Text = "ChangeName";
            this.btnChangeName.UseVisualStyleBackColor = true;
            this.btnChangeName.Click += new System.EventHandler(this.btnChangeName_Click);
            // 
            // txtVMName2
            // 
            this.txtVMName2.AllowDrop = true;
            this.txtVMName2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtVMName2.Location = new System.Drawing.Point(85, 346);
            this.txtVMName2.Name = "txtVMName2";
            this.txtVMName2.Size = new System.Drawing.Size(143, 21);
            this.txtVMName2.TabIndex = 28;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(7, 350);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(91, 12);
            this.label58.TabIndex = 27;
            this.label58.Text = "VM Rename  : ";
            // 
            // txtVMName1
            // 
            this.txtVMName1.AllowDrop = true;
            this.txtVMName1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtVMName1.Location = new System.Drawing.Point(85, 319);
            this.txtVMName1.Name = "txtVMName1";
            this.txtVMName1.Size = new System.Drawing.Size(143, 21);
            this.txtVMName1.TabIndex = 26;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(7, 323);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(74, 12);
            this.label57.TabIndex = 25;
            this.label57.Text = "VM Name : ";
            // 
            // txtDatacenterName
            // 
            this.txtDatacenterName.AllowDrop = true;
            this.txtDatacenterName.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtDatacenterName.Location = new System.Drawing.Point(84, 259);
            this.txtDatacenterName.Name = "txtDatacenterName";
            this.txtDatacenterName.Size = new System.Drawing.Size(143, 21);
            this.txtDatacenterName.TabIndex = 24;
            this.txtDatacenterName.Text = "Datacenter";
            // 
            // lblDatacenterName
            // 
            this.lblDatacenterName.AutoSize = true;
            this.lblDatacenterName.Location = new System.Drawing.Point(6, 263);
            this.lblDatacenterName.Name = "lblDatacenterName";
            this.lblDatacenterName.Size = new System.Drawing.Size(77, 12);
            this.lblDatacenterName.TabIndex = 23;
            this.lblDatacenterName.Text = "Datacenter : ";
            // 
            // btnClusterDetail
            // 
            this.btnClusterDetail.Location = new System.Drawing.Point(170, 89);
            this.btnClusterDetail.Name = "btnClusterDetail";
            this.btnClusterDetail.Size = new System.Drawing.Size(55, 25);
            this.btnClusterDetail.TabIndex = 22;
            this.btnClusterDetail.Text = "Detail";
            this.btnClusterDetail.UseVisualStyleBackColor = true;
            this.btnClusterDetail.Click += new System.EventHandler(this.btnClusterDetail_Click);
            // 
            // txtCenterCluster
            // 
            this.txtCenterCluster.AllowDrop = true;
            this.txtCenterCluster.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtCenterCluster.Location = new System.Drawing.Point(61, 92);
            this.txtCenterCluster.Name = "txtCenterCluster";
            this.txtCenterCluster.Size = new System.Drawing.Size(100, 21);
            this.txtCenterCluster.TabIndex = 21;
            this.txtCenterCluster.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtCenterCluster_DragDrop);
            this.txtCenterCluster.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtCenterCluster_DragEnter);
            // 
            // lblCluster
            // 
            this.lblCluster.AutoSize = true;
            this.lblCluster.Location = new System.Drawing.Point(6, 96);
            this.lblCluster.Name = "lblCluster";
            this.lblCluster.Size = new System.Drawing.Size(57, 12);
            this.lblCluster.TabIndex = 20;
            this.lblCluster.Text = "Cluster : ";
            // 
            // btnvCenterClusterJson
            // 
            this.btnvCenterClusterJson.Location = new System.Drawing.Point(162, 56);
            this.btnvCenterClusterJson.Name = "btnvCenterClusterJson";
            this.btnvCenterClusterJson.Size = new System.Drawing.Size(65, 30);
            this.btnvCenterClusterJson.TabIndex = 17;
            this.btnvCenterClusterJson.Text = "Json";
            this.btnvCenterClusterJson.UseVisualStyleBackColor = true;
            this.btnvCenterClusterJson.Click += new System.EventHandler(this.btnvCenterClusterJson_Click);
            // 
            // btnvCenterVLANJson
            // 
            this.btnvCenterVLANJson.Location = new System.Drawing.Point(162, 225);
            this.btnvCenterVLANJson.Name = "btnvCenterVLANJson";
            this.btnvCenterVLANJson.Size = new System.Drawing.Size(65, 30);
            this.btnvCenterVLANJson.TabIndex = 19;
            this.btnvCenterVLANJson.Text = "Json";
            this.btnvCenterVLANJson.UseVisualStyleBackColor = true;
            this.btnvCenterVLANJson.Click += new System.EventHandler(this.btnvCenterVLANJson_Click);
            // 
            // btnvCenterVLAN
            // 
            this.btnvCenterVLAN.Location = new System.Drawing.Point(6, 225);
            this.btnvCenterVLAN.Name = "btnvCenterVLAN";
            this.btnvCenterVLAN.Size = new System.Drawing.Size(150, 30);
            this.btnvCenterVLAN.TabIndex = 13;
            this.btnvCenterVLAN.Text = "vLan List";
            this.btnvCenterVLAN.UseVisualStyleBackColor = true;
            this.btnvCenterVLAN.Click += new System.EventHandler(this.btnvCenterVLAN_Click);
            // 
            // btnvCenterFolderJson
            // 
            this.btnvCenterFolderJson.Location = new System.Drawing.Point(162, 283);
            this.btnvCenterFolderJson.Name = "btnvCenterFolderJson";
            this.btnvCenterFolderJson.Size = new System.Drawing.Size(65, 30);
            this.btnvCenterFolderJson.TabIndex = 18;
            this.btnvCenterFolderJson.Text = "Json";
            this.btnvCenterFolderJson.UseVisualStyleBackColor = true;
            this.btnvCenterFolderJson.Click += new System.EventHandler(this.btnvCenterFolderJson_Click);
            // 
            // btnvCenterFolder
            // 
            this.btnvCenterFolder.Location = new System.Drawing.Point(6, 283);
            this.btnvCenterFolder.Name = "btnvCenterFolder";
            this.btnvCenterFolder.Size = new System.Drawing.Size(150, 30);
            this.btnvCenterFolder.TabIndex = 12;
            this.btnvCenterFolder.Text = "Folder List";
            this.btnvCenterFolder.UseVisualStyleBackColor = true;
            this.btnvCenterFolder.Click += new System.EventHandler(this.btnvCenterFolder_Click);
            // 
            // btnvCenterHostJson
            // 
            this.btnvCenterHostJson.Location = new System.Drawing.Point(162, 118);
            this.btnvCenterHostJson.Name = "btnvCenterHostJson";
            this.btnvCenterHostJson.Size = new System.Drawing.Size(65, 30);
            this.btnvCenterHostJson.TabIndex = 14;
            this.btnvCenterHostJson.Text = "Json";
            this.btnvCenterHostJson.UseVisualStyleBackColor = true;
            this.btnvCenterHostJson.Click += new System.EventHandler(this.btnvCenterHostJson_Click);
            // 
            // btnvCenterVMList
            // 
            this.btnvCenterVMList.Location = new System.Drawing.Point(6, 20);
            this.btnvCenterVMList.Name = "btnvCenterVMList";
            this.btnvCenterVMList.Size = new System.Drawing.Size(85, 30);
            this.btnvCenterVMList.TabIndex = 0;
            this.btnvCenterVMList.Text = "VMList";
            this.btnvCenterVMList.UseVisualStyleBackColor = true;
            this.btnvCenterVMList.Click += new System.EventHandler(this.btnvCenterVMList_Click);
            // 
            // btnvCenterResourcePoolJson
            // 
            this.btnvCenterResourcePoolJson.Location = new System.Drawing.Point(162, 153);
            this.btnvCenterResourcePoolJson.Name = "btnvCenterResourcePoolJson";
            this.btnvCenterResourcePoolJson.Size = new System.Drawing.Size(65, 30);
            this.btnvCenterResourcePoolJson.TabIndex = 15;
            this.btnvCenterResourcePoolJson.Text = "Json";
            this.btnvCenterResourcePoolJson.UseVisualStyleBackColor = true;
            this.btnvCenterResourcePoolJson.Click += new System.EventHandler(this.btnvCenterResourcePoolJson_Click);
            // 
            // btnvCenterTemplateList
            // 
            this.btnvCenterTemplateList.Location = new System.Drawing.Point(93, 20);
            this.btnvCenterTemplateList.Name = "btnvCenterTemplateList";
            this.btnvCenterTemplateList.Size = new System.Drawing.Size(90, 30);
            this.btnvCenterTemplateList.TabIndex = 0;
            this.btnvCenterTemplateList.Text = "TemplateList";
            this.btnvCenterTemplateList.UseVisualStyleBackColor = true;
            this.btnvCenterTemplateList.Click += new System.EventHandler(this.btnvCenterTemplateList_Click);
            // 
            // btnvCenterStorageJson
            // 
            this.btnvCenterStorageJson.Location = new System.Drawing.Point(162, 189);
            this.btnvCenterStorageJson.Name = "btnvCenterStorageJson";
            this.btnvCenterStorageJson.Size = new System.Drawing.Size(65, 30);
            this.btnvCenterStorageJson.TabIndex = 16;
            this.btnvCenterStorageJson.Text = "Json";
            this.btnvCenterStorageJson.UseVisualStyleBackColor = true;
            this.btnvCenterStorageJson.Click += new System.EventHandler(this.btnvCenterStorageJson_Click);
            // 
            // btnvCenterHost
            // 
            this.btnvCenterHost.Location = new System.Drawing.Point(6, 118);
            this.btnvCenterHost.Name = "btnvCenterHost";
            this.btnvCenterHost.Size = new System.Drawing.Size(150, 30);
            this.btnvCenterHost.TabIndex = 4;
            this.btnvCenterHost.Text = "Host List";
            this.btnvCenterHost.UseVisualStyleBackColor = true;
            this.btnvCenterHost.Click += new System.EventHandler(this.btnvCenterHost_Click);
            // 
            // btnvCenterResourcePool
            // 
            this.btnvCenterResourcePool.Location = new System.Drawing.Point(6, 153);
            this.btnvCenterResourcePool.Name = "btnvCenterResourcePool";
            this.btnvCenterResourcePool.Size = new System.Drawing.Size(150, 30);
            this.btnvCenterResourcePool.TabIndex = 5;
            this.btnvCenterResourcePool.Text = "ResourcePool List";
            this.btnvCenterResourcePool.UseVisualStyleBackColor = true;
            this.btnvCenterResourcePool.Click += new System.EventHandler(this.btnvCenterResourcePool_Click);
            // 
            // btnvCenterStorage
            // 
            this.btnvCenterStorage.Location = new System.Drawing.Point(6, 189);
            this.btnvCenterStorage.Name = "btnvCenterStorage";
            this.btnvCenterStorage.Size = new System.Drawing.Size(150, 30);
            this.btnvCenterStorage.TabIndex = 5;
            this.btnvCenterStorage.Text = "Storage List";
            this.btnvCenterStorage.UseVisualStyleBackColor = true;
            this.btnvCenterStorage.Click += new System.EventHandler(this.btnvCenterStorage_Click);
            // 
            // chkXenServerOnlyCustomTemplate
            // 
            this.chkXenServerOnlyCustomTemplate.AutoSize = true;
            this.chkXenServerOnlyCustomTemplate.Checked = true;
            this.chkXenServerOnlyCustomTemplate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkXenServerOnlyCustomTemplate.Location = new System.Drawing.Point(189, 26);
            this.chkXenServerOnlyCustomTemplate.Name = "chkXenServerOnlyCustomTemplate";
            this.chkXenServerOnlyCustomTemplate.Size = new System.Drawing.Size(94, 16);
            this.chkXenServerOnlyCustomTemplate.TabIndex = 9;
            this.chkXenServerOnlyCustomTemplate.Text = "OnlyCustom";
            this.chkXenServerOnlyCustomTemplate.UseVisualStyleBackColor = true;
            // 
            // btnvCenterCluster
            // 
            this.btnvCenterCluster.Location = new System.Drawing.Point(6, 57);
            this.btnvCenterCluster.Name = "btnvCenterCluster";
            this.btnvCenterCluster.Size = new System.Drawing.Size(150, 30);
            this.btnvCenterCluster.TabIndex = 5;
            this.btnvCenterCluster.Text = "Cluster List";
            this.btnvCenterCluster.UseVisualStyleBackColor = true;
            this.btnvCenterCluster.Click += new System.EventHandler(this.btnvCenterCluster_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtbResult);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.lstHorizonResult);
            this.tabPage2.Controls.Add(this.groupBox12);
            this.tabPage2.Controls.Add(this.groupBox11);
            this.tabPage2.Controls.Add(this.groupBox10);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1086, 565);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "HorizonView";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rtbResult
            // 
            this.rtbResult.Location = new System.Drawing.Point(8, 406);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(426, 146);
            this.rtbResult.TabIndex = 36;
            this.rtbResult.Text = "";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label56);
            this.groupBox5.Controls.Add(this.txtDomainSetUserPW);
            this.groupBox5.Controls.Add(this.btnAgentSetting);
            this.groupBox5.Controls.Add(this.txtAddressStateCallBack);
            this.groupBox5.Controls.Add(this.label48);
            this.groupBox5.Controls.Add(this.label49);
            this.groupBox5.Controls.Add(this.txtAddressDNS1);
            this.groupBox5.Controls.Add(this.label50);
            this.groupBox5.Controls.Add(this.txtAddressDNS2);
            this.groupBox5.Controls.Add(this.txtAddressGateway);
            this.groupBox5.Controls.Add(this.txtAddressSubnet);
            this.groupBox5.Controls.Add(this.txtAddressIP);
            this.groupBox5.Controls.Add(this.label51);
            this.groupBox5.Controls.Add(this.label52);
            this.groupBox5.Controls.Add(this.label53);
            this.groupBox5.Controls.Add(this.txtDomainOUDC);
            this.groupBox5.Controls.Add(this.label47);
            this.groupBox5.Controls.Add(this.label46);
            this.groupBox5.Controls.Add(this.txtDomainAdminPW);
            this.groupBox5.Controls.Add(this.label45);
            this.groupBox5.Controls.Add(this.label44);
            this.groupBox5.Controls.Add(this.txtDomainSetUserID);
            this.groupBox5.Controls.Add(this.txtDomainAdminID);
            this.groupBox5.Controls.Add(this.txtDomainFQDN);
            this.groupBox5.Controls.Add(this.txtDomainNetbios);
            this.groupBox5.Controls.Add(this.label43);
            this.groupBox5.Controls.Add(this.label42);
            this.groupBox5.Controls.Add(this.label41);
            this.groupBox5.Controls.Add(this.label40);
            this.groupBox5.Location = new System.Drawing.Point(441, 117);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(221, 435);
            this.groupBox5.TabIndex = 18;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "System Setting";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(7, 149);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(61, 12);
            this.label56.TabIndex = 35;
            this.label56.Text = "UserPW : ";
            // 
            // txtDomainSetUserPW
            // 
            this.txtDomainSetUserPW.Location = new System.Drawing.Point(75, 144);
            this.txtDomainSetUserPW.Name = "txtDomainSetUserPW";
            this.txtDomainSetUserPW.PasswordChar = '*';
            this.txtDomainSetUserPW.Size = new System.Drawing.Size(141, 21);
            this.txtDomainSetUserPW.TabIndex = 27;
            // 
            // btnAgentSetting
            // 
            this.btnAgentSetting.Location = new System.Drawing.Point(19, 383);
            this.btnAgentSetting.Name = "btnAgentSetting";
            this.btnAgentSetting.Size = new System.Drawing.Size(184, 36);
            this.btnAgentSetting.TabIndex = 35;
            this.btnAgentSetting.Text = "Agent Setting";
            this.btnAgentSetting.UseVisualStyleBackColor = true;
            this.btnAgentSetting.Click += new System.EventHandler(this.btnAgentSetting_Click);
            // 
            // txtAddressStateCallBack
            // 
            this.txtAddressStateCallBack.Location = new System.Drawing.Point(7, 357);
            this.txtAddressStateCallBack.Name = "txtAddressStateCallBack";
            this.txtAddressStateCallBack.Size = new System.Drawing.Size(209, 21);
            this.txtAddressStateCallBack.TabIndex = 34;
            this.txtAddressStateCallBack.Text = "192.168.1.95,9081/192.168.1.96,9081";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(8, 339);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(169, 12);
            this.label48.TabIndex = 24;
            this.label48.Text = "StateCallBack_IPAddressPort";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(8, 317);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(48, 12);
            this.label49.TabIndex = 23;
            this.label49.Text = "DNS2 : ";
            // 
            // txtAddressDNS1
            // 
            this.txtAddressDNS1.Location = new System.Drawing.Point(75, 289);
            this.txtAddressDNS1.Name = "txtAddressDNS1";
            this.txtAddressDNS1.Size = new System.Drawing.Size(141, 21);
            this.txtAddressDNS1.TabIndex = 32;
            this.txtAddressDNS1.Text = "192.168.50.2";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(8, 295);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(48, 12);
            this.label50.TabIndex = 22;
            this.label50.Text = "DNS1 : ";
            // 
            // txtAddressDNS2
            // 
            this.txtAddressDNS2.Location = new System.Drawing.Point(75, 311);
            this.txtAddressDNS2.Name = "txtAddressDNS2";
            this.txtAddressDNS2.Size = new System.Drawing.Size(141, 21);
            this.txtAddressDNS2.TabIndex = 33;
            this.txtAddressDNS2.Text = "192.168.50.3";
            // 
            // txtAddressGateway
            // 
            this.txtAddressGateway.Location = new System.Drawing.Point(75, 267);
            this.txtAddressGateway.Name = "txtAddressGateway";
            this.txtAddressGateway.Size = new System.Drawing.Size(141, 21);
            this.txtAddressGateway.TabIndex = 31;
            this.txtAddressGateway.Text = "192.168.101.1";
            // 
            // txtAddressSubnet
            // 
            this.txtAddressSubnet.Location = new System.Drawing.Point(75, 245);
            this.txtAddressSubnet.Name = "txtAddressSubnet";
            this.txtAddressSubnet.Size = new System.Drawing.Size(141, 21);
            this.txtAddressSubnet.TabIndex = 30;
            this.txtAddressSubnet.Text = "255.255.255.0";
            // 
            // txtAddressIP
            // 
            this.txtAddressIP.Location = new System.Drawing.Point(75, 223);
            this.txtAddressIP.Name = "txtAddressIP";
            this.txtAddressIP.Size = new System.Drawing.Size(141, 21);
            this.txtAddressIP.TabIndex = 29;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(8, 274);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(67, 12);
            this.label51.TabIndex = 16;
            this.label51.Text = "Gateway : ";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(8, 251);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(56, 12);
            this.label52.TabIndex = 15;
            this.label52.Text = "Subnet : ";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(8, 229);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(28, 12);
            this.label53.TabIndex = 14;
            this.label53.Text = "IP : ";
            // 
            // txtDomainOUDC
            // 
            this.txtDomainOUDC.Location = new System.Drawing.Point(75, 166);
            this.txtDomainOUDC.Name = "txtDomainOUDC";
            this.txtDomainOUDC.Size = new System.Drawing.Size(141, 21);
            this.txtDomainOUDC.TabIndex = 28;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(7, 171);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(51, 12);
            this.label47.TabIndex = 12;
            this.label47.Text = "OUDC : ";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(7, 127);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(54, 12);
            this.label46.TabIndex = 11;
            this.label46.Text = "UserID : ";
            // 
            // txtDomainAdminPW
            // 
            this.txtDomainAdminPW.Location = new System.Drawing.Point(75, 100);
            this.txtDomainAdminPW.Name = "txtDomainAdminPW";
            this.txtDomainAdminPW.PasswordChar = '*';
            this.txtDomainAdminPW.Size = new System.Drawing.Size(141, 21);
            this.txtDomainAdminPW.TabIndex = 25;
            this.txtDomainAdminPW.Text = "namudev!23$";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(7, 105);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(71, 12);
            this.label45.TabIndex = 10;
            this.label45.Text = "AdminPW : ";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(7, 205);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(180, 12);
            this.label44.TabIndex = 9;
            this.label44.Text = "-------- Address info --------";
            // 
            // txtDomainSetUserID
            // 
            this.txtDomainSetUserID.Location = new System.Drawing.Point(75, 122);
            this.txtDomainSetUserID.Name = "txtDomainSetUserID";
            this.txtDomainSetUserID.Size = new System.Drawing.Size(141, 21);
            this.txtDomainSetUserID.TabIndex = 26;
            // 
            // txtDomainAdminID
            // 
            this.txtDomainAdminID.Location = new System.Drawing.Point(75, 78);
            this.txtDomainAdminID.Name = "txtDomainAdminID";
            this.txtDomainAdminID.Size = new System.Drawing.Size(141, 21);
            this.txtDomainAdminID.TabIndex = 24;
            this.txtDomainAdminID.Text = "administrator";
            // 
            // txtDomainFQDN
            // 
            this.txtDomainFQDN.Location = new System.Drawing.Point(75, 56);
            this.txtDomainFQDN.Name = "txtDomainFQDN";
            this.txtDomainFQDN.Size = new System.Drawing.Size(141, 21);
            this.txtDomainFQDN.TabIndex = 23;
            this.txtDomainFQDN.Text = "namurnd.io";
            // 
            // txtDomainNetbios
            // 
            this.txtDomainNetbios.Location = new System.Drawing.Point(75, 34);
            this.txtDomainNetbios.Name = "txtDomainNetbios";
            this.txtDomainNetbios.Size = new System.Drawing.Size(141, 21);
            this.txtDomainNetbios.TabIndex = 22;
            this.txtDomainNetbios.Text = "namurnd";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(7, 84);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(64, 12);
            this.label43.TabIndex = 3;
            this.label43.Text = "AdminID : ";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(7, 61);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(50, 12);
            this.label42.TabIndex = 2;
            this.label42.Text = "FQDN : ";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(7, 39);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(60, 12);
            this.label41.TabIndex = 1;
            this.label41.Text = "Netbios : ";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(7, 17);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(176, 12);
            this.label40.TabIndex = 0;
            this.label40.Text = "---------- Domain ----------";
            // 
            // lstHorizonResult
            // 
            this.lstHorizonResult.AllowDrop = true;
            this.lstHorizonResult.FormattingEnabled = true;
            this.lstHorizonResult.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lstHorizonResult.ItemHeight = 12;
            this.lstHorizonResult.Location = new System.Drawing.Point(8, 102);
            this.lstHorizonResult.Name = "lstHorizonResult";
            this.lstHorizonResult.Size = new System.Drawing.Size(426, 292);
            this.lstHorizonResult.Sorted = true;
            this.lstHorizonResult.TabIndex = 6;
            this.lstHorizonResult.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstHorizonResult_MouseDown);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.cmbMessageType);
            this.groupBox12.Controls.Add(this.btnSendMessage);
            this.groupBox12.Controls.Add(this.txtSendMessage);
            this.groupBox12.Controls.Add(this.lblSendMessage);
            this.groupBox12.Controls.Add(this.lblServicePort);
            this.groupBox12.Controls.Add(this.txtServicePort);
            this.groupBox12.Controls.Add(this.txtServiceIP);
            this.groupBox12.Controls.Add(this.lblServiceIP);
            this.groupBox12.Controls.Add(this.btnPoolVMStatus);
            this.groupBox12.Controls.Add(this.cmbVMAction);
            this.groupBox12.Controls.Add(this.btnVMStatus);
            this.groupBox12.Controls.Add(this.groupBox3);
            this.groupBox12.Controls.Add(this.btnVMAction);
            this.groupBox12.Controls.Add(this.txtHorizonSystemName);
            this.groupBox12.Controls.Add(this.label23);
            this.groupBox12.Controls.Add(this.btnPoolDetails);
            this.groupBox12.Controls.Add(this.txtHorizonPoolName);
            this.groupBox12.Controls.Add(this.label6);
            this.groupBox12.Location = new System.Drawing.Point(668, 6);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(410, 546);
            this.groupBox12.TabIndex = 17;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Control";
            // 
            // cmbMessageType
            // 
            this.cmbMessageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMessageType.FormattingEnabled = true;
            this.cmbMessageType.Items.AddRange(new object[] {
            "INFO",
            "ERROR"});
            this.cmbMessageType.Location = new System.Drawing.Point(303, 85);
            this.cmbMessageType.Name = "cmbMessageType";
            this.cmbMessageType.Size = new System.Drawing.Size(50, 20);
            this.cmbMessageType.TabIndex = 55;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(353, 83);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(50, 23);
            this.btnSendMessage.TabIndex = 54;
            this.btnSendMessage.Text = "Send";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // txtSendMessage
            // 
            this.txtSendMessage.AllowDrop = true;
            this.txtSendMessage.Location = new System.Drawing.Point(87, 84);
            this.txtSendMessage.Name = "txtSendMessage";
            this.txtSendMessage.Size = new System.Drawing.Size(215, 21);
            this.txtSendMessage.TabIndex = 53;
            // 
            // lblSendMessage
            // 
            this.lblSendMessage.AutoSize = true;
            this.lblSendMessage.Location = new System.Drawing.Point(10, 89);
            this.lblSendMessage.Name = "lblSendMessage";
            this.lblSendMessage.Size = new System.Drawing.Size(70, 12);
            this.lblSendMessage.TabIndex = 52;
            this.lblSendMessage.Text = "Message : ";
            // 
            // lblServicePort
            // 
            this.lblServicePort.AutoSize = true;
            this.lblServicePort.Location = new System.Drawing.Point(278, 17);
            this.lblServicePort.Name = "lblServicePort";
            this.lblServicePort.Size = new System.Drawing.Size(35, 12);
            this.lblServicePort.TabIndex = 51;
            this.lblServicePort.Text = "Port :";
            // 
            // txtServicePort
            // 
            this.txtServicePort.AllowDrop = true;
            this.txtServicePort.Location = new System.Drawing.Point(319, 12);
            this.txtServicePort.Name = "txtServicePort";
            this.txtServicePort.Size = new System.Drawing.Size(50, 21);
            this.txtServicePort.TabIndex = 50;
            this.txtServicePort.Text = "9721";
            // 
            // txtServiceIP
            // 
            this.txtServiceIP.AllowDrop = true;
            this.txtServiceIP.Location = new System.Drawing.Point(87, 12);
            this.txtServiceIP.Name = "txtServiceIP";
            this.txtServiceIP.Size = new System.Drawing.Size(185, 21);
            this.txtServiceIP.TabIndex = 49;
            this.txtServiceIP.Text = "192.168.50.70";
            // 
            // lblServiceIP
            // 
            this.lblServiceIP.AutoSize = true;
            this.lblServiceIP.Location = new System.Drawing.Point(6, 18);
            this.lblServiceIP.Name = "lblServiceIP";
            this.lblServiceIP.Size = new System.Drawing.Size(70, 12);
            this.lblServiceIP.TabIndex = 48;
            this.lblServiceIP.Text = "Service IP :";
            // 
            // btnPoolVMStatus
            // 
            this.btnPoolVMStatus.Location = new System.Drawing.Point(272, 35);
            this.btnPoolVMStatus.Name = "btnPoolVMStatus";
            this.btnPoolVMStatus.Size = new System.Drawing.Size(65, 23);
            this.btnPoolVMStatus.TabIndex = 47;
            this.btnPoolVMStatus.Text = "Status";
            this.btnPoolVMStatus.UseVisualStyleBackColor = true;
            this.btnPoolVMStatus.Click += new System.EventHandler(this.btnPoolVMStatus_Click);
            // 
            // cmbVMAction
            // 
            this.cmbVMAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVMAction.FormattingEnabled = true;
            this.cmbVMAction.Items.AddRange(new object[] {
            "Start",
            "Restart",
            "ForceRestart",
            "Shutdown",
            "ForceShutdown",
            "Suspend",
            "Resume",
            "LogOff",
            "Disconnect",
            "MaintenanceOn",
            "MaintenanceOff"});
            this.cmbVMAction.Location = new System.Drawing.Point(262, 60);
            this.cmbVMAction.Name = "cmbVMAction";
            this.cmbVMAction.Size = new System.Drawing.Size(91, 20);
            this.cmbVMAction.TabIndex = 46;
            // 
            // btnVMStatus
            // 
            this.btnVMStatus.Location = new System.Drawing.Point(212, 59);
            this.btnVMStatus.Name = "btnVMStatus";
            this.btnVMStatus.Size = new System.Drawing.Size(50, 23);
            this.btnVMStatus.TabIndex = 7;
            this.btnVMStatus.Text = "Status";
            this.btnVMStatus.UseVisualStyleBackColor = true;
            this.btnVMStatus.Click += new System.EventHandler(this.btnVMStatus_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnClusterResourcePoolDiscovery);
            this.groupBox3.Controls.Add(this.btnVMFolderDiscovery);
            this.groupBox3.Controls.Add(this.btnClusterDiscovery);
            this.groupBox3.Controls.Add(this.cmbLanguage);
            this.groupBox3.Controls.Add(this.lbl_Language);
            this.groupBox3.Controls.Add(this.cmbCustType);
            this.groupBox3.Controls.Add(this.label55);
            this.groupBox3.Controls.Add(this.label54);
            this.groupBox3.Controls.Add(this.btnDatastoreList);
            this.groupBox3.Controls.Add(this.btnResourcePoolList);
            this.groupBox3.Controls.Add(this.btnHostClusterList);
            this.groupBox3.Controls.Add(this.btnVMFolderList);
            this.groupBox3.Controls.Add(this.btnSnapshotList);
            this.groupBox3.Controls.Add(this.btnParentVMList);
            this.groupBox3.Controls.Add(this.btnTemplateList);
            this.groupBox3.Controls.Add(this.btnClonePool);
            this.groupBox3.Controls.Add(this.btnDeletePool);
            this.groupBox3.Controls.Add(this.btnEditPool);
            this.groupBox3.Controls.Add(this.btnNewPool);
            this.groupBox3.Controls.Add(this.txtNamingRule);
            this.groupBox3.Controls.Add(this.label39);
            this.groupBox3.Controls.Add(this.txtDatastorePaths);
            this.groupBox3.Controls.Add(this.txtResourcePoolPath);
            this.groupBox3.Controls.Add(this.txtHostOrClusterPath);
            this.groupBox3.Controls.Add(this.txtVmFolderPath);
            this.groupBox3.Controls.Add(this.txtDatacenterPath);
            this.groupBox3.Controls.Add(this.txtSnapshotPath);
            this.groupBox3.Controls.Add(this.txtParentVMPath);
            this.groupBox3.Controls.Add(this.label38);
            this.groupBox3.Controls.Add(this.label37);
            this.groupBox3.Controls.Add(this.label36);
            this.groupBox3.Controls.Add(this.label35);
            this.groupBox3.Controls.Add(this.label34);
            this.groupBox3.Controls.Add(this.label33);
            this.groupBox3.Controls.Add(this.label32);
            this.groupBox3.Controls.Add(this.txtTemplatePath);
            this.groupBox3.Controls.Add(this.nudNumSessions);
            this.groupBox3.Controls.Add(this.nudNumMachines);
            this.groupBox3.Controls.Add(this.cmbProvisionEnabled);
            this.groupBox3.Controls.Add(this.cmbPoolEnabled);
            this.groupBox3.Controls.Add(this.cmbPoolSource);
            this.groupBox3.Controls.Add(this.chkUserAutoAssign);
            this.groupBox3.Controls.Add(this.cmbUserAssign);
            this.groupBox3.Controls.Add(this.cmbPoolType);
            this.groupBox3.Controls.Add(this.label31);
            this.groupBox3.Controls.Add(this.label30);
            this.groupBox3.Controls.Add(this.label29);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.label27);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Location = new System.Drawing.Point(7, 112);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(396, 432);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Detail/Provisioning";
            // 
            // btnClusterResourcePoolDiscovery
            // 
            this.btnClusterResourcePoolDiscovery.Location = new System.Drawing.Point(282, 343);
            this.btnClusterResourcePoolDiscovery.Name = "btnClusterResourcePoolDiscovery";
            this.btnClusterResourcePoolDiscovery.Size = new System.Drawing.Size(50, 23);
            this.btnClusterResourcePoolDiscovery.TabIndex = 50;
            this.btnClusterResourcePoolDiscovery.Text = "disc";
            this.btnClusterResourcePoolDiscovery.UseVisualStyleBackColor = true;
            this.btnClusterResourcePoolDiscovery.Click += new System.EventHandler(this.btnClusterResourcePoolDiscovery_Click);
            // 
            // btnVMFolderDiscovery
            // 
            this.btnVMFolderDiscovery.Location = new System.Drawing.Point(282, 299);
            this.btnVMFolderDiscovery.Name = "btnVMFolderDiscovery";
            this.btnVMFolderDiscovery.Size = new System.Drawing.Size(50, 23);
            this.btnVMFolderDiscovery.TabIndex = 49;
            this.btnVMFolderDiscovery.Text = "disc";
            this.btnVMFolderDiscovery.UseVisualStyleBackColor = true;
            this.btnVMFolderDiscovery.Click += new System.EventHandler(this.btnVMFolderDiscovery_Click);
            // 
            // btnClusterDiscovery
            // 
            this.btnClusterDiscovery.Location = new System.Drawing.Point(282, 321);
            this.btnClusterDiscovery.Name = "btnClusterDiscovery";
            this.btnClusterDiscovery.Size = new System.Drawing.Size(50, 23);
            this.btnClusterDiscovery.TabIndex = 48;
            this.btnClusterDiscovery.Text = "disc";
            this.btnClusterDiscovery.UseVisualStyleBackColor = true;
            this.btnClusterDiscovery.Click += new System.EventHandler(this.btnClusterDiscovery_Click);
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Items.AddRange(new object[] {
            "EN",
            "KO"});
            this.cmbLanguage.Location = new System.Drawing.Point(311, 116);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(78, 20);
            this.cmbLanguage.TabIndex = 47;
            // 
            // lbl_Language
            // 
            this.lbl_Language.AutoSize = true;
            this.lbl_Language.Location = new System.Drawing.Point(233, 120);
            this.lbl_Language.Name = "lbl_Language";
            this.lbl_Language.Size = new System.Drawing.Size(73, 12);
            this.lbl_Language.TabIndex = 46;
            this.lbl_Language.Text = "Language : ";
            // 
            // cmbCustType
            // 
            this.cmbCustType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustType.FormattingEnabled = true;
            this.cmbCustType.Items.AddRange(new object[] {
            "CLONE_PREP",
            "QUICK_PREP",
            "SYS_PREP",
            "NONE"});
            this.cmbCustType.Location = new System.Drawing.Point(132, 191);
            this.cmbCustType.Name = "cmbCustType";
            this.cmbCustType.Size = new System.Drawing.Size(200, 20);
            this.cmbCustType.TabIndex = 14;
            this.cmbCustType.SelectedIndexChanged += new System.EventHandler(this.cmbCustType_SelectedIndexChanged);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(5, 195);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(127, 12);
            this.label55.TabIndex = 45;
            this.label55.Text = "CustomizationType : ";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label54.Font = new System.Drawing.Font("굴림", 9.5F);
            this.label54.Location = new System.Drawing.Point(211, 139);
            this.label54.Margin = new System.Windows.Forms.Padding(5);
            this.label54.Name = "label54";
            this.label54.Padding = new System.Windows.Forms.Padding(3);
            this.label54.Size = new System.Drawing.Size(132, 21);
            this.label54.TabIndex = 44;
            this.label54.Text = "ex) vm-{n:fixed=1}";
            // 
            // btnDatastoreList
            // 
            this.btnDatastoreList.Location = new System.Drawing.Point(333, 365);
            this.btnDatastoreList.Name = "btnDatastoreList";
            this.btnDatastoreList.Size = new System.Drawing.Size(56, 23);
            this.btnDatastoreList.TabIndex = 43;
            this.btnDatastoreList.Text = "search";
            this.btnDatastoreList.UseVisualStyleBackColor = true;
            this.btnDatastoreList.Click += new System.EventHandler(this.btnDatastoreList_Click);
            // 
            // btnResourcePoolList
            // 
            this.btnResourcePoolList.Location = new System.Drawing.Point(333, 343);
            this.btnResourcePoolList.Name = "btnResourcePoolList";
            this.btnResourcePoolList.Size = new System.Drawing.Size(56, 23);
            this.btnResourcePoolList.TabIndex = 42;
            this.btnResourcePoolList.Text = "search";
            this.btnResourcePoolList.UseVisualStyleBackColor = true;
            this.btnResourcePoolList.Click += new System.EventHandler(this.btnResourcePoolList_Click);
            // 
            // btnHostClusterList
            // 
            this.btnHostClusterList.Location = new System.Drawing.Point(333, 321);
            this.btnHostClusterList.Name = "btnHostClusterList";
            this.btnHostClusterList.Size = new System.Drawing.Size(56, 23);
            this.btnHostClusterList.TabIndex = 41;
            this.btnHostClusterList.Text = "search";
            this.btnHostClusterList.UseVisualStyleBackColor = true;
            this.btnHostClusterList.Click += new System.EventHandler(this.btnHostClusterList_Click);
            // 
            // btnVMFolderList
            // 
            this.btnVMFolderList.Location = new System.Drawing.Point(333, 299);
            this.btnVMFolderList.Name = "btnVMFolderList";
            this.btnVMFolderList.Size = new System.Drawing.Size(56, 23);
            this.btnVMFolderList.TabIndex = 40;
            this.btnVMFolderList.Text = "search";
            this.btnVMFolderList.UseVisualStyleBackColor = true;
            this.btnVMFolderList.Click += new System.EventHandler(this.btnVMFolderList_Click);
            // 
            // btnSnapshotList
            // 
            this.btnSnapshotList.Location = new System.Drawing.Point(333, 255);
            this.btnSnapshotList.Name = "btnSnapshotList";
            this.btnSnapshotList.Size = new System.Drawing.Size(56, 23);
            this.btnSnapshotList.TabIndex = 39;
            this.btnSnapshotList.Text = "search";
            this.btnSnapshotList.UseVisualStyleBackColor = true;
            this.btnSnapshotList.Click += new System.EventHandler(this.btnSnapshotList_Click);
            // 
            // btnParentVMList
            // 
            this.btnParentVMList.Location = new System.Drawing.Point(333, 233);
            this.btnParentVMList.Name = "btnParentVMList";
            this.btnParentVMList.Size = new System.Drawing.Size(56, 23);
            this.btnParentVMList.TabIndex = 38;
            this.btnParentVMList.Text = "search";
            this.btnParentVMList.UseVisualStyleBackColor = true;
            this.btnParentVMList.Click += new System.EventHandler(this.btnParentVMList_Click);
            // 
            // btnTemplateList
            // 
            this.btnTemplateList.Location = new System.Drawing.Point(333, 211);
            this.btnTemplateList.Name = "btnTemplateList";
            this.btnTemplateList.Size = new System.Drawing.Size(56, 23);
            this.btnTemplateList.TabIndex = 37;
            this.btnTemplateList.Text = "search";
            this.btnTemplateList.UseVisualStyleBackColor = true;
            this.btnTemplateList.Click += new System.EventHandler(this.btnTemplateList_Click);
            // 
            // btnClonePool
            // 
            this.btnClonePool.Location = new System.Drawing.Point(294, 393);
            this.btnClonePool.Name = "btnClonePool";
            this.btnClonePool.Size = new System.Drawing.Size(95, 35);
            this.btnClonePool.TabIndex = 36;
            this.btnClonePool.Text = "Clone Pool";
            this.btnClonePool.UseVisualStyleBackColor = true;
            this.btnClonePool.Click += new System.EventHandler(this.btnClonePool_Click);
            // 
            // btnDeletePool
            // 
            this.btnDeletePool.Location = new System.Drawing.Point(198, 393);
            this.btnDeletePool.Name = "btnDeletePool";
            this.btnDeletePool.Size = new System.Drawing.Size(95, 35);
            this.btnDeletePool.TabIndex = 35;
            this.btnDeletePool.Text = "Delete Pool";
            this.btnDeletePool.UseVisualStyleBackColor = true;
            this.btnDeletePool.Click += new System.EventHandler(this.btnDeletePool_Click);
            // 
            // btnEditPool
            // 
            this.btnEditPool.Location = new System.Drawing.Point(102, 393);
            this.btnEditPool.Name = "btnEditPool";
            this.btnEditPool.Size = new System.Drawing.Size(95, 35);
            this.btnEditPool.TabIndex = 34;
            this.btnEditPool.Text = "Edit Pool";
            this.btnEditPool.UseVisualStyleBackColor = true;
            this.btnEditPool.Click += new System.EventHandler(this.btnEditPool_Click);
            // 
            // btnNewPool
            // 
            this.btnNewPool.Location = new System.Drawing.Point(6, 393);
            this.btnNewPool.Name = "btnNewPool";
            this.btnNewPool.Size = new System.Drawing.Size(95, 35);
            this.btnNewPool.TabIndex = 33;
            this.btnNewPool.Text = "New Pool";
            this.btnNewPool.UseVisualStyleBackColor = true;
            this.btnNewPool.Click += new System.EventHandler(this.btnNewPool_Click);
            // 
            // txtNamingRule
            // 
            this.txtNamingRule.Location = new System.Drawing.Point(95, 139);
            this.txtNamingRule.Name = "txtNamingRule";
            this.txtNamingRule.Size = new System.Drawing.Size(111, 21);
            this.txtNamingRule.TabIndex = 12;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(5, 145);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(90, 12);
            this.label39.TabIndex = 31;
            this.label39.Text = "Naming Rule : ";
            // 
            // txtDatastorePaths
            // 
            this.txtDatastorePaths.AllowDrop = true;
            this.txtDatastorePaths.Location = new System.Drawing.Point(132, 366);
            this.txtDatastorePaths.Name = "txtDatastorePaths";
            this.txtDatastorePaths.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDatastorePaths.Size = new System.Drawing.Size(200, 21);
            this.txtDatastorePaths.TabIndex = 21;
            this.datastoreTootip.SetToolTip(this.txtDatastorePaths, "VM을 저장할 데이터 스토어 이름 (Full, Linked, Instant Clone Pools.)");
            this.txtDatastorePaths.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtDatastorePaths_DragDrop);
            this.txtDatastorePaths.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtDatastorePaths_DragEnter);
            // 
            // txtResourcePoolPath
            // 
            this.txtResourcePoolPath.AllowDrop = true;
            this.txtResourcePoolPath.Location = new System.Drawing.Point(132, 344);
            this.txtResourcePoolPath.Name = "txtResourcePoolPath";
            this.txtResourcePoolPath.Size = new System.Drawing.Size(150, 21);
            this.txtResourcePoolPath.TabIndex = 20;
            this.resourcepoolTootip.SetToolTip(this.txtResourcePoolPath, "VM을 배포하기위한 리소스 풀 (Full, Linked, Instant Clone Pools.)");
            this.txtResourcePoolPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtResourcePoolPath_DragDrop);
            this.txtResourcePoolPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtResourcePoolPath_DragEnter);
            // 
            // txtHostOrClusterPath
            // 
            this.txtHostOrClusterPath.AllowDrop = true;
            this.txtHostOrClusterPath.Location = new System.Drawing.Point(132, 322);
            this.txtHostOrClusterPath.Name = "txtHostOrClusterPath";
            this.txtHostOrClusterPath.Size = new System.Drawing.Size(150, 21);
            this.txtHostOrClusterPath.TabIndex = 19;
            this.hostclusterTootip.SetToolTip(this.txtHostOrClusterPath, "VM을 배포하기위한 Host or Cluster (Full, Linked, Instant Clone Pools.)");
            this.txtHostOrClusterPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtHostOrClusterPath_DragDrop);
            this.txtHostOrClusterPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtHostOrClusterPath_DragEnter);
            // 
            // txtVmFolderPath
            // 
            this.txtVmFolderPath.AllowDrop = true;
            this.txtVmFolderPath.Location = new System.Drawing.Point(132, 300);
            this.txtVmFolderPath.Name = "txtVmFolderPath";
            this.txtVmFolderPath.Size = new System.Drawing.Size(150, 21);
            this.txtVmFolderPath.TabIndex = 18;
            this.vmfolderTootip.SetToolTip(this.txtVmFolderPath, "VM을 배포하기위한 VM 폴더 (Full, Linked, Instant Clone Pools.)");
            this.txtVmFolderPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtVmFolderPath_DragDrop);
            this.txtVmFolderPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtVmFolderPath_DragEnter);
            // 
            // txtDatacenterPath
            // 
            this.txtDatacenterPath.Location = new System.Drawing.Point(132, 278);
            this.txtDatacenterPath.Name = "txtDatacenterPath";
            this.txtDatacenterPath.ReadOnly = true;
            this.txtDatacenterPath.Size = new System.Drawing.Size(200, 21);
            this.txtDatacenterPath.TabIndex = 0;
            // 
            // txtSnapshotPath
            // 
            this.txtSnapshotPath.AllowDrop = true;
            this.txtSnapshotPath.Location = new System.Drawing.Point(132, 256);
            this.txtSnapshotPath.Name = "txtSnapshotPath";
            this.txtSnapshotPath.Size = new System.Drawing.Size(200, 21);
            this.txtSnapshotPath.TabIndex = 17;
            this.snapshotTooltip.SetToolTip(this.txtSnapshotPath, "Linked Clone Pool 의 기본 이미지 VM 및 Instant Clone 현재 이미지(Current Image).");
            this.txtSnapshotPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtSnapshotPath_DragDrop);
            this.txtSnapshotPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtSnapshotPath_DragEnter);
            // 
            // txtParentVMPath
            // 
            this.txtParentVMPath.AllowDrop = true;
            this.txtParentVMPath.Location = new System.Drawing.Point(132, 234);
            this.txtParentVMPath.Name = "txtParentVMPath";
            this.txtParentVMPath.Size = new System.Drawing.Size(200, 21);
            this.txtParentVMPath.TabIndex = 16;
            this.parentVMTootip.SetToolTip(this.txtParentVMPath, "가상 머신을 복제하기위한 상위 가상 머신. (Linked, Instant Clone Pools.)");
            this.txtParentVMPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtParentVMPath_DragDrop);
            this.txtParentVMPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtParentVMPath_DragEnter);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(5, 369);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(102, 12);
            this.label38.TabIndex = 23;
            this.label38.Text = "DatastorePaths : ";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(5, 347);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(121, 12);
            this.label37.TabIndex = 22;
            this.label37.Text = "ResourcePoolPath : ";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(5, 325);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(120, 12);
            this.label36.TabIndex = 21;
            this.label36.Text = "HostOrClusterPath : ";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(5, 303);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(96, 12);
            this.label35.TabIndex = 20;
            this.label35.Text = "VmFolderPath : ";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(5, 282);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(102, 12);
            this.label34.TabIndex = 19;
            this.label34.Text = "DatacenterPath : ";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(5, 261);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(95, 12);
            this.label33.TabIndex = 18;
            this.label33.Text = "SnapshotPath : ";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(5, 238);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(97, 12);
            this.label32.TabIndex = 17;
            this.label32.Text = "ParentVMPath : ";
            // 
            // txtTemplatePath
            // 
            this.txtTemplatePath.AllowDrop = true;
            this.txtTemplatePath.BackColor = System.Drawing.Color.White;
            this.txtTemplatePath.Location = new System.Drawing.Point(132, 212);
            this.txtTemplatePath.Name = "txtTemplatePath";
            this.txtTemplatePath.Size = new System.Drawing.Size(200, 21);
            this.txtTemplatePath.TabIndex = 15;
            this.templateTooltip.SetToolTip(this.txtTemplatePath, "가상 머신을 복제 할 VM 템플릿 명 (Only Full Clone Pools.)");
            this.txtTemplatePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtTemplatePath_DragDrop);
            this.txtTemplatePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtTemplatePath_DragEnter);
            // 
            // nudNumSessions
            // 
            this.nudNumSessions.Enabled = false;
            this.nudNumSessions.Location = new System.Drawing.Point(316, 164);
            this.nudNumSessions.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudNumSessions.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNumSessions.Name = "nudNumSessions";
            this.nudNumSessions.ReadOnly = true;
            this.nudNumSessions.Size = new System.Drawing.Size(73, 21);
            this.nudNumSessions.TabIndex = 0;
            this.nudNumSessions.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudNumMachines
            // 
            this.nudNumMachines.Location = new System.Drawing.Point(132, 163);
            this.nudNumMachines.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudNumMachines.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNumMachines.Name = "nudNumMachines";
            this.nudNumMachines.Size = new System.Drawing.Size(73, 21);
            this.nudNumMachines.TabIndex = 13;
            this.nudNumMachines.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbProvisionEnabled
            // 
            this.cmbProvisionEnabled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvisionEnabled.FormattingEnabled = true;
            this.cmbProvisionEnabled.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbProvisionEnabled.Location = new System.Drawing.Point(128, 116);
            this.cmbProvisionEnabled.Name = "cmbProvisionEnabled";
            this.cmbProvisionEnabled.Size = new System.Drawing.Size(78, 20);
            this.cmbProvisionEnabled.TabIndex = 11;
            // 
            // cmbPoolEnabled
            // 
            this.cmbPoolEnabled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPoolEnabled.FormattingEnabled = true;
            this.cmbPoolEnabled.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbPoolEnabled.Location = new System.Drawing.Point(128, 90);
            this.cmbPoolEnabled.Name = "cmbPoolEnabled";
            this.cmbPoolEnabled.Size = new System.Drawing.Size(78, 20);
            this.cmbPoolEnabled.TabIndex = 10;
            // 
            // cmbPoolSource
            // 
            this.cmbPoolSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPoolSource.FormattingEnabled = true;
            this.cmbPoolSource.Items.AddRange(new object[] {
            "VIEW_COMPOSER",
            "VIRTUAL_CENTER"});
            this.cmbPoolSource.Location = new System.Drawing.Point(64, 65);
            this.cmbPoolSource.Name = "cmbPoolSource";
            this.cmbPoolSource.Size = new System.Drawing.Size(142, 20);
            this.cmbPoolSource.TabIndex = 9;
            this.cmbPoolSource.SelectedIndexChanged += new System.EventHandler(this.cmbPoolSource_SelectedIndexChanged);
            this.cmbPoolSource.MouseHover += new System.EventHandler(this.cmbPoolSource_MouseHover);
            // 
            // chkUserAutoAssign
            // 
            this.chkUserAutoAssign.AutoSize = true;
            this.chkUserAutoAssign.Checked = true;
            this.chkUserAutoAssign.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUserAutoAssign.Location = new System.Drawing.Point(212, 43);
            this.chkUserAutoAssign.Name = "chkUserAutoAssign";
            this.chkUserAutoAssign.Size = new System.Drawing.Size(92, 16);
            this.chkUserAutoAssign.TabIndex = 8;
            this.chkUserAutoAssign.Text = "Auto Assign";
            this.chkUserAutoAssign.UseVisualStyleBackColor = true;
            // 
            // cmbUserAssign
            // 
            this.cmbUserAssign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserAssign.FormattingEnabled = true;
            this.cmbUserAssign.Items.AddRange(new object[] {
            "FLOATING",
            "DEDICATED"});
            this.cmbUserAssign.Location = new System.Drawing.Point(116, 40);
            this.cmbUserAssign.Name = "cmbUserAssign";
            this.cmbUserAssign.Size = new System.Drawing.Size(90, 20);
            this.cmbUserAssign.TabIndex = 7;
            this.cmbUserAssign.SelectedIndexChanged += new System.EventHandler(this.cmbUserAssign_SelectedIndexChanged);
            // 
            // cmbPoolType
            // 
            this.cmbPoolType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPoolType.FormattingEnabled = true;
            this.cmbPoolType.Items.AddRange(new object[] {
            "AUTOMATED",
            "MANUAL"});
            this.cmbPoolType.Location = new System.Drawing.Point(64, 17);
            this.cmbPoolType.Name = "cmbPoolType";
            this.cmbPoolType.Size = new System.Drawing.Size(142, 20);
            this.cmbPoolType.TabIndex = 6;
            this.cmbPoolType.SelectedIndexChanged += new System.EventHandler(this.cmbPoolType_SelectedIndexChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(4, 216);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(95, 12);
            this.label31.TabIndex = 7;
            this.label31.Text = "TemplatePath : ";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(212, 169);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(70, 12);
            this.label30.TabIndex = 6;
            this.label30.Text = "Sessions : ";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(5, 168);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(100, 12);
            this.label29.TabIndex = 5;
            this.label29.Text = "NumMachines : ";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(5, 121);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(132, 12);
            this.label28.TabIndex = 4;
            this.label28.Text = "ProvisioningEnabled : ";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(5, 96);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(63, 12);
            this.label27.TabIndex = 3;
            this.label27.Text = "Enabled : ";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(5, 69);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(57, 12);
            this.label26.TabIndex = 2;
            this.label26.Text = "Source : ";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(5, 44);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(114, 12);
            this.label25.TabIndex = 1;
            this.label25.Text = "User Assignment : ";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(5, 21);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(46, 12);
            this.label24.TabIndex = 0;
            this.label24.Text = "Type : ";
            // 
            // btnVMAction
            // 
            this.btnVMAction.Location = new System.Drawing.Point(353, 59);
            this.btnVMAction.Name = "btnVMAction";
            this.btnVMAction.Size = new System.Drawing.Size(50, 23);
            this.btnVMAction.TabIndex = 5;
            this.btnVMAction.Text = "Run";
            this.btnVMAction.UseVisualStyleBackColor = true;
            this.btnVMAction.Click += new System.EventHandler(this.btnVMAction_Click);
            // 
            // txtHorizonSystemName
            // 
            this.txtHorizonSystemName.AllowDrop = true;
            this.txtHorizonSystemName.Location = new System.Drawing.Point(87, 60);
            this.txtHorizonSystemName.Name = "txtHorizonSystemName";
            this.txtHorizonSystemName.Size = new System.Drawing.Size(125, 21);
            this.txtHorizonSystemName.TabIndex = 5;
            this.txtHorizonSystemName.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtHorizonSystemName_DragDrop);
            this.txtHorizonSystemName.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtHorizonSystemName_DragEnter);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 65);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(70, 12);
            this.label23.TabIndex = 3;
            this.label23.Text = "VM Name :";
            // 
            // btnPoolDetails
            // 
            this.btnPoolDetails.Location = new System.Drawing.Point(336, 35);
            this.btnPoolDetails.Name = "btnPoolDetails";
            this.btnPoolDetails.Size = new System.Drawing.Size(65, 23);
            this.btnPoolDetails.TabIndex = 2;
            this.btnPoolDetails.Text = "Info.";
            this.btnPoolDetails.UseVisualStyleBackColor = true;
            this.btnPoolDetails.Click += new System.EventHandler(this.btnPoolDetails_Click);
            // 
            // txtHorizonPoolName
            // 
            this.txtHorizonPoolName.AllowDrop = true;
            this.txtHorizonPoolName.Location = new System.Drawing.Point(87, 36);
            this.txtHorizonPoolName.Name = "txtHorizonPoolName";
            this.txtHorizonPoolName.Size = new System.Drawing.Size(185, 21);
            this.txtHorizonPoolName.TabIndex = 4;
            this.txtHorizonPoolName.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtHorizonPoolName_DragDrop);
            this.txtHorizonPoolName.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtHorizonPoolName_DragEnter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "Pool Name :";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.chkAllVMList);
            this.groupBox11.Controls.Add(this.btnAvailableVMList);
            this.groupBox11.Controls.Add(this.btnHorizonVMList);
            this.groupBox11.Controls.Add(this.btnHorizonPoolList);
            this.groupBox11.Location = new System.Drawing.Point(441, 6);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(221, 109);
            this.groupBox11.TabIndex = 15;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "List Information";
            // 
            // chkAllVMList
            // 
            this.chkAllVMList.AutoSize = true;
            this.chkAllVMList.Location = new System.Drawing.Point(131, 76);
            this.chkAllVMList.Name = "chkAllVMList";
            this.chkAllVMList.Size = new System.Drawing.Size(61, 16);
            this.chkAllVMList.TabIndex = 3;
            this.chkAllVMList.Text = "All VM";
            this.chkAllVMList.UseVisualStyleBackColor = true;
            // 
            // btnAvailableVMList
            // 
            this.btnAvailableVMList.Location = new System.Drawing.Point(26, 71);
            this.btnAvailableVMList.Name = "btnAvailableVMList";
            this.btnAvailableVMList.Size = new System.Drawing.Size(95, 25);
            this.btnAvailableVMList.TabIndex = 2;
            this.btnAvailableVMList.Text = "VM List";
            this.btnAvailableVMList.UseVisualStyleBackColor = true;
            this.btnAvailableVMList.Click += new System.EventHandler(this.btnAvailableVMList_Click);
            this.btnAvailableVMList.MouseHover += new System.EventHandler(this.btnAvailableVMList_MouseHover);
            // 
            // btnHorizonVMList
            // 
            this.btnHorizonVMList.Location = new System.Drawing.Point(26, 44);
            this.btnHorizonVMList.Name = "btnHorizonVMList";
            this.btnHorizonVMList.Size = new System.Drawing.Size(170, 25);
            this.btnHorizonVMList.TabIndex = 1;
            this.btnHorizonVMList.Text = "Pool VM List";
            this.btnHorizonVMList.UseVisualStyleBackColor = true;
            this.btnHorizonVMList.Click += new System.EventHandler(this.btnHorizonVMList_Click);
            // 
            // btnHorizonPoolList
            // 
            this.btnHorizonPoolList.Location = new System.Drawing.Point(26, 17);
            this.btnHorizonPoolList.Name = "btnHorizonPoolList";
            this.btnHorizonPoolList.Size = new System.Drawing.Size(170, 25);
            this.btnHorizonPoolList.TabIndex = 0;
            this.btnHorizonPoolList.Text = "Horizon PoolList";
            this.btnHorizonPoolList.UseVisualStyleBackColor = true;
            this.btnHorizonPoolList.Click += new System.EventHandler(this.btnHorizonPoolList_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.txtHorizonUUID);
            this.groupBox10.Controls.Add(this.label60);
            this.groupBox10.Controls.Add(this.txtHorizonName);
            this.groupBox10.Controls.Add(this.label59);
            this.groupBox10.Controls.Add(this.txtHorizonID);
            this.groupBox10.Controls.Add(this.btnSaveHV);
            this.groupBox10.Controls.Add(this.txtHorizonPW);
            this.groupBox10.Controls.Add(this.btnSDSHorizon);
            this.groupBox10.Controls.Add(this.btnNamuHorizon);
            this.groupBox10.Controls.Add(this.label101);
            this.groupBox10.Controls.Add(this.label102);
            this.groupBox10.Controls.Add(this.label103);
            this.groupBox10.Controls.Add(this.txtHorizonURL);
            this.groupBox10.Location = new System.Drawing.Point(8, 6);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(426, 101);
            this.groupBox10.TabIndex = 0;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Connection Information";
            // 
            // txtHorizonUUID
            // 
            this.txtHorizonUUID.Location = new System.Drawing.Point(223, 69);
            this.txtHorizonUUID.Name = "txtHorizonUUID";
            this.txtHorizonUUID.Size = new System.Drawing.Size(197, 21);
            this.txtHorizonUUID.TabIndex = 9;
            this.txtHorizonUUID.Text = "dda2c190-2ff8-4f11-838f-259acc89d7e";
            // 
            // label60
            // 
            this.label60.Location = new System.Drawing.Point(182, 74);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(71, 12);
            this.label60.TabIndex = 10;
            this.label60.Text = "UUID :";
            // 
            // txtHorizonName
            // 
            this.txtHorizonName.Location = new System.Drawing.Point(56, 69);
            this.txtHorizonName.Name = "txtHorizonName";
            this.txtHorizonName.Size = new System.Drawing.Size(119, 21);
            this.txtHorizonName.TabIndex = 7;
            this.txtHorizonName.Text = "Horizon View SDS";
            // 
            // label59
            // 
            this.label59.Location = new System.Drawing.Point(9, 74);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(71, 12);
            this.label59.TabIndex = 8;
            this.label59.Text = "Name :";
            // 
            // txtHorizonID
            // 
            this.txtHorizonID.Location = new System.Drawing.Point(56, 44);
            this.txtHorizonID.Name = "txtHorizonID";
            this.txtHorizonID.Size = new System.Drawing.Size(140, 21);
            this.txtHorizonID.TabIndex = 2;
            this.txtHorizonID.Text = "Cloud\\vdi_master";
            // 
            // btnSaveHV
            // 
            this.btnSaveHV.Location = new System.Drawing.Point(351, 44);
            this.btnSaveHV.Name = "btnSaveHV";
            this.btnSaveHV.Size = new System.Drawing.Size(65, 23);
            this.btnSaveHV.TabIndex = 6;
            this.btnSaveHV.Text = "Save";
            this.btnSaveHV.UseVisualStyleBackColor = true;
            this.btnSaveHV.Click += new System.EventHandler(this.btnSaveHV_Click);
            // 
            // txtHorizonPW
            // 
            this.txtHorizonPW.Location = new System.Drawing.Point(266, 45);
            this.txtHorizonPW.Name = "txtHorizonPW";
            this.txtHorizonPW.PasswordChar = '*';
            this.txtHorizonPW.Size = new System.Drawing.Size(83, 21);
            this.txtHorizonPW.TabIndex = 3;
            this.txtHorizonPW.Text = "wpfl21)@Cp";
            // 
            // btnSDSHorizon
            // 
            this.btnSDSHorizon.Location = new System.Drawing.Point(334, 17);
            this.btnSDSHorizon.Name = "btnSDSHorizon";
            this.btnSDSHorizon.Size = new System.Drawing.Size(65, 23);
            this.btnSDSHorizon.TabIndex = 5;
            this.btnSDSHorizon.Text = "SDS";
            this.btnSDSHorizon.UseVisualStyleBackColor = true;
            this.btnSDSHorizon.Click += new System.EventHandler(this.btnSDSHorizon_Click);
            // 
            // btnNamuHorizon
            // 
            this.btnNamuHorizon.Location = new System.Drawing.Point(265, 17);
            this.btnNamuHorizon.Name = "btnNamuHorizon";
            this.btnNamuHorizon.Size = new System.Drawing.Size(65, 23);
            this.btnNamuHorizon.TabIndex = 4;
            this.btnNamuHorizon.Text = "Namu";
            this.btnNamuHorizon.UseVisualStyleBackColor = true;
            this.btnNamuHorizon.Click += new System.EventHandler(this.btnNamuHorizon_Click);
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(9, 22);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(44, 12);
            this.label101.TabIndex = 3;
            this.label101.Text = "HV IP :";
            // 
            // label102
            // 
            this.label102.Location = new System.Drawing.Point(9, 49);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(71, 12);
            this.label102.TabIndex = 3;
            this.label102.Text = "HV ID :";
            // 
            // label103
            // 
            this.label103.Location = new System.Drawing.Point(212, 50);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(78, 12);
            this.label103.TabIndex = 3;
            this.label103.Text = "HV PW :";
            // 
            // txtHorizonURL
            // 
            this.txtHorizonURL.Location = new System.Drawing.Point(56, 18);
            this.txtHorizonURL.Name = "txtHorizonURL";
            this.txtHorizonURL.Size = new System.Drawing.Size(203, 21);
            this.txtHorizonURL.TabIndex = 1;
            this.txtHorizonURL.Text = "sec.sbc-cloud.com";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 591);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainFrm";
            this.Text = "Tester";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudvCenterHdd3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudvCenterHdd2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudvCenterHdd1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudvCenterRAM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudvCenterVCPUCore)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumSessions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumMachines)).EndInit();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.ListBox lstvCenterResult;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.CheckBox loopyn;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.RadioButton rbtnXenServerFastClone;
		private System.Windows.Forms.TextBox txtDomainUserID;
		private System.Windows.Forms.RadioButton rbtnXenServerFullCopy;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnXenServerRemove;
		private System.Windows.Forms.TextBox txtLocalPW;
		private System.Windows.Forms.Button btnXenServerVMProvision;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox txtXenServerTemplateKind;
		private System.Windows.Forms.TextBox txtLocalID;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.TextBox txtXenServerStorageKind;
		private System.Windows.Forms.TextBox txtNetbios;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox txtDomainPW;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox txtDNS2_F;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox txtDNS1_F;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox txtGateway_F;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox txtSubnetMask_F;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox txtDomainID;
		private System.Windows.Forms.TextBox txtIPAddress_F;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Button btnvCenterAddNIC;
		private System.Windows.Forms.Button btnvCenterRemoveNIC;
		private System.Windows.Forms.TextBox txtvCenterNicName;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label lblPowerState;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnvCenterResourceChange;
		private System.Windows.Forms.Label lblvCenterCPU;
		private System.Windows.Forms.NumericUpDown nudvCenterRAM;
		private System.Windows.Forms.Label lblvCenterRam;
		private System.Windows.Forms.NumericUpDown nudvCenterVCPUCore;
		private System.Windows.Forms.TextBox txtvCenterVMName;
		private System.Windows.Forms.Button btnXenServerDebug;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnvCenterFolder;
		private System.Windows.Forms.Button btnvCenterVMList;
		private System.Windows.Forms.Button btnvCenterTemplateList;
		private System.Windows.Forms.Button btnvCenterHost;
		private System.Windows.Forms.Button btnvCenterResourcePool;
		private System.Windows.Forms.Button btnvCenterStorage;
		private System.Windows.Forms.CheckBox chkXenServerOnlyCustomTemplate;
		private System.Windows.Forms.Button btnvCenterCluster;
		private System.Windows.Forms.Button btnVMDetails;
		private System.Windows.Forms.ComboBox cmbvCenterPowerKind;
		private System.Windows.Forms.Button btnvCenterPowerRun;
		private System.Windows.Forms.Label lblvCenterHDD3;
		private System.Windows.Forms.NumericUpDown nudvCenterHdd3;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label lblvCenterHDD2;
		private System.Windows.Forms.NumericUpDown nudvCenterHdd2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label lblvCenterHDD1;
		private System.Windows.Forms.NumericUpDown nudvCenterHdd1;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtvCenterURL;
        private System.Windows.Forms.TextBox txtvCenterID;
        private System.Windows.Forms.TextBox txtvCenterPW;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstHorizonResult;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.TextBox txtHorizonURL;
        private System.Windows.Forms.TextBox txtHorizonID;
        private System.Windows.Forms.TextBox txtHorizonPW;
        private System.Windows.Forms.Button btnHorizonPoolList;
        private System.Windows.Forms.Button btnHorizonVMList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnVMAction;
        private System.Windows.Forms.TextBox txtHorizonSystemName;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnPoolDetails;
        private System.Windows.Forms.TextBox txtHorizonPoolName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbPoolSource;
        private System.Windows.Forms.CheckBox chkUserAutoAssign;
        private System.Windows.Forms.ComboBox cmbUserAssign;
        private System.Windows.Forms.ComboBox cmbPoolType;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cmbProvisionEnabled;
        private System.Windows.Forms.ComboBox cmbPoolEnabled;
        private System.Windows.Forms.NumericUpDown nudNumSessions;
        private System.Windows.Forms.NumericUpDown nudNumMachines;
        private System.Windows.Forms.TextBox txtDatastorePaths;
        private System.Windows.Forms.TextBox txtResourcePoolPath;
        private System.Windows.Forms.TextBox txtHostOrClusterPath;
        private System.Windows.Forms.TextBox txtVmFolderPath;
        private System.Windows.Forms.TextBox txtDatacenterPath;
        private System.Windows.Forms.TextBox txtSnapshotPath;
        private System.Windows.Forms.TextBox txtParentVMPath;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtTemplatePath;
        private System.Windows.Forms.TextBox txtNamingRule;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Button btnEditPool;
        private System.Windows.Forms.Button btnNewPool;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TextBox txtAddressStateCallBack;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox txtAddressDNS1;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.TextBox txtAddressDNS2;
        private System.Windows.Forms.TextBox txtAddressGateway;
        private System.Windows.Forms.TextBox txtAddressSubnet;
        private System.Windows.Forms.TextBox txtAddressIP;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.TextBox txtDomainOUDC;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox txtDomainAdminPW;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txtDomainSetUserID;
        private System.Windows.Forms.TextBox txtDomainAdminID;
        private System.Windows.Forms.TextBox txtDomainFQDN;
        private System.Windows.Forms.TextBox txtDomainNetbios;
        private System.Windows.Forms.Button btnAgentSetting;
        private System.Windows.Forms.CheckBox chkAllVMList;
        private System.Windows.Forms.Button btnAvailableVMList;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Button btnClonePool;
        private System.Windows.Forms.Button btnDeletePool;
        private System.Windows.Forms.ToolTip templateTooltip;
        private System.Windows.Forms.ToolTip parentVMTootip;
        private System.Windows.Forms.ToolTip snapshotTooltip;
        private System.Windows.Forms.ToolTip vmfolderTootip;
        private System.Windows.Forms.ToolTip hostclusterTootip;
        private System.Windows.Forms.ToolTip resourcepoolTootip;
        private System.Windows.Forms.ToolTip datastoreTootip;
        private System.Windows.Forms.Button btnDatastoreList;
        private System.Windows.Forms.Button btnResourcePoolList;
        private System.Windows.Forms.Button btnHostClusterList;
        private System.Windows.Forms.Button btnVMFolderList;
        private System.Windows.Forms.Button btnSnapshotList;
        private System.Windows.Forms.Button btnParentVMList;
        private System.Windows.Forms.Button btnTemplateList;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.ComboBox cmbCustType;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.TextBox txtDomainSetUserPW;
        private System.Windows.Forms.ComboBox cmbVMAction;
        private System.Windows.Forms.Button btnVMStatus;
        private RichTextBox rtbResult;
        private Button btnPoolVMStatus;
        private Label lblServicePort;
        private TextBox txtServicePort;
        private TextBox txtServiceIP;
        private Label lblServiceIP;
        private Label lblSendMessage;
        private Button btnSendMessage;
        private TextBox txtSendMessage;
        private ComboBox cmbMessageType;
        private Button btnSDSCenter;
        private Button btnNamuCenter;
        private Button btnSDSHorizon;
        private Button btnNamuHorizon;
        private ComboBox cmbLanguage;
        private Label lbl_Language;
        private Button btnClusterDiscovery;
        private RichTextBox rtbResultVI;
        private Button btnSaveVI;
        private Button btnSaveHV;
        private Button btnvCenterVLAN;
        private Button btnvCenterVLANJson;
        private Button btnvCenterFolderJson;
        private Button btnvCenterHostJson;
        private Button btnvCenterResourcePoolJson;
        private Button btnvCenterStorageJson;
        private Button btnvCenterClusterJson;
        private Button btnClusterDetail;
        private TextBox txtCenterCluster;
        private Label lblCluster;
        private TextBox txtDatacenterName;
        private Label lblDatacenterName;
        private Button btnChangeName;
        private TextBox txtVMName2;
        private Label label58;
        private TextBox txtVMName1;
        private Label label57;
        private Button btnResetVM;
        private TextBox txtHorizonUUID;
        private Label label60;
        private TextBox txtHorizonName;
        private Label label59;
        private TextBox txtCenterName;
        private TextBox txtCenterUUID;
        private Label label61;
        private Label label62;
        private Button btnClusterResourcePoolDiscovery;
        private Button btnVMFolderDiscovery;
    }
}


namespace Rlkt_LauncherLibServer
{
    partial class ServiceWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceInfo = new System.Windows.Forms.GroupBox();
            this.voipChannelsCounter = new System.Windows.Forms.Label();
            this.serverStatus = new System.Windows.Forms.Label();
            this.onlineCounter = new System.Windows.Forms.Label();
            this.serverStatuslibl = new System.Windows.Forms.Label();
            this.clientList = new System.Windows.Forms.DataGridView();
            this.GUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nickname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverOnBtn = new System.Windows.Forms.Button();
            this.serviceStatusBox = new System.Windows.Forms.GroupBox();
            this.serverOffBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.serviceInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientList)).BeginInit();
            this.serviceStatusBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serviceInfo
            // 
            this.serviceInfo.Controls.Add(this.voipChannelsCounter);
            this.serviceInfo.Controls.Add(this.serverStatus);
            this.serviceInfo.Controls.Add(this.onlineCounter);
            this.serviceInfo.Controls.Add(this.serverStatuslibl);
            this.serviceInfo.Location = new System.Drawing.Point(12, 16);
            this.serviceInfo.Name = "serviceInfo";
            this.serviceInfo.Size = new System.Drawing.Size(176, 93);
            this.serviceInfo.TabIndex = 0;
            this.serviceInfo.TabStop = false;
            this.serviceInfo.Text = "Service Data";
            // 
            // voipChannelsCounter
            // 
            this.voipChannelsCounter.AutoSize = true;
            this.voipChannelsCounter.Location = new System.Drawing.Point(6, 65);
            this.voipChannelsCounter.Name = "voipChannelsCounter";
            this.voipChannelsCounter.Size = new System.Drawing.Size(87, 13);
            this.voipChannelsCounter.TabIndex = 3;
            this.voipChannelsCounter.Text = "Voip Channels: 0";
            // 
            // serverStatus
            // 
            this.serverStatus.AutoSize = true;
            this.serverStatus.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.serverStatus.ForeColor = System.Drawing.Color.LimeGreen;
            this.serverStatus.Location = new System.Drawing.Point(79, 27);
            this.serverStatus.Name = "serverStatus";
            this.serverStatus.Size = new System.Drawing.Size(42, 13);
            this.serverStatus.TabIndex = 2;
            this.serverStatus.Text = "Online";
            // 
            // onlineCounter
            // 
            this.onlineCounter.AutoSize = true;
            this.onlineCounter.Location = new System.Drawing.Point(6, 45);
            this.onlineCounter.Name = "onlineCounter";
            this.onlineCounter.Size = new System.Drawing.Size(107, 13);
            this.onlineCounter.TabIndex = 1;
            this.onlineCounter.Text = "Connected Clients: 0";
            // 
            // serverStatuslibl
            // 
            this.serverStatuslibl.AutoSize = true;
            this.serverStatuslibl.Location = new System.Drawing.Point(6, 27);
            this.serverStatuslibl.Name = "serverStatuslibl";
            this.serverStatuslibl.Size = new System.Drawing.Size(77, 13);
            this.serverStatuslibl.TabIndex = 0;
            this.serverStatuslibl.Text = "Server Status:";
            // 
            // clientList
            // 
            this.clientList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clientList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GUID,
            this.ID,
            this.Username,
            this.Nickname,
            this.Status,
            this.IP});
            this.clientList.Location = new System.Drawing.Point(6, 8);
            this.clientList.Name = "clientList";
            this.clientList.Size = new System.Drawing.Size(643, 380);
            this.clientList.TabIndex = 1;
            // 
            // GUID
            // 
            this.GUID.HeaderText = "Client GUID";
            this.GUID.Name = "GUID";
            this.GUID.ReadOnly = true;
            // 
            // ID
            // 
            this.ID.HeaderText = "User ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Username
            // 
            this.Username.HeaderText = "Username";
            this.Username.Name = "Username";
            this.Username.ReadOnly = true;
            // 
            // Nickname
            // 
            this.Nickname.HeaderText = "Nicname";
            this.Nickname.Name = "Nickname";
            this.Nickname.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Client Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // IP
            // 
            this.IP.HeaderText = "Connected IP";
            this.IP.Name = "IP";
            this.IP.ReadOnly = true;
            // 
            // serverOnBtn
            // 
            this.serverOnBtn.Location = new System.Drawing.Point(9, 30);
            this.serverOnBtn.Name = "serverOnBtn";
            this.serverOnBtn.Size = new System.Drawing.Size(75, 23);
            this.serverOnBtn.TabIndex = 2;
            this.serverOnBtn.Text = "ON";
            this.serverOnBtn.UseVisualStyleBackColor = true;
            this.serverOnBtn.Click += new System.EventHandler(this.serverOnBtn_Click);
            // 
            // serviceStatusBox
            // 
            this.serviceStatusBox.Controls.Add(this.serverOffBtn);
            this.serviceStatusBox.Controls.Add(this.serverOnBtn);
            this.serviceStatusBox.Location = new System.Drawing.Point(12, 115);
            this.serviceStatusBox.Name = "serviceStatusBox";
            this.serviceStatusBox.Size = new System.Drawing.Size(176, 74);
            this.serviceStatusBox.TabIndex = 2;
            this.serviceStatusBox.TabStop = false;
            this.serviceStatusBox.Text = "Service Status";
            // 
            // serverOffBtn
            // 
            this.serverOffBtn.Location = new System.Drawing.Point(95, 30);
            this.serverOffBtn.Name = "serverOffBtn";
            this.serverOffBtn.Size = new System.Drawing.Size(75, 23);
            this.serverOffBtn.TabIndex = 3;
            this.serverOffBtn.Text = "OFF";
            this.serverOffBtn.UseVisualStyleBackColor = true;
            this.serverOffBtn.Click += new System.EventHandler(this.serverOffBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(12, 195);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(176, 74);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Update Notify";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(9, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(161, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "SEND";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(194, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(663, 420);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.clientList);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(655, 394);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Clients";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ServiceWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 446);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.serviceStatusBox);
            this.Controls.Add(this.serviceInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(875, 473);
            this.Name = "ServiceWindow";
            this.Text = "LauncherServer Service";
            this.Load += new System.EventHandler(this.ServiceWindow_Load);
            this.Shown += new System.EventHandler(this.ServiceWindow_Shown);
            this.serviceInfo.ResumeLayout(false);
            this.serviceInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientList)).EndInit();
            this.serviceStatusBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox serviceInfo;
        private System.Windows.Forms.Label onlineCounter;
        private System.Windows.Forms.Label serverStatuslibl;
        private System.Windows.Forms.DataGridView clientList;
        private System.Windows.Forms.Button serverOnBtn;
        private System.Windows.Forms.GroupBox serviceStatusBox;
        private System.Windows.Forms.Button serverOffBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Username;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nickname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.Label serverStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label voipChannelsCounter;
    }
}
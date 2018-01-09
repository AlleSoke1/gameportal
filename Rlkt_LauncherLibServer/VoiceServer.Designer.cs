namespace Rlkt_LauncherLibServer
{
    partial class VoiceServer
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
            this.serverStatuslibl = new System.Windows.Forms.Label();
            this.serverOnBtn = new System.Windows.Forms.Button();
            this.serviceStatusBox = new System.Windows.Forms.GroupBox();
            this.serverOffBtn = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.voipChannelList = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.serviceInfo.SuspendLayout();
            this.serviceStatusBox.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.voipChannelList)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serviceInfo
            // 
            this.serviceInfo.Controls.Add(this.voipChannelsCounter);
            this.serviceInfo.Controls.Add(this.serverStatus);
            this.serviceInfo.Controls.Add(this.serverStatuslibl);
            this.serviceInfo.Location = new System.Drawing.Point(12, 16);
            this.serviceInfo.Name = "serviceInfo";
            this.serviceInfo.Size = new System.Drawing.Size(176, 77);
            this.serviceInfo.TabIndex = 0;
            this.serviceInfo.TabStop = false;
            this.serviceInfo.Text = "Service Data";
            // 
            // voipChannelsCounter
            // 
            this.voipChannelsCounter.AutoSize = true;
            this.voipChannelsCounter.Location = new System.Drawing.Point(6, 48);
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
            // serverStatuslibl
            // 
            this.serverStatuslibl.AutoSize = true;
            this.serverStatuslibl.Location = new System.Drawing.Point(6, 27);
            this.serverStatuslibl.Name = "serverStatuslibl";
            this.serverStatuslibl.Size = new System.Drawing.Size(77, 13);
            this.serverStatuslibl.TabIndex = 0;
            this.serverStatuslibl.Text = "Server Status:";
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
            this.serviceStatusBox.Location = new System.Drawing.Point(12, 99);
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.voipChannelList);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(655, 394);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Voip Channels";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // voipChannelList
            // 
            this.voipChannelList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.voipChannelList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn2});
            this.voipChannelList.Location = new System.Drawing.Point(6, 8);
            this.voipChannelList.Name = "voipChannelList";
            this.voipChannelList.Size = new System.Drawing.Size(643, 380);
            this.voipChannelList.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "User IDs";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Channel Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Channel ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(194, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(663, 420);
            this.tabControl1.TabIndex = 5;
            // 
            // VoiceServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 446);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.serviceStatusBox);
            this.Controls.Add(this.serviceInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(875, 473);
            this.Name = "VoiceServer";
            this.Text = "LauncherServer Voice";
            this.Shown += new System.EventHandler(this.ServiceWindow_Shown);
            this.serviceInfo.ResumeLayout(false);
            this.serviceInfo.PerformLayout();
            this.serviceStatusBox.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.voipChannelList)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox serviceInfo;
        private System.Windows.Forms.Label serverStatuslibl;
        private System.Windows.Forms.Button serverOnBtn;
        private System.Windows.Forms.GroupBox serviceStatusBox;
        private System.Windows.Forms.Button serverOffBtn;
        private System.Windows.Forms.Label serverStatus;
        private System.Windows.Forms.Label voipChannelsCounter;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView voipChannelList;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.TabControl tabControl1;
    }
}
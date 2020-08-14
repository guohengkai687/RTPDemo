namespace WindowsFormsVoIP
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Monitor = new System.Windows.Forms.Button();
            this.cb_NetworkCard = new System.Windows.Forms.ComboBox();
            this.list_ReceivedData = new System.Windows.Forms.ListBox();
            this.list_SendData = new System.Windows.Forms.ListBox();
            this.txt_ReceivedIP = new System.Windows.Forms.TextBox();
            this.txt_SendIP = new System.Windows.Forms.TextBox();
            this.lb_ReceivedIP = new System.Windows.Forms.Label();
            this.lb_SendIP = new System.Windows.Forms.Label();
            this.lb_ReceivedPort = new System.Windows.Forms.Label();
            this.lb_SendPort = new System.Windows.Forms.Label();
            this.txt_ReceivedPort = new System.Windows.Forms.TextBox();
            this.txt_SendPort = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_Monitor
            // 
            this.btn_Monitor.Location = new System.Drawing.Point(13, 13);
            this.btn_Monitor.Name = "btn_Monitor";
            this.btn_Monitor.Size = new System.Drawing.Size(75, 23);
            this.btn_Monitor.TabIndex = 0;
            this.btn_Monitor.Text = "开启监控";
            this.btn_Monitor.UseVisualStyleBackColor = true;
            this.btn_Monitor.Click += new System.EventHandler(this.btn_Monitor_Click);
            // 
            // cb_NetworkCard
            // 
            this.cb_NetworkCard.FormattingEnabled = true;
            this.cb_NetworkCard.Location = new System.Drawing.Point(94, 15);
            this.cb_NetworkCard.Name = "cb_NetworkCard";
            this.cb_NetworkCard.Size = new System.Drawing.Size(694, 20);
            this.cb_NetworkCard.TabIndex = 1;
            // 
            // list_ReceivedData
            // 
            this.list_ReceivedData.FormattingEnabled = true;
            this.list_ReceivedData.ItemHeight = 12;
            this.list_ReceivedData.Location = new System.Drawing.Point(13, 67);
            this.list_ReceivedData.Name = "list_ReceivedData";
            this.list_ReceivedData.ScrollAlwaysVisible = true;
            this.list_ReceivedData.Size = new System.Drawing.Size(775, 160);
            this.list_ReceivedData.TabIndex = 2;
            // 
            // list_SendData
            // 
            this.list_SendData.FormattingEnabled = true;
            this.list_SendData.ItemHeight = 12;
            this.list_SendData.Location = new System.Drawing.Point(13, 270);
            this.list_SendData.Name = "list_SendData";
            this.list_SendData.ScrollAlwaysVisible = true;
            this.list_SendData.Size = new System.Drawing.Size(775, 172);
            this.list_SendData.TabIndex = 3;
            // 
            // txt_ReceivedIP
            // 
            this.txt_ReceivedIP.Location = new System.Drawing.Point(94, 40);
            this.txt_ReceivedIP.Name = "txt_ReceivedIP";
            this.txt_ReceivedIP.Size = new System.Drawing.Size(144, 21);
            this.txt_ReceivedIP.TabIndex = 4;
            // 
            // txt_SendIP
            // 
            this.txt_SendIP.Location = new System.Drawing.Point(94, 240);
            this.txt_SendIP.Name = "txt_SendIP";
            this.txt_SendIP.Size = new System.Drawing.Size(144, 21);
            this.txt_SendIP.TabIndex = 5;
            // 
            // lb_ReceivedIP
            // 
            this.lb_ReceivedIP.AutoSize = true;
            this.lb_ReceivedIP.Location = new System.Drawing.Point(15, 43);
            this.lb_ReceivedIP.Name = "lb_ReceivedIP";
            this.lb_ReceivedIP.Size = new System.Drawing.Size(71, 12);
            this.lb_ReceivedIP.TabIndex = 6;
            this.lb_ReceivedIP.Text = "ReceivedIP:";
            // 
            // lb_SendIP
            // 
            this.lb_SendIP.AutoSize = true;
            this.lb_SendIP.Location = new System.Drawing.Point(15, 243);
            this.lb_SendIP.Name = "lb_SendIP";
            this.lb_SendIP.Size = new System.Drawing.Size(47, 12);
            this.lb_SendIP.TabIndex = 7;
            this.lb_SendIP.Text = "SendIP:";
            // 
            // lb_ReceivedPort
            // 
            this.lb_ReceivedPort.AutoSize = true;
            this.lb_ReceivedPort.Location = new System.Drawing.Point(266, 43);
            this.lb_ReceivedPort.Name = "lb_ReceivedPort";
            this.lb_ReceivedPort.Size = new System.Drawing.Size(83, 12);
            this.lb_ReceivedPort.TabIndex = 8;
            this.lb_ReceivedPort.Text = "ReceivedPort:";
            // 
            // lb_SendPort
            // 
            this.lb_SendPort.AutoSize = true;
            this.lb_SendPort.Location = new System.Drawing.Point(268, 243);
            this.lb_SendPort.Name = "lb_SendPort";
            this.lb_SendPort.Size = new System.Drawing.Size(59, 12);
            this.lb_SendPort.TabIndex = 9;
            this.lb_SendPort.Text = "SendPort:";
            // 
            // txt_ReceivedPort
            // 
            this.txt_ReceivedPort.Location = new System.Drawing.Point(355, 40);
            this.txt_ReceivedPort.Name = "txt_ReceivedPort";
            this.txt_ReceivedPort.Size = new System.Drawing.Size(144, 21);
            this.txt_ReceivedPort.TabIndex = 10;
            // 
            // txt_SendPort
            // 
            this.txt_SendPort.Location = new System.Drawing.Point(355, 240);
            this.txt_SendPort.Name = "txt_SendPort";
            this.txt_SendPort.Size = new System.Drawing.Size(144, 21);
            this.txt_SendPort.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txt_SendPort);
            this.Controls.Add(this.txt_ReceivedPort);
            this.Controls.Add(this.lb_SendPort);
            this.Controls.Add(this.lb_ReceivedPort);
            this.Controls.Add(this.lb_SendIP);
            this.Controls.Add(this.lb_ReceivedIP);
            this.Controls.Add(this.txt_SendIP);
            this.Controls.Add(this.txt_ReceivedIP);
            this.Controls.Add(this.list_SendData);
            this.Controls.Add(this.list_ReceivedData);
            this.Controls.Add(this.cb_NetworkCard);
            this.Controls.Add(this.btn_Monitor);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "显示界面";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Monitor;
        private System.Windows.Forms.ComboBox cb_NetworkCard;
        private System.Windows.Forms.ListBox list_ReceivedData;
        private System.Windows.Forms.ListBox list_SendData;
        private System.Windows.Forms.TextBox txt_ReceivedIP;
        private System.Windows.Forms.TextBox txt_SendIP;
        private System.Windows.Forms.Label lb_ReceivedIP;
        private System.Windows.Forms.Label lb_SendIP;
        private System.Windows.Forms.Label lb_ReceivedPort;
        private System.Windows.Forms.Label lb_SendPort;
        private System.Windows.Forms.TextBox txt_ReceivedPort;
        private System.Windows.Forms.TextBox txt_SendPort;
    }
}


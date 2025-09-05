namespace KEYENCE_inkjet_printing_control_DEMO
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            this.pannelMain = new Guna.UI2.WinForms.Guna2Panel();
            this.panelContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.panelTool = new Guna.UI2.WinForms.Guna2Panel();
            this.btnOpenFolderStatus = new Guna.UI2.WinForms.Guna2Button();
            this.btnBrowseStatus = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.panelHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblFrmMain = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblCloseForm = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pannelMain.SuspendLayout();
            this.panelTool.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pannelMain
            // 
            this.pannelMain.BackColor = System.Drawing.Color.White;
            this.pannelMain.BorderColor = System.Drawing.Color.Black;
            this.pannelMain.BorderThickness = 2;
            this.pannelMain.Controls.Add(this.panelContainer);
            this.pannelMain.Controls.Add(this.panelTool);
            this.pannelMain.Controls.Add(this.guna2Separator1);
            this.pannelMain.Controls.Add(this.panelHeader);
            this.pannelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pannelMain.Location = new System.Drawing.Point(0, 0);
            this.pannelMain.Margin = new System.Windows.Forms.Padding(4);
            this.pannelMain.Name = "pannelMain";
            this.pannelMain.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pannelMain.Size = new System.Drawing.Size(1280, 680);
            this.pannelMain.TabIndex = 0;
            // 
            // panelContainer
            // 
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(2, 74);
            this.panelContainer.Margin = new System.Windows.Forms.Padding(4);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1276, 603);
            this.panelContainer.TabIndex = 4;
            // 
            // panelTool
            // 
            this.panelTool.Controls.Add(this.guna2HtmlLabel1);
            this.panelTool.Controls.Add(this.btnOpenFolderStatus);
            this.panelTool.Controls.Add(this.btnBrowseStatus);
            this.panelTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTool.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.panelTool.Location = new System.Drawing.Point(2, 28);
            this.panelTool.Margin = new System.Windows.Forms.Padding(4);
            this.panelTool.Name = "panelTool";
            this.panelTool.Size = new System.Drawing.Size(1276, 46);
            this.panelTool.TabIndex = 3;
            // 
            // btnOpenFolderStatus
            // 
            this.btnOpenFolderStatus.BorderRadius = 10;
            this.btnOpenFolderStatus.BorderThickness = 2;
            this.btnOpenFolderStatus.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnOpenFolderStatus.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnOpenFolderStatus.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnOpenFolderStatus.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnOpenFolderStatus.FillColor = System.Drawing.Color.Gray;
            this.btnOpenFolderStatus.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenFolderStatus.ForeColor = System.Drawing.Color.White;
            this.btnOpenFolderStatus.Location = new System.Drawing.Point(177, 2);
            this.btnOpenFolderStatus.Name = "btnOpenFolderStatus";
            this.btnOpenFolderStatus.Size = new System.Drawing.Size(153, 39);
            this.btnOpenFolderStatus.TabIndex = 31;
            this.btnOpenFolderStatus.Text = "Open Folder Status";
            this.btnOpenFolderStatus.Click += new System.EventHandler(this.btnOpenFolderStatus_Click);
            // 
            // btnBrowseStatus
            // 
            this.btnBrowseStatus.BackColor = System.Drawing.Color.White;
            this.btnBrowseStatus.BorderRadius = 10;
            this.btnBrowseStatus.BorderThickness = 2;
            this.btnBrowseStatus.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBrowseStatus.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBrowseStatus.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBrowseStatus.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBrowseStatus.FillColor = System.Drawing.Color.Gray;
            this.btnBrowseStatus.Font = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseStatus.ForeColor = System.Drawing.Color.White;
            this.btnBrowseStatus.Location = new System.Drawing.Point(10, 1);
            this.btnBrowseStatus.Name = "btnBrowseStatus";
            this.btnBrowseStatus.Size = new System.Drawing.Size(161, 40);
            this.btnBrowseStatus.TabIndex = 30;
            this.btnBrowseStatus.Text = "Inkjet Output Status";
            this.btnBrowseStatus.Click += new System.EventHandler(this.btnBrowseStatus_Click);
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Separator1.FillColor = System.Drawing.Color.Black;
            this.guna2Separator1.Location = new System.Drawing.Point(2, 18);
            this.guna2Separator1.Margin = new System.Windows.Forms.Padding(0);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(1276, 10);
            this.guna2Separator1.TabIndex = 2;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblFrmMain);
            this.panelHeader.Controls.Add(this.lblCloseForm);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(2, 3);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1276, 15);
            this.panelHeader.TabIndex = 0;
            // 
            // lblFrmMain
            // 
            this.lblFrmMain.AutoSize = false;
            this.lblFrmMain.BackColor = System.Drawing.Color.Transparent;
            this.lblFrmMain.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrmMain.Location = new System.Drawing.Point(11, 0);
            this.lblFrmMain.Margin = new System.Windows.Forms.Padding(4);
            this.lblFrmMain.Name = "lblFrmMain";
            this.lblFrmMain.Size = new System.Drawing.Size(306, 21);
            this.lblFrmMain.TabIndex = 1;
            this.lblFrmMain.Text = "KEYENCE Inkjet Printing Control";
            // 
            // lblCloseForm
            // 
            this.lblCloseForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCloseForm.BackColor = System.Drawing.Color.Transparent;
            this.lblCloseForm.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCloseForm.Location = new System.Drawing.Point(1250, -3);
            this.lblCloseForm.Margin = new System.Windows.Forms.Padding(4);
            this.lblCloseForm.Name = "lblCloseForm";
            this.lblCloseForm.Size = new System.Drawing.Size(15, 25);
            this.lblCloseForm.TabIndex = 0;
            this.lblCloseForm.Text = "X";
            this.lblCloseForm.Click += new System.EventHandler(this.lblCloseForm_Click);
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.panelHeader;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2HtmlLabel1.AutoSize = false;
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(1064, 12);
            this.guna2HtmlLabel1.Margin = new System.Windows.Forms.Padding(4);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(201, 23);
            this.guna2HtmlLabel1.TabIndex = 32;
            this.guna2HtmlLabel1.Text = "Fri, 25 Jul 2025 15:59:59";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 680);
            this.Controls.Add(this.pannelMain);
            this.Font = new System.Drawing.Font("Century Gothic", 10.2F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMain2";
            this.pannelMain.ResumeLayout(false);
            this.panelTool.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pannelMain;
        private Guna.UI2.WinForms.Guna2Panel panelHeader;
        private Guna.UI2.WinForms.Guna2Panel panelTool;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private Guna.UI2.WinForms.Guna2Panel panelContainer;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCloseForm;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        public Guna.UI2.WinForms.Guna2HtmlLabel lblFrmMain;
        private Guna.UI2.WinForms.Guna2Button btnOpenFolderStatus;
        private Guna.UI2.WinForms.Guna2Button btnBrowseStatus;
        public Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
    }
}
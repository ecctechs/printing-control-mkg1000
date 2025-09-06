using System.Drawing;
using System.Windows.Forms;

namespace KEYENCE_inkjet_printing_control_DEMO.UserControls
{
    partial class ucItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucItem));
            this.panelPaddingLeft = new System.Windows.Forms.Panel();
            this.panelPaddingRight = new System.Windows.Forms.Panel();
            this.panelMain = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.circleStatus = new Guna.UI2.WinForms.Guna2CircleButton();
            this.lblStatusValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblStatus = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblIpAddressValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblInkjetNameValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblIpAddress = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblInkjetName = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.imgSetting = new Guna.UI2.WinForms.Guna2ImageButton();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.lblErrorManual = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblError = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnSaveManual = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnClearManual = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnEditManual = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.panelDelete = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.lblCloseForm = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtLaterPrintDetail = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtCurrentData = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtWaitingPrintDetail = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtQueueDataValue = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblLaterPrintDetail = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblCurrentData = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblWaitingPrintDetail = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblQueueData = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.panelMain.SuspendLayout();
            this.panelDetails.SuspendLayout();
            this.panelDelete.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPaddingLeft
            // 
            this.panelPaddingLeft.BackColor = System.Drawing.Color.White;
            this.panelPaddingLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelPaddingLeft.Location = new System.Drawing.Point(0, 0);
            this.panelPaddingLeft.Name = "panelPaddingLeft";
            this.panelPaddingLeft.Size = new System.Drawing.Size(5, 139);
            this.panelPaddingLeft.TabIndex = 0;
            // 
            // panelPaddingRight
            // 
            this.panelPaddingRight.BackColor = System.Drawing.Color.White;
            this.panelPaddingRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelPaddingRight.Location = new System.Drawing.Point(1275, 0);
            this.panelPaddingRight.Name = "panelPaddingRight";
            this.panelPaddingRight.Size = new System.Drawing.Size(5, 139);
            this.panelPaddingRight.TabIndex = 1;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.BorderColor = System.Drawing.Color.Green;
            this.panelMain.BorderRadius = 25;
            this.panelMain.BorderThickness = 4;
            this.panelMain.Controls.Add(this.circleStatus);
            this.panelMain.Controls.Add(this.lblStatusValue);
            this.panelMain.Controls.Add(this.lblStatus);
            this.panelMain.Controls.Add(this.lblIpAddressValue);
            this.panelMain.Controls.Add(this.lblInkjetNameValue);
            this.panelMain.Controls.Add(this.lblIpAddress);
            this.panelMain.Controls.Add(this.lblInkjetName);
            this.panelMain.Controls.Add(this.imgSetting);
            this.panelMain.Controls.Add(this.panelDetails);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panelMain.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panelMain.Location = new System.Drawing.Point(5, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1270, 139);
            this.panelMain.TabIndex = 2;
            // 
            // circleStatus
            // 
            this.circleStatus.BorderThickness = 2;
            this.circleStatus.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.circleStatus.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.circleStatus.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.circleStatus.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.circleStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.circleStatus.ForeColor = System.Drawing.Color.White;
            this.circleStatus.Location = new System.Drawing.Point(102, 78);
            this.circleStatus.Name = "circleStatus";
            this.circleStatus.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.circleStatus.Size = new System.Drawing.Size(20, 18);
            this.circleStatus.TabIndex = 31;
            // 
            // lblStatusValue
            // 
            this.lblStatusValue.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusValue.Location = new System.Drawing.Point(128, 76);
            this.lblStatusValue.Name = "lblStatusValue";
            this.lblStatusValue.Size = new System.Drawing.Size(114, 19);
            this.lblStatusValue.TabIndex = 19;
            this.lblStatusValue.Text = "guna2HtmlLabel9";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(18, 75);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(59, 19);
            this.lblStatus.TabIndex = 18;
            this.lblStatus.Text = "Status :";
            // 
            // lblIpAddressValue
            // 
            this.lblIpAddressValue.BackColor = System.Drawing.Color.Transparent;
            this.lblIpAddressValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIpAddressValue.Location = new System.Drawing.Point(128, 49);
            this.lblIpAddressValue.Name = "lblIpAddressValue";
            this.lblIpAddressValue.Size = new System.Drawing.Size(114, 19);
            this.lblIpAddressValue.TabIndex = 17;
            this.lblIpAddressValue.Text = "guna2HtmlLabel9";
            // 
            // lblInkjetNameValue
            // 
            this.lblInkjetNameValue.BackColor = System.Drawing.Color.Transparent;
            this.lblInkjetNameValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInkjetNameValue.Location = new System.Drawing.Point(144, 19);
            this.lblInkjetNameValue.Name = "lblInkjetNameValue";
            this.lblInkjetNameValue.Size = new System.Drawing.Size(114, 19);
            this.lblInkjetNameValue.TabIndex = 8;
            this.lblInkjetNameValue.Text = "guna2HtmlLabel9";
            // 
            // lblIpAddress
            // 
            this.lblIpAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblIpAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIpAddress.Location = new System.Drawing.Point(18, 48);
            this.lblIpAddress.Name = "lblIpAddress";
            this.lblIpAddress.Size = new System.Drawing.Size(91, 19);
            this.lblIpAddress.TabIndex = 16;
            this.lblIpAddress.Text = "IP Address :";
            // 
            // lblInkjetName
            // 
            this.lblInkjetName.BackColor = System.Drawing.Color.Transparent;
            this.lblInkjetName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInkjetName.Location = new System.Drawing.Point(18, 18);
            this.lblInkjetName.Name = "lblInkjetName";
            this.lblInkjetName.Size = new System.Drawing.Size(98, 19);
            this.lblInkjetName.TabIndex = 0;
            this.lblInkjetName.Text = "Inkjet Name :";
            // 
            // imgSetting
            // 
            this.imgSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.imgSetting.BackColor = System.Drawing.Color.Transparent;
            this.imgSetting.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.imgSetting.HoverState.ImageSize = new System.Drawing.Size(24, 24);
            this.imgSetting.Image = global::KEYENCE_inkjet_printing_control_DEMO.Properties.Resources.settings_22dp_FFFFFF;
            this.imgSetting.ImageOffset = new System.Drawing.Point(0, 0);
            this.imgSetting.ImageRotate = 0F;
            this.imgSetting.ImageSize = new System.Drawing.Size(20, 20);
            this.imgSetting.Location = new System.Drawing.Point(317, 13);
            this.imgSetting.Name = "imgSetting";
            this.imgSetting.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.imgSetting.Size = new System.Drawing.Size(36, 31);
            this.imgSetting.TabIndex = 30;
            this.imgSetting.Click += new System.EventHandler(this.imgSetting_Click);
            // 
            // panelDetails
            // 
            this.panelDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetails.BackColor = System.Drawing.Color.White;
            this.panelDetails.Controls.Add(this.lblErrorManual);
            this.panelDetails.Controls.Add(this.lblError);
            this.panelDetails.Controls.Add(this.btnSaveManual);
            this.panelDetails.Controls.Add(this.btnClearManual);
            this.panelDetails.Controls.Add(this.btnEditManual);
            this.panelDetails.Controls.Add(this.guna2Separator1);
            this.panelDetails.Controls.Add(this.panelDelete);
            this.panelDetails.Controls.Add(this.txtLaterPrintDetail);
            this.panelDetails.Controls.Add(this.txtCurrentData);
            this.panelDetails.Controls.Add(this.txtWaitingPrintDetail);
            this.panelDetails.Controls.Add(this.txtQueueDataValue);
            this.panelDetails.Controls.Add(this.lblLaterPrintDetail);
            this.panelDetails.Controls.Add(this.lblCurrentData);
            this.panelDetails.Controls.Add(this.lblWaitingPrintDetail);
            this.panelDetails.Controls.Add(this.lblQueueData);
            this.panelDetails.Location = new System.Drawing.Point(375, 3);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(892, 133);
            this.panelDetails.TabIndex = 20;
            // 
            // lblErrorManual
            // 
            this.lblErrorManual.BackColor = System.Drawing.Color.Transparent;
            this.lblErrorManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorManual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblErrorManual.Location = new System.Drawing.Point(623, 60);
            this.lblErrorManual.Name = "lblErrorManual";
            this.lblErrorManual.Size = new System.Drawing.Size(90, 19);
            this.lblErrorManual.TabIndex = 36;
            this.lblErrorManual.Text = "ER DETECT";
            this.lblErrorManual.Visible = false;
            // 
            // lblError
            // 
            this.lblError.BackColor = System.Drawing.Color.Transparent;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblError.Location = new System.Drawing.Point(197, 60);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(90, 19);
            this.lblError.TabIndex = 35;
            this.lblError.Text = "ER DETECT";
            this.lblError.Visible = false;
            // 
            // btnSaveManual
            // 
            this.btnSaveManual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveManual.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveManual.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnSaveManual.HoverState.ImageSize = new System.Drawing.Size(24, 24);
            this.btnSaveManual.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveManual.Image")));
            this.btnSaveManual.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnSaveManual.ImageRotate = 0F;
            this.btnSaveManual.ImageSize = new System.Drawing.Size(20, 20);
            this.btnSaveManual.Location = new System.Drawing.Point(840, 88);
            this.btnSaveManual.Name = "btnSaveManual";
            this.btnSaveManual.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnSaveManual.Size = new System.Drawing.Size(36, 31);
            this.btnSaveManual.TabIndex = 34;
            this.btnSaveManual.Visible = false;
            this.btnSaveManual.Click += new System.EventHandler(this.btnSaveManual_Click);
            // 
            // btnClearManual
            // 
            this.btnClearManual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearManual.BackColor = System.Drawing.Color.Transparent;
            this.btnClearManual.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnClearManual.HoverState.ImageSize = new System.Drawing.Size(24, 24);
            this.btnClearManual.Image = ((System.Drawing.Image)(resources.GetObject("btnClearManual.Image")));
            this.btnClearManual.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnClearManual.ImageRotate = 0F;
            this.btnClearManual.ImageSize = new System.Drawing.Size(20, 20);
            this.btnClearManual.Location = new System.Drawing.Point(809, 88);
            this.btnClearManual.Name = "btnClearManual";
            this.btnClearManual.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnClearManual.Size = new System.Drawing.Size(36, 31);
            this.btnClearManual.TabIndex = 33;
            this.btnClearManual.Visible = false;
            this.btnClearManual.Click += new System.EventHandler(this.btnClearManual_Click);
            // 
            // btnEditManual
            // 
            this.btnEditManual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditManual.BackColor = System.Drawing.Color.Transparent;
            this.btnEditManual.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnEditManual.HoverState.ImageSize = new System.Drawing.Size(24, 24);
            this.btnEditManual.Image = ((System.Drawing.Image)(resources.GetObject("btnEditManual.Image")));
            this.btnEditManual.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnEditManual.ImageRotate = 0F;
            this.btnEditManual.ImageSize = new System.Drawing.Size(20, 20);
            this.btnEditManual.Location = new System.Drawing.Point(809, 88);
            this.btnEditManual.Name = "btnEditManual";
            this.btnEditManual.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnEditManual.Size = new System.Drawing.Size(36, 31);
            this.btnEditManual.TabIndex = 31;
            this.btnEditManual.Click += new System.EventHandler(this.btnEditManual_Click);
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.BackColor = System.Drawing.Color.Black;
            this.guna2Separator1.FillColor = System.Drawing.Color.Black;
            this.guna2Separator1.Location = new System.Drawing.Point(404, 19);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(2, 100);
            this.guna2Separator1.TabIndex = 32;
            // 
            // panelDelete
            // 
            this.panelDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDelete.BackColor = System.Drawing.Color.Silver;
            this.panelDelete.BorderColor = System.Drawing.Color.Black;
            this.panelDelete.BorderRadius = 5;
            this.panelDelete.BorderThickness = 2;
            this.panelDelete.Controls.Add(this.lblCloseForm);
            this.panelDelete.Location = new System.Drawing.Point(853, 10);
            this.panelDelete.Name = "panelDelete";
            this.panelDelete.Size = new System.Drawing.Size(26, 24);
            this.panelDelete.TabIndex = 30;
            // 
            // lblCloseForm
            // 
            this.lblCloseForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCloseForm.BackColor = System.Drawing.Color.Transparent;
            this.lblCloseForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCloseForm.ForeColor = System.Drawing.Color.White;
            this.lblCloseForm.Location = new System.Drawing.Point(6, 0);
            this.lblCloseForm.Name = "lblCloseForm";
            this.lblCloseForm.Size = new System.Drawing.Size(15, 22);
            this.lblCloseForm.TabIndex = 33;
            this.lblCloseForm.Text = "X";
            this.lblCloseForm.Click += new System.EventHandler(this.lblCloseForm_Click);
            // 
            // txtLaterPrintDetail
            // 
            this.txtLaterPrintDetail.BorderColor = System.Drawing.Color.Black;
            this.txtLaterPrintDetail.BorderRadius = 5;
            this.txtLaterPrintDetail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLaterPrintDetail.DefaultText = "";
            this.txtLaterPrintDetail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtLaterPrintDetail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtLaterPrintDetail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLaterPrintDetail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLaterPrintDetail.Enabled = false;
            this.txtLaterPrintDetail.FillColor = System.Drawing.Color.WhiteSmoke;
            this.txtLaterPrintDetail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLaterPrintDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLaterPrintDetail.ForeColor = System.Drawing.Color.Black;
            this.txtLaterPrintDetail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLaterPrintDetail.Location = new System.Drawing.Point(18, 85);
            this.txtLaterPrintDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLaterPrintDetail.Name = "txtLaterPrintDetail";
            this.txtLaterPrintDetail.PlaceholderText = "";
            this.txtLaterPrintDetail.ReadOnly = true;
            this.txtLaterPrintDetail.SelectedText = "";
            this.txtLaterPrintDetail.Size = new System.Drawing.Size(345, 41);
            this.txtLaterPrintDetail.TabIndex = 28;
            // 
            // txtCurrentData
            // 
            this.txtCurrentData.BorderColor = System.Drawing.Color.Black;
            this.txtCurrentData.BorderRadius = 5;
            this.txtCurrentData.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCurrentData.DefaultText = "";
            this.txtCurrentData.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCurrentData.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCurrentData.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCurrentData.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCurrentData.Enabled = false;
            this.txtCurrentData.FillColor = System.Drawing.Color.WhiteSmoke;
            this.txtCurrentData.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCurrentData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentData.ForeColor = System.Drawing.Color.Black;
            this.txtCurrentData.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCurrentData.Location = new System.Drawing.Point(18, 26);
            this.txtCurrentData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCurrentData.Name = "txtCurrentData";
            this.txtCurrentData.PlaceholderText = "";
            this.txtCurrentData.ReadOnly = true;
            this.txtCurrentData.SelectedText = "";
            this.txtCurrentData.Size = new System.Drawing.Size(345, 30);
            this.txtCurrentData.TabIndex = 27;
            // 
            // txtWaitingPrintDetail
            // 
            this.txtWaitingPrintDetail.BorderColor = System.Drawing.Color.Black;
            this.txtWaitingPrintDetail.BorderRadius = 5;
            this.txtWaitingPrintDetail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtWaitingPrintDetail.DefaultText = "";
            this.txtWaitingPrintDetail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtWaitingPrintDetail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtWaitingPrintDetail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtWaitingPrintDetail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtWaitingPrintDetail.FillColor = System.Drawing.Color.WhiteSmoke;
            this.txtWaitingPrintDetail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtWaitingPrintDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWaitingPrintDetail.ForeColor = System.Drawing.Color.Black;
            this.txtWaitingPrintDetail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtWaitingPrintDetail.Location = new System.Drawing.Point(458, 85);
            this.txtWaitingPrintDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWaitingPrintDetail.Name = "txtWaitingPrintDetail";
            this.txtWaitingPrintDetail.PlaceholderText = "";
            this.txtWaitingPrintDetail.ReadOnly = true;
            this.txtWaitingPrintDetail.SelectedText = "";
            this.txtWaitingPrintDetail.Size = new System.Drawing.Size(345, 41);
            this.txtWaitingPrintDetail.TabIndex = 26;
            // 
            // txtQueueDataValue
            // 
            this.txtQueueDataValue.BackColor = System.Drawing.Color.White;
            this.txtQueueDataValue.BorderColor = System.Drawing.Color.Black;
            this.txtQueueDataValue.BorderRadius = 5;
            this.txtQueueDataValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtQueueDataValue.DefaultText = "";
            this.txtQueueDataValue.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtQueueDataValue.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtQueueDataValue.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtQueueDataValue.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtQueueDataValue.Enabled = false;
            this.txtQueueDataValue.FillColor = System.Drawing.Color.WhiteSmoke;
            this.txtQueueDataValue.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtQueueDataValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQueueDataValue.ForeColor = System.Drawing.Color.Black;
            this.txtQueueDataValue.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtQueueDataValue.Location = new System.Drawing.Point(458, 26);
            this.txtQueueDataValue.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtQueueDataValue.Name = "txtQueueDataValue";
            this.txtQueueDataValue.PlaceholderText = "";
            this.txtQueueDataValue.ReadOnly = true;
            this.txtQueueDataValue.SelectedText = "";
            this.txtQueueDataValue.Size = new System.Drawing.Size(345, 30);
            this.txtQueueDataValue.TabIndex = 25;
            // 
            // lblLaterPrintDetail
            // 
            this.lblLaterPrintDetail.BackColor = System.Drawing.Color.Transparent;
            this.lblLaterPrintDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold);
            this.lblLaterPrintDetail.Location = new System.Drawing.Point(458, 60);
            this.lblLaterPrintDetail.Name = "lblLaterPrintDetail";
            this.lblLaterPrintDetail.Size = new System.Drawing.Size(144, 19);
            this.lblLaterPrintDetail.TabIndex = 24;
            this.lblLaterPrintDetail.Text = "Latest Print Detail :";
            // 
            // lblCurrentData
            // 
            this.lblCurrentData.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold);
            this.lblCurrentData.Location = new System.Drawing.Point(458, 0);
            this.lblCurrentData.Name = "lblCurrentData";
            this.lblCurrentData.Size = new System.Drawing.Size(106, 19);
            this.lblCurrentData.TabIndex = 23;
            this.lblCurrentData.Text = "Current Data :";
            // 
            // lblWaitingPrintDetail
            // 
            this.lblWaitingPrintDetail.BackColor = System.Drawing.Color.Transparent;
            this.lblWaitingPrintDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold);
            this.lblWaitingPrintDetail.Location = new System.Drawing.Point(18, 60);
            this.lblWaitingPrintDetail.Name = "lblWaitingPrintDetail";
            this.lblWaitingPrintDetail.Size = new System.Drawing.Size(153, 19);
            this.lblWaitingPrintDetail.TabIndex = 22;
            this.lblWaitingPrintDetail.Text = "Waiting Print Detail :";
            // 
            // lblQueueData
            // 
            this.lblQueueData.BackColor = System.Drawing.Color.Transparent;
            this.lblQueueData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold);
            this.lblQueueData.Location = new System.Drawing.Point(18, 2);
            this.lblQueueData.Name = "lblQueueData";
            this.lblQueueData.Size = new System.Drawing.Size(100, 19);
            this.lblQueueData.TabIndex = 21;
            this.lblQueueData.Text = "Queue Data :";
            // 
            // ucItem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelPaddingRight);
            this.Controls.Add(this.panelPaddingLeft);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucItem";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.Size = new System.Drawing.Size(1280, 144);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            this.panelDelete.ResumeLayout(false);
            this.panelDelete.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPaddingLeft;
        private System.Windows.Forms.Panel panelPaddingRight;
        public Guna.UI2.WinForms.Guna2HtmlLabel lblInkjetName;
        public Guna.UI2.WinForms.Guna2HtmlLabel lblInkjetNameValue;
        public Guna.UI2.WinForms.Guna2HtmlLabel lblStatusValue;
        public Guna.UI2.WinForms.Guna2HtmlLabel lblStatus;
        public Guna.UI2.WinForms.Guna2HtmlLabel lblIpAddressValue;
        public Guna.UI2.WinForms.Guna2HtmlLabel lblIpAddress;
        public Guna.UI2.WinForms.Guna2GradientPanel panelMain;
        private Panel panelDetails;
        private Guna.UI2.WinForms.Guna2GradientPanel panelDelete;
        private Guna.UI2.WinForms.Guna2TextBox txtLaterPrintDetail;
        private Guna.UI2.WinForms.Guna2TextBox txtCurrentData;
        private Guna.UI2.WinForms.Guna2TextBox txtWaitingPrintDetail;
        private Guna.UI2.WinForms.Guna2TextBox txtQueueDataValue;
        public Guna.UI2.WinForms.Guna2HtmlLabel lblLaterPrintDetail;
        public Guna.UI2.WinForms.Guna2HtmlLabel lblCurrentData;
        public Guna.UI2.WinForms.Guna2HtmlLabel lblWaitingPrintDetail;
        public Guna.UI2.WinForms.Guna2HtmlLabel lblQueueData;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private Guna.UI2.WinForms.Guna2ImageButton imgSetting;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCloseForm;
        private Guna.UI2.WinForms.Guna2ImageButton btnEditManual;
        private Guna.UI2.WinForms.Guna2CircleButton circleStatus;
        private Guna.UI2.WinForms.Guna2ImageButton btnSaveManual;
        private Guna.UI2.WinForms.Guna2ImageButton btnClearManual;
        public Guna.UI2.WinForms.Guna2HtmlLabel lblError;
        public Guna.UI2.WinForms.Guna2HtmlLabel lblErrorManual;
    }
}

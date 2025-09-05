namespace KEYENCE_inkjet_printing_control_DEMO.UserControls
{
    partial class ucAdd
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.txtAddInkjet = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnAddInkjet = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2GradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(5, 160);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1275, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(5, 160);
            this.panel2.TabIndex = 1;
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BorderColor = System.Drawing.Color.Gray;
            this.guna2GradientPanel1.BorderRadius = 25;
            this.guna2GradientPanel1.BorderThickness = 4;
            this.guna2GradientPanel1.Controls.Add(this.txtAddInkjet);
            this.guna2GradientPanel1.Controls.Add(this.btnAddInkjet);
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.White;
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.White;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(5, 0);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(1270, 160);
            this.guna2GradientPanel1.TabIndex = 2;
            // 
            // txtAddInkjet
            // 
            this.txtAddInkjet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtAddInkjet.BackColor = System.Drawing.Color.Transparent;
            this.txtAddInkjet.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddInkjet.ForeColor = System.Drawing.Color.Gray;
            this.txtAddInkjet.Location = new System.Drawing.Point(579, 102);
            this.txtAddInkjet.Name = "txtAddInkjet";
            this.txtAddInkjet.Size = new System.Drawing.Size(137, 35);
            this.txtAddInkjet.TabIndex = 4;
            this.txtAddInkjet.Text = "Add Inkjet";
            // 
            // btnAddInkjet
            // 
            this.btnAddInkjet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnAddInkjet.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddInkjet.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddInkjet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddInkjet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddInkjet.FillColor = System.Drawing.Color.Gray;
            this.btnAddInkjet.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddInkjet.ForeColor = System.Drawing.Color.White;
            this.btnAddInkjet.Image = global::KEYENCE_inkjet_printing_control_DEMO.Properties.Resources.add_80dp_FFFFFF;
            this.btnAddInkjet.ImageSize = new System.Drawing.Size(80, 80);
            this.btnAddInkjet.Location = new System.Drawing.Point(601, 28);
            this.btnAddInkjet.Name = "btnAddInkjet";
            this.btnAddInkjet.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnAddInkjet.Size = new System.Drawing.Size(75, 68);
            this.btnAddInkjet.TabIndex = 0;
            this.btnAddInkjet.Click += new System.EventHandler(this.btnAddInkjet_Click);
            // 
            // ucAdd
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.guna2GradientPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucAdd";
            this.Size = new System.Drawing.Size(1280, 160);
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2GradientPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        public Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2CircleButton btnAddInkjet;
        public Guna.UI2.WinForms.Guna2HtmlLabel txtAddInkjet;
    }
}

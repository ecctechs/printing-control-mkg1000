using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KEYENCE_inkjet_printing_control_DEMO.Class;
using KEYENCE_inkjet_printing_control_DEMO.UserControls;
using Newtonsoft.Json;
using Ookii.Dialogs.WinForms;

namespace KEYENCE_inkjet_printing_control_DEMO
{
    public partial class frmMain : Form
    {
        public string path_browse_status;
        public frmMain()
        {
            InitializeComponent();
            GetucOverView();

            var configs = ConfigManager.Load();

            LiveStatusManager.Initialize(configs);
        }

        public void GetTimerRealTime()
        {
            DateTime now = DateTime.Now;
            string realtime = now.ToString("ddd, dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            guna2HtmlLabel1.Text = realtime;    
        }

        public void GetucOverView()
        {
            ucOverview uc = new ucOverview();
            panelContainer.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
            uc.BringToFront();
        }

        private void lblCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowseStatus_Click(object sender, EventArgs e)
        {
            using (VistaFolderBrowserDialog folderDialog = new VistaFolderBrowserDialog())
            {
                folderDialog.Description = "Please select a folder";
                folderDialog.UseDescriptionForTitle = true;

                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    AppSettings.SaveAppSettings(folderDialog.SelectedPath);
                    var configs = ConfigManager.Load();
                    LiveStatusManager.Initialize(configs);
                }
            }
        }

        private async void btnOpenFolderStatus_Click(object sender, EventArgs e)
        {
            string loadedStatusCsvPath = AppSettings.LoadAppSettings();
            if (!string.IsNullOrEmpty(loadedStatusCsvPath) && Directory.Exists(loadedStatusCsvPath))
            {
                // เปิดโฟลเดอร์ใน Windows Explorer
                System.Diagnostics.Process.Start("explorer.exe", loadedStatusCsvPath);
            }
            else
            {
                MessageBox.Show("ยังไม่ได้เลือกโฟลเดอร์ หรือโฟลเดอร์ไม่พบ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timerRealTime_Tick(object sender, EventArgs e)
        {
            GetTimerRealTime();
        }
    }
}

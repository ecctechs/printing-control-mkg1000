using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            get_inkjet();

            var configs = ConfigManager.Load();

            LiveStatusManager.Initialize(configs);

            //string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "keyence_status_codes.json");

            //if (!File.Exists(jsonPath))
            //{
            //    Console.WriteLine("ไม่พบไฟล์ JSON ที่: " + jsonPath);
            //    return;
            //}

            //// อ่านไฟล์เป็น UTF-8
            //string jsonString = File.ReadAllText(jsonPath, Encoding.UTF8);
            //StatusData codes = JsonConvert.DeserializeObject<StatusData>(jsonString);

            //// แสดงผล
            //Console.WriteLine("Status 01: " + codes.StatusCodes["01"]);
            //Console.WriteLine("Error 02: " + codes.ErrorCodes["02"]);
            //Console.WriteLine("Warning 101: " + codes.WarningCodes["101"]);
        }

        public void get_inkjet()
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using KEYENCE_inkjet_printing_control_DEMO.Class;
using Ookii.Dialogs.WinForms;

namespace KEYENCE_inkjet_printing_control_DEMO
{
    public partial class frmAddEdit : Form
    {
        private bool isEditMode = false;
        private InkjetConfig _originalConfig;

        private string AssembledIpAddress => $"{txtIpAddress1.Text}.{txtIpAddress2.Text}.{txtIpAddress3.Text}.{txtIpAddress4.Text}";

        public frmAddEdit()
        {
            InitializeComponent();
            lblFrmMain.Text = "Add New Inkjet";
            btnAddInkjet.Text = "Add";
        }

        public frmAddEdit(InkjetConfig configToEdit)
        {
            InitializeComponent();
            isEditMode = true;
            _originalConfig = configToEdit;

            lblFrmMain.Text = "Edit Inkjet Info";
            btnAddInkjet.Text = "Save";

            txtInkjetName.Text = configToEdit.InkjetName;
            txtIpAddress1.Text = configToEdit.IpAddress;
            txtPort.Text = configToEdit.Port.ToString();
            txtInputDirectory.Text = configToEdit.InputDirectory;
            txtOutputDirectory.Text = configToEdit.OutputDirectory;

            // ✅ Split the existing IP address into the 4 textboxes.
            if (!string.IsNullOrEmpty(configToEdit.IpAddress))
            {
                string[] ipParts = configToEdit.IpAddress.Split('.');
                if (ipParts.Length == 4)
                {
                    txtIpAddress1.Text = ipParts[0];
                    txtIpAddress2.Text = ipParts[1];
                    txtIpAddress3.Text = ipParts[2];
                    txtIpAddress4.Text = ipParts[3];
                }
            }
        }

        private void lblCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddInkjet_Click(object sender, EventArgs e)
        {
            // เรียกใช้ฟังก์ชันตรวจสอบข้อมูล ถ้าไม่ผ่านก็จบการทำงาน
            if (!IsDataValid()) return;
            try
            {
                // สร้าง object ใหม่จากค่าใน TextBox (เหมือนเดิม)
                var updatedConfig = new InkjetConfig
                {
                    InkjetName = txtInkjetName.Text,
                    IpAddress = this.AssembledIpAddress,
                    Port = int.Parse(txtPort.Text),
                    InputDirectory = txtInputDirectory.Text,
                    OutputDirectory = txtOutputDirectory.Text
                };

                if (isEditMode)
                {
                    // --- โหมดแก้ไข ---
                    // เรียกใช้ฟังก์ชัน Edit โดยใช้ชื่อเดิมในการค้นหา
                    ConfigManager.Edit(_originalConfig.InkjetName, updatedConfig);
                    MessageBox.Show("แก้ไขข้อมูลสำเร็จ ✅", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // --- โหมดเพิ่ม ---
                    ConfigManager.Add(updatedConfig);
                    MessageBox.Show("เพิ่มข้อมูลสำเร็จ ✅", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                DialogResult = DialogResult.OK; // ส่งผลลัพธ์ OK กลับไป
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsDataValid()
        {
            // ตรวจสอบช่องกรอกครบ
            if (string.IsNullOrWhiteSpace(txtInkjetName.Text) ||
                string.IsNullOrWhiteSpace(txtIpAddress1.Text) ||
                string.IsNullOrWhiteSpace(txtIpAddress2.Text) ||
                string.IsNullOrWhiteSpace(txtIpAddress3.Text) ||
                string.IsNullOrWhiteSpace(txtIpAddress4.Text) ||
                string.IsNullOrWhiteSpace(txtPort.Text) ||
                string.IsNullOrWhiteSpace(txtInputDirectory.Text) ||
                string.IsNullOrWhiteSpace(txtOutputDirectory.Text))
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบทุกช่อง", "ข้อมูลไม่สมบูรณ์", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // รวม IP Address จาก 4 ช่อง
            string currentIp = $"{txtIpAddress1.Text}.{txtIpAddress2.Text}.{txtIpAddress3.Text}.{txtIpAddress4.Text}";

            // โหลด configs ทั้งหมด
            var allConfigs = ConfigManager.Load();

            // ถ้าอยู่โหมดแก้ไข: ข้ามตัวเอง
            List<InkjetConfig> configsToCheck = isEditMode
                ? allConfigs.Where(c => c.InkjetName != _originalConfig.InkjetName).ToList()
                : allConfigs;

            // ตรวจสอบ IP ซ้ำกับเครื่องอื่น
            if (configsToCheck.Any(c => c.IpAddress.Equals(currentIp, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("IP Address นี้มีอยู่แล้วในเครื่องอื่น กรุณาใช้ IP อื่น", "ข้อมูลซ้ำ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // ตรวจสอบชื่อ Inkjet ซ้ำ
            if (configsToCheck.Any(c => c.InkjetName.Equals(txtInkjetName.Text, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("ชื่อ Inkjet นี้มีอยู่แล้ว กรุณาใช้ชื่ออื่น", "ข้อมูลซ้ำ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // ตรวจสอบ Input/Output Directory ซ้ำกับเครื่องอื่น
            if (configsToCheck.Any(c => c.InputDirectory.Equals(txtInputDirectory.Text, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Input Directory นี้มีอยู่แล้วในเครื่องอื่น", "ข้อมูลซ้ำ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (configsToCheck.Any(c => c.OutputDirectory.Equals(txtOutputDirectory.Text, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Output Directory นี้มีอยู่แล้วในเครื่องอื่น", "ข้อมูลซ้ำ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // ตรวจสอบ Input ≠ Output ในเครื่องเดียวกัน
            if (txtInputDirectory.Text.Equals(txtOutputDirectory.Text, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Input Directory และ Output Directory ต้องไม่ซ้ำกัน", "ข้อมูลไม่ถูกต้อง", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true; // ผ่านทุกการตรวจสอบ
        }



        public void SelectFolder(Guna.UI2.WinForms.Guna2TextBox targetTextBox, string description)
        {
            using (var folderDialog = new VistaFolderBrowserDialog())
            {
                folderDialog.Description = description;
                // Use the description as the title of the dialog window.
                folderDialog.UseDescriptionForTitle = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // The rest of the code is the same!
                    targetTextBox.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            SelectFolder(txtInputDirectory, "เลือกโฟลเดอร์สำหรับ Input");
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            SelectFolder(txtOutputDirectory, "เลือกโฟลเดอร์สำหรับ Output");
        }

        // ตรวจสอบว่าเป็นค่า 0-255 และไม่เกิน 3 หลัก
        private void ValidateIpOctet(object sender, EventArgs e)
        {
            int maxIpOctet = 255;
            var textBox = sender as Guna.UI2.WinForms.Guna2TextBox;

            if (textBox == null) return;

            // จำกัดจำนวนตัวอักษรสูงสุด 3 หลัก
            textBox.MaxLength = 3;

            // ตรวจสอบค่าตัวเลข
            if (int.TryParse(textBox.Text, out int value))
            {
                if (value > maxIpOctet)
                {
                    textBox.Text = maxIpOctet.ToString();
                    textBox.SelectionStart = textBox.Text.Length; // ย้าย cursor ไปท้าย
                }
            }
        }


        private void AllowNumbersOnly(object sender, KeyPressEventArgs e)
        {
            // อนุญาตเฉพาะตัวเลข และปุ่มควบคุม (เช่น Backspace, Delete)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            int maxPort = 65535;
            // ตรวจสอบค่าตัวเลข
            if (int.TryParse(txtPort.Text, out int value))
            {
                if (value > maxPort)
                {
                    txtPort.Text = maxPort.ToString();
                    txtPort.SelectionStart = txtPort.Text.Length; // ย้าย cursor ไปท้าย
                }
            }
        }

        // hide caret
        private void txtInputDirectory_Enter(object sender, EventArgs e)
        {
            btnAddInkjet.Focus();
        }

        // hide caret
        private void txtOutputDirectory_Enter(object sender, EventArgs e)
        {
            btnAddInkjet.Focus();
        }
    }
}

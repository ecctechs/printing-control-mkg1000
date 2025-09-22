using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms; 
using KEYENCE_inkjet_printing_control_DEMO.Class;
using System.IO;     
using System.Linq;       
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Guna.UI2.WinForms;
using System.Collections.Generic;
using StatusMapping;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Windows.Interop;
using System.Diagnostics.Eventing.Reader;
using Guna.UI2.WinForms.Suite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace KEYENCE_inkjet_printing_control_DEMO.UserControls
{
    public partial class ucItem : UserControl
    {
        public event EventHandler ItemDeleted;
        public event EventHandler ItemEdited;
        private InkjetConfig _currentConfig;

        private Panel _leftEdgePanel;

        // เพิ่ม Timer และตัวแปรสถานะสำหรับการประมวลผลไฟล์
        private Timer _fileMonitorTimer;
        private bool _isProcessingFile = false;

        private LoadMapping _mapping;

        public ucItem()
        {
            InitializeComponent();
            InitializeLeftEdgePanel();
            SetPanel3RoundedCorners(panelDetails, 50);

            // ปิด Hover Effect
            circleStatus.HoverState.FillColor = circleStatus.FillColor;
            circleStatus.HoverState.ForeColor = circleStatus.ForeColor;
            circleStatus.HoverState.BorderColor = circleStatus.BorderColor;

            // ถ้าไม่อยากให้กดแล้วเปลี่ยนสีก็ปิดด้วย
            circleStatus.PressedColor = circleStatus.FillColor;
            circleStatus.CheckedState.FillColor = circleStatus.FillColor;

            // ✅ ตั้งค่าและเริ่มการทำงานของ Timer
            _fileMonitorTimer = new Timer();
            _fileMonitorTimer.Interval = 2000; // ตรวจสอบทุกๆ 2 วินาที
            _fileMonitorTimer.Tick += ProcessFileTimerTick;
            _fileMonitorTimer.Start();

            // ✅ สมัครรับ Event จาก Manager
            KeyenceConnectionManager.OnStatusReceived += ConnectionManager_OnStatusReceived;

            // Load Status Mapping 
            _mapping = new LoadMapping();
            _mapping.LoadStatus();
            _mapping.LoadCommunicationError();
        }

        /// <summary>
        /// เมธอดนี้จะถูกเรียกทุกครั้งที่ Manager ได้รับสถานะใหม่จากเครื่องพิมพ์ใดๆ
        /// </summary>
        private void ConnectionManager_OnStatusReceived(string inkjetName, List<string> statusCodes , string type)
        {
            if (_currentConfig?.InkjetName == inkjetName)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    UpdateStatus(statusCodes , type);
                });
            }
        }

        // สร้างเมธอดสำหรับจัดการ Timer Tick (ทำงานแบบ async)
        private async void ProcessFileTimerTick(object sender, EventArgs e)
        {
            if (_isProcessingFile) return;
            if (_currentConfig == null || string.IsNullOrEmpty(_currentConfig.InputDirectory) || !Directory.Exists(_currentConfig.InputDirectory)) // กรณีนี้ที่โฟลเดอร์ถูกเซ็ตไว้ แล้วโดนลบจะเข้าเงือนไขนี้
            {
                // อาจแสดงข้อความสถานะได้
                txtCurrentData.Text = "Invalid Input Directory";
                return;
            }

            try
            {
                var txtFile = Directory.GetFiles(_currentConfig.InputDirectory, "*.txt").FirstOrDefault();
                if (txtFile != null) // --- ถ้าเจอไฟล์ ---
                {
                
                    _isProcessingFile = true;

                    var processTime = DateTime.Now;
                    string fileName = Path.GetFileName(txtFile);
                    string fileContent = await Task.Run(() => File.ReadAllText(txtFile));

                    // อัพเดท UI ทันทีที่เจอไฟล์
                    txtCurrentData.Text = $"{processTime:G} - {fileName}";
                    txtLaterPrintDetail.Text = fileContent;

                    // รอ 10 วิ (จำลองหน่วงเวลา)
                    await Task.Delay(10000);

                    var isSendSuccess = await KeyenceConnectionManager.SendMessageAsync(_currentConfig.InkjetName, fileContent);

                    if (isSendSuccess.Success)
                    {
                        txtLaterPrintDetail.BorderColor = Color.Black;
                        lblError.Visible = false;
    
                        await Task.Run(() =>
                        {
                            string logFilePath = Path.Combine(_currentConfig.OutputDirectory, "processing_log.txt");
                            string logEntry = $"{processTime:G},{_currentConfig.InkjetName},Auto,{fileContent.Replace(Environment.NewLine, " ")}";

                            // --- Create a new status object to send to the manager ---
                            var currentStatus = new CurrentInkjetStatus
                            {
                                Timestamp = DateTime.Now,
                                InkjetName = _currentConfig.InkjetName,
                                CurrentMessage = fileContent, // Use existing data                       
                            };

                            // ✅ Call the new manager to update this printer's status and rewrite the file.
                            LiveStatusManager.UpdateAndSaveStatus(currentStatus);

                            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);

                            // ลบไฟล์ต้นทาง
                            File.Delete(txtFile);
                        });

                        // อัพเดท UI หลังประมวลผลเสร็จ
                        txtQueueDataValue.Text = $"{processTime:G} - {fileName}";
                        txtWaitingPrintDetail.Text = fileContent;

                        // บันทึกการเปลี่ยนแปลงลงไฟล์ JSON
                        _currentConfig.CurrentData = $"{processTime:G} - {fileName}";
                        _currentConfig.LatestPrintDetail = fileContent;
                        ConfigManager.Edit(_currentConfig.InkjetName, _currentConfig);

                        txtCurrentData.Text = "Waiting Data From Server";
                        txtLaterPrintDetail.Text = "";
                        _isProcessingFile = false;
                    }
                    else
                    {
                        string lastPart = isSendSuccess.ErrorCode?.Split(',').Last(); // "22"
                        // สมมติว่า errorCode มาจากเครื่อง (ในที่นี้อาจเป็น "01" หรือ "90" เป็นต้น)
                        string errorCode = lastPart; // TODO: ดึงจากผลลัพธ์จริง
                        var error = _mapping.GetCommunicationErrorByCode(errorCode);

                        lblError.Visible = true;
                        txtLaterPrintDetail.BorderColor = Color.Red;

                        if (error != null)
                        {
                            lblError.Text = $"❌ ERROR [{lastPart}] {error.Description}";
                        }
                        else
                        {
                            lblError.Text = $"❌ ERROR [{lastPart}] ไม่พบรายละเอียดใน mapping";
                        }

                        _isProcessingFile = false;
                    }             
                }
                else
                {
                    // ถ้าไม่เจอไฟล์
                    txtCurrentData.Text = "Waiting Data From Server";
                }
            }
            catch (Exception ex)
            {
                txtCurrentData.Text = $"Error: {ex.Message}";
                _isProcessingFile = false;
            }
        }


        public void SetData(InkjetConfig config)
        {
            _currentConfig = config;
            lblInkjetNameValue.Text = config.InkjetName;
            lblIpAddressValue.Text = config.IpAddress;
            lblStatusValue.Text = config.Status ?? "";
            SetStatusColor(config.Status);

            txtCurrentData.Text = "Waiting Data From Server";
            txtCurrentData.ForeColor = Color.Red;
            txtQueueDataValue.Text = config.CurrentData;
            txtWaitingPrintDetail.Text = config.LatestPrintDetail;
        }

        public void UpdateStatus(List<string> statusCodes , string type)
        {
            if (_currentConfig == null) return;

            string mainCategory = "Unknown";
            string mainDetail = "Unknown";
            List<string> tooltipList = new List<string>();

            foreach (var code in statusCodes)
            {
                string trimmed = code.Trim();
                string msg = _mapping.GetStatus(trimmed, type);

                // เก็บรายละเอียดทั้งหมดสำหรับ tooltip
                tooltipList.Add($"{trimmed}: {msg}");

                // --- Mapping ด้วยช่วงของรหัส ---
                int codeNum;
                if (int.TryParse(trimmed, out codeNum))
                {
                    if (codeNum >= 1 && codeNum <= 99 && type == "EV") // Error
                    {
                        if (mainCategory != "Error")
                        {
                            mainCategory = "Error";
                            mainDetail = msg.Trim();
                        }
                    }
                    else if (codeNum >= 101 && codeNum <= 192 && type == "EV") // Warning
                    {
                        if (mainCategory != "Error" && mainCategory != "Warning")
                        {
                            mainCategory = "Warning";
                            mainDetail = msg.Trim();
                        }
                    }
                    else
                    {
                        if(codeNum == 04)
                        {
                            mainCategory = "Suspended";
                        }
                        else if(codeNum == 05)
                        {
                            mainCategory = "Starting";
                        }
                        else if(codeNum == 06)
                        {
                            mainCategory = "Shutting Down";
                        }
                        else if (codeNum == 01)
                        {
                            mainCategory = "Printable";
                        }
                        else
                        {
                            mainCategory = "Stopped";
                        }
                    }
                }
                else
                {
                    mainCategory = "Disconnect";
                }
            }

            // --- Update UI ---
            lblStatusValue.Text = mainCategory;
            lblStatusDetailValue.Text = mainDetail;
            SetStatusColor(mainCategory);

            // --- ตั้ง Visible ตาม Category ---
            lblStatusDetailValue.Visible = (mainCategory == "Error" || mainCategory == "Warning");

            // --- Tooltip แสดงรายละเอียดทั้งหมด ---
            string detail = string.Join(Environment.NewLine, tooltipList);
            ToolTip tt = new ToolTip();
            tt.SetToolTip(lblStatusValue, detail);
            tt.SetToolTip(lblStatusDetailValue, detail);

            // --- สร้าง status object ---
            var currentStatus = new CurrentInkjetStatus
            {
                Timestamp = DateTime.Now,
                InkjetName = _currentConfig.InkjetName,
                Status = mainCategory,
                CurrentMessage = _currentConfig.LatestPrintDetail
            };

            // ทำ ErrorDetail / ErrorCode เฉพาะกรณี EV
            if (type == "EV")
            {
                var details = statusCodes
                    .Select(code => _mapping.GetStatus(code.Trim(), type))
                    .Select(msg => msg
                        .Replace("[ERROR]", "")
                        .Replace("[WARNING]", "")
                        .Replace("[STATUS]", "")
                        .Trim())
                    .ToList();

                var codes = statusCodes.Select(code => code.Trim()).ToList();

                currentStatus.ErrorDetail = string.Join(",", details) != "---"
                ? $"\"[{string.Join(",", details)}]\""
                : "---";
                currentStatus.ErrorCode = string.Join(",", details) != "---"
                ? $"\"[{string.Join(",", details)}]\""
                : "---";
            }
            else
            {
                currentStatus.ErrorDetail = "---";
                currentStatus.ErrorCode = "---";
            }

            LiveStatusManager.UpdateAndSaveStatus(currentStatus);
        }

        private void SetStatusColor(string status)
        {
            Color baseColor;
            switch (status)
            {
                case "Printable":
                    baseColor = Color.FromArgb(144, 238, 144); // LightGreen (นุ่มนวล)
                    break;
                case "Warning":
                    baseColor = Color.FromArgb(255, 200, 100); // Soft Orange
                    break;
                case "Error":
                    baseColor = Color.FromArgb(255, 160, 160); // Soft Red / Light Coral
                    break;
                case "Stop":
                case "Suspended":
                case "Disconnected":
                case "Offline":
                    baseColor = Color.FromArgb(190, 190, 190); // Soft Gray
                    break;
                default:
                    baseColor = Color.FromArgb(220, 220, 220); // Very Light Gray
                    break;
            }

            // ทำสีขอบให้เข้มนิดหน่อย (แต่ไม่เข้มเกิน)
            Color darkerBorderColor = ControlPaint.Dark(baseColor, 0.1f);

            // --- กำหนดสีให้กับ Controls ---
            panelMain.FillColor = baseColor;
            panelMain.FillColor2 = baseColor;
            panelMain.BorderColor = darkerBorderColor;
            circleStatus.FillColor = darkerBorderColor;

            if (_leftEdgePanel != null)
            {
                _leftEdgePanel.BackColor = darkerBorderColor;
            }
        }

        private void InitializeLeftEdgePanel() // ตั้งค่าเริ่มต้น ขอบซ้าย pannelDetail โค้ง
        {
            _leftEdgePanel = new Panel();
            _leftEdgePanel.Size = new Size(5, panelDetails.Height); // กว้าง 10 px
            _leftEdgePanel.Location = new Point(0, 0);
            _leftEdgePanel.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
            panelDetails.Controls.Add(_leftEdgePanel);
        }

        private void SetPanel3RoundedCorners(Panel panel, int radius)  // ฟังก์ชันสำหรับทำให้ Panel มีมุมโค้งมนตาม radius ที่กำหนด
        {
            GraphicsPath path = new GraphicsPath();
            int w = panel.Width;
            int h = panel.Height;
            path.StartFigure();
            path.AddLine(0, 0, w - radius, 0);
            path.AddArc(w - radius, 0, radius, radius, 270, 90);
            path.AddLine(w, radius, w, h - radius);
            path.AddArc(w - radius, h - radius, radius, radius, 0, 90);
            path.AddLine(w - radius, h, 0, h);
            path.AddLine(0, h, 0, 0);
            path.CloseFigure();
            panel.Region = new Region(path);
            panel.Resize += (s, e) => { SetPanel3RoundedCorners(panel, radius); };
        }
        private void lblCloseForm_Click(object sender, EventArgs e)
        {
            if (_currentConfig == null) return;
            var confirmResult = MessageBox.Show($"คุณต้องการลบ '{_currentConfig.InkjetName}' ใช่หรือไม่?",
                                     "ยืนยันการลบ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    ConfigManager.Delete(_currentConfig.InkjetName);
                    ItemDeleted?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการลบ: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void imgSetting_Click(object sender, EventArgs e)
        {
            if (_currentConfig == null) return;
            using (frmAddEdit frm = new frmAddEdit(_currentConfig))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    ItemEdited?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void btnEditManual_Click(object sender, EventArgs e)
        {
            txtWaitingPrintDetail.Text = "";
            txtWaitingPrintDetail.FillColor = Color.White;
            txtWaitingPrintDetail.ReadOnly = false;
            btnClearManual.Visible = true;
            btnSaveManual.Visible = true;
        }

        private void btnClearManual_Click(object sender, EventArgs e)
        {
            txtWaitingPrintDetail.Text = _currentConfig.LatestPrintDetail;
            txtWaitingPrintDetail.FillColor = Color.WhiteSmoke;
            txtWaitingPrintDetail.ReadOnly = true;
            btnEditManual.Visible = true;
            btnClearManual.Visible = false;
            btnSaveManual.Visible = false;
            imgSetting.Focus();
        }

        private async void btnSaveManual_Click(object sender, EventArgs e)
        {
            if(txtWaitingPrintDetail.Text == "")
            {
                MessageBox.Show("กรุณาพิมพ์ข้อความลงในช่อง Latest Print Detail ก่อนทําการส่งข้อมูล", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult result = MessageBox.Show(
                "คุณต้องการส่งข้อความ "+ txtWaitingPrintDetail.Text + " หรือไม่?",
                "Confirm Action",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Prepare data for logging
                    var processTime = DateTime.Now;
                    string contentToLog = txtWaitingPrintDetail.Text;
                    string logFilePath = Path.Combine(_currentConfig.OutputDirectory, "processing_log.txt");

                    // Create the log entry with the "Manual" type
                    string logEntry = $"{processTime:G},{_currentConfig.InkjetName},Manual,{contentToLog.Replace(Environment.NewLine, " ")}";

                    // ส่งข้อความไป Keyence
                    var isSendSuccess = await KeyenceConnectionManager.SendMessageAsync(_currentConfig.InkjetName, contentToLog);

                    if (isSendSuccess.Success)
                    {
                        lblErrorManual.Visible = false;
                        txtWaitingPrintDetail.BorderColor = Color.Black;

                        File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
                        MessageBox.Show("ส่งข้อมูล Manual สำเร็จ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        _currentConfig.LatestPrintDetail = txtWaitingPrintDetail.Text;
                        ConfigManager.Edit(_currentConfig.InkjetName, _currentConfig);

                        txtWaitingPrintDetail.FillColor = Color.WhiteSmoke;
                        txtWaitingPrintDetail.ReadOnly = true;
                        btnEditManual.Visible = true;
                        btnClearManual.Visible = false;
                        btnSaveManual.Visible = false;
                        imgSetting.Focus();
                    }
                    else
                    {
                        // ❌ ส่งไม่สำเร็จ ดึง ErrorCode ตัวสุดท้าย
                        string lastPart = isSendSuccess.ErrorCode?.Split(',').Last();
                        var error = _mapping.GetCommunicationErrorByCode(lastPart);

                        lblErrorManual.Visible = true;
                        txtWaitingPrintDetail.BorderColor = Color.Red;
                        lblErrorManual.Text = error != null
                            ? $"❌ ERROR [{lastPart}] {error.Description}"
                            : $"❌ ERROR [{lastPart}] ไม่พบรายละเอียดใน mapping";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการบันทึก Log: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtCurrentData_Enter(object sender, EventArgs e)
        {
            imgSetting.Focus();
        }

        private void txtQueueDataValue_Enter(object sender, EventArgs e)
        {
            imgSetting.Focus();
        }

        private void txtLaterPrintDetail_Enter(object sender, EventArgs e)
        {
            imgSetting.Focus();
        }
    }
}




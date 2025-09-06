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

        public ucItem()
        {
            InitializeComponent();
            InitializeLeftEdgePanel();
            SetPanel3RoundedCorners(panelDetails, 50);

            // ✅ 5. ตั้งค่าและเริ่มการทำงานของ Timer
            _fileMonitorTimer = new Timer();
            _fileMonitorTimer.Interval = 2000; // ตรวจสอบทุกๆ 2 วินาที
            _fileMonitorTimer.Tick += FileMonitorTimer_Tick;
            _fileMonitorTimer.Start();
        }

        // สร้างเมธอดสำหรับจัดการ Timer Tick (ทำงานแบบ async)
        private async void FileMonitorTimer_Tick(object sender, EventArgs e)
        {
            if (_isProcessingFile) return;
            if (_currentConfig == null || string.IsNullOrEmpty(_currentConfig.InputDirectory) || !Directory.Exists(_currentConfig.InputDirectory))
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

                    // ถ้าเป็นไฟล์ Error ให้ทำเฉพาะ log และแสดง error message เท่านั้น
                    if (fileName.Contains("ER"))
                    {
                        await Task.Run(() =>
                        {
                            // --- Create a new status object to send to the manager ---
                            var currentStatus = new CurrentInkjetStatus
                            {
                                Timestamp = DateTime.Now,
                                InkjetName = _currentConfig.InkjetName,
                                ErrorDetail = "Send_Data_Fail",
                                ErrorCode = "E100"
                            };

                            // ✅ Call the new manager to update this printer's status and rewrite the file.
                            LiveStatusManager.UpdateAndSaveStatus(currentStatus);
                        });

                        // แจ้ง UI ว่าเกิด Error (ต้องใช้ Invoke เพราะเราอยู่ใน async event)
                        this.Invoke((MethodInvoker)(() =>
                        {
                            lblError.Visible = true;
                            txtLaterPrintDetail.BorderColor = Color.Red;
                            lblError.Text = $"❌ ERROR [ {fileContent} ]";
                        }));

                        // ตั้งสถานะให้พร้อมรับไฟล์ใหม่ (ไม่ลบไฟล์ และไม่อัพเดตอื่น ๆ)
                        _isProcessingFile = false;

                        // ออกจากเมธอดไม่ทำงานต่อ
                        return;
                    }
                    txtLaterPrintDetail.BorderColor = Color.Black;
                    lblError.Visible = false;

                    // ถ้าไม่ใช่ไฟล์ Error ทำงานตามปกติ
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

        public void UpdateStatus(string newStatus)
        {
            if (_currentConfig != null)
            {
                // ADD THIS CHECK: If the new status is the same as the current one, do nothing.
                if (_currentConfig.Status == newStatus)
                {
                    return; // Exit the method early
                }

                // --- Part 1: Update the object and UI (Existing code) ---
                _currentConfig.Status = newStatus;
                lblStatusValue.Text = newStatus;
                SetStatusColor(newStatus);

                // --- Part 2: Add code to log this status change ---
                string logLevel;

                // Map the status to a LogLevel for the log file
                switch (newStatus)
                {
                    case "Warning":
                        logLevel = "WARN";
                        break;
                    case "Error":
                        logLevel = "ERROR";
                        break;
                    default: // "Printable", "Stop", "Suspended", etc.
                        logLevel = "INFO";
                        break;
                }

                string message = $"Status changed to '{newStatus}'";

                string loadedStatusCsvPath = AppSettings.LoadAppSettings();

                // --- Create a new status object to send to the manager ---
                var currentStatus = new CurrentInkjetStatus
                {
                    Timestamp = DateTime.Now,
                    InkjetName = _currentConfig.InkjetName,
                    Status = newStatus,
                    CurrentMessage = _currentConfig.LatestPrintDetail, // Use existing data
                                                                 // Populate error details if the status is "Error"
                    ErrorDetail = newStatus == "Error" ? "An error was detected" : "",
                    ErrorCode = newStatus == "Error" ? "E500" : ""
                };

                // ✅ Call the new manager to update this printer's status and rewrite the file.
                LiveStatusManager.UpdateAndSaveStatus(currentStatus);
            }
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


        private void InitializeLeftEdgePanel()
        {
            _leftEdgePanel = new Panel();
            _leftEdgePanel.Size = new Size(5, panelDetails.Height); // กว้าง 10 px
            _leftEdgePanel.Location = new Point(0, 0);
            _leftEdgePanel.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
            panelDetails.Controls.Add(_leftEdgePanel);
        }
        private void SetPanel3RoundedCorners(Panel panel, int radius)
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
        }

        private void btnSaveManual_Click(object sender, EventArgs e)
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
                    // 3. Prepare data for logging
                    var processTime = DateTime.Now;
                    string contentToLog = txtWaitingPrintDetail.Text;
                    string logFilePath = Path.Combine(_currentConfig.OutputDirectory, "processing_log.txt");

                    // 4. Create the log entry with the "Manual" type
                    string logEntry = $"{processTime:G},{_currentConfig.InkjetName},Manual,{contentToLog.Replace(Environment.NewLine, " ")}";

                    // ถ้าเป็นไฟล์ Error ให้ทำเฉพาะ log และแสดง error message เท่านั้น
                    if (contentToLog.Contains("ER"))
                    {        
                            StatusLogger.LogEvent(
                                _currentConfig.InkjetName,
                                "ERROR",
                                "Send_Data_Fail",
                                contentToLog
                            );
                            lblErrorManual.Visible = true;
                            txtWaitingPrintDetail.BorderColor = Color.Red;
                            lblErrorManual.Text = $"❌ ERROR [ {contentToLog} ]";

                        // ออกจากเมธอดไม่ทำงานต่อ
                        return;
                    }
                    lblErrorManual.Visible = false;
                    txtWaitingPrintDetail.BorderColor = Color.Black;

                    // 5. Write to the log file
                    File.AppendAllText(logFilePath, logEntry + Environment.NewLine);

                    StatusLogger.LogEvent(
                          _currentConfig.InkjetName,
                          "INFO",                 // LogLevel for a successful job
                          "Job_Processed_Manual",   // A clear EventType
                          contentToLog             // The message is the content of the file
                      );
                    MessageBox.Show("ส่งข้อมูล Manual สำเร็จ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _currentConfig.LatestPrintDetail = txtWaitingPrintDetail.Text;
                    ConfigManager.Edit(_currentConfig.InkjetName, _currentConfig);
                    txtWaitingPrintDetail.FillColor = Color.WhiteSmoke;
                    txtWaitingPrintDetail.ReadOnly = true;
                    btnEditManual.Visible = true;
                    btnClearManual.Visible = false;
                    btnSaveManual.Visible = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("เกิดข้อผิดพลาดในการบันทึก Log: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
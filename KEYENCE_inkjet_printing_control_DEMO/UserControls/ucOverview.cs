using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KEYENCE_inkjet_printing_control_DEMO.Class;

namespace KEYENCE_inkjet_printing_control_DEMO.UserControls
{
    public partial class ucOverview : UserControl
    {
        private Timer _statusTimer;

        public ucOverview()
        {
            InitializeComponent();
            _statusTimer = new Timer { Interval = 3000 };
            _statusTimer.Tick += StatusPollTimer_Tick;
        }

        private async void StatusPollTimer_Tick(object sender, EventArgs e)
        {
            // ✅ เปลี่ยนโค้ดเพื่อสร้าง Reconnection Loop
            var configs = ConfigManager.Load();

            // วนลูปผ่านเครื่องพิมพ์ทั้งหมด
            foreach (var config in configs)
            {
                string inkjetName = config.InkjetName;

                try
                {
                    // 1. ถ้าขาดการเชื่อมต่อ ให้พยายามเชื่อมต่อใหม่
                    if (!KeyenceConnectionManager.IsConnected(inkjetName))
                    {
                        Console.WriteLine($"[{inkjetName}] -  try auto connecting... ({config.IpAddress}:{config.Port})...");
                        await KeyenceConnectionManager.ConnectAsync(inkjetName, config.IpAddress, config.Port);
                        Console.WriteLine($"[{inkjetName}] - ✅ connect success!");
                    }

                    // 2. ถ้าเชื่อมต่อแล้ว (หรือเชื่อมต่อกลับไปสำเร็จ) ให้ Polling สถานะ
                    if (KeyenceConnectionManager.IsConnected(inkjetName))
                    {
                        // ❗ เรียกเมธอด Polling เครื่องพิมพ์เดียว
                        await KeyenceConnectionManager.PollSingleStatusAsync(inkjetName);
                    }
                }
                catch (Exception ex)
                {
                    // ดักจับ Exception ที่หลุดออกมาจากการเชื่อมต่อ/Polling (เช่น TimeoutException)
                    Console.WriteLine($"[{inkjetName}] - ❌ ERROR ใน Reconnection/Polling: {ex.Message}");
                    // สถานะจะถูกตั้งเป็น Disconnected โดย KeyenceConnectionManager อยู่แล้ว
                }
            }
        }

        public void GetListInkjet()
        {
            flowLayoutPanel1.Controls.Clear();
            var configs = ConfigManager.Load();

            // ✅ เริ่มต้นการทำงานของ Connection Manager ที่นี่
            KeyenceConnectionManager.Initialize(configs);

            foreach (var cfg in configs)
            {
                var item = new ucItem();
                item.SetData(cfg);
                item.ItemDeleted += OnItemChanged;
                item.ItemEdited += OnItemChanged;
                flowLayoutPanel1.Controls.Add(item);
            }

            if (configs.Count < 4)
            {
                var ucAdd = new ucAdd();
                ucAdd.ItemAdded += OnItemChanged; 
                flowLayoutPanel1.Controls.Add(ucAdd);
            }
            SetCardViewSize();

            // ✅ เริ่มการทำงานของ Timer หลังจากสร้าง item ทั้งหมดแล้ว
            _statusTimer.Start();
        }

        private void OnItemChanged(object sender, EventArgs e)
        {
            GetListInkjet();
        }

        private void ucOverview_Load(object sender, EventArgs e)
        {
            GetListInkjet();
        }

        public void SetCardViewSize()
        {
            // ปรับขนาด
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                control.Size = new Size(flowLayoutPanel1.Width - control.Margin.Horizontal,
                                        control.Height);
            }
        }
    }
}

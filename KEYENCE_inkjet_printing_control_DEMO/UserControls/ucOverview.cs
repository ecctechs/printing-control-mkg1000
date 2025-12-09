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
            // สั่งให้ Manager ไปถามสถานะเครื่องพิมพ์ทั้งหมด
            await KeyenceConnectionManager.PollAllStatusesAsync();
        }

        public void GetListInkjet()
        {
            // 🛑 1. Stop polling ก่อนเริ่ม refresh UI
            _statusTimer.Stop();


            // 🧹 2. Dispose controls เก่าทั้งหมดก่อน Clear
            // เพื่อให้ Dispose() ของ ucItem ถูกเรียก, Timer ถูก Dispose, Event ถูก Unsubscribe
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                ctrl.Dispose();
            }

            // 🧹 3. Clear UI
            flowLayoutPanel1.Controls.Clear();


            // 📝 4. โหลด config ล่าสุด
            var configs = ConfigManager.Load();


            // 🔌 5. Initialize Connection Manager
            KeyenceConnectionManager.Initialize(configs);


            // 📦 6. สร้างและ add ucItem ใหม่
            foreach (var cfg in configs)
            {
                var item = new ucItem();
                item.SetData(cfg);

                // subscribe event
                item.ItemDeleted += OnItemChanged;
                item.ItemEdited += OnItemChanged;

                flowLayoutPanel1.Controls.Add(item);
            }


            // ➕ 7. ถ้าจำนวนน้อยกว่า 4 แสดงปุ่ม Add
            if (configs.Count < 4)
            {
                var ucAdd = new ucAdd();
                ucAdd.ItemAdded += OnItemChanged;
                flowLayoutPanel1.Controls.Add(ucAdd);
            }


            // 🎨 8. ปรับขนาด UI
            SetCardViewSize();


            // ▶️ 9. Start polling อีกครั้งหลัง Refresh UI เสร็จ
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

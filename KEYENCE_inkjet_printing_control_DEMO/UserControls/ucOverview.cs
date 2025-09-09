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
        //private Random _random = new Random();
        //private List<string> _possibleStatuses = new List<string> { "Printable", "Warning", "Error", "Stop", "Suspended", "Disconnected" };

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

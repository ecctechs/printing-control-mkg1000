using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KEYENCE_inkjet_printing_control_DEMO.Class
{
    public class InkjetConfig
    {
        public string InkjetName { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string InputDirectory { get; set; }
        public string OutputDirectory { get; set; }
        public string Status { get; set; }
        public string QueueData { get; set; }
        public string WaitingPrintDetail { get; set; }
        public string CurrentData { get; set; }
        public string LatestPrintDetail { get; set; }

        public InkjetConfig()
        {
            Status = "Unknown";
            QueueData = string.Empty; // หรือ string.Empty ตามความเหมาะสม
            WaitingPrintDetail = string.Empty;
            CurrentData = string.Empty;
            LatestPrintDetail = string.Empty;
        }
    }

    public class ConfigManager
    {
        private static string configFile = "inkjet_config.json";
        private static readonly object _fileLock = new object();

        // โหลดข้อมูลจากไฟล์
        public static List<InkjetConfig> Load()
        {
            // ✅ เพิ่ม lock ที่นี่เพื่อป้องกันการอ่านไฟล์ขณะที่มีการเขียน
            lock (_fileLock)
            {
                if (!File.Exists(configFile))
                    return new List<InkjetConfig>();

                string json = File.ReadAllText(configFile);
                return JsonConvert.DeserializeObject<List<InkjetConfig>>(json) ?? new List<InkjetConfig>();
            }
        }

        // บันทึกข้อมูลลงไฟล์
        public static void Save(List<InkjetConfig> configs)
        {
            lock (_fileLock)
            {
                string json = JsonConvert.SerializeObject(configs, Formatting.Indented);
                File.WriteAllText(configFile, json);
            }
        }

        // เพิ่ม Config ใหม่
        public static void Add(InkjetConfig newConfig)
        {
            var configs = Load();
            configs.Add(newConfig);
            Save(configs);
        }

        // แก้ไข Config (ค้นหาโดย InkjetName)
        public static void Edit(string inkjetName, InkjetConfig updatedConfig)
        {
            var configs = Load();
            var index = configs.FindIndex(c => c.InkjetName == inkjetName);
            if (index >= 0)
            {
                configs[index] = updatedConfig;
                Save(configs);
            }
        }

        public static void Delete(string inkjetName)
        {
            var configs = Load();
            configs.RemoveAll(c => c.InkjetName == inkjetName);
            Save(configs);
        }
    }
}

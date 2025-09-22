using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEYENCE_inkjet_printing_control_DEMO.Class
{
    /// <summary>
    /// Class สำหรับเก็บข้อมูลสถานะปัจจุบันของเครื่องพิมพ์ Inkjet แต่ละเครื่อง
    /// </summary>
    public class CurrentInkjetStatus
    {
        public DateTime Timestamp { get; set; }
        public string InkjetName { get; set; }
        public string Status { get; set; }
        public string ErrorDetail { get; set; }
        public string ErrorCode { get; set; }
        public string CurrentMessage { get; set; }
    }

    /// <summary>
    /// Class แบบ static สำหรับจัดการและบันทึกสถานะล่าสุด (Live Status) ของเครื่องพิมพ์ทั้งหมดลงในไฟล์ CSV
    /// </summary>
    public static class LiveStatusManager
    {
        // Dictionary สำหรับเก็บสถานะล่าสุดของแต่ละเครื่องพิมพ์ โดยใช้ InkjetName เป็น key
        private static Dictionary<string, CurrentInkjetStatus> _liveStatuses = new Dictionary<string, CurrentInkjetStatus>();
        private static readonly object _fileLock = new object();
        private static string _statusFilePath;

        /// <summary>
        /// เริ่มต้นการทำงานของ Manager ด้วยข้อมูลการกำหนดค่าของเครื่องพิมพ์ ควรเรียกใช้เมื่อโปรแกรมเริ่มต้น
        /// </summary>
        public static void Initialize(List<InkjetConfig> configs)
        {
            // โหลด path สำหรับบันทึกไฟล์จาก AppSettings
            string loadedStatusCsvPath = AppSettings.LoadAppSettings();

            // ❗ ถ้า path ว่างเปล่า จะไม่ดำเนินการต่อ
            if (string.IsNullOrWhiteSpace(loadedStatusCsvPath))
                return;

            _statusFilePath = Path.Combine(loadedStatusCsvPath, "live_status.csv");

            // สร้าง Dictionary เริ่มต้นจาก config ที่ได้รับมา
            _liveStatuses = configs.ToDictionary(
                cfg => cfg.InkjetName,
                cfg => new CurrentInkjetStatus
                {
                    InkjetName = cfg.InkjetName,
                    Timestamp = DateTime.Now,
                    Status = cfg.Status ?? "Unknown",
                    CurrentMessage = "Initializing..."
                }
            );
            SaveStatusFile(); // บันทึกสถานะเริ่มต้นลงไฟล์
        }

        /// <summary>
        /// อัปเดตสถานะสำหรับเครื่องพิมพ์ที่ระบุ แล้วบันทึกข้อมูลทั้งหมดลงไฟล์ใหม่
        /// </summary>
        public static void UpdateAndSaveStatus(CurrentInkjetStatus newStatus)
        {
            lock (_fileLock)
            {
                if (_liveStatuses.ContainsKey(newStatus.InkjetName))
                {
                    _liveStatuses[newStatus.InkjetName] = newStatus;
                    SaveStatusFile();
                }
            }
        }

        /// <summary>
        /// เขียนทับไฟล์ live_status.csv ด้วยข้อมูลล่าสุดจากเครื่องพิมพ์ทั้งหมด
        /// </summary>
        private static void SaveStatusFile()
        {
            if (string.IsNullOrEmpty(_statusFilePath) || _liveStatuses.Count == 0)
            {
                return;
            }

            var csvBuilder = new StringBuilder();
            // เพิ่ม Header ของไฟล์ CSV
            csvBuilder.AppendLine("Timestamp,InkjetName,Status,ErrorDetail,ErrorCode,CurrentMessage");

            // เพิ่มข้อมูลของแต่ละเครื่องพิมพ์จาก Dictionary ในหน่วยความจำ
            foreach (var status in _liveStatuses.Values)
            {
                csvBuilder.AppendLine(
                    $"{status.Timestamp:yyyy-MM-dd HH:mm:ss}," +
                    $"{status.InkjetName}," +
                    $"{status.Status}," +
                    $"{status.ErrorDetail ?? "---"}," +
                    $"{status.ErrorCode ?? "---"}," +
                    $"\"{(string.IsNullOrEmpty(status.CurrentMessage) ? "---" : status.CurrentMessage.Replace("\"", "\"\""))}\""
                );
            }

            try
            {
                // เขียนทับไฟล์ด้วยเนื้อหาใหม่
                File.WriteAllText(_statusFilePath, csvBuilder.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write live status file: {ex.Message}");
            }
        }
    }
}
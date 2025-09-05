using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace KEYENCE_inkjet_printing_control_DEMO.Class
{
    public class StatusLogger
    {
        // ใช้สำหรับป้องกันการเขียนไฟล์พร้อมกันจากหลาย Thread
        private static readonly object _logLock = new object();

        public static void LogEvent(string inkjetName, string logLevel, string eventType, string message)
        {
            try
            {
                string loadedStatusCsvPath = AppSettings.LoadAppSettings();

                // ❗ ถ้า path ว่าง ไม่ทำอะไรเลย
                if (string.IsNullOrWhiteSpace(loadedStatusCsvPath))
                    return;

                Directory.CreateDirectory(loadedStatusCsvPath);

                string fileName = $"status_log_{DateTime.Now:yyyy-MM-dd}.csv";
                string fullPath = Path.Combine(loadedStatusCsvPath, fileName);
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string formattedMessage = $"\"{message.Replace("\"", "\"\"")}\"";
                string logEntry = $"{timestamp},{inkjetName},{logLevel},{eventType},{formattedMessage}";

                lock (_logLock)
                {
                    // ✅ เขียน header ถ้าไฟล์ยังไม่มี
                    if (!File.Exists(fullPath))
                    {
                        string header = "Timestamp,InkjetName,LogLevel,Event,Message";
                        using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                        using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                        {
                            sw.WriteLine(header);
                        }
                    }

                    // ✅ เขียน log โดยไม่ล็อกไฟล์ exclusive
                    using (FileStream fs = new FileStream(fullPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.WriteLine(logEntry);
                    }
                }
            }
            catch (Exception ex)
            {
                // ⚠️ กรณีเขียน log ไม่สำเร็จ (เช่น ไฟล์ถูกล็อกโดยแอปอื่น)
                Console.WriteLine($"Failed to write to log: {ex.Message}");
            }
        }


    }
}

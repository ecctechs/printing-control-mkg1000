using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace StatusMapping
{
    // คลาสนี้จะใช้เป็นโครงสร้างข้อมูลจาก JSON
    public class StatusData
    {
        public Dictionary<string, string> StatusCodes { get; set; }
        public Dictionary<string, string> ErrorCodes { get; set; }
        public Dictionary<string, string> WarningCodes { get; set; }
    }

    public class LoadMapping
    {
        private StatusData _statusData;

        // โหลดไฟล์ JSON เข้าสู่ memory
        public bool Load(string jsonPath)
        {
            if (!File.Exists(jsonPath))
                return false; // ไฟล์ไม่เจอ

            string jsonString = File.ReadAllText(jsonPath);
            _statusData = JsonConvert.DeserializeObject<StatusData>(jsonString);

            return _statusData != null;
        }

        // คืนข้อความสถานะตามรหัส (ค้นหาจากทุก section)
        public string GetStatus(string code)
        {
            if (_statusData == null)
                throw new InvalidOperationException("กรุณาเรียกใช้ Load() ก่อน");

            if (_statusData.StatusCodes != null && _statusData.StatusCodes.TryGetValue(code, out var status))
                return $"[STATUS] {status}";

            if (_statusData.ErrorCodes != null && _statusData.ErrorCodes.TryGetValue(code, out var error))
                return $"[ERROR] {error}";

            if (_statusData.WarningCodes != null && _statusData.WarningCodes.TryGetValue(code, out var warning))
                return $"[WARNING] {warning}";

            return $"Unknown code: {code}";
        }

        public static string MapCategory(string msg)
        {
            if (msg.StartsWith("[ERROR]"))
                return "Error";

            if (msg.StartsWith("[WARNING]"))
                return "Warning";

            if (msg.Contains("Stop"))
                return "Stop";

            if (msg.Contains("Suspended") || msg.Contains("Pause"))
                return "Suspended";

            if (msg.Contains("Disconnected") || msg.Contains("disconnection"))
                return "Disconnected";

            return "Printable";
        }

    }
}

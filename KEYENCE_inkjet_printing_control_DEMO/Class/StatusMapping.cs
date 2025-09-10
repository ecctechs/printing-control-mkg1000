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
        public string GetStatus(string code, string type)
        {
            if (_statusData == null)
                return "[UNKNOWN] Mapping not loaded";

            if (type == "EV")
            {
                if (_statusData.ErrorCodes != null && _statusData.ErrorCodes.ContainsKey(code))
                    return  _statusData.ErrorCodes[code];

                if (_statusData.WarningCodes != null && _statusData.WarningCodes.ContainsKey(code))
                    return _statusData.WarningCodes[code];
            }
            else if (type == "SB")
            {
                if (_statusData.StatusCodes != null && _statusData.StatusCodes.ContainsKey(code))
                    return  _statusData.StatusCodes[code];
            }

            return "[UNKNOWN] Code not found";
        }

    }
}

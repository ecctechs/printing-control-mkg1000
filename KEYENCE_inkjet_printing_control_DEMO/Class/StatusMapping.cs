using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace StatusMapping
{
    // ใช้เป็นโครงสร้างข้อมูลจาก status_mapping.json
    public class StatusData
    {
        public Dictionary<string, string> StatusCodes { get; set; }
        public Dictionary<string, string> ErrorCodes { get; set; }
        public Dictionary<string, string> WarningCodes { get; set; }
    }

    // ใช้เป็นโครงสร้างข้อมูลจาก communication_errors.json
    public class ErrorCollection
    {
        [JsonProperty("communication_errors")]
        public List<CommunicationError> CommunicationErrors { get; set; }
    }

    public class CommunicationError
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("solution")]
        public string Solution { get; set; }
    }

    public class LoadMapping
    {
        private StatusData _statusData;
        private ErrorCollection _errorCollection;

        private string jsonPathStatus = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "status_mapping.json");
        private string jsonPathError = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "communication_errors.json");

        // โหลดไฟล์ status_mapping.json
        public bool LoadStatus()
        {
            if (!File.Exists(jsonPathStatus))
                return false;

            string jsonString = File.ReadAllText(jsonPathStatus);
            _statusData = JsonConvert.DeserializeObject<StatusData>(jsonString);

            return _statusData != null;
        }

        // โหลดไฟล์ communication_errors.json
        public bool LoadCommunicationError()
        {
            if (!File.Exists(jsonPathError))
                return false;

            string jsonString = File.ReadAllText(jsonPathError);
            _errorCollection = JsonConvert.DeserializeObject<ErrorCollection>(jsonString);

            return _errorCollection != null;
        }

        // คืน CommunicationError ทั้งหมด

        public CommunicationError GetCommunicationErrorByCode(string code)
        {
            if (_errorCollection?.CommunicationErrors == null)
                return null;

            return _errorCollection.CommunicationErrors
                .FirstOrDefault(err => err.Code == code);
        }

        // คืนข้อความสถานะตามรหัส
        public string GetStatus(string code, string type)
        {
            if (_statusData == null)
                return "[UNKNOWN] Mapping not loaded";

            if (type == "EV")
            {
                if (_statusData.ErrorCodes != null && _statusData.ErrorCodes.ContainsKey(code))
                    return _statusData.ErrorCodes[code];

                if (_statusData.WarningCodes != null && _statusData.WarningCodes.ContainsKey(code))
                    return _statusData.WarningCodes[code];
            }
            else if (type == "SB")
            {
                if (_statusData.StatusCodes != null && _statusData.StatusCodes.ContainsKey(code))
                    return _statusData.StatusCodes[code];
            }

            return "---";
        }
    }
}
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

        private string jsonPathStatus = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "status_mapping.json");
        private string jsonPathError = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data" ,"communication_errors.json");

        // โหลดไฟล์ status_mapping.json
        public bool LoadStatus()
        {
            try // ✅ เพิ่ม try-catch
            {
                Console.WriteLine(jsonPathStatus);
                Console.WriteLine(jsonPathError);
                if (!File.Exists(jsonPathStatus))
                    return false;

                string jsonString = File.ReadAllText(jsonPathStatus);

                _statusData = JsonConvert.DeserializeObject<StatusData>(jsonString);

                return _statusData != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error function LoadStatus: {ex.Message}");
                return false; // คืนค่า false หากมีปัญหา
            }
        }

        // โหลดไฟล์ communication_errors.json
        public bool LoadCommunicationError()
        {
            try // ✅ เพิ่ม try-catch
            {
                if (!File.Exists(jsonPathError))
                    return false;

                string jsonString = File.ReadAllText(jsonPathError);
                _errorCollection = JsonConvert.DeserializeObject<ErrorCollection>(jsonString);

                return _errorCollection != null;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error LoadCommunicationError : {ex.Message}");
                return false; // คืนค่า false หากมีปัญหา
            }
        }

        // คืน CommunicationError ทั้งหมด

        public CommunicationError GetCommunicationErrorByCode(string code)
        {
            try // ✅ เพิ่ม try-catch
            {
                if (_errorCollection?.CommunicationErrors == null)
                    return null;

                return _errorCollection.CommunicationErrors
                    .FirstOrDefault(err => err.Code == code);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error LoadCommunicationError : {ex.Message}");
                return null;
            }

        }

        // คืนข้อความสถานะตามรหัส
        public string GetStatus(string code, string type)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetStatus : {ex.Message}");
                return null;
            }
        }
    }
}
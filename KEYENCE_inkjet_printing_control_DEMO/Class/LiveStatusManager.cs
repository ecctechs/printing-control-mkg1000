using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KEYENCE_inkjet_printing_control_DEMO.Class
{
    public static class LiveStatusManager
    {
        // A dictionary to hold the most current status of each printer, keyed by InkjetName.
        private static Dictionary<string, CurrentInkjetStatus> _liveStatuses = new Dictionary<string, CurrentInkjetStatus>();
        private static readonly object _fileLock = new object();
        private static string _statusFilePath;

        /// <summary>
        /// Initializes the manager with the configured printers. Call this at application startup.
        /// </summary>
        public static void Initialize(List<InkjetConfig> configs)
        {
            // Set the path from the central configuration.
            string loadedStatusCsvPath = AppSettings.LoadAppSettings();

            // ❗ ถ้า path ว่าง ไม่ทำอะไรเลย
            if (string.IsNullOrWhiteSpace(loadedStatusCsvPath))
                return;

            _statusFilePath = Path.Combine(loadedStatusCsvPath, "live_status.csv");

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
            SaveStatusFile(); // Save the initial state.
        }

        /// <summary>
        /// Updates the status for a specific printer and saves the entire file again.
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
        /// Overwrites the live_status.csv file with the latest data from all printers.
        /// </summary>
        private static void SaveStatusFile()
        {
            if (string.IsNullOrEmpty(_statusFilePath) || _liveStatuses.Count == 0)
            {
                return;
            }

            var csvBuilder = new StringBuilder();
            // Add Header
            csvBuilder.AppendLine("Timestamp,InkjetName,Status,ErrorDetail,ErrorCode,CurrentMessage");

            // Add a row for each printer from our in-memory dictionary.
            foreach (var status in _liveStatuses.Values)
            {
                csvBuilder.AppendLine(
                    $"{status.Timestamp:yyyy-MM-dd HH:mm:ss}," +
                    $"{status.InkjetName}," +
                    $"{status.Status}," +
                    $"{status.ErrorDetail ?? ""}," +
                    $"{status.ErrorCode ?? ""}," +
                    $"\"{status.CurrentMessage?.Replace("\"", "\"\"") ?? ""}\""
                );
            }

            try
            {
                // Overwrite the file with the new content.
                File.WriteAllText(_statusFilePath, csvBuilder.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write live status file: {ex.Message}");
            }
        }
    }
}
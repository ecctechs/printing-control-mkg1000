using KEYENCE_inkjet_printing_control_DEMO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class KeyenceConnectionManager
{
    private static Dictionary<string, KeyencePrinterConnector> _printers = new Dictionary<string, KeyencePrinterConnector>();
    public static event Action<string, List<string>, string> OnStatusReceived;

    public class SendResult
    {
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
    }

    public static void Initialize(List<InkjetConfig> configs)
    {
        var newPrinterNames = configs.Select(c => c.InkjetName).ToList();

        var printersToRemove = _printers.Keys.Where(name => !newPrinterNames.Contains(name)).ToList();
        foreach (var name in printersToRemove)
        {
            _printers[name].Disconnect();
            _printers.Remove(name);
        }

        foreach (var config in configs)
        {
            if (!_printers.ContainsKey(config.InkjetName))
            {
                _printers.Add(config.InkjetName, new KeyencePrinterConnector());
            }
        }
    }

    /// <summary>
    /// ส่งข้อความ (Print Data) ไปยังเครื่องพิมพ์ Inkjet ที่ระบุ
    /// </summary>
    /// <param name="inkjetName">ชื่อของเครื่องพิมพ์ Inkjet เป้าหมาย</param>
    /// <param name="message">ข้อความที่ต้องการส่งไปพิมพ์</param>
    /// <returns>คืนค่า true หากส่งสำเร็จ, มิฉะนั้นคืนค่า false</returns>
    public static async Task<SendResult> SendMessageAsync(string inkjetName, string message)
    {
        var result = new SendResult();

        // ตรวจสอบว่ามีเครื่องพิมพ์ชื่อนี้ใน Manager หรือไม่
        if (!_printers.TryGetValue(inkjetName, out var connector))
        {
            result.Success = false;
            result.ErrorCode = $"No printer found with name '{inkjetName}'";
            Console.WriteLine(result.ErrorCode);
            return result;
        }

        // โหลด Config ล่าสุดสำหรับเครื่องพิมพ์นี้
        var config = ConfigManager.Load().FirstOrDefault(c => c.InkjetName == inkjetName);
        if (config == null)
        {
            result.Success = false;
            result.ErrorCode = $"No config found for printer '{inkjetName}'";
            Console.WriteLine(result.ErrorCode);
            return result;
        }

        try
        {
            // เชื่อมต่อถ้ายังไม่เชื่อม
            if (!connector.IsConnected)
            {
                await connector.ConnectAsync(config.IpAddress, config.Port);
            }

            string command = $"BK,1,0,{message}";
            string response = await connector.SendCommandAsync(command);

            if (response.StartsWith("ER"))
            {
                result.Success = false;
                result.ErrorCode = response;
                Console.WriteLine($"Error Send Text {inkjetName}: {response}");
            }
            else
            {
                result.Success = true;
                result.ErrorCode = null;
                Console.WriteLine($"Send Text : '{message}' To {inkjetName} Success, Response: {response}");
            }

            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.ErrorCode = ex.Message;
            Console.WriteLine($"Exception sending message to {inkjetName}: {ex.Message}");
            connector?.Disconnect();
            return result;
        }
    }


    public static async Task PollAllStatusesAsync()
    {
        var latestConfigs = ConfigManager.Load();
        var printersToPoll = new Dictionary<string, KeyencePrinterConnector>(_printers);
        string type = "System";
        foreach (var pair in printersToPoll)
        {
            string inkjetName = pair.Key;
            var connector = pair.Value;
            var config = latestConfigs.FirstOrDefault(c => c.InkjetName == inkjetName);
   
            if (config == null) continue;

            List<string> finalStatusCodes = new List<string> { "Unknown" };

            try
            {
                if (connector != null)
                {
                    if (!connector.IsConnected)
                    {
                        await connector.ConnectAsync(config.IpAddress, config.Port);
                    }

                    string errorResponse = await connector.SendCommandAsync("EV");
                    var parts = errorResponse.Split(',');

                    // ✅ CORRECTED CONDITION: Check for 2 or more parts (e.g., "EV", "015")
                    if (parts.Length >= 2 && parts[0] == "EV")
                    {
                        type = "EV";
                        finalStatusCodes = new List<string>();

                        // ✅ CORRECTED LOOP: Start from index 1 to get the codes
                        for (int i = 1; i < parts.Length; i++)
                        {
                            string rawCode = parts[i].Trim();
                            // ✅ Remove leading zeros by converting to an integer and back
                            if (int.TryParse(rawCode, out int numericCode))
                            {
                                finalStatusCodes.Add(numericCode.ToString()); // "015" becomes "15"
                            }
                            else
                            {
                                finalStatusCodes.Add(rawCode); // Add as-is if not a number
                            }
                        }
                    }
                    else
                    {
                        type = "SB";
                        string statusResponse = await connector.SendCommandAsync("SB");
                        string statusCode = statusResponse.Split(',').LastOrDefault()?.Trim() ?? "Unknown";
                        finalStatusCodes = new List<string> { statusCode };
                    }
                }
            }
            catch (Exception)
            {
                connector?.Disconnect();
                finalStatusCodes = new List<string> { "Disconnected" };
            }
            finally
            {
                OnStatusReceived?.Invoke(inkjetName, finalStatusCodes, type);
            }
        }
    }
}
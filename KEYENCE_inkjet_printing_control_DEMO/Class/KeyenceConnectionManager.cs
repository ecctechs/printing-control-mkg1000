using KEYENCE_inkjet_printing_control_DEMO.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class KeyenceConnectionManager
{
    private static Dictionary<string, KeyencePrinterConnector> _printers = new Dictionary<string, KeyencePrinterConnector>();
    public static event Action<string, string> OnStatusReceived;

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

    public static async Task PollAllStatusesAsync()
    {
        // 1. โหลดการตั้งค่าล่าสุดจากไฟล์
        var latestConfigs = ConfigManager.Load();

        // 2. ตรวจสอบว่ามีการเปลี่ยนแปลง Config หรือไม่ (เพิ่ม/ลบเครื่องพิมพ์)
        var currentPrinterKeys = new HashSet<string>(_printers.Keys);
        var latestPrinterNames = new HashSet<string>(latestConfigs.Select(c => c.InkjetName));

        // 3. ถ้ามีการเปลี่ยนแปลง ให้ทำการ Initialize ใหม่
        if (!currentPrinterKeys.SetEquals(latestPrinterNames))
        {
            Initialize(latestConfigs);
        }

        // ✅ 4. สร้างสำเนา (Copy) ของ Dictionary ขึ้นมาเพื่อป้องกันปัญหา Race Condition
        var printersToPoll = new Dictionary<string, KeyencePrinterConnector>(_printers);

        // 5. Loop โดยใช้ "สำเนา" ที่ปลอดภัยแทน
        foreach (var pair in printersToPoll)
        {
            string inkjetName = pair.Key;
            var connector = pair.Value;
            var config = latestConfigs.FirstOrDefault(c => c.InkjetName == inkjetName);

            if (config == null) continue;

            try
            {
                if (connector != null)
                {
                    if (!connector.IsConnected)
                    {
                        Console.WriteLine("___>>" + config.IpAddress);
                        await connector.ConnectAsync(config.IpAddress, config.Port);
                    }
                }

                string response = await connector.SendCommandAsync("SB");
                OnStatusReceived?.Invoke(inkjetName, response);
            }
            catch (Exception)
            {
                connector.Disconnect();
                OnStatusReceived?.Invoke(inkjetName, "SB,Disconnected");
            }
        }
    }
}
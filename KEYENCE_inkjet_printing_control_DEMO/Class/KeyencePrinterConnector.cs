using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// จัดการการเชื่อมต่อและการสื่อสารระดับล่าง (Low-Level) ผ่าน TCP/IP
/// กับเครื่องพิมพ์ Keyence MK-G Series หนึ่งเครื่อง
/// </summary>
public class KeyencePrinterConnector
{
    // #region เป็นการจัดกลุ่มโค้ดเพื่อให้อ่านง่ายใน Visual Studio
    #region Fields and Properties (ตัวแปรและคุณสมบัติ)

    private TcpClient _tcpClient;
    private NetworkStream _stream;

    /// <summary>
    /// ตรวจสอบว่า Client เชื่อมต่อกับเครื่องพิมพ์อยู่หรือไม่
    /// </summary>
    public bool IsConnected => _tcpClient?.Connected ?? false;

    #endregion

    #region Public Methods (เมธอดสาธารณะ)

    /// <summary>
    /// เชื่อมต่อกับเครื่องพิมพ์ Keyence ตาม IP Address และ Port ที่ระบุ
    /// พร้อมกลไก Timeout (จำกัดเวลา) เพื่อป้องกันโปรแกรมค้าง
    /// </summary>
    /// <param name="ipAddress">IP Address ของเครื่องพิมพ์</param>
    /// <param name="port">Port Number (ค่าเริ่มต้นของรุ่น MK-G คือ 9004)</param>
    /// <param name="timeoutMilliseconds">เวลาสูงสุดที่รอการเชื่อมต่อ (หน่วย: มิลลิวินาที)</param>
    public async Task ConnectAsync(string ipAddress, int port = 9004, int timeoutMilliseconds = 3000)
    {
        // ถ้ามีการเชื่อมต่อเก่าค้างอยู่ ให้ตัดการเชื่อมต่อก่อน
        if (IsConnected)
        {
            Disconnect();
        }

        _tcpClient = new TcpClient();

        // --- ตรรกะการเชื่อมต่อพร้อม Timeout ที่เสถียรขึ้น ---
        // 1. สร้าง Task สำหรับการพยายามเชื่อมต่อ
        Task connectTask = _tcpClient.ConnectAsync(ipAddress, port);
        // 2. สร้าง Task สำหรับการหน่วงเวลาเพื่อทำเป็น Timeout
        Task delayTask = Task.Delay(timeoutMilliseconds);
        // 3. รอให้ Task ตัวใดตัวหนึ่งทำงานเสร็จก่อน (เหมือนการจับเวลาแข่งกัน)
        Task completedTask = await Task.WhenAny(connectTask, delayTask);

        // 4. ตรวจสอบผลลัพธ์:
        //    - ถ้า Task ที่เสร็จก่อนคือ delayTask (หมดเวลา)
        //    - หรือสถานะยังไม่ใช่ Connected (กรณี IP ผิด)
        //    ให้ถือว่าการเชื่อมต่อล้มเหลว
        if (completedTask == delayTask || _tcpClient == null || !_tcpClient.Connected)
        {
            _tcpClient?.Close(); // ป้องกัน null
            _tcpClient = null;
            throw new TimeoutException($"การเชื่อมต่อไปยัง {ipAddress}:{port} หมดเวลาหลังจาก {timeoutMilliseconds}ms.");
        }

        // --- สิ้นสุดตรรกะ Timeout ---

        // ถ้าเชื่อมต่อสำเร็จ ให้เปิด Stream สำหรับรับส่งข้อมูล
        _stream = _tcpClient.GetStream();
        _stream.ReadTimeout = 3000;  // ตั้งค่า Timeout สำหรับการอ่านข้อมูล
        _stream.WriteTimeout = 3000; // ตั้งค่า Timeout สำหรับการส่งข้อมูล
    }

    /// <summary>
    /// ตัดการเชื่อมต่อจากเครื่องพิมพ์ และเคลียร์ทรัพยากร
    /// </summary>
    public void Disconnect()
    {
        // ใช้เครื่องหมาย ?. เพื่อป้องกัน Error หาก object เป็น null
        try
        {
            _stream?.Close();
            _tcpClient?.Close();
        }
        finally
        {
            _stream = null;
            _tcpClient = null;
        }
    }

    /// <summary>
    /// ส่งคำสั่งไปยังเครื่องพิมพ์ และรอรับข้อมูลตอบกลับ
    /// </summary>
    /// <param name="command">คำสั่งที่ต้องการส่ง (เช่น "SB" สำหรับขอสถานะ)</param>
    /// <returns>ข้อมูลที่เครื่องพิมพ์ตอบกลับมาในรูปแบบ string</returns>
    public async Task<string> SendCommandAsync(string command)
    {
        // ตรวจสอบก่อนว่าเชื่อมต่ออยู่หรือไม่
        if (!IsConnected)
        {
            throw new InvalidOperationException("ยังไม่ได้เชื่อมต่อกับเครื่องพิมพ์");
        }

        // ตามคู่มือ คำสั่งต้องลงท้ายด้วย CR (Carriage Return)
        string fullCommand = command + "\r";
        byte[] dataToSend = Encoding.ASCII.GetBytes(fullCommand);

        // ส่งข้อมูลคำสั่ง
        await _stream.WriteAsync(dataToSend, 0, dataToSend.Length);

        // รอรับข้อมูลตอบกลับจากเครื่องพิมพ์
        byte[] buffer = new byte[2048]; // สร้าง Buffer ขนาดใหญ่พอสำหรับรับข้อมูล
        int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
        string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

        // ทำความสะอาดข้อมูลตอบกลับ โดยตัดอักขระพิเศษ (เช่น CR) ที่อาจติดมาตอนท้ายออก
        return response.Trim();
    }

    #endregion
}
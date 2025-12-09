using System;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace KEYENCE_inkjet_printing_control_DEMO
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // 1. เปิดใช้งาน visual styles และ compatibility settings
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 2. **✅ ตั้งค่า Global Exception Handlers (จุดสำคัญ)**
            // ดักจับ Exception บน UI Thread
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            // ดักจับ Unhandled Exception บน Thread อื่นๆ ทั้งหมด (เช่น Task.Run)
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            // 3. รันโปรแกรมหลัก
            Application.Run(new frmMain()); // เปลี่ยน frmMain เป็นชื่อ Form หลักของคุณ
        }

        // ----------------------------------------------------------------------
        // Global Exception Handlers Implementation
        // ----------------------------------------------------------------------

        /// <summary>
        /// ดักจับ Exception ที่ไม่ได้จัดการบน UI Thread (ThreadException)
        /// </summary>
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs t)
        {
            LogAndShowError(t.Exception, "Application UI Thread Exception");
            // เมื่อเกิด Exception บน UI Thread ต้องสั่งให้โปรแกรมปิดด้วย
            Application.Exit();
        }

        /// <summary>
        /// ดักจับ Exception ที่ไม่ได้จัดการบน Thread พื้นหลัง (UnhandledException)
        /// </summary>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // UnhandledExceptionEventArgs.ExceptionObject มักจะเป็น Exception
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                LogAndShowError(ex, "Unhandled AppDomain Background Thread Exception");
            }
            // หาก e.IsTerminating เป็น true โปรแกรมจะปิดตัวลงเอง ไม่ต้องเรียก Application.Exit() ซ้ำ
        }

        /// <summary>
        /// บันทึก Log ข้อผิดพลาดและแสดง MessageBox แจ้งเตือนผู้ใช้
        /// </summary>
        private static void LogAndShowError(Exception ex, string source)
        {
            // ใช้ try-catch ครอบการบันทึก Log เพื่อป้องกัน Error ซ้ำซ้อน
            try
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CrashLog");
                string logFile = Path.Combine(logPath, $"CrashLog_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }

                string errorMessage = $"*** {source} ***\n" +
                                      $"Timestamp: {DateTime.Now:G}\n" +
                                      $"Message: {ex.Message}\n" +
                                      $"Source: {ex.Source}\n" +
                                      $"StackTrace:\n{ex.StackTrace}\n\n";

                // 💾 บันทึกข้อผิดพลาดลงไฟล์
                File.AppendAllText(logFile, errorMessage);

                // ⚠️ แสดงข้อความให้ผู้ใช้เห็น (ควรพยายามให้รันบน UI Thread เพื่อความเสถียร)
                MessageBox.Show($"พบข้อผิดพลาดร้ายแรง: {ex.Message}\nโปรแกรมอาจต้องปิดตัวลง\nรายละเอียดบันทึกใน: {logFile}",
                                "Critical Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            catch (Exception secondaryEx)
            {
                // หากเกิดข้อผิดพลาดในการบันทึก Log ให้แสดงข้อความแจ้งเตือนแทน
                MessageBox.Show($"เกิดข้อผิดพลาดในการบันทึก Log: {secondaryEx.Message}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}

# 🚀 โปรแกรมจัดการเครื่องพิมพ์ Inkjet Keyence

![GitHub repo size](https://img.shields.io/github/repo-size/username/repo-name)
![GitHub contributors](https://img.shields.io/github/contributors/username/repo-name)
![GitHub stars](https://img.shields.io/github/stars/username/repo-name?style=social)
![GitHub forks](https://img.shields.io/github/forks/username/repo-name?style=social)

## 📖 คำอธิบาย
โปรเจคต์นี้เป็นระบบสำหรับ **ควบคุมเครื่องพิมพ์ Inkjet Keyence MK-G1000 Series**  
ช่วยให้ผู้ใช้งานสามารถ **ส่งข้อความ, บันทึกประวัติการพิมพ์ และตรวจสอบสถานะล่าสุดของเครื่อง Inkjet** ได้อย่างสะดวก  
รองรับการเชื่อมต่อ **สูงสุด 4 เครื่องพร้อมกัน** ผ่าน IP Address และสามารถควบคุมผ่าน **คอมพิวเตอร์หรือโน๊ตบุ้คทุกรุ่น**

---

## ✨ ฟีเจอร์หลัก

- 🖥️ **ควบคุม Inkjet ผ่านคอมพิวเตอร์/โน๊ตบุ้ค**  
- ✉️ **ส่งข้อความไปยัง Inkjet** ได้อัตโนมัติ  
- 📋 **บันทึกประวัติการพิมพ์** ทั้งข้อความและเวลาที่พิมพ์  
- ⏱️ **แสดงสถานะล่าสุดของ Inkjet** แบบเรียลไทม์  
- 🌐 **เชื่อมต่อพร้อมกันสูงสุด 4 เครื่อง** ผ่าน IP Address  
- ⚡ **จัดการข้อความและลำดับการพิมพ์** อย่างสะดวก 

---

## 📸 ตัวอย่างหน้าจอ (Screenshots)

ภาพรวมการทำงานของระบบ:

![หน้าจอ Dashboard](./images/dashboard.png)  
*รูปที่ 1: หน้า Dashboard แสดงข้อมูล Inkjet ทั้งหมด*

![หน้าจอฟอร์มเพิ่มสินค้า](./images/add_inkjet.png)  
*รูปที่ 2: ฟอร์มเพิ่ม Inkjet ใหม่*

---

## ⚙️ วิธีการติดตั้ง

1. Clone โปรเจกต์นี้ลงเครื่องของคุณ:
   ```bash 
   git clone https://github.com/ecctechs/KEYENCE_inkjet_printing_control_DEMO.git
   cd KEYENCE_inkjet_printing_control_DEMO


# 🖥️ วิธีใช้งานโปรแกรม Inkjet

1. **เชื่อมต่อสาย LAN**  
   - เชื่อมสาย LAN จากคอมพิวเตอร์หรือโน้ตบุ๊กของคุณเข้ากับเครื่อง Inkjet  
   - กรณีต้องการควบคุมหลายเครื่องพร้อมกัน ให้เชื่อมเครื่อง Inkjet แต่ละเครื่องเข้ากับ **Hub/Switch**

2. **ทดสอบการเชื่อมต่อ**  
   - ไปที่ CMD และใช้คำสั่ง PING หา Inkjet แต่ละเครื่อง  
   - ถ้า PING เจอ แสดงว่าเชื่อมต่อสำเร็จ

3. **เปิดโปรแกรม Inkjet**

4. **ตั้งค่าไฟล์สถานะแบบ Real Time**  
   - คลิกปุ่ม **Browse Output Status Path**  
   - เลือกตำแหน่งไฟล์ `live_status.csv` เพื่อดูสถานะเครื่องพิมพ์แบบเรียลไทม์

5. **เพิ่มเครื่อง Inkjet**  
   - กดสร้าง INKJET และกรอกข้อมูลดังนี้ให้ครบถ้วน:  
     - Inkjet Name  
     - IPAddress  
     - Port  
     - Input Directory  
     - Output Directory  
   - **หมายเหตุ:** Status Inkjet ไม่ควรเป็น Disconnect ก่อนทำขั้นตอนต่อไป

6. **ส่งไฟล์ข้อความสำหรับพิมพ์**  
   - ลากไฟล์ `.txt` เข้าไปในโฟลเดอร์ Input Directory ที่กำหนดไว้ในข้อ 5  
   - ไฟล์ `.txt` ต้องมีข้อความข้างใน เช่น `BATCH-ABC`

7. **ตรวจสอบคิวงานพิมพ์**  
   - หลังลากไฟล์ ชื่อไฟล์ `.txt` จะถูกปรากฎในช่อง **Queue Data**  
   - ข้อความที่กำหนดในไฟล์จะปรากฎในช่อง **Waiting Print Detail**

8. **ระบบการพิมพ์อัตโนมัติ**  
   - โปรแกรมจะวนลูปตรวจสอบทุก ๆ 10 วินาที  
   - กรณีที่เครื่องอยู่ในสถานะพร้อมพิมพ์ (ไม่มี error ใด ๆ):  
     - ข้อมูลจะถูกย้ายจาก **Queue Data → Current Data**  
     - ข้อความจะถูกย้ายจาก **Waiting Print Detail → Lastest Print Detail**  
     - ไฟล์ใน Input Directory จะถูกลบทันที

9. **ตรวจสอบประวัติข้อความ**  
   - สามารถดูประวัติข้อความที่ส่งได้ที่ **Output Directory** ที่กำหนด


## ⚙️ โหมดการทำงาน (Auto / Manual)

1. โหมด Auto
- โปรแกรมจะ ตรวจสอบ Queue และสถานะเครื่อง Inkjet โดยอัตโนมัติ ทุก ๆ 10 วินาที  
- เมื่อเครื่องพร้อมพิมพ์ (ไม่มี Error) ข้อมูลจะถูกส่งไปที่ Inkjet โดยอัตโนมัติ
- ไฟล์ใน Input Directory จะถูกลบทันทีหลังส่งสำเร็จ และบันทึกในประวัติการพิมพ์ว่า Auto
- เหมาะสำหรับการส่งไฟล์หลาย ๆ ชุดต่อเนื่องโดยไม่ต้องควบคุมเอง

2. โหมด Manual
- ผู้ใช้สามารถกดคลิกที่ปุ่ม ดินสอ เพื่อพิมพ์ข้อความลงในช่อง Latest Print Detail และส่งไปที่ Inkjet ด้วยตนเอง  
- เมื่อเครื่องพร้อมพิมพ์ (ไม่มี Error) ข้อมูลจะถูกส่งไปที่ Inkjet  
- ไฟล์ใน Input Directory จะถูกลบทันทีหลังส่งสำเร็จ และบันทึกในประวัติการพิมพ์ว่า Manual 
- เหมาะสำหรับการส่งข้อความเดียว เพื่อส่งให้ Inkjet ทันที

## 🖥️ เครื่องมือที่ใช้ในการพัฒนา

1. ระบบปฏิบัติการ Windows 10
2. Microsoft Visual Studio 2022
 - Guna.UI2.WinForms 2.0.4.7
 - Newtonsoft.Json 13.0.3
 - .NET Framework 4.7.2

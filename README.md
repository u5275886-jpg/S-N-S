# COLLEGE ID MANAGEMENT SYSTEM

## Overview / overview
A complete, production-ready application for **Shyam Nandan Sahay College, Muzaffarpur, Bihar** built with **C# .NET 8**, **WPF**, **Entity Framework Core (SQLite)**, **QuestPDF**, **ClosedXML**, and **QRCoder**.

---

## 🌐 APK Download Website & VPS Hosting Guide / वेबसाइट से ऐप (APK) डाउनलोड और VPS पर होस्टिंग की पूरी जानकारी

A fast, responsive, and ready-to-use landing website is included in the `website/` directory. It allows users to view app details and download the Android APK directly when hosted on any Linux VPS (Ubuntu/Debian) or server.

### 🌟 Website Highlights:
- **Responsive Landing Page** (`website/index.html`) using official App Logo and institutional colors (Maroon `#751A38`, Gold `#B89135`).
- **Direct APK Download Link**: Configured to serve files automatically from `website/downloads/CollegeIdManagement.apk`.
- **Ready Node.js / Express Server** (`website/server.js`) with proper Android APK MIME headers (`application/vnd.android.package-archive`).
- **Nginx Configuration** (`website/nginx.conf`) provided for standalone Nginx reverse proxy / static hosting.

---

### 🚀 How to Host on VPS (Step-by-Step / VPS पर कैसे सेट करें):

#### Step 1: Upload Project Files to VPS
Connect to your VPS via SSH and upload or clone the codebase:
```bash
# Connect to VPS
ssh root@YOUR_VPS_IP

# Clone or move code to web root
mkdir -p /var/www/college-id
# Copy project files into /var/www/college-id
```

#### Step 2: Put APK File in Downloads Folder
Place your generated APK file inside `website/downloads/`:
```bash
cd /var/www/college-id/website
mkdir -p downloads
# Copy your build APK file here
cp /path/to/CollegeIdManagement.apk downloads/CollegeIdManagement.apk
```

#### Step 3: Run Website on VPS (Option A or Option B)

##### 🟢 Option A: Run using Node.js & PM2 (Quickest & Easiest)
1. Install Node.js & PM2 on VPS:
   ```bash
   curl -fsSL https://deb.nodesource.com/setup_18.x | sudo -E bash -
   sudo apt-get install -y nodejs
   sudo npm install -g pm2
   ```
2. Install dependencies & start server:
   ```bash
   cd /var/www/college-id/website
   npm install
   pm2 start server.js --name "college-id-web"
   pm2 save
   pm2 startup
   ```
3. Website is now live at `http://YOUR_VPS_IP:3000` !

---

##### 🔵 Option B: Run using Nginx (Recommended for Production / Domain)
1. Install Nginx:
   ```bash
   sudo apt update
   sudo apt install nginx -y
   ```
2. Copy configuration:
   ```bash
   sudo cp /var/www/college-id/website/nginx.conf /etc/nginx/sites-available/college-id
   sudo ln -s /etc/nginx/sites-available/college-id /etc/nginx/sites-enabled/
   sudo rm /etc/nginx/sites-enabled/default
   ```
3. Update server name in `/etc/nginx/sites-available/college-id` with your domain or VPS IP, then restart Nginx:
   ```bash
   sudo nginx -t
   sudo systemctl restart nginx
   ```
4. Now visiting `http://YOUR_VPS_IP` or `http://your-domain.com` will directly open the landing page and allow downloading `CollegeIdManagement.apk`!

---

## 📱 Android APK Generation Guide / Android APK कैसे बनाएं (Complete Guide)

### 📌 Technical Overview (तकनीकी जानकारी)
Current application **WPF (Windows Presentation Foundation)** standard per built hai, jo **Windows Desktop OS** (`.exe`) ke liye native hai. Android devices par Windows `.exe` directly install/run nahi hota.

Yadi aap is project ka **Android APK (`.apk`)** banana chahte hain, toh neeche दिए गए methods aur steps ko follow karein:

---

### 🚀 Method 1: .NET MAUI ke dwara APK banana (Recommended / सुझाई गई विधि)

Since current app **C# .NET 8** me likhi gayi hai, aap **.NET MAUI (Multi-platform App UI)** ka upayog karke apne **80%+ C# code (Database, ViewModels, Business Logic, Services)** ko direct reuse kar sakte hain.

#### Prerequisites (ज़रूरी चीज़ें):
1. **.NET 8 SDK** installed
2. **Android SDK** & **JDK 17+** (Visual Studio Installer se "Mobile development with .NET" workload select karein)
3. MAUI Workload install karne ke liye terminal me run karein:
   ```bash
   dotnet workload install maui
   dotnet workload install maui-android
   ```

#### Steps to Create APK:
1. **Architecture Migration**:
   - `CollegeIdManagement.Core` / `Models` / `ViewModels` / `Data (EF Core SQLite)` ko class library me separation karein.
   - WPF XAML Views ko **.NET MAUI ContentPage** (XAML) me translate karein.

2. **Build Release APK**:
   Project directory me terminal kholkar nimn command chalayein:
   ```bash
   # Build Signed / Unsigned APK
   dotnet publish -f net8.0-android -c Release -p:AndroidPackageFormat=apk
   ```
   *Output file directory:* `bin/Release/net8.0-android/publish/*.apk`

3. **APK Signing (Keystore se digital sign karna):**
   ```bash
   # Create Keystore (if not created already)
   keytool -genkey -v -keystore my-release-key.keystore -alias my-alias -keyalg RSA -keysize 2048 -validity 10000

   # Build & Sign directly with dotnet CLI
   dotnet publish -f net8.0-android -c Release -p:AndroidPackageFormat=apk -p:AndroidKeyStore=true -p:AndroidSigningKeyStore=my-release-key.keystore -p:AndroidSigningKeyAlias=my-alias -p:AndroidSigningKeyPass=YOUR_KEY_PASSWORD -p:AndroidSigningStorePass=YOUR_STORE_PASSWORD
   ```

---

### 🌐 Method 2: Avalonia UI (Cross-Platform Framework)

Aap **Avalonia UI** (jo WPF XAML ke jaisa hi syntax follow karta hai) ka upayog karke single codebase se **Windows (`.exe`)** aur **Android (`.apk`)** dono build kar sakte hain.

#### Steps:
1. Avalonia Android project template create karein:
   ```bash
   dotnet new avalonia.xplat -o CollegeIdManagement.Mobile
   ```
2. Existing EF Core SQLite models aur Services ko sync karein.
3. Android project build karke APK nikalein:
   ```bash
   dotnet publish -f net8.0-android -c Release -p:AndroidPackageFormat=apk
   ```

---

### 🔍 Method 3: Mobile Companion App (QR Code Scanner & Verification)

Yadi aap Mobile me sirf **Student Verification & QR Code Scan** karna chahte hain:
1. ASP.NET Core Web API / Local Network Server set up karein.
2. Lightweight MAUI / Flutter / React Native app banayein jo Android Camera se QR Code read karke student details fetch kare.

---

## 💻 Windows Desktop Build & Run Guide (Windows (.exe) कैसे बनाएं)

### Prerequisites:
- Windows 10 / 11
- .NET 8.0 SDK or higher
- Visual Studio 2022+ with WPF Workload

### Build & Run Commands:
```bash
# Dependencies restore karein
dotnet restore

# Release Build karein
dotnet build -c Release

# Run application
dotnet run --project src/CollegeIdManagement/CollegeIdManagement.csproj

# Run Unit Tests
dotnet test
```

### Creating Windows Setup (.exe Installer):
1. Install **Inno Setup Compiler**.
2. Open `installer/CollegeIdManagement.iss` in Inno Setup.
3. Click **Compile** to generate `College_ID_Management_Setup.exe`.

---

## 📁 Data Directory Location
All databases, uploaded photos, signatures, and exported files are stored at:
`%LOCALAPPDATA%\CollegeIdManagement\`

---

## 🔑 Demo Login Credentials
- **Username:** `admin`
- **Password:** `admin123`

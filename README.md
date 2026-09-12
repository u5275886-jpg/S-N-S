# COLLEGE ID MANAGEMENT SYSTEM
### Shyam Nandan Sahay College, Muzaffarpur, Bihar

---

## 📌 Complete Repository Guide / सम्पूर्ण गाइड (Repository, Clone, Builds, Updates & Hosting)

यह रिपॉजिटरी **Shyam Nandan Sahay College** के **College ID Management System** का ऑफिशियल सोर्स कोड है। इस गाइड में गिटहब से कोड क्लोन करने, ऐप/APK बनाने, यूज़र्स को अपडेट्स भेजने और वेबसाइट VPS पर होस्ट करने की पूरी प्रक्रिया विस्तार से दी गई है।

---

## 📥 1. Repository Clone Kaise Karein? (How & Where to Clone)

### 🔗 Repository Link:
`https://github.com/u5275886-jpg/S-N-S.git`

---

### 💻 A. Windows PC / Laptop par Clone karna (Development Environment)

#### 1️⃣ Prerequisites (आवश्यक टूल्स):
- **Git** ([Git Download](https://git-scm.com/))
- **.NET 8.0 SDK** ([.NET 8 Download](https://dotnet.microsoft.com/download/dotnet/8.0))
- **Visual Studio 2022** (with .NET Desktop Development workload) या **VS Code**

#### 2️⃣ Step-by-Step Commands:
1. Command Prompt (`cmd`), Terminal, ya Git Bash kholien.
2. Us folder me jayein jahan aap project save karna chahte hain (Jaise `Desktop` ya `Documents`):
   ```bash
   cd C:\Users\YOUR_USERNAME\Documents
   ```
3. GitHub repository clone karein:
   ```bash
   git clone https://github.com/u5275886-jpg/S-N-S.git
   ```
4. Cloned directory me enter karein:
   ```bash
   cd S-N-S
   ```
5. Application test/run karein:
   ```bash
   dotnet restore
   dotnet build
   dotnet run --project src/CollegeIdManagement/CollegeIdManagement.csproj
   ```

---

### 🖥️ B. Linux VPS Server par Clone karna (For Hosting Website & Distribution)

#### Step-by-Step Commands:
```bash
# SSH se VPS me login karein
ssh root@YOUR_VPS_IP

# Web directory create karein
mkdir -p /var/www
cd /var/www

# Repo clone karein
git clone https://github.com/u5275886-jpg/S-N-S.git college-id
cd college-id
```

---

## 📦 2. Application & APK Kaise Milega? (How to Get App & APK)

### 💻 A. Windows Desktop Application (`.exe` / Installer)

Current system Windows Desktop (WPF .NET 8) par running hai.

#### 1️⃣ Directly Run / Publish Binary:
```bash
# Release Build karein
dotnet publish src/CollegeIdManagement/CollegeIdManagement.csproj -c Release -o ./publish
```
- Built `.exe` file aapko `./publish/CollegeIdManagement.exe` me mil jayegi.

#### 2️⃣ Windows Setup (`.exe` Installer) Banana:
1. **Inno Setup Compiler** install karein.
2. Repository me maujood `installer/CollegeIdManagement.iss` file ko Inno Setup me open karein.
3. **Compile** button press karein -> `College_ID_Management_Setup.exe` ready ho jayega.

---

### 📱 B. Android App (`.apk`) Kaise Banayein?

Current app C# .NET 8 me likhi gayi hai. Android APK banane ke 2 mukhya tarike hain:

#### 🟢 Method 1: .NET MAUI se Cross-Platform APK (Recommended)
1. `.NET 8 MAUI` workload install karein:
   ```bash
   dotnet workload install maui-android
   ```
2. Existing ViewModel, Database (EF Core SQLite), aur Services code ko direct MAUI project me reference karein.
3. APK Release build karein:
   ```bash
   dotnet publish -f net8.0-android -c Release -p:AndroidPackageFormat=apk
   ```
4. Generated APK file location: `bin/Release/net8.0-android/publish/*.apk`

#### 🔵 Method 2: Avalonia UI Framework
Avalonia UI se aap same XAML code se Windows (`.exe`) aur Android (`.apk`) dono generate kar sakte hain:
```bash
dotnet publish -f net8.0-android -c Release -p:AndroidPackageFormat=apk
```

---

## 🔄 3. Users ke App ko Update Kaise Karwayenge? (How to Manage & Push Updates)

Jab bhi aap app me naya feature add karenge ya bug fix karenge, toh users tak update pahunchane ke liye nimn steps follow karein:

### ⚙️ Step-by-Step Update Workflow:

#### Step 1: Version Number Update Karein
- Project file `src/CollegeIdManagement/CollegeIdManagement.csproj` me Version update karein (e.g., `1.0.0` -> `1.1.0`).

#### Step 2: Naya Build (Setup / APK) Prepare Karein
- Windows ke liye naya `.exe` setup compile karein.
- Android ke liye naya `.apk` release build karein.

#### Step 3: VPS Website par Naya File Upload Karein
- VPS par `website/downloads/` folder me nayi `.apk` ya `.exe` file replace karein:
  ```bash
  # VPS downloads folder
  /var/www/college-id/website/downloads/CollegeIdManagement.apk
  ```

#### Step 4: GitHub Repo Update Karein
```bash
git add .
git commit -m "Version 1.1.0 update release"
git push origin main
```

#### Step 5: User Distribution & In-App Notification
- **Download Website**: Users apne mobile ya PC se download portal visit karke hamesha latest version download kar sakte hain.
- **In-App Auto/Manual Check**: Desktop/Mobile app start hone par `website/downloads/version.json` ya API se current version compare karke user ko popup notify karega: *"New Update v1.1.0 Available! Click here to Download"*.

---

## 🌐 4. Website VPS par Kaise Host Hogi? (Complete Hosting Guide)

Repository me `website/` folder ke andar complete landing & download page structured hai.

---

### 🚀 Setup Method 1: Node.js & Express + PM2 (Easiest)

#### 1️⃣ VPS par Node.js & PM2 Install Karein:
```bash
curl -fsSL https://deb.nodesource.com/setup_18.x | sudo -E bash -
sudo apt-get install -y nodejs
sudo npm install -g pm2
```

#### 2️⃣ Web Server Dependencies Install & Start Karein:
```bash
cd /var/www/college-id/website
npm install

# Download directory me APK/Setup place karein
mkdir -p downloads
# Copy your CollegeIdManagement.apk to downloads/

# PM2 se background me server start karein
pm2 start server.js --name "college-id-web"
pm2 save
pm2 startup
```
- App Live on: `http://YOUR_VPS_IP:3000`

---

### 🛡️ Setup Method 2: Nginx Reverse Proxy & SSL (Production Recommended)

#### 1️⃣ Nginx Install Karein:
```bash
sudo apt update
sudo apt install nginx -y
```

#### 2️⃣ Nginx Config Copy & Enable Karein:
```bash
sudo cp /var/www/college-id/website/nginx.conf /etc/nginx/sites-available/college-id
sudo ln -s /etc/nginx/sites-available/college-id /etc/nginx/sites-enabled/
sudo rm /etc/nginx/sites-enabled/default
```

#### 3️⃣ Nginx Domain / IP Configuration:
File `/etc/nginx/sites-available/college-id` ko edit karke apna domain name ya VPS IP set karein, fir restart karein:
```bash
sudo nginx -t
sudo systemctl restart nginx
```

#### 4️⃣ Free HTTPS / SSL Certificate (Certbot):
```bash
sudo apt install certbot python3-certbot-nginx -y
sudo certbot --nginx -d yourdomain.com -d www.yourdomain.com
```

---

## 📁 5. Directory & Data Storage Info

### System Data Path (Windows):
All databases, student photos, signatures, and exported cards are stored at:
`%LOCALAPPDATA%\CollegeIdManagement\`

### Demo Login Credentials:
- **Username:** `admin`
- **Password:** `admin123`

---

## 🛠️ Summary of Quick Commands

| Action / कार्य | Command |
| :--- | :--- |
| **Clone Repo** | `git clone https://github.com/u5275886-jpg/S-N-S.git` |
| **Run Desktop App** | `dotnet run --project src/CollegeIdManagement/CollegeIdManagement.csproj` |
| **Build Executable** | `dotnet publish -c Release` |
| **Start Web Server** | `cd website && npm start` |
| **PM2 Process Management** | `pm2 start server.js --name "college-id-web"` |

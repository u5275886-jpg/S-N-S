# COLLEGE ID MANAGEMENT SYSTEM

## Overview
A complete, production-ready Windows desktop application built with **C# .NET 8**, **WPF**, **Entity Framework Core (SQLite)**, **QuestPDF**, **ClosedXML**, and **QRCoder** for **Shyam Nandan Sahay College, Muzaffarpur, Bihar**.

## Features
- Complete Student Management (Add, Edit, Delete, Filter, Search)
- Auto Member Code Generation (`SNSEC/BA/2026/001`)
- Auto Roll Number Generation (`001`, `002`)
- High-Resolution Front/Back ID Card Preview & PDF Generation
- Bulk ID Card Generation with Async Progress Tracking
- Excel Import (`.xlsx`) and Export
- QR Code Verification System
- Visual ID Card Template Designer
- Database Backup & Restore (`.zip`)
- Multi-User Authentication (Admin, Operator, Viewer) with hashed passwords
- Audit Logging & Activity Tracking

## Demo Login
- **Username:** `admin`
- **Password:** `admin123`

## How to Build & Run
1. Open Visual Studio 2022 / 2025.
2. Load `CollegeIdManagement.sln`.
3. Run `dotnet restore`
4. Run `dotnet build -c Release`
5. Execute `dotnet run --project src/CollegeIdManagement/CollegeIdManagement.csproj`

## Data Directory
All databases and uploaded photos/signatures are automatically created under:
`%LOCALAPPDATA%\CollegeIdManagement\`

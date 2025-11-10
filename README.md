
# ⚙️ DataSyncCLI — Workflow Automation Utility in C#

**DataSyncCLI** is a lightweight command-line tool written in **C# (.NET)** for automating repetitive **file synchronization and versioning** tasks across directories.  
It’s designed for developers, engineers, and analysts who often need to sync, compare, or archive large sets of files efficiently — without relying on heavy GUI-based sync tools.

---

## 🚀 Features

- 🔁 **Directory Synchronization** — Mirrors files from source to target with automatic directory creation.  
- 🧩 **Checksum-based Comparison** — Uses SHA-256 hashing to detect identical files and skip redundant copies.  
- 🕒 **Version Tracking** — Automatically renames changed files with a timestamp (e.g., `report_20251110_173255.csv`).  
- 🧱 **Robust Architecture** — Modular functions built using `System.IO` and LINQ for efficient I/O operations.  
- 🧾 **Detailed Logging** — Every action (copy, skip, rename, or error) is written to `datasync.log` with timestamps.  
- 🧰 **Error Handling** — Gracefully handles missing directories, locked files, and other common file I/O issues.

---

## 🧠 How It Works

1. Compares each file in the source directory with its target counterpart using SHA-256 checksums.  
2. If files are identical → skips copying.  
3. If contents differ → renames the old version with a timestamp and copies the new file.  
4. Keeps a full audit trail in `datasync.log`.

---

## 🖥️ Usage

### 1️⃣ Build the project

```bash
dotnet build

### 2️⃣ Run the CLI

```bash
dotnet run -- "<sourceDir>" "<targetDir>"
```

### Example

```bash
dotnet run -- "C:\Data\Source" "D:\Backup\Target"
```

Output:

```
=== DataSyncCLI — Workflow Automation Utility ===
2025-11-10 19:45:21 | INFO | Copied: sales\data.csv
2025-11-10 19:45:22 | INFO | File updated: reports\Q1.xlsx -> versioned as Q1_20251110_194522.xlsx
✅ Synchronization completed successfully.
```

---

## 🗂️ Log File Example (`datasync.log`)

```
2025-11-10 19:45:21 | INFO | Copied: sales\data.csv
2025-11-10 19:45:22 | INFO | File updated: reports\Q1.xlsx -> versioned as Q1_20251110_194522.xlsx
2025-11-10 19:45:23 | INFO | Copied: config\settings.json
```

---

## 🏗️ Project Structure

```
DataSyncCLI/
├── Program.cs
├── DataSyncCLI.csproj
├── datasync.log
└── README.md
```

---

## 🧩 Tech Stack

* **Language:** C#
* **Framework:** .NET 6+
* **Core Libraries:** `System.IO`, `System.Linq`, `System.Security.Cryptography`

---

## ⚡ Future Enhancements

* [ ] Add CLI argument parsing (e.g., `--dry-run`, `--log-dir`, `--include *.csv`).
* [ ] Add parallel file copy for large batch performance.
* [ ] Support for remote synchronization via FTP/S3.
* [ ] Include JSON-based configuration for repeatable workflows.

---


> “Automate the boring stuff. Sync smarter, not harder.” ⚡


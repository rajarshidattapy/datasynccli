using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DataSyncCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== DataSyncCLI — Workflow Automation Utility ===");
            
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: datasynccli <sourceDir> <targetDir>");
                return;
            }

            string sourceDir = args[0];
            string targetDir = args[1];

            if (!Directory.Exists(sourceDir))
            {
                Console.WriteLine($"Error: Source directory '{sourceDir}' not found.");
                return;
            }

            Directory.CreateDirectory(targetDir);

            try
            {
                SyncDirectories(sourceDir, targetDir);
                Console.WriteLine("✅ Synchronization completed successfully.");
            }
            catch (Exception ex)
            {
                LogError($"Exception: {ex.Message}");
                Console.WriteLine($"❌ Error during sync: {ex.Message}");
            }
        }

        static void SyncDirectories(string sourceDir, string targetDir)
        {
            var sourceFiles = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);

            foreach (var sourceFile in sourceFiles)
            {
                string relativePath = Path.GetRelativePath(sourceDir, sourceFile);
                string targetFile = Path.Combine(targetDir, relativePath);
                string targetFolder = Path.GetDirectoryName(targetFile)!;
                Directory.CreateDirectory(targetFolder);

                bool shouldCopy = true;

                if (File.Exists(targetFile))
                {
                    string sourceHash = GetFileChecksum(sourceFile);
                    string targetHash = GetFileChecksum(targetFile);

                    if (sourceHash == targetHash)
                    {
                        shouldCopy = false; // identical files
                    }
                    else
                    {
                        // Rename old version with timestamp
                        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        string versionedFile = $"{Path.GetFileNameWithoutExtension(targetFile)}_{timestamp}{Path.GetExtension(targetFile)}";
                        string versionedPath = Path.Combine(targetFolder, versionedFile);
                        File.Move(targetFile, versionedPath);
                        Log($"File updated: {relativePath} -> versioned as {versionedFile}");
                    }
                }

                if (shouldCopy)
                {
                    File.Copy(sourceFile, targetFile, true);
                    Log($"Copied: {relativePath}");
                }
            }
        }

        static string GetFileChecksum(string filePath)
        {
            using var sha256 = SHA256.Create();
            using var stream = File.OpenRead(filePath);
            var hash = sha256.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        static void Log(string message)
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | INFO | {message}";
            Console.WriteLine(logEntry);
            File.AppendAllText("datasync.log", logEntry + Environment.NewLine, Encoding.UTF8);
        }

        static void LogError(string message)
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | ERROR | {message}";
            Console.WriteLine(logEntry);
            File.AppendAllText("datasync.log", logEntry + Environment.NewLine, Encoding.UTF8);
        }
    }
}

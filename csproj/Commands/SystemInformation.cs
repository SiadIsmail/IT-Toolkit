using System;
using System.Management;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace ITToolkit.Commands
{
    public static class SystemInformation
    {
        public static void PrintOSSummary()
        {
            Console.WriteLine($"OS: {RuntimeInformation.OSDescription}");
            Console.WriteLine($"OS Architecture: {RuntimeInformation.OSArchitecture}");
            Console.WriteLine($"Process Architecture: {RuntimeInformation.ProcessArchitecture}");
            Console.WriteLine($"Machine Name: {Environment.MachineName}");
            Console.WriteLine($".NET Version: {Environment.Version}");
        }
        public static void PrintSystemSummary()
        {
            if (!OperatingSystem.IsWindows())
            {
                Console.WriteLine("Detailed hardware information via WMI is only available on Windows.");
                return;
            }

            Console.WriteLine($"CPU: {GetFirstStringValue("SELECT Name FROM Win32_Processor", "Name") ?? "Unknown"}");

            ulong? totalMemory = GetFirstUInt64Value(
                "SELECT TotalPhysicalMemory FROM Win32_ComputerSystem",
                "TotalPhysicalMemory");

            if (totalMemory.HasValue)
            {
                Console.WriteLine($"RAM: {FormatBytes(totalMemory.Value)}");
            }

            PrintDiskInformation();
        }

        [SupportedOSPlatform("windows")]
        private static void PrintDiskInformation()
        {
            using var searcher = new ManagementObjectSearcher(
                "SELECT DeviceID, VolumeName, Size, FreeSpace FROM Win32_LogicalDisk WHERE DriveType = 3");

            foreach (ManagementObject drive in searcher.Get())
            {
                string deviceId = drive["DeviceID"]?.ToString() ?? "Unknown";
                string? volumeName = drive["VolumeName"]?.ToString();
                ulong? size = TryToUInt64(drive["Size"]);
                ulong? freeSpace = TryToUInt64(drive["FreeSpace"]);
                string driveName = string.IsNullOrWhiteSpace(volumeName)
                    ? deviceId
                    : $"{deviceId} ({volumeName})";

                if (size.HasValue && freeSpace.HasValue)
                {
                    Console.WriteLine(
                        $"Disk {driveName}: {FormatBytes(freeSpace.Value)} free of {FormatBytes(size.Value)}");
                }
                else
                {
                    Console.WriteLine($"Disk {driveName}: size information unavailable");
                }
            }
        }

        [SupportedOSPlatform("windows")]
        private static string? GetFirstStringValue(string query, string propertyName)
        {
            using var searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject result in searcher.Get())
            {
                return result[propertyName]?.ToString();
            }

            return null;
        }

        [SupportedOSPlatform("windows")]
        private static ulong? GetFirstUInt64Value(string query, string propertyName)
        {
            using var searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject result in searcher.Get())
            {
                return TryToUInt64(result[propertyName]);
            }

            return null;
        }

        private static ulong? TryToUInt64(object? value)
        {
            return value switch
            {
                ulong ulongValue => ulongValue,
                string stringValue when ulong.TryParse(stringValue, out ulong parsedValue) => parsedValue,
                _ => null
            };
        }

        private static string FormatBytes(ulong bytes)
        {
            string[] units = ["B", "KB", "MB", "GB", "TB"];
            double size = bytes;
            int unitIndex = 0;

            while (size >= 1024 && unitIndex < units.Length - 1)
            {
                size /= 1024;
                unitIndex++;
            }

            return $"{size:F1} {units[unitIndex]}";
        }
    }
}

/*public static void PrintGpuInformation()
{
    using var searcher = new ManagementObjectSearcher(
        "SELECT Name, AdapterRAM, DriverVersion FROM Win32_VideoController");

    foreach (ManagementObject gpu in searcher.Get())
    {
        string name = gpu["Name"]?.ToString() ?? "Unknown";
        ulong? adapterRam = TryToUInt64(gpu["AdapterRAM"]);
        string driverVersion = gpu["DriverVersion"]?.ToString() ?? "Unknown";

        Console.WriteLine($"GPU: {name}");
        Console.WriteLine($"Driver Version: {driverVersion}");

        if (adapterRam.HasValue)
        {
            Console.WriteLine($"VRAM: {FormatBytes(adapterRam.Value)}");
        }

        Console.WriteLine();
    }
}*/
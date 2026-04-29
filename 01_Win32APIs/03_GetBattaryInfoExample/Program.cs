using System;
using System.Runtime.InteropServices;

public class BatteryMonitor
{
    // Win32 Structure for power status
    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEM_POWER_STATUS
    {
        public byte ACLineStatus;      // 0: Offline, 1: Online, 255: Unknown
        public byte BatteryFlag;       // Battery charge status flags
        public byte BatteryLifePercent; // 0-100, or 255 if unknown
        public byte SystemStatusFlag;  // Reserved
        public uint BatteryLifeTime;   // Remaining seconds (uint.MaxValue if unknown)
        public uint BatteryFullLifeTime; // Full battery life in seconds
    }

    [DllImport("kernel32.dll")]
    private static extern bool GetSystemPowerStatus(out SYSTEM_POWER_STATUS lpSystemPowerStatus);

    public static void PrintStatus()
    {
        if (GetSystemPowerStatus(out SYSTEM_POWER_STATUS status))
        {
            Console.WriteLine($"Power Source: {(status.ACLineStatus == 1 ? "AC Power" : "Battery")}");
            Console.WriteLine($"Charge Level: {(status.BatteryLifePercent == 255 ? "Unknown" : status.BatteryLifePercent + "%")}");

            if (status.BatteryLifeTime != uint.MaxValue)
                Console.WriteLine($"Time Remaining: {status.BatteryLifeTime / 60} minutes");
        }
    }

    static void Main()
    {
        PrintStatus();
    }
}

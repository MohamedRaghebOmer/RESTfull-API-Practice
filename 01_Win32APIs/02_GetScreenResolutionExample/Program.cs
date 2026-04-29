using System;
using System.Runtime.InteropServices;

internal class GetScreenResolutionExample
{
    [DllImport("user32.dll")]
    static extern int GetSystemMetrics(int nIndex);

    static void Main(string[] args)
    {
        int screenWidth = GetSystemMetrics(0);
        int screenHeight = GetSystemMetrics(1);

        Console.WriteLine("Screen Width {0}, Screen Height {1}", screenWidth, screenHeight);
    }
}
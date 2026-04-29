using System;
using System.Runtime.InteropServices;

class Program
{
    // Import the SystemParametersInfo function from user32.dll
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool SystemParametersInfo(uint action, uint uParam, string vParam, uint winIni);

    // Constants for the function
    public static readonly uint SPI_SETDESKWALLPAPER = 0x14;
    public static readonly uint SPIF_UPDATEINIFILE = 0x01;
    public static readonly uint SPIF_SENDCHANGE = 0x02;

    static void Main()
    {
        bool again = true;

        while (again)
        {
            // The path to the wallpaper image
            Console.Write("Enter wallpaper path: ");
            string wallpaperPath = Console.ReadLine();

            // Set the wallpaper
            SetWallpaper(wallpaperPath);

            Console.Write("Do you want to change wallpaper again? [Y|N]: ");
            again = string.Equals(Console.ReadLine(), "y", StringComparison.OrdinalIgnoreCase);
            Console.Clear();
        }
    }

    public static void SetWallpaper(string path)
    {
        if (SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE))
        {
            Console.WriteLine("Wallpaper changed successfully!");
        }
        else
        {
            Console.WriteLine("Error changing wallpaper.");
        }
    }
}

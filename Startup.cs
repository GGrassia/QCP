using System.Runtime.Serialization;
namespace QCP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text.Json;
    using Claunia.PropertyList;
    using Microsoft.Win32;

    // Struct managing startup options, also responsible of serializing the settings json.
    public struct Startup
    {
        public bool SilentMode { get; set; }

        public bool LaunchAtLogin { get; set; }

        public bool SlowMode { get; set; }

        public string SettingsJson { get; set; }

        public List<string> DefaultFolder { get; set; }

        public static bool UseDefaults(Startup x)
        {
            if (!x.SilentMode)
            {
                Console.WriteLine("Silent mode is not active, if you want me to run without needing inputs, set the field to 'true' in the startup json, press enter to continue");
                Console.ReadLine();
            }

            if (!x.LaunchAtLogin && !x.SilentMode)
            {
                Console.WriteLine("Launch at login is disabled. If you want it to be enabled, set the field to 'true' in the startup json, it will be active from the next QCP execution. Press enter to continue");
                Console.ReadLine();
            }

            string defaults;
            if (x.SilentMode)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Welcome, do you want to use defaults? [y/n]");
                defaults = Console.ReadLine().ToLower();
                if (defaults == "y")
                {
                    return true;
                }

                Console.WriteLine("Ok, using manual mode and default folder settings");
                return false;
            }
        }

        public static void RunAtLogin(Startup x)
        {
            // if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            // {
            //     RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            //     if (x.LaunchAtLogin)
            //     {
            //         rk.SetValue("QCP", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QCP.exe"));
            //     }
            //     else
            //     {
            //         rk.DeleteValue("QCP", false);
            //     }
            // }
            // else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            // {
                string launchAgentsFolder = "/home/giulio/Downloads/QCP.plist";
                string runFolder = AppDomain.CurrentDomain.BaseDirectory;
                string plistPath = Path.Combine(runFolder, "QCP.plist");

                if (x.LaunchAtLogin)
                {
                    if (File.Exists(launchAgentsFolder))
                    {
                        File.Delete(launchAgentsFolder);
                    }

                    NSDictionary root = new NSDictionary();
                    NSString plistID = new NSString("QCP");
                    NSString programPath = new NSString(Path.Combine(runFolder, "QCP", "QCP"));
                    root.Add("Label", plistID);
                    root.Add("Program", programPath);
                    root.Add("RunAtLoad", true);
                    PropertyListParser.SaveAsXml(root, new FileInfo(launchAgentsFolder));
                }
           // }
        }

        public static List<Correlation> LoadSettings(bool usingDefaults, Startup settings)
        {
            if (usingDefaults)
            {
                return JsonSerializer.Deserialize<List<Correlation>>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jsons", settings.SettingsJson)));
            }
            else
            {
                return JsonSerializer.Deserialize<List<Correlation>>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jsons", "defaultSettings.json")));
            }
        }

        public static List<string> SetDefaultFolder(bool usingDefaults, Startup startup)
        {
            List<string> foldersToTidy = new();
            if (usingDefaults)
            {
                foreach (string x in startup.DefaultFolder)
                {
                    foldersToTidy.Add(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jsons", x));
                }
            }
            else
            {
                Console.WriteLine("Tell me the path of the folder you want to tidy up");
                string source = Console.ReadLine();
                bool dirExists = Directory.Exists(source);
                while (!dirExists)
                {
                    Console.WriteLine("Sorry, could not find the folder, please write it again");
                    source = Console.ReadLine();
                    dirExists = Directory.Exists(source);
                }

                foldersToTidy.Add(source);
            }

            return foldersToTidy;
        }
    }
}

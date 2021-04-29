namespace QCP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text.Json;
    using Microsoft.Win32;

    // Struct managing startup options, also responsible of serializing the settings json. Instanced by deserializing startup json.
    public struct Startup
    {
        // Silentmode assumes you want to use defaults and prevents info or communication unless exceptions arise.
        public string SilentMode { get; set; }

        // Sets a regkey to launch QCP at startup. Should only work on W10 for the time being (needs testing).
        public string LaunchAtLogin { get; set; }

        // Settings json file name read by the defaults, recomended for custom rules.
        public string SettingsJson { get; set; }

        // Default folder to be cleaned up. Also default parent folder for non custom filetype folders.
        public string DefaultFolder { get; set; }

        // Check for silent mode and startup, then asks if you want to load defaults when silent mode is off.
        public static string UseDefaults(Startup x)
        {
            if (x.SilentMode.ToLower() != "true")
            {
                Console.WriteLine("Silent mode is not active, if you want me to run without needing inputs, set the field to 'true' in the startup json, press enter to continue");
                Console.ReadLine();
            }

            if (x.LaunchAtLogin.ToLower() != "true" && x.SilentMode.ToLower() != "true")
            {
                Console.WriteLine("Launch at login is disabled. If you want it to be enabled, set the field to 'true' in the startup json, it will be active from the next QCP execution. Press enter to continue");
                Console.ReadLine();
            }

            string defaults;
            if (x.SilentMode.ToLower() == "true")
            {
                return "y";
            }
            else
            {
                Console.WriteLine("Welcome, do you want to use defaults? [y/n]");
                defaults = Console.ReadLine().ToLower();
                if (defaults == "y")
                {
                    return defaults;
                }

                Console.WriteLine("Ok, using manual mode and default folder settings");
                return "n";
            }
        }

        // Sets automatic run at login for the current user.
        public static void RunAtLogin(Startup x)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return;
            }

            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (x.LaunchAtLogin == "true")
            {
                rk.SetValue("QCP", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QCP.exe"));
            }
            else
            {
                rk.DeleteValue("QCP", false);
            }
        }

        // Deserializes the settings json, default or not
        public static List<Correlation> LoadSettings(string usingDefaults, Startup settings)
        {
            if (usingDefaults == "y")
            {
                return JsonSerializer.Deserialize<List<Correlation>>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jsons", settings.SettingsJson)));
            }
            else
            {
                return JsonSerializer.Deserialize<List<Correlation>>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jsons", "defaultSettings.json")));
            }
        }

        // Sets the folder you want to clean up, default or custom
        public static string SetDefaultFolder(string usingDefaults, Startup settings)
        {
            if (usingDefaults == "y")
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jsons", settings.DefaultFolder);
            }
            else
            {
                Console.WriteLine("Ok, tell me the path of the folder you want to tidy up");
                string source = Console.ReadLine();
                bool dirExists = Directory.Exists(source);
                while (!dirExists)
                {
                    Console.WriteLine("Sorry, could not find the folder, please write it again");
                    source = Console.ReadLine();
                    dirExists = Directory.Exists(source);
                }

                return source;
            }
        }
    }
}

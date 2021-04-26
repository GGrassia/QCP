namespace QCP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;
    using System.Threading.Tasks;

    public static class Program
    {
        private static Dictionary<string, string> dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public static void Main(string[] args)
        {
            // Creating a startup with the basic settings of the program and see if user wants to use defaults.
            Startup startup = JsonSerializer.Deserialize<Startup>(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "jsons/startup.json")));
            string defaults = Startup.UseDefaults(startup);

            // Generates the ruleset for moving files.
            List<Correlation> folders = Startup.LoadSettings(defaults, startup);

            // Folder to be cleaned up.
            string source = Startup.SetDefaultFolder(defaults, startup);

            // Dictionary of correlations between file extension and destination folder.
            dict = DictGen.GenerateDictionary(folders, dict);

            // List of the files to move.
            List<Locations> fileList = new List<Locations>();

            // Populate the list with the locations then moves files
            try
            {
                PopulateList(fileList, source);
            }
            catch (Exception e)
            {
                Console.WriteLine($"I had a problem, please, send the error to the dev. Here's the error: {e}");
            }

            // Move the files
            Parallel.ForEach(fileList, i => Locations.TidyUp(i));

            // Check if run at login settings are changed and if silent mode is off give feedback.
            Startup.RunAtLogin(startup);
            if (startup.Silentmode != "true")
            {
                Console.WriteLine("All done, see you next time. Press enter to exit.");
                Console.ReadLine();
            }
        }

        // Populate the list of files to be moved.
        private static void PopulateList(List<Locations> list, string source)
        {
            foreach (string fileLocation in Directory.EnumerateFiles(source))
            {
                string destination = LocationGenerator(source, fileLocation);
                string fileName = Path.GetFileName(fileLocation);
                Locations fileToMove = new Locations(fileLocation, Path.Combine(destination, fileName));
                list.Add(fileToMove);
            }
        }

        // Generate destination and create corresponding folder if not present. Returns custom folder if existing.
        private static string LocationGenerator(string source, string fileName)
        {
            string extension = Path.GetExtension(fileName).Replace(".","");
            var entry = dict.ContainsKey(extension) ? dict[extension] : "Misc";
            string destination;
            if (Directory.Exists(entry))
            {
                destination = entry;
            }
            else
            {
                destination = Path.Combine(source, entry);
                Directory.CreateDirectory(destination);
            }

            return destination;
        }
    }
}

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
            Startup startup = JsonSerializer.Deserialize<Startup>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jsons", "startup.json")));
            bool defaults = Startup.UseDefaults(startup);

            // Folders is a wrong name. Find a new one
            List<Correlation> folders = Startup.LoadSettings(defaults, startup);

            // This needs to be a list of source folders
            List<string> source = Startup.SetDefaultFolder(defaults, startup);

            dict = DictGen.GenerateDictionary(folders, dict);

            // List of the files to move. Will contain files from all folders.
            List<FileMover> fileList = new List<FileMover>();

            // Put a foreach and iter through the folder list
            try
            {
                PopulateList(fileList, source);
            }
            catch (Exception e)
            {
                Console.WriteLine($"I had a problem, please, send the error to the dev. Here's the error: {e}");
            }

            // Parallel.ForEach(fileList, i => i.TidyUp());
            // Commented because of satisfying cleanup
            foreach (FileMover i in fileList)
            {
                i.TidyUp();
            }

            Startup.RunAtLogin(startup);
            if (!startup.SilentMode)
            {
                Console.WriteLine("All done, see you next time. Press enter to exit.");
                Console.ReadLine();
            }
        }

        private static void PopulateList(List<FileMover> list, List<string> source)
        {
            foreach (string folderWithFiles in source)
            {
                foreach (string fileLocation in Directory.EnumerateFiles(folderWithFiles))
                {
                    string destinationFolder = LocationGenerator(folderWithFiles, fileLocation);
                    string fileName = Path.GetFileNameWithoutExtension(fileLocation);
                    string fileExtension = Path.GetExtension(fileLocation);
                    FileMover fileToMove;
                    if (File.Exists(Path.Combine(destinationFolder, fileName)))
                    {
                        int i = 1;
                        while (File.Exists(Path.Combine(destinationFolder, fileName + i + fileExtension)))
                        {
                            i++;
                        }

                        fileToMove = new FileMover(fileLocation, Path.Combine(destinationFolder, fileName + i + fileExtension));
                    }
                    else
                    {
                        fileToMove = new FileMover(fileLocation, Path.Combine(destinationFolder, fileName + fileExtension));
                    }

                    list.Add(fileToMove);
            }
        }

            // Generate destination and create corresponding folder if not present. Returns custom folder if existing.
            static string LocationGenerator(string sourceFolder, string fileName)
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
                    destination = Path.Combine(sourceFolder, entry);
                    Directory.CreateDirectory(destination);
                }

                return destination;
            }
        }
    }
}
